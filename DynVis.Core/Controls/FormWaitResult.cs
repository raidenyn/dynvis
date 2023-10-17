using System.ComponentModel;
using System.Windows.Forms;

namespace DynVis.Core.Controls
{
    public partial class FormWaitResult : Form
    {
        public FormWaitResult()
        {
            InitializeComponent();
        }

        [DefaultValue("")]
        [Description("Заголовок полосы прокрутки")]
        [Category("Appearance")]
        public string Tittle
        {
            get { return labelTittle.Text; }
            set { labelTittle.Text = value; }
        }

        public void Complite(string textTittle)
        {
            Tittle = textTittle;
            progressBar.Visible = false;
            buttonClose.Visible = true;
        }
    }
}
