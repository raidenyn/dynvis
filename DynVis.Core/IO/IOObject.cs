using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DynVis.Math;

namespace DynVis.Core.IO
{
    /// <summary>
    /// Тип посика заголовка блока
    /// </summary>
    public enum FindBlockOption
    {
        None, //Блок возращается сразу после названия
        ToEndLine //Блок возращается с новой строки
    }

    /// <summary>
    /// Интерфей поддержки сохранения состояния класса в строку
    /// </summary>
    public interface IStringable
    {
        bool SetFromString(string st, int startPosition);
        string GetAsString();
    }

    /// <summary>
    /// Класс для работы с записью объектов в текст
    /// </summary>
    public class ObjectTextWriter: IDisposable
    {
        /// <summary>
        /// Создает класс без использования потока данных.
        /// Все данные пишутся в строку, которую можно получить методом ToString.
        /// </summary>
        public ObjectTextWriter(){ }

        /// <summary>
        /// Создает класс c использования потока данных.
        /// По завершении работы класса используется метод Dispose, который перености данные в поток.
        /// </summary>
        /// <param name="stream"></param>
        public ObjectTextWriter(Stream stream)
        {
            Result = new StreamWriter(stream);
        }

        #region Core Functions
        private readonly TextWriter Result;
        private readonly StringBuilder ResultString = new StringBuilder();

        private void Write(string str)
        {
            ResultString.Append(str);
        }

        private void WriteLine(string str)
        {
            ResultString.AppendLine(str);
        }

        private void WriteCaption(string str)
        {
            AppendLine();
            ResultString.AppendLine(String.Format("{0} : ", str));
        }

        public void AppendLine()
        {
            WriteLine(String.Empty);
        }

        public override string ToString()
        {
            return ResultString.ToString();
        }

        private bool Disposed;

        public void Dispose()
        {
            if (!Disposed)
            {
                if (Result != null)
                {
                    Result.Write(ToString());
                    Result.Flush(); //Не закрываем, а только флашим, чтобы поток не закрывался.
                }
                Disposed = true;
            }
        }

        public void Close()
        {
            Dispose();
        }
        #endregion

        #region Write Functions
        public static readonly int StandartLineWidth = 80;

        public void WriteString(string caption, string str)
        {
            WriteLine(String.Format("{0} : {1}", caption, str));
        }

        

        #region Write Object
        public void WriteObject(string caption, IStringable obj)
        {
            WriteString(caption, obj.GetAsString());
        }

        public void WriteObject<T>(string caption, T obj)
        {
            WriteString(caption, obj.ToString());
        }

        public void WriteObject<T>(string caption, T obj, string format) where T: IFormattable
        {
            WriteString(caption, obj.GetFormat(format));
        }

        #endregion

        #region Write Array
        public void WriteObjectArray<T>(string caption, IEnumerable<T> array, string separator, Func<T, string> itemString, int limitLineLength)
        {
            WriteCaption(caption);
            var line = new StringBuilder();
            foreach (var item in array)
            {
                line.Append(itemString(item));
                line.Append(separator);

                if (limitLineLength > 0 && line.Length >= limitLineLength)
                {
                    WriteLine(line.ToString());
                    line.Clear();
                }
            }
            Write(line.ToString());
            AppendLine();
        }

        public void WriteObjectArray<T>(string caption, IEnumerable<T> array, string separator, Func<T, string> itemString)
        {
            WriteObjectArray(caption, array, separator, itemString, 0);
        }

        public void WriteObjectArray(string caption, IEnumerable<IStringable> array, string separator)
        {
            WriteObjectArray(caption, array, separator, item => item.GetAsString());
        }

        public void WriteObjectArray<T>(string caption, IEnumerable<T> array, string separator, int limitLineLength)
        {
            WriteObjectArray(caption, array, separator, item => item.ToString(), limitLineLength);
        }

        public void WriteObjectArray<T>(string caption, IEnumerable<T> array, string separator, int limitLineLength, string format) where T : IFormattable
        {
            WriteObjectArray(caption, array, separator, item => item.GetFormat(format), limitLineLength);
        }

        public void WriteObjectArray<T>(string caption, IEnumerable<T> array, string separator)
        {
            WriteObjectArray(caption, array, separator, item => item.ToString(), 0);
        }

