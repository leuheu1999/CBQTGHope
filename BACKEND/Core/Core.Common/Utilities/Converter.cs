using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace Core.Common.Utilities
{
    public static class Converter
    {
        public static readonly string[] DateTimeFormatStringList =
        {
            "dd/MM/yyyy HH:mm:ss",
            "dd/MM/yyyy HH:mm",
            "dd/MM/yyyy"
        };

        public static DateTime ToDateTime(this string strDate)
        {
            DateTime value = new DateTime();
            foreach (string item in DateTimeFormatStringList)
            {
                if (DateTime.TryParseExact(strDate, item, CultureInfo.InvariantCulture, DateTimeStyles.None, out value))
                {
                    break;
                }
            }
            return value;
        }

        public static DateTime? ToDateTimeNullable(this string str)
        {
            try
            {
                DateTime output;
                foreach (string item in DateTimeFormatStringList)
                {
                    if (DateTime.TryParseExact(str, item, CultureInfo.InvariantCulture, DateTimeStyles.None, out output))
                    {
                        return output;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool? ToBoolean(this string str)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(str)) { 
                    return str.Equals("1");
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool ToBool(this string str)
        {
            return !string.IsNullOrWhiteSpace(str) && str.ToLower().IndexOf("true", StringComparison.Ordinal) >= 0;
        }

        public static bool? ToBoolNullable(this string str)
        {
            try
            {
                bool output;
                if (bool.TryParse(str, out output)) return output;

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static byte ToByte(this string str)
        {
            byte result = 0;
            if (!string.IsNullOrWhiteSpace(str))
            {
                if (byte.TryParse(str, out result))
                    return result;
            }
            return result;
        }

        public static byte? ToByteNullable(this string str)
        {
            try
            {
                byte output;
                if (byte.TryParse(str, out output)) return output;

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static short ToShort(this string str)
        {
            short result = 0;
            if (!string.IsNullOrWhiteSpace(str))
            {
                if (short.TryParse(str, out result))
                    return result;
            }
            return result;
        }

        public static short? ToShortNullable(this string str)
        {
            try
            {
                short output;
                if (short.TryParse(str, out output)) return output;

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static int ToInt(this string str)
        {
            int result = 0;
            if (!string.IsNullOrWhiteSpace(str))
            {
                if (int.TryParse(str, out result))
                    return result;
            }
            return result;
        }

        public static int? ToIntNullable(this string str)
        {
            try
            {
                int output;
                if (int.TryParse(str, out output)) return output;

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static long ToLong(this string str)
        {
            long result = 0;
            if (!string.IsNullOrWhiteSpace(str))
            {
                if (long.TryParse(str.Replace(",", ""), out result))
                    return result;
            }
            return result;
        }

        public static long? ToLongNullable(this string str)
        {
            try
            {
                long output;
                if (long.TryParse(str.Replace(",",""), out output)) return output;

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static float ToFloat(this string str)
        {
            float result = 0;
            if (!string.IsNullOrWhiteSpace(str))
            {
                if (float.TryParse(str.Replace(",", ""), out result))
                    return result;
            }
            return result;
        }

        public static float? ToFloatNullable(this string str)
        {
            try
            {
                float output;
                if (float.TryParse(str.Replace(",", ""), out output)) return output;

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static double ToDouble(this string str)
        {
            double result = 0;
            if (!string.IsNullOrWhiteSpace(str))
            {
                if (double.TryParse(str.Replace(",", ""), out result))
                    return result;
            }
            return result;
        }

        public static double? ToDoubleNullable(this string str)
        {
            try
            {
                double output;
                if (double.TryParse(str.Replace(",", ""), out output)) return output;

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static decimal ToDecimal(this string str)
        {
            decimal result = 0;
            if (!string.IsNullOrWhiteSpace(str))
            {
                if (decimal.TryParse(str.Replace(",", ""), out result))
                    return result;
            }
            return result;
        }

        public static decimal? ToDecimalNullable(this string str)
        {
            try
            {
                decimal output;
                if (decimal.TryParse(str.Replace(",", ""), out output)) return output;

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static T ConvertTo<T>(NameValueCollection nvc) where T : class, new()
        {
            T e = new T();
            Type type = e.GetType();
            foreach (string kvp in nvc.AllKeys)
            {
                if (!string.IsNullOrEmpty(nvc[kvp]))
                {
                    PropertyInfo pi = type.GetProperty(kvp, BindingFlags.Public | BindingFlags.Instance);
                    if (pi != null)
                    {
                        Type propertyType = pi.PropertyType;
                        if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            propertyType = propertyType.GetGenericArguments()[0];
                        }


                        if (pi.PropertyType.IsEnum)
                        {
                            pi.SetValue(e, Enum.ToObject(propertyType, Convert.ChangeType(nvc[kvp], typeof(int))), null);
                        }
                        else if (pi.PropertyType.IsArray)
                        {
                            string valueArray = nvc[kvp];
                            if (!string.IsNullOrEmpty(valueArray))
                            {
                                string[] arr = valueArray.Split(',');
                                Array value = Array.CreateInstance(propertyType.GetElementType(), arr.Length);
                                for (int i = 0; i < arr.Length; i++)
                                {
                                    value.SetValue(Convert.ChangeType(arr[i], propertyType.GetElementType()), i);
                                }
                                pi.SetValue(e, value, null);
                            }
                        }
                        else
                        {
                            object obj;
                            if (propertyType == typeof(DateTime))
                            {
                                obj = (nvc[kvp]).ToDateTime();
                            }
                            else if (propertyType == typeof(bool))
                            {
                                obj = (nvc[kvp]).ToBool();
                            }
                            else
                            {
                                obj = Convert.ChangeType(nvc[kvp], propertyType);
                            }
                            pi.SetValue(e, obj, null);
                        }
                    }
                }
            }
            return e;
        }

        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            if (data == null) return new DataTable();

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
    }
}
