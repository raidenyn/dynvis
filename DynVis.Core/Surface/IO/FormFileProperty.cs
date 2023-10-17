using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DynVis.Core.Surface.IO
{
    public partial class FormFileProperty : Form
    {
        public FormFileProperty()
        {
            InitializeComponent();

            UpdateValue();
        }

        private int _Q1Count = 100;
        [DefaultValue(100)]
        public int Q1Count
        {
            get { return _Q1Count; }
            set { _Q1Count = value; UpdateValue(); }
        }
        private int _Q2Count = 100;
        [DefaultValue(100)]
        public int Q2Count
        {
            get { return _Q2Count; }
            set { _Q2Count = value; UpdateValue(); }
        }

        public bool FirstDerivative
        {
            get { return checkBoxFirstDerivative.Checked; }
            set { checkBoxFirstDerivative.Checked = value; UpdateValue(); }
        }

        public bool SecondDerivative
        {
            get { return checkBoxSecondDerivative.Checked; }
            set { checkBoxSecondDerivative.Checked = value; UpdateValue(); }
        }

        private void UpdateValue()
        {
            textBoxQ1Count.Text = Q1Count.ToString();
            textBoxQ2Count.Text = Q2Count.ToString();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void FormNumberPoint_FormClosing(object sender, FormClosingEventArgs e)
        {
            int i;
            if (int.TryParse(textBoxQ1Count.Text, out i) && i > 2)
            {
                _Q1Count = i;
            }
            else
            {
                textBoxQ1Count.Select();
                e.Cancel = true;
                return;
            }

            if (int.TryParse(textBoxQ2Count.Text, out i) && i > 2)
            {
                _Q2Count = i;
            }
            else
            {
                textBoxQ2Count.Select();
                e.Cancel = true;
                return;
            }


        }
    }
}
