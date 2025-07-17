using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using com.ih.util.generic.Enums.Domain;

namespace com.ih.util.generic.Enums
{
    public enum EnumDescriptionType
    {
        Description,
        Display
    }

    public static class EnumUtil
    {
        public static T? GetEnum<T>(this string value)
        {
            try
            {
                return (T)System.Enum.Parse(typeof(T), value, true);
            }
            catch (Exception e)
            {
                return default(T?);
            }
        }

        public static T GetEnum<T>(T obj, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return (T)System.Enum.Parse(typeof(T), value, true);
            }

            return obj;
        }

        public static string GetDisplayValue<T>(this string value)
        {
            var isEnum = (T)System.Enum.Parse(typeof(T), value, true);

            FieldInfo field = isEnum.GetType().GetField(value);

            DisplayAttribute attribute =
                Attribute.GetCustomAttribute(field, typeof(DisplayAttribute)) as DisplayAttribute;

            return attribute == null ? value : attribute.Description;
        }

        public static string GetDescriptionValue<T>(this string value)
        {
            var isEnum = (T)System.Enum.Parse(typeof(T), value, true);

            FieldInfo field = isEnum.GetType().GetField(value);

            DescriptionAttribute attribute =
                Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

            return attribute == null ? value : attribute.Description;
        }

        public static EnumModel GetEnumModel<T>(this string value,
            EnumDescriptionType enumDescriptionType = EnumDescriptionType.Description) where T : System.Enum
        {
            if (!string.IsNullOrEmpty(value))
            {
                var enumIs = (T)System.Enum.Parse(typeof(T), value, true);

                if (enumIs != null)
                {
                    var enumModel = new EnumModel()
                    {
                        Index = enumIs.GetHashCode(),
                        Key = enumIs.ToString(),
                        Description = enumDescriptionType.Equals(EnumDescriptionType.Description)
                            ? GetDescriptionValue<T>(enumIs.ToString())
                            : GetDisplayValue<T>(enumIs.ToString())
                    };

                    return enumModel;
                }
            }

            return null;
        }

        public static List<EnumModel> GetValuesEnum<T>(
            EnumDescriptionType enumDescriptionType = EnumDescriptionType.Description) where T : System.Enum
        {
            var enumType = typeof(T);

            var names = System.Enum.GetNames(enumType).Cast<object>();
            var values = System.Enum.GetValues(enumType).Cast<int>();

            var list = names.Zip(values, (name, key) =>
                new EnumModel
                {
                    Index = key,
                    Key = name.ToString(),
                    Description = enumDescriptionType.Equals(EnumDescriptionType.Description)
                        ? GetDescriptionValue<T>(name.ToString())
                        : GetDisplayValue<T>(name.ToString())
                }).ToList();

            return list;
        }
    }
}