        public void WriteObjectArray<T>(string caption, IEnumerable<T> array, string separator, string format) where T : IFormattable
        {
            WriteObjectArray(caption, array, separator, item => item.GetFormat(format));
        }
        #endregion

        #region Write Series

        public void WriteDoubleSeries(string caption, double min, double max, int count, string separator, Func<double, string> itemString, int limitLineLength)
        {
            WriteCaption(caption);
            var line = new StringBuilder();

            var step = (max - min) / (count - 1);

            for (var counter = 0; counter < count; counter++)
            {
                var item = min + counter*step;
                
                line.Append(itemString(item));
                line.Append(separator);

                if (limitLineLength > 0 && line.Length >= limitLineLength)
                {
                    WriteLine(line.ToString());
                    line.Clear();
                }
            }
            Write(line.ToString());
            AppendLine();
        }

        public void WriteDoubleSeries(string caption, double min, double max, int count, string separator, Func<double, string> itemString) 
        {
            WriteDoubleSeries(caption, min, max, count, separator, itemString, 0);
        }

        public void WriteDoubleSeries(string caption, double min, double max, int count, string separator, int limitLineLength) 
        {
            WriteDoubleSeries(caption, min, max, count, separator, item => item.ToString(), limitLineLength);
        }

        public void WriteDoubleSeries(string caption, double min, double max, int count, string separator, int limitLineLength, string format)
        {
            WriteDoubleSeries(caption, min, max, count, separator, item => item.GetFormat(format), limitLineLength);
        }

        public void WriteDoubleSeries(string caption, double min, double max, int count, string separator) 
        {
            WriteDoubleSeries(caption, min, max, count, separator, item => item.ToString(), 0);
        }

        public void WriteDoubleSeries(string caption, double min, double max, int count, string separator, string format) 
        {
            WriteDoubleSeries(caption, min, max, count, separator, item => item.GetFormat(format));
        }
        #endregion

        #region Write Series 2D
        public void WriteObjectSeries2D(string caption, double min1, double max1, int count1, double min2, double max2, int count2, Func<double, double, string> itemString, string separator, int limitLineLength)
        {
            WriteCaption(caption);
            var line = new StringBuilder();

            var step1 = (max1 - min1) / (count1 - 1);
            var step2 = (max2 - min2) / (count2 - 1);

            for (var counter1 = 0; counter1 < count1; counter1++)
            {
                for (var counter2 = 0; counter2 < count2; counter2++)
                {
                    var item1 = min1 + counter1 * step1;
                    var item2 = min2 + counter2 * step2;

                    line.Append(itemString(item1, item2));
                    line.Append(separator);

                    if (limitLineLength > 0 && line.Length >= limitLineLength)
                    {
                        WriteLine(line.ToString());
                        line.Clear();
                    }
                }
            }

            Write(line.ToString());
            AppendLine();
        }

        public void WriteObjectSeries2D(string caption, double min1, double max1, int count1, double min2, double max2, int count2, Func<double, double, string> itemString, string separator) 
        {
            WriteObjectSeries2D(caption, min1, max1, count1, min2, max2, count2, itemString, separator, 0);
        }

        public void WriteObjectSeries2D(string caption, double min1, double max1, int count1, double min2, double max2, int count2, Func<double, double, double> getItem, string separator, int limitLineLength)
        {
            WriteObjectSeries2D(caption, min1, max1, count1, min2, max2, count2, (item1, item2) => getItem(item1, item2).ToString(), separator, limitLineLength);
        }

        public void WriteObjectSeries2D(string caption, double min1, double max1, int count1, double min2, double max2, int count2, Func<double, double, double> getItem, string separator, int limitLineLength, string format)
        {
            WriteObjectSeries2D(caption, min1, max1, count1, min2, max2, count2, (item1, item2) => getItem(item1, item2).GetFormat(format), separator, limitLineLength);
        }

        public void WriteObjectSeries2D(string caption, double min1, double max1, int count1, double min2, double max2, int count2, Func<double, double, double> getItem, string separator)
        {
            WriteObjectSeries2D(caption, min1, max1, count1, min2, max2, count2, (item1, item2) => getItem(item1, item2).ToString(), separator, 0);
        }

