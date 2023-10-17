using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DynVis.Core.Controls
{
    public partial class BrushEditor : UserControl
    {
        public BrushEditor()
        {
            InitializeComponent();
        }

        private Brush _EditableBrush;

        [Description("Редактируемая кисть")]
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Brush EditableBrush
        {
            get { return _EditableBrush; }
            set
            {
                if (_EditableBrush != value)
                {
                    _EditableBrush = value;
                    UpdateView();
                    if (EditableBrushChanged != null) EditableBrushChanged(this, new EventArgs());
                }
            }
        }

        private void UpdateView()
        {
            if (EditableBrush is SolidBrush)
            {
                colorButton.Color = ((SolidBrush)EditableBrush).Color;
            } 
        }

        public event EventHandler EditableBrushChanged;

        private void colorButton_ColorChanged(object sender, EventArgs e)
        {
            EditableBrush = new SolidBrush(colorButton.Color);
            var editableBrushBinding = DataBindings["EditableBrush"];
            if (editableBrushBinding != null)
            {
                editableBrushBinding.WriteValue();
            }
        }
    }
}
