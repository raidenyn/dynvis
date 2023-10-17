using System.ComponentModel;
using System.Windows.Forms;
using DynVis.Core;

namespace DynVis.Forms
{
    public partial class FormBase : Form
    {
        public FormBase()
        {
            InitializeComponent();
        }

        private ReactionData _ReactionData;
        [Browsable(false)]
        public ReactionData ReactionData
        {
            get
            {
                return _ReactionData;
            }
            set
            {
                if (ReactionData != null)
                {
                    UnsetEvents();
                }

                _ReactionData = value;

                if (ReactionData != null)
                {
                    SetEvents();
                }
            }
        }

        protected virtual void SetEvents()
        {
            
        }

        protected virtual void UnsetEvents()
        {

        }

        private void buttonAllwaysOntop_Click(object sender, System.EventArgs e)
        {
            TopMost = !TopMost;
        }

        public new bool TopMost
        {
            get { return base.TopMost; }
            set
            {
                base.TopMost = value;

                buttonAllwaysOntop.ImageIndex = TopMost ? 1 : 0;
            }
        }

        private void FormBase_Load(object sender, System.EventArgs e)
        {
            buttonAllwaysOntop.Left = ClientSize.Width - buttonAllwaysOntop.Width;
            buttonAllwaysOntop.BringToFront();
        }

        private void FormBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }


    }
}
