using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DynVis.Core.PropertyEditor
{
    internal partial class FormProperties : Form
    {
        public FormProperties()
        {
            InitializeComponent();
        }

        [DefaultValue(null)]
        [Description("Объекты редактирования")]
        [Category("Appearance")]
        public CollectionWithCurrentItems<IPropertiesObject> EditableObjects
        {
            get
            {
                return objectPropertyEditor.PropertiesObjects;
            }
        }

        [DefaultValue(null)]
        [Description("Выбранный объект")]
        [Category("Appearance")]
        public IPropertiesObject SelectedObject
        {
            get
            {
                return objectPropertyEditor.SelectedObject;
            }
            set { objectPropertyEditor.SelectedObject = value; }
        }

        private void objectPropertyEditor_CaptionChanged(object sender, EventArgs e)
        {
            Text = objectPropertyEditor.Caption;
        }

        private void SaveChanges()
        {
            objectPropertyEditor.SaveChanges();
            buttonApply.Enabled = false;
            GlobalProperiesEditor.SaveAllProperites();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            SaveChanges();
            Hide();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            objectPropertyEditor.DiscartChanges();
            Hide();
        }

        private void objectPropertyEditor_EditableObjectsValueChanged(object sender, EventArgs e)
        {
            buttonApply.Enabled = true;
        }

        private void FormProperties_VisibleChanged(object sender, EventArgs e)
        {
            objectPropertyEditor.ClearCash();
        }




    }
}
