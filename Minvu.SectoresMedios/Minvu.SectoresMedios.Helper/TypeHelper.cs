using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.SectoresMedios.Helper
{
    public static class TypeHelper
    {
        public static int? ConvertToIntNullable(object o)
        {
            if (o == null)
                return null;
            else
                return (int?)Convert.ToInt32(o);
        }

        public static int ConvertToInt(object o)
        {
            if (o == null)
                return 0;
            else
                return Convert.ToInt32(o);
        }

        public static DateTime? ConvertToDateTimeNullable(object o)
        {
            return (o == null) ? null : (DateTime?)Convert.ToDateTime(o);
        }

        public static decimal? ConvertToDecimalNullable(object o)
        {
            return (o == null) ? null : (decimal?)Convert.ToDecimal(o);
        }

        public static Dictionary<string, object> ResultDictionary(object o)
        {
            if (o == null)
                return null;
            else
            {
                Dictionary<string, object> item = new Dictionary<string, object>();
                PropertyInfo[] propiedades = o.GetType().GetProperties();
                foreach (PropertyInfo p in propiedades)
                {
                    item.Add(p.Name, (p.GetValue(o, null) == null) ? null : p.GetValue(o, null));
                }
                return item;
            }
        }

        public static string ConvertObjectToString(object o)
        {
            return (o == null) ? null : o.ToString();
        }

        public static bool ConvertToBoolean(object o)
        {
            return (o == null) ? false : Convert.ToBoolean(Convert.ToInt32(o));
        }

        public static Dictionary<string, string> MergeDictionary(Dictionary<string, string> d1, Dictionary<string, string> d2)
        {
            if (d1 != null && d2 != null)
            {
                foreach (var item in d2)
                {
                    d1[item.Key] = item.Value;
                }
            }

            if (d1 == null && d2 != null)
                return d2;

            return d1;
        }

        public static string UppercaseWords(string value)
        {
            value = value.ToLower();
            char[] array = value.ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            // Scan through the letters, checking for spaces.
            // ... Uppercase the lowercase letters following spaces.
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }
    }
}
