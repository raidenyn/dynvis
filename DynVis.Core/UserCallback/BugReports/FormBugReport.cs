using System;
using System.Windows.Forms;

namespace DynVis.Core.UserCallBack.BugReports
{
    public partial class FormBugReport : Form
    {
        public FormBugReport()
        {
            InitializeComponent();
        }

        private void userMessage_SendMessage(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        public bool IsUserMail
        {
            get
            {
                return !String.IsNullOrEmpty(UserMail);
            }
        }

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

        private void FormBugReport_Load(object sender, EventArgs e)
        {
            userMessage.Visible = EMail.CanSendEmail;
            buttonClose.Visible = !userMessage.Visible;
            if (!userMessage.Visible) Height -= userMessage.Height;
        }
    }
}
