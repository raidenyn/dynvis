using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace DynVis.Core.About
{
    /// <summary>
    /// Форма "О Программе"
    /// </summary>
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {
            labelProjectName.Text = Version.NameWithFullVersion;
        }

        private void linkLabelLinkToSite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(String.Format("http://{0}", linkLabelLinkToSite.Text));
        }

        private void linkLabelMailTo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(String.Format("mailto:{0}", linkLabelMailTo.Text));
        }
    }
}
