using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DynVis.Core.Controls
{
    public class ColorButton: Button
    {
        public ColorButton()
        {
            Click += ColorButton_Click;
            
            FlatStyle = FlatStyle.Flat;
        }

        private void ColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog.FullOpen = true;
            ColorDialog.Color = Color;
            if (ColorDialog.ShowDialog() == DialogResult.OK)
            {
                Color =  ColorDialog.Color;
            }
        }

        [Description("Цвет")]
        [Category("Appearance")]
        public Color Color
        {
            get
            {
                return BackColor;
            }
            set
            {
                if (BackColor != value)
                {
                    BackColor = value;

                    if (ColorChanged != null) ColorChanged(this, new EventArgs());
                }
            }
        }

        public readonly ColorDialog ColorDialog = new ColorDialog();

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get
            {
                return String.Empty;
            }
            set { }
        }

        public event EventHandler ColorChanged;
    }
}
