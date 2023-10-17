using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using DynVis.Core.Properties;
using ICSharpCode.SharpZipLib.BZip2;

namespace DynVis.Core.IO
{
    /// <summary>
    /// Класс реализующий пользовательские диалоги открытия / сохранения файлов
    /// </summary>
    public static class IOFileDialog
    {
        /// <summary>
        /// Владелей диалогов
        /// </summary>
        private static IWin32Window GlobalOwner
        {
            get
            {
                return ApplicationExtension.GlobalOwner();
            }
        }

        private static readonly SaveFileDialog SaveFileDialog = new SaveFileDialog();
        private static readonly OpenFileDialog OpenFileDialog = new OpenFileDialog();

        /// <summary>
        /// Устанавливает фильтры в диалоги
        /// </summary>
        /// <param name="fileDialog"></param>
        /// <param name="filters"></param>
        private static void SetFilter(FileDialog fileDialog, IEnumerable<FileFilter> filters)
        {
            fileDialog.Filter = String.Empty;
            foreach (var filter in filters)
            {
                fileDialog.Filter += String.IsNullOrEmpty(fileDialog.Filter) ? filter.ToString() : "|" + filter;
            }
        }

        
        /// <summary>
        /// Проверяет возможность сохранения объекта
        /// </summary>
        /// <param name="fileObject"></param>
        /// <returns></returns>
        public static bool IsSaveable(ISaveFile fileObject)
        {
            return fileObject.SaveFilters != null && fileObject.SaveFilters.Length > 0;
        }

        /// <summary>
        /// Сохранить объект в файл
        /// </summary>
        /// <param name="fileObject"></param>
        /// <returns></returns>
        public static bool SaveToFile(ISaveFile fileObject)
        {
            if (IsSaveable(fileObject))
            {
                SetFilter(SaveFileDialog, fileObject.SaveFilters);
                SaveFileDialog.FileName = String.Empty;
                if (SaveFileDialog.ShowDialog(GlobalOwner) == DialogResult.OK)
                {
                    try
                    {
                        using (var Stream = new FileStream(SaveFileDialog.FileName, FileMode.OpenOrCreate))
                        {
                            Stream.SetLength(0);
                            var filter = fileObject.SaveFilters[SaveFileDialog.FilterIndex - 1];
                            if (filter.FileType == FileFilter.Type.Comressed)
                            {
                                var memStream = new MemoryStream();
                                fileObject.SaveToFile(memStream, filter);
                                memStream.Position = 0;
                                BZip2.Compress(memStream, Stream, IOFile.CompressBlockSize);
                            }
                            else
                            {
                                fileObject.SaveToFile(Stream, filter);
                            }
                        }
                        return true;
                    }
                    catch (IOException e)
                    {
                        MessageBox.Show(GlobalOwner, e.Message);
                        
                    }
                }
                
            } 
            else
            {
                MessageBox.Show(GlobalOwner, LangGeneral.NothingToSave, LangGeneral.Saving, MessageBoxButtons.OK,
                                MessageBoxIcon.Asterisk);
                
            }
            return false;
        }

        /// <summary>
        /// Открыть текстовый файл и получить его как строку
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public static string OpenTextFile(FileFilter[] filters)
        {
            SetFilter(OpenFileDialog, filters);
            OpenFileDialog.FileName = String.Empty;
            if (OpenFileDialog.ShowDialog(GlobalOwner) == DialogResult.OK)
            {
                try
                {
                    return IOFile.OpenTextFile(OpenFileDialog.FileName, filters[OpenFileDialog.FilterIndex - 1].FileType);
                }
                catch (IOException e)
                {
                    MessageBox.Show(GlobalOwner, e.Message);
                }
            }
            return null;
        }

