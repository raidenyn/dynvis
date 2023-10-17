using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DynVis.Core.Properties;

namespace DynVis.Core.Controls
{
    public partial class FontEditor : UserControl
    {
        public FontEditor()
        {
            InitializeComponent();
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

        private Font _EditableFont;

        [Description("Редактируемый шрифт")]
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Font EditableFont
        {
            get
            {
                return _EditableFont;
            }
            set
            {
                if (EditableFont != value)
                {
                    _EditableFont = value;
                    UpdateView();
                    if (EditableFontChanged != null) EditableFontChanged(this, new EventArgs());
                }
            }
        }

        [Description("Редактируемый кисть")]
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Brush EditableBrush
        {
            get
            {
                return brushEditor.EditableBrush;
            }
            set
            {
                if (EditableBrush != value)
                {
                    brushEditor.EditableBrush = value;
                    UpdateView();
                    if (EditableBrushChanged != null) EditableBrushChanged(this, new EventArgs());
                }
            }
        }

        public event EventHandler EditableFontChanged;
        public event EventHandler EditableBrushChanged;


        private void UpdateView()
        {
            if (EditableFont != null)
            {
                try
                {
                    linkLabelFontView.Font = EditableFont;
                    linkLabelFontView.Text = EditableFont.Name;
                }
                catch (ArgumentException)
                {
                    linkLabelFontView.Font = Font;
                    linkLabelFontView.Text = LangGeneral.FontIsWrong;
                    EditableFont = null;
                }
            }
            brushEditor.Visible = EditableBrush != null;
        }

        private void linkLabelFontView_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            fontDialog.Font = EditableFont;
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                EditableFont = fontDialog.Font;
            }
        }

        private void FontEditor_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = EditableFont == null;
        }

        private void brushEditor_EditableBrushChanged(object sender, EventArgs e)
        {
            var editableBrushBinding = DataBindings["EditableBrush"];
            if (editableBrushBinding != null)
            {
                editableBrushBinding.WriteValue();
            }
        }
    }
}
