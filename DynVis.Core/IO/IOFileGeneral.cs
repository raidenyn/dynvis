using System.IO;

namespace DynVis.Core.IO
{
    partial class IOFile
    {
        /// <summary>
        /// Возращает список dll файлов в каталоге
        /// </summary>
        /// <param name="pathFolder">Путь к каталогу</param>
        /// <returns></returns>
        public static FileInfo[] GetDllsFromFolder(string pathFolder)
        {
            try
            {
                var directory = new DirectoryInfo(pathFolder);

                return directory.GetFiles("*.dll", SearchOption.AllDirectories);
            } 
            catch (IOException)
            {
                return new FileInfo[0];
            }
        }
    }
}
