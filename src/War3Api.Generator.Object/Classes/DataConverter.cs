using System;

using War3Net.Common.Extensions;

namespace War3Api.Object
{
    internal static class DataConverter
    {
        public static int ToRaw(this int value, int? minValue, int? maxValue)
        {
            if ((minValue.HasValue && value < minValue.Value) || (maxValue.HasValue && value > maxValue.Value))
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            return value;
        }

        public static string ToRaw(this string value, int? minValue, int? maxValue)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if ((minValue.HasValue && value.Length < minValue.Value) || (maxValue.HasValue && value.Length > maxValue.Value))
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            return value;
        }

        public static int ToRaw(this bool value, object? minValue, object? maxValue) => value ? 1 : 0;

        public static string ToRaw(this char value, object? minValue, object? maxValue) => value.ToString();

        public static string ToString(this string value, BaseObject baseObject) => value;

        public static int ToInt(this string value, BaseObject baseObject) => int.Parse(value);

        public static bool ToBool(this int value, BaseObject baseObject) => value.ToBool();

        public static char ToChar(this string value, BaseObject baseObject) => value.Length == 1 ? value[0] : throw new ArgumentException("String can only be cast to char if its length is 1.", nameof(value));
    }
}