using System;
using System.Windows.Forms;
using DynVis.Core.Controls;
using DynVis.Core.Properties;
using DynVis.Core.UserCallBack;
using DynVis.EventsLog;

namespace DynVis.Core.UserCallback
{
    /// <summary>
    /// Класс работы с обратной связью
    /// </summary>
    public static class Callback
    {
        [ThreadStatic]
        private static FormWaitResult WaitForm;

        /// <summary>
        /// Показывает диалог обратной связи
        /// </summary>
        public static void SendCallbackDialog()
        {
            var form = new FormCallBack();
            if (form.ShowDialog() == DialogResult.OK)
            {
                WaitForm = new FormWaitResult { Tittle = LangGeneral.SendingMessage };

                var message = form.MessageText;
                if (!String.IsNullOrEmpty(form.UserMail))
                {
                    message += Environment.NewLine + LangGeneral.CallbackEmail + form.UserMail;
                }
                message += Environment.NewLine + Environment.NewLine;
                message += Log.ToString();

                EMail.MailToHome(LangGeneral.DynVisResponse, message, SendResult);

                WaitForm.ShowDialog();

                WaitForm.Dispose();
            }
        }

        [STAThread]
        private static void SendResult(bool result)
        {
            WaitForm.Complite(result ? LangGeneral.MessageSent : LangGeneral.MessageDoesNotSend);
        }
    }
}
