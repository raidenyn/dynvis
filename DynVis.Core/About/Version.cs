using System;
using System.Windows.Forms;

namespace DynVis.Core.About
{
    /// <summary>
    /// Класс определяющий версию программы
    /// </summary>
    public static class Version
    {
        /// <summary>
        /// Название приложения
        /// </summary>
        public static string Name
        {
            get { return Application.ProductName; }
        }

        /// <summary>
        /// Полная версия приложения
        /// </summary>
        public static string FullVersionNumber
        {
            get { return Application.ProductVersion; }
        }

        /// <summary>
        /// Кораткоя версия приложения
        /// </summary>
        public static string VersionNumber
        {
            get { return FullVersionNumber.Substring(0, FullVersionNumber.IndexOf('.', 2)); }
        }

        /// <summary>
        /// Название с полной версией
        /// </summary>
        public static string NameWithFullVersion
        {
            get { return String.Format("{0} {1}", Name, FullVersionNumber); }
        }

        /// <summary>
        /// Название с короткой версией
        /// </summary>
        public static string NameWithVersion
        {
            get { return String.Format("{0} {1}", Name, VersionNumber); }
        }

    }
}