        /// <summary>
        /// Открыть объект из фала
        /// </summary>
        /// <param name="fileObject"></param>
        /// <returns></returns>
        public static bool OpenFromFile(IOpenFile fileObject)
        {
            SetFilter(OpenFileDialog, fileObject.OpenFilters);
            OpenFileDialog.FileName = String.Empty;
            if (OpenFileDialog.ShowDialog(GlobalOwner) == DialogResult.OK)
            {
                try
                {
                    using (var stream = new FileStream(OpenFileDialog.FileName, FileMode.Open))
                    {
                        fileObject.OpenFromFile(stream, fileObject.OpenFilters[OpenFileDialog.FilterIndex - 1]);
                    }
                    return true;
                }
                catch (IOException e)
                {
                    MessageBox.Show(GlobalOwner, e.Message);
                }
            }
            return false;
        }

        /// <summary>
        /// Сохранить текстовый файл из строки
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool SaveTextFile(FileFilter filter, string str)
        {
            SetFilter(SaveFileDialog, new[] {filter});
            SaveFileDialog.FileName = String.Empty;
            if (SaveFileDialog.ShowDialog(GlobalOwner) == DialogResult.OK)
            {
                try
                {
                    IOFile.SaveTextFile(SaveFileDialog.FileName, filter.FileType, str);
                }
                catch (IOException e)
                {
                    MessageBox.Show(GlobalOwner, e.Message);
                }
            }
            return false;
        }

        /// <summary>
        /// Открыть объект из фала
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filters"></param>
        /// <param name="openFunc"></param>
        /// <returns></returns>
        public static T OpenObjectFromFile<T>(FileFilter[] filters, OpenObjectForm<T> openFunc)
        {
            SetFilter(OpenFileDialog, filters);
            OpenFileDialog.FileName = String.Empty;
            if (OpenFileDialog.ShowDialog(GlobalOwner) == DialogResult.OK)
            {
                try
                {
                    using (var Stream = new FileStream(OpenFileDialog.FileName, FileMode.Open))
                    {
                        var filter = filters[OpenFileDialog.FilterIndex - 1];
                        if (filter.FileType == FileFilter.Type.Comressed)
                        {
                            var memStream = new MemoryStream();
                            BZip2.Decompress(Stream, memStream, BZip2.Options.NotCloseStream);
                            memStream.Position = 0;
                            return openFunc(memStream, filter);
                        }
                        return openFunc(Stream, filter);
                    }
                }
                catch (IOException e)
                {
                    MessageBox.Show(GlobalOwner, e.Message);
                }
            }
            return default(T);
        }
    }

    /// <summary>
    /// Интерфейс сохраяемых объекстов
    /// </summary>
    public interface ISaveFile
    {
        void SaveToFile(Stream stream, FileFilter filter);
        FileFilter[] SaveFilters { get; }
    }

    /// <summary>
    /// Интерфейс открываемых объекстов
    /// </summary>
    public interface IOpenFile
    {
        void OpenFromFile(Stream stream, FileFilter filter);
        FileFilter[] OpenFilters { get; }
    }

    public delegate void FileOperation(Stream stream, FileFilter filter);
    public delegate T OpenObjectForm<T>(Stream stream, FileFilter filter);

    /// <summary>
    /// Класс фильтр файла
    /// </summary>
    public class FileFilter
    {
        /// <summary>
        /// Описание файла
        /// </summary>
        public readonly string Description;
        /// <summary>
        /// Фильтр
        /// </summary>
        public readonly string Filter;
        /// <summary>
        /// Тип файла
        /// </summary>
        public readonly Type FileType;

        public FileFilter(string description, string filter)
            : this(description, filter, Type.Normal)
        {

        }

        public FileFilter(string description, string filter, Type fileType)
        {
            Description = description;
            FileType = fileType;
            Filter = filter;
        }

        public override string ToString()
        {
            return String.Format("{0}|{1}", Description, Filter);
        }

        /// <summary>
        /// Тип файла
        /// </summary>
        public enum Type
        {
            Normal, //Нормальный
            Comressed //Сжатый
        }
    }

}
