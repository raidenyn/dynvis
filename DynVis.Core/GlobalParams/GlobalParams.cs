using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DynVis.Core.IO;

namespace DynVis.Core
{
    /// <summary>
    /// Класс работы с глобальными параметрами системы
    /// </summary>
    public static class GlobalParams
    {
        private static readonly Dictionary<string, string> Params = new Dictionary<string, string>();
        /// <summary>
        /// Название файла параметров
        /// </summary>
        private const string ParamsFileName = "options.ini";

        /// <summary>
        /// Заголовок файла
        /// </summary>
        private const string FileCaption = "DynVis global options file";

        private const string FormatLineParam = "{0,-30}=   {1}"; 

        static GlobalParams()
        {
            var ParamsString = IOFile.TryOpenTextFile(ParamsFileName);
            if (!String.IsNullOrEmpty(ParamsString)) ParseParamsString(ParamsString);

            Application.ApplicationExit += GlobalParamsDestructor;
        }

        #region Get Functions

        //Функции получения данных различных типов
        //Если указан параметер поумолчанию, то он используется в случае, если в таблице данный параметер остустствовал

        public static int GetInt(string paramName)
        {
            return GetInt(paramName, 0);
        }

        public static int GetInt(string paramName, int defaultValue)
        {
            var str = GetParamStrValue(paramName);
            if (!String.IsNullOrEmpty(str))
            {
                int result;
                if (int.TryParse(str, out result))
                {
                    return result;
                }
            }
            SetValue(paramName, defaultValue);
            return defaultValue;
        }

        public static double GetDouble(string paramName)
        {
            return GetDouble(paramName, 0);
        }

        public static double GetDouble(string paramName, double defaultValue)
        {
            var str = GetParamStrValue(paramName);
            if (!String.IsNullOrEmpty(str))
            {
                double result;
                if (double.TryParse(str, out result))
                {
                    return result;
                }
            }
            SetValue(paramName, defaultValue);
            return defaultValue;
        }

        public static float GetFloat(string paramName)
        {
            return GetFloat(paramName, 0.0f);
        }

        public static float GetFloat(string paramName, float defaultValue)
        {
            var str = GetParamStrValue(paramName);
            if (!String.IsNullOrEmpty(str))
            {
                float result;
                if (float.TryParse(str, out result))
                {
                    return result;
                }
            }
            SetValue(paramName, defaultValue);
            return defaultValue;
        }


        public static string GetString(string paramName)
        {
            return GetString(paramName, String.Empty);
        }

        public static string GetString(string paramName, string defaultValue)
        {
            var str = GetParamStrValue(paramName);
            if (!String.IsNullOrEmpty(str))
            {
                return str;
            }
            SetValue(paramName, defaultValue);
            return defaultValue;
        }

        public static bool GetBoolean(string paramName)
        {
            return GetBoolean(paramName, false);
        }

        public static bool GetBoolean(string paramName, bool defaultValue)
        {
            var str = GetParamStrValue(paramName);
            if (!String.IsNullOrEmpty(str))
            {
                bool result;
                if (bool.TryParse(str, out result))
                {
                    return result;
                }
            }
            SetValue(paramName, defaultValue);
            return defaultValue;
        }

        public static T GetValue<T>(string paramName, T defaultValue)
        {
            try
            {
                return ConvertFromStringe<T>(GetParamStrValue(paramName));
            }
            catch (ArgumentException){}
            catch (InvalidCastException){}

            SetValue(paramName, defaultValue);

            return defaultValue;
        }

        #endregion

        private static T ConvertFromStringe<T>(string obj)
        {
            var type = typeof (T);
            return (T)(type.IsEnum ? Enum.Parse(type, obj, true) : Convert.ChangeType(obj, type));
        }

        private static string GetParamStrValue(string paramName)
        {
            string strVal;
            if (Params.TryGetValue(paramName, out strVal))
            {
                return strVal;
            }
            return String.Empty;
        }

        private static void ParseParamsString(string str)
        {
            var Lines = str.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in Lines)
            {
                var SeparatorIndex = line.IndexOf('=');
                if (SeparatorIndex < 0 || SeparatorIndex == line.Length-1) continue;

                var ParamName = line.Substring(0, SeparatorIndex).Replace(" ", "");
                var value = line.Substring(SeparatorIndex + 1, line.Length - SeparatorIndex - 1).Trim();

                Params.Add(ParamName, value);
            }
        }

        private static string GetParamsAsString()
        {
            var SB = new StringBuilder(FileCaption);
            SB.AppendLine();
            SB.AppendLine();

            foreach (var param in Params)
            {
                SB.AppendLine(String.Format(FormatLineParam, param.Key, param.Value));
            }

            SB.AppendLine();

            return SB.ToString();
        }

        private static void SaveFile()
        {
            IOFile.TrySaveTextFile(ParamsFileName, GetParamsAsString());
        }

        private static void GlobalParamsDestructor(object sender, EventArgs e)
        {
            SaveFile();
        }

        /// <summary>
        /// Устанавливает значение параметра
        /// </summary>
        /// <param name="nameParam"></param>
        /// <param name="value"></param>
        public static void SetValue(string nameParam, object value)
        {
            if (Params.ContainsKey(nameParam))
            {
                Params[nameParam] = value.ToString();
            }
            else
            {
                Params.Add(nameParam, value.ToString());
            }
        }
    }
}
