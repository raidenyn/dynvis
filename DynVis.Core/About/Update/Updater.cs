using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace DynVis.Core.About.Update
{
    /// <summary>
    /// Класс для проверки новой весрии на сайте 
    /// </summary>
    public static class Updater
    {
        //Адреса проверки
#if DEBUG
        /*
        public static readonly string CheckUpdateUrl = "http://dynvis/downloads/availableversion.xml";

        public static readonly string DownloadUrl = "http://dynvis/download.htm";
        */
        public static readonly string CheckUpdateUrl = "http://www.dynvis.narod.ru/downloads/availableversion.xml";

        public static readonly string DownloadUrl = "http://www.dynvis.narod.ru/download.htm";
#else
        public static readonly string CheckUpdateUrl = "http://www.dynvis.narod.ru/downloads/availableversion.xml";

        public static readonly string DownloadUrl = "http://www.dynvis.narod.ru/download.htm";
#endif

        /// <summary>
        /// Вспомогательный класс для асинхронной проверки
        /// </summary>
        class NewVersionChecker
        {
            private readonly Proc<string> ResultFunction;

            private readonly Control Control;

            public NewVersionChecker(Control control, Proc<string> resultFunction)
            {
                Control = control;
                ResultFunction = resultFunction;

                CheckVersionThread = new Thread(CheckVersion) {IsBackground = true};
                CheckVersionThread.Start();
            }

            private readonly Thread CheckVersionThread;

            private void CheckVersion()
            {
                string version = GetAvailableVersion();
                if (Control != null)
                {
                    if (Control.Visible)
                        Control.Invoke(ResultFunction, version);
                } 
                else
                {
                    ResultFunction.Invoke(version);
                }
            }

            private static string GetAvailableVersion()
            {
                try
                {
                    var xmlAvailableVersion = new XmlTextReader(CheckUpdateUrl);

                    while (xmlAvailableVersion.Read())
                    {
                        var name = xmlAvailableVersion.Name;
                        if (name == Version.Name)
                        {
                            return xmlAvailableVersion.ReadElementContentAsString();
                        }
                    }

                }
                catch (WebException) { }
                catch (UriFormatException) { }
                catch (InvalidOperationException) { }
                catch (IOException) { }

                return String.Empty;
            }
        }

        /// <summary>
        /// Функция получения доступной версии. Работает асинхронно.
        /// </summary>
        /// <param name="control">Контрол внешнего потока</param>
        /// <param name="resultFunction">Функция вызываемая по окночании проверки</param>
        public static void GetAvailableVersion(Control control, Proc<string> resultFunction)
        {
            new NewVersionChecker(control, resultFunction);
        }

        /// <summary>
        /// Функция получения доступной версии. Работает асинхронно.
        /// </summary>
        /// <param name="resultFunction">Функция вызываемая по окночании проверки</param>
        public static void GetAvailableVersion(Proc<string> resultFunction)
        {
            GetAvailableVersion(null, resultFunction);
        }

        /// <summary>
        /// Возможные результаты проверки новой версии
        /// </summary>
        public enum CheckNewVersionResult
        {
            /// <summary>
            /// Доступна новая версия
            /// </summary>
            NewVersionAvailable, 
            /// <summary>
            /// Новоая версия недоступна (равна текущей)
            /// </summary>
            NewVersionNotAvailable,
            /// <summary>
            /// Невозможно установить соединение с сервером (ошибка при проверке)
            /// </summary>
            CanNotConnect
        }

        /// <summary>
        /// Функция идентифицирующая строку полученную с сайта и возращающая результат проверки.
        /// </summary>
        /// <param name="availibleVersion">Строка с сайта</param>
        /// <returns></returns>
        public static CheckNewVersionResult CheckNewVersion(string availibleVersion)
        {
            if (String.IsNullOrEmpty(availibleVersion))
            {
                return CheckNewVersionResult.CanNotConnect;
            }
            if (availibleVersion.CompareTo(Version.FullVersionNumber) <= 0)
            {
                return CheckNewVersionResult.NewVersionNotAvailable;
            }
            return CheckNewVersionResult.NewVersionAvailable;
        }

        /// <summary>
        /// Функция показывающая диалог проверки новой версии
        /// </summary>
        /// <returns>истина - если пользователь согласился скачать новую версию</returns>
        public static bool CheckNewVersionDialog()
        {
            var form = new FormCheckNewVersion();

            return form.ShowDialog(ApplicationExtension.GlobalOwner()) == DialogResult.OK;
        }

        /// <summary>
        /// Асинхронно проверяет доступность новой версии. При налии таковой показывает диалог для скачивания.
        /// </summary>
        public static void CheckNewVersion()
        {
            GetAvailableVersion(CheckNewVersionResultFunc);
        }

        private static void CheckNewVersionResultFunc(string availibleVersion)
        {
            if (CheckNewVersion(availibleVersion) == CheckNewVersionResult.NewVersionAvailable)
            {
                var form = new FormCheckNewVersion();
                form.ShowDialog();
            }
        }
    }
}
