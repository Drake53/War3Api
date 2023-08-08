using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using War3Net.Common.Extensions;
using War3Net.IO.Slk;

namespace War3Api.Generator.Object.Models
{
    internal sealed class TableModel
    {
        public TableModel(string path, string? keyColumn = null, string? nameColumn = null, string? tableName = null)
        {
            using var fileStream = File.OpenRead(path);
            Table = new SylkParser().Parse(fileStream);

            if (!string.IsNullOrEmpty(keyColumn))
            {
                TableKeyColumn = Table[keyColumn].Single();

                var mappings = new Dictionary<int, int>();
                for (var row = 1; row <= Table.Rows; row++)
                {
                    var key = (string)Table[TableKeyColumn, row];
                    if (string.IsNullOrEmpty(key))
                    {
                        continue;
                    }

                    var keyCode = key.FromRawcode();
                    if (!mappings.TryAdd(keyCode, row))
                    {
                        mappings[keyCode] = row;
                    }
                }

                ObjectToRowMappings = mappings;
            }

            if (!string.IsNullOrEmpty(nameColumn))
            {
                TableNameColumn = Table[nameColumn].Single();
            }

            if (!string.IsNullOrEmpty(tableName))
            {
                TableName = tableName;
            }
            else
            {
                var fileInfo = new FileInfo(path);
                TableName = fileInfo.Name[..^fileInfo.Extension.Length];
            }
        }

        public SylkTable Table { get; set; }

        public int TableKeyColumn { get; set; }

        public int TableNameColumn { get; set; }

        public string TableName { get; set; }

        public IReadOnlyDictionary<int, int>? ObjectToRowMappings { get; set; }

        public void AddValues(Dictionary<string, string> dict)
        {
            foreach (var row in Table.Skip(1))
            {
                var key = (string)row[TableKeyColumn];
                var value = (string)row[TableNameColumn];

                if (string.IsNullOrEmpty(key))
                {
                    continue;
                }

                dict.TryAdd(key, value);
            }
        }
    }
}