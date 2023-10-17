using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DynVis.Core.Controls;
using DynVis.Core.Properties;
using DynVis.EventsLog;
using Version=DynVis.Core.About.Version;

namespace DynVis.Core.UserCallBack.BugReports
{
    /// <summary>
    /// Класс для работы с отправкой сообщений об ошибках
    /// </summary>
    public static class BugReports
    {
        static BugReports()
        {
            Application.ThreadException += Application_ThreadException;
        }

        /// <summary>
        /// Функция для вызова конструктора класса
        /// </summary>
        public static void Init() { }

        /// <summary>
        /// Форма ожидания без точного прогресса
        /// </summary>
        [ThreadStatic]
        private static FormWaitResult WaitForm;

        /// <summary>
        /// Событие, срабаывающая при неперехваченном эксэпшене в приложении
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [STAThread]
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            SendBugReportDialog(e.Exception);
        }

        /// <summary>
        /// Показывает диалог отправки сообщения об ошибке
        /// </summary>
        /// <param name="e">Исключение - причина ошибки.</param>
        [STAThread]
        public static void SendBugReportDialog(Exception e)
        {
            var form = new FormBugReport();
            if (form.ShowDialog() == DialogResult.OK)
            {
                WaitForm = new FormWaitResult { Tittle = LangGeneral.SendingMessage };

                SendBugReport(form.MessageText, form.UserMail, e, SendResult);

                WaitForm.ShowDialog();

                WaitForm.Dispose();
            }
            Application.Exit();
        }

        [STAThread]
        private static void SendResult(bool result)
        {
            WaitForm.Complite(result ? LangGeneral.MessageSent : LangGeneral.MessageDoesNotSend);
        }

        /// <summary>
        /// Отправляет отчет об ошибке
        /// </summary>
        /// <param name="UserMessage">Комментарий пользователя</param>
        /// <param name="UserMail">Эмэйл пользователя</param>
        /// <param name="exception">Исключение</param>
        /// <param name="resultFunc">Функция, которая будет вызвана по окончании отправки</param>
        [STAThread]
        public static void SendBugReport(string UserMessage, string UserMail, Exception exception, Proc<bool> resultFunc)
        {
            var message = new StringBuilder();

            message.AppendLine(LangGeneral.BugReport + Version.NameWithFullVersion);
            message.AppendLine();
            message.AppendLine(LangGeneral.UserComment);
            message.AppendLine(UserMessage);
            message.AppendLine();
            message.AppendLine(LangGeneral.UserEmail + UserMail);
            message.AppendLine();
            message.AppendLine(LangGeneral.ExceptionText);
            message.AppendLine(exception.ToString());
            message.AppendLine();
            message.AppendLine(LangGeneral.ProgrammLog);
            message.AppendLine(Log.ToString());

            EMail.SendBugReport(message.ToString(), resultFunc);
        }
    }
}
