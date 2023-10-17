using System.Windows.Forms;

namespace DynVis.EventsLog
{
    public partial class FormEventLogs : Form
    {
        public FormEventLogs()
        {
            InitializeComponent();
        }

        private void FormEventLogs_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
