using System;

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

        public static int ToInt(this int value, BaseObject baseObject) => value;

        public static float ToFloat(this float value, BaseObject baseObject) => value;

        public static string ToString(this string value, BaseObject baseObject) => value;

        public static bool ToBool(this bool value, BaseObject baseObject) => value;

        public static char ToChar(this char value, BaseObject baseObject) => value;

        public static int ToInt(this string value, BaseObject baseObject)
        {
            return int.Parse(value);
        }
    }
}