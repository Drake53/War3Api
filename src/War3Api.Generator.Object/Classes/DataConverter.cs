using System;
using System.Collections.Generic;

using War3Net.Build.Common; // TEMP for Tileset enum

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

        public static float ToRaw(this float value, float? minValue, float? maxValue)
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

        public static bool ToRaw(this bool value, object minValue, object maxValue) => value;

        public static char ToRaw(this char value, object minValue, object maxValue) => value;

        public static string ToRaw<T>(this IList<T> list, int? minValue, int? maxValue)
        {
            // todo: replace ToString with something else?
            foreach (var value in list)
            {
                if ((minValue.HasValue && value.ToString().Length < minValue.Value) || (maxValue.HasValue && value.ToString().Length > maxValue.Value))
                {
                    throw new ArgumentOutOfRangeException(nameof(list));
                }
            }

            return $"\"{string.Join(',', list)}\"";
        }

        public static int ToInt(this int value, BaseObject baseObject) => value;

        public static float ToFloat(this float value, BaseObject baseObject) => value;

        public static string ToString(this string value, BaseObject baseObject) => value;

        public static bool ToBool(this bool value, BaseObject baseObject) => value;

        public static char ToChar(this char value, BaseObject baseObject) => value;

        public static int ToInt(this string value, BaseObject baseObject)
        {
            return int.Parse(value);
        }

        // todo: use TryParse instead? since that one is generic
        public static PathingPrevent ToPathingPrevent(this string value, BaseObject baseObject)
        {
            return (PathingPrevent)Enum.Parse(typeof(PathingPrevent), value, false);
        }

        public static PathingRequire ToPathingRequire(this string value, BaseObject baseObject)
        {
            return (PathingRequire)Enum.Parse(typeof(PathingRequire), value, false);
        }

        public static Target ToTarget(this string value, BaseObject baseObject)
        {
            return (Target)Enum.Parse(typeof(Target), value, false);
        }

        public static Tileset ToTileset(this string value, BaseObject baseObject)
        {
            return (Tileset)Enum.Parse(typeof(Tileset), value, false);
        }
    }
}