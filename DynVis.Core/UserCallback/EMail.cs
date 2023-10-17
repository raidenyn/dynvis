using System;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using DynVis.Core.Properties;
using DynVis.Core.UserCallback;

namespace DynVis.Core.UserCallBack
{
    /// <summary>
    /// Класс для отправки элесктронных писем
    /// </summary>
    public static class EMail
    {
        private const string SmptSever = "smtp.mail.ru";
        private const int SmptPort = 587;
        private const string Sender = "dynvis@mail.ru";
        private const string HomeAdress = "dynvis@narod.ru";

        private const string UserName = "dynvis@mail.ru";
        private const string Password = "dynvis";

        /// <summary>
        /// Заголовок сообщения об ошибке
        /// </summary>
        private static readonly string BugReportTitle = LangGeneral.DynVisBugReport;

        [STAThread]
        private static MailMessage CreateMessage(string subject, string messageText)
        {
            var maile = new MailMessage {From = new MailAddress(Sender), Body = messageText, Subject = subject};

            maile.To.Add(new MailAddress(HomeAdress));

            return maile;
        }

        [STAThread]
        private static void smptClient_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            var resultFunc = e.UserState as Proc<bool>;
            if (resultFunc != null) resultFunc(!e.Cancelled && e.Error == null);
        }

        /// <summary>
        /// Отправляет сообщение разработчику.
        /// Работает асинхронно.
        /// </summary>
        /// <param name="subject">Заголовок письма</param>
        /// <param name="messageText">Текст сообщение</param>
        /// <param name="resultFunc">Функция, которая вызывается по завершении отправки. Может быть null.</param>
        [STAThread]
        public static void MailToHome(string subject, string messageText, Proc<bool> resultFunc)
        {
            var maile = CreateMessage(subject, messageText);

            var smptClient = new SmtpClient(SmptSever, SmptPort)
                                 {
                                     EnableSsl = false,
                                     Credentials = new NetworkCredential(UserName, Password)
                                 };

            smptClient.SendCompleted += smptClient_SendCompleted;

            try
            {
                smptClient.SendAsync(maile, resultFunc);
            }
            catch (SmtpException)
            {
                resultFunc(false);
            }

        }

        /// <summary>
        /// Отпаравляет письмо разраюотчику о ошибке
        /// </summary>
        /// <param name="messageText">Описание ошибки</param>
        /// <param name="resultFunc">Функция, которая вызывается по завершении отправки. Может быть null.</param>
        [STAThread]
        public static void SendBugReport(string messageText, Proc<bool> resultFunc)
        {
            MailToHome(BugReportTitle, messageText, resultFunc);
        }

        /// <summary>
        /// Определяет возможность отправки письма
        /// </summary>
        public static bool CanSendEmail
        {
            get
            {
                return PlatformDepend.InternetConnectedState;
            }
        }
    }
}
