using System;
using System.Windows.Forms;
using System.ComponentModel;
using DynVis.Core.Properties;

namespace DynVis.Core.UserCallBack
{
    /// <summary>
    /// Форма ввода соббщения обратной связи
    /// </summary>
    public partial class UserMessage : UserControl
    {
        public UserMessage()
        {
            InitializeComponent();
        }

        private void checkBoxReceiveAnswer_CheckedChanged(object sender, EventArgs e)
        {
            textBoxUserMail.ReadOnly = !checkBoxReceiveAnswer.Checked;
        }

        [Description("Заголовок формы")]
        [Category("Input")]
        public string Tittle
        {
            get
            {
                return label1.Text;
            }
            set
            {
                label1.Text = value;
            }
        }

        /// <summary>
        /// Пользователь пожелал получить ответ
        /// </summary>
        [DefaultValue(false)]
        [Description("Пользователь хочет получить письмо ответ")]
        [Category("Input")]
        public bool IsUserMail
        {
            get
            {
                return checkBoxReceiveAnswer.Checked;
            }
            set
            {
                checkBoxReceiveAnswer.Checked  = value;
            }
        }

        [DefaultValue("")]
        [Description("Эдектронная почта пользователя")]
        [Category("Input")]
        public string UserMail
        {
            get
            {
                return checkBoxReceiveAnswer.Checked ? textBoxUserMail.Text : String.Empty;
            }
            set
            {
                textBoxUserMail.Text = value;
            }
        }

        [DefaultValue("")]
        [Description("Сообщение пользователя")]
        [Category("Input")]
        public string MessageText
        {
            get
            {
                return textBoxUserMessage.Text;
            }
            set
            {
                textBoxUserMessage.Text = value;
            }
        }

        /// <summary>
        /// Событи, возникающая при нажатии кнопки "Отправить"
        /// </summary>
        public event EventHandler SendMessage;

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (IsUserMail)
            {
                if (!UserMail.IsEmail())
                {
                    textBoxUserMail.Select();
                    MessageBox.Show(this, LangGeneral.InputWrongEmail, LangGeneral.ErrorMessage,
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (SendMessage != null) SendMessage(this, e);
        }
    }
}
