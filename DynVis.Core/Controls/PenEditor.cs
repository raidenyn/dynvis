using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DynVis.Core.Controls
{
    public partial class PenEditor : UserControl
    {
        public PenEditor()
        {
            InitializeComponent();
        }

        private Pen _Pen;

        [Description("Карандаш")]
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Pen Pen
        {
            get
            {
                return _Pen;
            }
            set
            {
                if (Pen != null)
                {
                    penBindingSource.Remove(Pen);
                }
                _Pen = value;
                if (Pen != null)
                {
                    penBindingSource.Add(Pen);
                }
                if (PenChanged != null) PenChanged(this, new EventArgs());
            }
        }

        [Description("Заголовок")]
        [Category("Appearance")]
        public string Tittle
        {
            get
            {
                return groupBox.Text;
            }
            set
            {
                groupBox.Text = value;
            }
        }

        public event EventHandler PenChanged;

    }
}
