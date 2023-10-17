using System;
using System.Windows.Forms;

namespace DynVis.Core.UserCallback
{
    /// <summary>
    /// Форма ввода соббщения обратной связи
    /// </summary>
    internal partial class FormCallBack : Form
    {
        public FormCallBack()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Пользователь пожелал получить ответ
        /// </summary>
        public bool IsUserMail
        {
            get
            {
                return !String.IsNullOrEmpty(UserMail);
            }
        }

        /// <summary>
        /// Электронная почта пользователя. Пустая, если ответ не требуется.
        /// </summary>
        public string UserMail
        {
            get
            {
                return userMessage.UserMail;
            }
            set
            {
                userMessage.UserMail = value;
            }
        }

        /// <summary>
        /// Сообщение пользователя
        /// </summary>
        public string MessageText
        {
            get
            {
                return userMessage.MessageText;
            }
            set
            {
                userMessage.MessageText = value;
            }
        }

        private void userMessage_SendMessage(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