        public void WriteObjectSeries2D(string caption, double min1, double max1, int count1, double min2, double max2, int count2, Func<double, double, double> getItem, string separator, string format)
        {
            WriteObjectSeries2D(caption, min1, max1, count1, min2, max2, count2, (item1, item2) => getItem(item1, item2).GetFormat(format), separator, 0);
        }
        #endregion

        #region Write Array 2D
        public void WriteObjectArray2D<T>(string caption, T[,] array, string separator, Func<T, string> itemString, int limitLineLength)
        {
            var array1D = array.GetArray();
            WriteObjectArray(caption, array1D, separator, itemString, limitLineLength);
        }

        public void WriteObjectArray2D<T>(string caption, T[,] array, string separator, Func<T, string> itemString)
        {
            WriteObjectArray2D(caption, array, separator, itemString, 0);
        }

        public void WriteObjectArray2D<T>(string caption, T[,] array, string separator, int limitLineLength)
        {
            WriteObjectArray2D(caption, array, separator, item => item.ToString(), limitLineLength);
        }

        public void WriteObjectArray2D<T>(string caption, T[,] array, string separator)
        {
            WriteObjectArray2D(caption, array, separator, 0);
        }

        public void WriteObjectArray2D<T>(string caption, T[,] array, string separator, string format, int limitLineLength) 
        {
            WriteObjectArray2D(caption, array, separator, item => item.GetFormat(format), limitLineLength);
        }

        public void WriteObjectArray2D<T>(string caption, T[,] array, string separator, string format)
        {
            WriteObjectArray2D(caption, array, separator, 0);
        }
        #endregion

        #endregion

        public readonly static string DecimalFormat = "15:F9";
        public readonly static string IntegerFormat = "15";
        public readonly static string StandartSeparator = "  ";
    }

    /// <summary>
    /// Класс для чтения массивов объектов
    /// </summary>
    public class ObjectTextReader
    {
        /// <summary>
        /// Создает класс c использования потока данных.
        /// По завершении работы класса используется метод Dispose, который перености данные в поток.
        /// </summary>
        /// <param name="source"></param>
        public ObjectTextReader(string source)
        {
            Source = source;
        }

        private readonly string Source;

        /// <summary>
        /// Находит позицию блока с заданым названием. Возращает позицию отвечающую началу блока (без имени)
        /// </summary>
        /// <param name="caption">имя блока</param>
        /// <returns>Позицию отвечающая началу блока</returns>
        private int FindBlock(string caption)
        {
            var blockFinder = new Regex(caption + " *:");

            var Match = blockFinder.Match(Source);
            if (Match.Success)
            {
                return Match.Index + Match.Length;
            }
            return -1;
        }

        private string FindBlockBody(string caption, string endSection)
        {
            var position = FindBlock(caption);
            if (position >= 0)
            {
                var EndOfBlockBody = new Regex(endSection);
                var endSectionMatch = EndOfBlockBody.Match(Source, position);
                int endPosition;
                if (endSectionMatch.Success)
                {
                    endPosition = endSectionMatch.Index;
                }
                else
                {
                    endPosition = Source.Length;
                }
                return Source.Substring(position, endPosition - position);
            }
            return null;
        }

        public char[] Separators = " \n\r".ToCharArray();
        public string EndSectionPattern = @"\n *\r?\n";
        
        
        public delegate bool GetObjectFromStringFunc<T>(string source, ref int position, out T obj);

        public T ReadObject<T>(string caption, GetObjectFromStringFunc<T> getObject, T defaultObj)
        {
            var position = FindBlock(caption);
            if (position >= 0)
            {
                T obj;
                if (getObject(Source, ref position, out obj))
                {
                    return obj;
                }
            }
            return defaultObj;
        }

        /// <summary>
        /// Читает из строки объект. Возращает true, если объекст востановлен верно.
        /// </summary>
        /// <param name="caption">Имя объекта</param>
        /// <param name="obj">Объект</param>
        /// <returns></returns>
        public bool ReadObject(string caption, IStringable obj)
        {
            var Position = FindBlock(caption);
            if (Position < 0)
            {
                return false;
            }
            return obj.SetFromString(Source, Position);
        }

