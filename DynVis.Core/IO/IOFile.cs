using System.IO;
using ICSharpCode.SharpZipLib.BZip2;

namespace DynVis.Core.IO
{
    /// <summary>
    /// Класс работы с файлами
    /// </summary>
    public static partial class IOFile
    {
        /// <summary>
        /// Размер блока упаковки файла
        /// </summary>
        public const int CompressBlockSize = 128;

        //Функции чтения и запяси фалов
        #region Input/Output Functions

        public static Stream OpenStreamFile(string fileName, FileFilter.Type type)
        {
            using (var Stream = new StreamReader(fileName))
            {
                if (type == FileFilter.Type.Comressed)
                {
                    var memStream = new MemoryStream();
                    BZip2.Decompress(Stream.BaseStream, memStream);
                    return Stream.BaseStream;
                }
                return Stream.BaseStream;
            }
        }

        public static Stream OpenStreamFile(string fileName)
        {
            return OpenStreamFile(fileName, FileFilter.Type.Normal);
        }

        public static string OpenTextFile(string fileName, FileFilter.Type type)
        {
            using (var Stream = new StreamReader(fileName))
            {
                if (type == FileFilter.Type.Comressed)
                {
                    var memStream = new MemoryStream();
                    BZip2.Decompress(Stream.BaseStream, memStream);
                    return Stream.ReadToEnd();
                }
                return Stream.ReadToEnd();
            }
        }

        public static string OpenTextFile(string fileName)
        {
            return OpenTextFile(fileName, FileFilter.Type.Normal);
        }

        public static string TryOpenTextFile(string fileName, FileFilter.Type type)
        {
            try
            {
                return OpenTextFile(fileName, type);
            } catch (IOException)
            {
                return null;
            }
        }

        public static string TryOpenTextFile(string fileName)
        {
            return TryOpenTextFile(fileName, FileFilter.Type.Normal);
        }

        public static void SaveTextFile(string fileName, FileFilter.Type type, string body)
        {
            using (var Stream = new StreamWriter(fileName))
            {
                if (type == FileFilter.Type.Comressed)
                {
                    var memStream = new StreamWriter(new MemoryStream());
                    memStream.WriteLine(body);
                    BZip2.Compress(memStream.BaseStream, Stream.BaseStream, CompressBlockSize);
                }
                else
                {
                    Stream.WriteLine(body);
                }
            }
        }

        public static void SaveTextFile(string fileName, string body)
        {
            SaveTextFile(fileName, FileFilter.Type.Comressed, body);
        }

        public static bool TrySaveTextFile(string fileName, FileFilter.Type type, string body)
        {
            try
            {
                SaveTextFile(fileName, type, body);
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }

        public static bool TrySaveTextFile(string fileName, string body)
        {
            return TrySaveTextFile(fileName, FileFilter.Type.Normal, body);
        }
        #endregion
    }

    public delegate void SetValue<TIndex, T>(TIndex index, T arg);
    public delegate void InitValue<TIndex>(TIndex index);

    public delegate bool ReadObjectFunc<T>(string str, ref int Position, out T obj);
}
