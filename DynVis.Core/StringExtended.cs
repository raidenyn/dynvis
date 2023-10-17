using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DynVis.Core
{
    /// <summary>
    /// Клас раширений методов для строк
    /// </summary>
    public static class StringExtended
    {
        /// <summary>
        /// Очищает строку
        /// </summary>
        /// <param name="sb"></param>
        public static void Clear(this StringBuilder sb)
        {
            sb.Remove(0, sb.Length);
        } 

        //Возращает подстроку с заданной позиции до конца строки
        public static string FromPositionToEndLine(this string sb, int position)
        {
            if (!String.IsNullOrEmpty(sb) && position < sb.Length)
            {
                var EndLine = sb.IndexOf(Environment.NewLine, position);
                if (EndLine < 0) EndLine = sb.Length;
                return sb.Substring(position, EndLine - position);
            }
            return String.Empty;
        }

        /// <summary>
        /// Читает подстроку определенную регулярным выражением и группой
        /// </summary>
        /// <param name="str">Строка</param>
        /// <param name="re">Регулярное выражеие</param>
        /// <param name="blockName">Название группы</param>
        /// <returns>Искомая подстрока</returns>
        public static string ReadValueFromString(ref string str, Regex re, string blockName)
        {
            var match = re.Match(str);
            string result = null;
            if (match.Success)
            {
                result = match.Groups[blockName].Value;
                str = str.FromPositionToEndLine(match.Index + match.Length);
            }
            return result;
        }

        /// <summary>
        /// Преобразовывает строку в элемент последовательности заданного типа
        /// </summary>
        /// <typeparam name="T">Тип последовательности</typeparam>
        /// <param name="str">Строка</param>
        /// <returns>Элемент</returns>
        public static T GetEnum<T>(string str)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), str, true);
            } 
            catch (ArgumentException)
            {
                return default(T);
            }
        }

        /// <summary>
        /// Разбивает строку на массив определенного типа
        /// </summary>
        /// <typeparam name="T">Тип массива</typeparam>
        /// <param name="str">строка</param>
        /// <param name="separator">разделители</param>
        /// <param name="convertFunction">Функция конвертации строки в объект типа Т</param>
        /// <returns>Массив</returns>
        public static T[] SplitToTypeArray<T>(this string str, string separator, ConvertFunction<string, T> convertFunction)
        {
            var strArray = str.Split(separator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            var Result = new List<T>();

            foreach (var st in strArray)
            {
                T obj;
                if (convertFunction(st, out obj))
                {
                    Result.Add(obj);
                } 
            }

            return Result.ToArray();
        }

        /// <summary>
        /// Разбивает строку на массив чисел
        /// </summary>
        /// <param name="str">строка</param>
        /// <param name="separator">разделители</param>
        /// <returns></returns>
        public static double[] SplitToDoubleArray(this string str, string separator)
        {
            return SplitToTypeArray<double>(str, separator, double.TryParse);
        }

        public delegate bool ConvertFunction<InputT, ResultT>(InputT input, out ResultT result);

        /// <summary>
        /// Возращает позицию новой строки начиная с указанной позиции
        /// </summary>
        /// <param name="str"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static int GetNextLinePosition(this string str, int position)
        {
            if (position >= str.Length) return position;

            var pos = str.IndexOf(Environment.NewLine, position);
            if (pos >= 0)
            {
                return pos + Environment.NewLine.Length;
            }
            return position;
        }

        /// <summary>
        /// Удаялет указанные символы из строки
        /// </summary>
        /// <param name="str"></param>
        /// <param name="CharArray"></param>
        /// <returns></returns>
        public static string RemoveChar(this string str, char[] CharArray)
        {
            var SB = new StringBuilder(str);
            foreach (var ch in CharArray)
            {
                SB.Replace(ch.ToString(), String.Empty);
            }
            return SB.ToString();
        }

        /// <summary>
        /// Удаялет указанные символы из строки
        /// </summary>
        /// <param name="str"></param>
        /// <param name="CharArray"></param>
        /// <returns></returns>
        public static string RemoveChar(this string str, string CharArray)
        {
            return RemoveChar(str, CharArray.ToCharArray());
        }

        /// <summary>
        /// Паттерн регулярного выражения для Email'a
        /// </summary>
        private readonly static Regex EmailRegex = new Regex(@"(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})");
        /// <summary>
        /// Проверяет содержимое строки на корректный адресс элесктрнной почты
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsEmail(this string str)
        {
            return EmailRegex.Match(str).Success;
        }

        /// <summary>
        /// Проверяет содержимое строки на соответсвие числу
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDouble(this string str)
        {
            double D;
            return double.TryParse(str, out D);
        }

        public static string GetFormat(this object obj, string format)
        {
            return String.Format("{0," + format + "}", obj);
        }
    }
}