        public T[] ReadObjectArray<T>(string caption, GetObjectFromStringFunc<T> getObject)
        {
            var position = FindBlock(caption);
            if (position >= 0)
            {
                var result = new List<T>();
                T obj;
                while (getObject(Source, ref position, out obj))
                {
                    result.Add(obj);
                }
                return result.ToArray();
            }
            return null;
        }

        public string[] ReadStringArray(string caption, char[] separators, string endSection)
        {
            var BlockBody = FindBlockBody(caption, endSection);
            if (!String.IsNullOrEmpty(BlockBody))
            {
                return BlockBody.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            }
            return null;
        }

        public T[] ReadArray<T>(string caption, char[] separators, string endSection)
        {
            var stringArray = ReadStringArray(caption, separators, endSection);
            var resultArray = new List<T>();
            if (stringArray != null)
            {
                foreach (var str in stringArray)
                {
                    resultArray.Add((T)Convert.ChangeType(str, typeof(T)));
                }
                return resultArray.ToArray();
            }
            return null;
        }

        public T[,] ReadArray2D<T>(string caption, char[] separators, string endSection, int length1, int length2)
        {
            var array1D = ReadArray<T>(caption, separators, endSection);
            return array1D.GetArray2D(length1, length2);
        }

    }

    /// <summary>
    /// Класс для работы с записью массивов в виде таблицы
    /// </summary>
    public class TextTableWriter : IDisposable
    {
        private readonly List<List<string>> Columns = new List<List<string>>();

        private readonly TextWriter TextWriter;

        public TextTableWriter(Stream stream)
        {
            TextWriter = new StreamWriter(stream);
        }

        private static void AppendRow(ICollection<string> column, int length, string str)
        {
            column.Add(str.GetFormat(length.ToString()));
        }

        private static void AppendHeader(ICollection<string> column, int length, string str)
        {
            column.Add(str.GetFormat(length.ToString()));
        }

        public readonly static string DecimalFormat = "15:F9";
        public readonly static string StandartSeparator = "  ";


        #region ArrayFunctions
        public void AppendArrayColumn<T>(string caption, int length, IEnumerable<T> array, Func<T, string> itemString)
        {
            var Column = new List<string>();

            AppendHeader(Column, length, caption);

            foreach (var item in array)
            {
                AppendRow(Column, length, itemString(item));
            }

            Columns.Add(Column);
        }

        public void AppendArrayColumn<T>(string caption, int length, IEnumerable<T> array, Func<T, object> itemFunc, string format)
        {
            AppendArrayColumn(caption, length, array, item => itemFunc(item).GetFormat(format));
        }

        public void AppendArrayColumn<T>(string caption, int length, IEnumerable<T> array)
        {
            AppendArrayColumn(caption, length, array, item => item.ToString());
        }

        public void AppendArrayColumn<T>(string caption, int length, IEnumerable<T> array, string format)
        {
            AppendArrayColumn(caption, length, array, item => item.GetFormat(format));
        }
        #endregion

        #region Series Functions
        public void AppendSeriesColumn<T>(string caption, int length, T min, T max, Func<T, T> increment, Func<T, string> itemString) where T : IComparable
        {
            var Column = new List<string>();

            AppendHeader(Column, length, caption);

            for (var item = min; item.CompareTo(max) <=0; item = increment(item))
            {
                AppendRow(Column, length, itemString(item));
            }

            Columns.Add(Column);
        }

        public void AppendSeriesColumn<T>(string caption, int length, T min, T max, Func<T, T> increment) where T : IComparable
        {
            AppendSeriesColumn(caption, length, min, max, increment, item => item.ToString());
        }

        public void AppendSeriesColumn<T>(string caption, int length, T min, T max, Func<T, T> increment, string format) where T : IComparable
        {
            AppendSeriesColumn(caption, length, min, max, increment, item => item.GetFormat(format));
        }
        #endregion

        #region Series 2D Function
        public void AppendSeries2DColumn<T>(string caption, int length, T min1, T max1, Func<T, T> increment1, T min2, T max2, Func<T, T> increment2, Func<T, T, string> itemString) where T : IComparable
        {
            var Column = new List<string>();

            AppendHeader(Column, length, caption);

            for (var item1 = min1; item1.CompareTo(max1) <= 0; item1 = increment1(item1))
            {
                for (var item2 = min2; item2.CompareTo(max2) <= 0; item2 = increment2(item2))
                {
                    AppendRow(Column, length, itemString(item1, item2));
                }
            }

            Columns.Add(Column);
        }

