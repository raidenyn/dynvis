using System;
using System.Diagnostics;
using System.Windows.Forms;
using DynVis.Core.Properties;

namespace DynVis.Core.About.Update
{
    /// <summary>
    /// Форма для работы с проверкой обнавлений
    /// </summary>
    public partial class FormCheckNewVersion : Form
    {
        public FormCheckNewVersion()
        {
            InitializeComponent();
        }

        public FormCheckNewVersion(string availableVersion) : this()
        {
            NewVersionAvailable(availableVersion);
            CheckVersion = false;
        }

        private readonly bool CheckVersion = true;

        private void FormCheckNewVersion_Shown(object sender, EventArgs e)
        {
            if (CheckVersion) Updater.GetAvailableVersion(this, ViewResult);
        }

        private void ViewResult(string availableVersion)
        {
            progressBar.Visible = false;

            switch (Updater.CheckNewVersion(availableVersion))
            {
                case Updater.CheckNewVersionResult.CanNotConnect:
                    NoConnect(); break;

                case Updater.CheckNewVersionResult.NewVersionAvailable:
                    NewVersionAvailable(availableVersion); break;

                case Updater.CheckNewVersionResult.NewVersionNotAvailable:
                    NewVersionNotAvailable(); break;
            }
        }

        private void NoConnect()
        {
            textBoxMessage.Text = LangGeneral.CanNotConnect;
        }

        private void NewVersionAvailable(string availableVersion)
        {
            textBoxMessage.Text = LangGeneral.NewVersionAvailable + Environment.NewLine + Environment.NewLine;
            textBoxMessage.Text += LangGeneral.CurrentVersion + Version.FullVersionNumber + Environment.NewLine;
            textBoxMessage.Text += LangGeneral.AvailableVersion + availableVersion;

            buttonLoadNewVersion.Visible = true;
        }

        private void NewVersionNotAvailable()
        {
            textBoxMessage.Text = LangGeneral.CurrentVersionIsUpToDate;
        }

        private void buttonLoadNewVersion_Click(object sender, EventArgs e)
        {
            Process.Start(Updater.DownloadUrl);
            DialogResult = DialogResult.OK;
        }
    }
}