        public void AppendSeries2DColumn<T>(string caption, int length, T min1, T max1, Func<T, T> increment1, T min2, T max2, Func<T, T> increment2, Func<T, T, T> itemString) where T : IComparable
        {
            AppendSeries2DColumn(caption, length, min1, max1, increment1, min2, max2, increment2, (item1, item2) => itemString(item1, item2).ToString());
        }

        public void AppendSeries2DColumn<T>(string caption, int length, T min1, T max1, Func<T, T> increment1, T min2, T max2, Func<T, T> increment2, Func<T, T, T> itemString, string format) where T : IComparable
        {
            AppendSeries2DColumn(caption, length, min1, max1, increment1, min2, max2, increment2, (item1, item2) => itemString(item1, item2).GetFormat(format));
        }
        #endregion

        #region Array 2D Function
        public void AppendArray2DColumn<T>(string caption, int length, T[,] array, Func<T, string> itemString) where T : IComparable
        {
            AppendSeries2DColumn(caption, length, 0, array.GetLength(0), i => ++i, 0, array.GetLength(1), i => ++i,
                                 (q1, q2) => itemString(array[q1, q2]));
        }

        public void AppendArray2DColumn<T>(string caption, int length, T[,] array) where T : IComparable
        {
            AppendArray2DColumn(caption, length, array, item => item.ToString());
        }

        public void AppendArray2DColumn<T>(string caption, int length, T[,] array, string format) where T : IComparable
        {
            AppendArray2DColumn(caption, length, array, item => item.GetFormat(format));
        }
        #endregion

        #region Addition Functions
        public void AppendDoubleSeries2DColumn(string caption, int length, double min1, double max1, int count1, double min2, double max2, int count2, Func<double, double, double> item)
        {
            var step1 = (max1 - min1) / (count1 - 1);
            var step2 = (max2 - min2) / (count2 - 1);

            AppendSeries2DColumn(caption, length, min1, max1, q => q + step1, min2, max2, q => q + step2, item, DecimalFormat);
        }
        #endregion

        private bool Disposed;

        /// <summary>
        /// Разрушает объект и записывает данные в поток.
        /// </summary>
        public void Dispose()
        {
            if (!Disposed)
            {
                CreateTable();
                TextWriter.Flush(); //Не закрываем, а только флашим, чтобы поток не закрывался.
                Disposed = true;
            }
        }

        public void Close()
        {
            Dispose();
        }

        /// <summary>
        /// Создает конечную таблицу
        /// </summary>
        private void CreateTable()
        {
            var Table = new StringBuilder();
            var maxRowCount = MaxRowCount();
            
            for (var i= 0; i < maxRowCount; i++)
            {
                foreach (var column in Columns)
                {
                    Table.Append(column[i]);
                }
                Table.AppendLine();
            }
            TextWriter.WriteLine(Table.ToString());
        }

        private int MaxRowCount()
        {
            return Columns.Max(columns => columns.Count);
        }
    }

    /// <summary>
    /// Класс для работы с чтения массивов из таблицы
    /// </summary>
    public class TextTableReader
    {
        private readonly string Source; 
        
        public TextTableReader(string source)
        {
            Source = source;
        }

        public void ReadTable(int startPosition, InitValue<int> NewLine, SetValue<int, double>[] setValue)
        {
            var CurrPos = startPosition;
            string Line;
            int numberCount;
            int CurrentLineCount = 0;

            do
            {
                Line = Source.FromPositionToEndLine(CurrPos);

                var Array = Line.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                numberCount = Array.Length;

                if (numberCount > 0)
                {
                    NewLine(CurrentLineCount);

                    var i = 0;
                    foreach (var str in Array)
                    {
                        double D;
                        if (double.TryParse(str, out D))
                        {
                            setValue[i++](CurrentLineCount, D);
                        }
                    }

                }

                CurrentLineCount++;
                CurrPos += Line.Length + Environment.NewLine.Length;
            } while (numberCount != 0);

        }
    }
}
