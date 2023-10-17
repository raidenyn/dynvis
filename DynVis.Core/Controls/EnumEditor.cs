using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DynVis.Core.Controls
{
    class EnumEditor: ComboBox
    {
        public EnumEditor()
        {
            base.DropDownStyle = ComboBoxStyle.DropDownList;

            SelectedValueChanged += EnumEditor_SelectedValueChanged;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ComboBoxStyle DropDownStyle
        {
            get
            {
                return base.DropDownStyle;
            }
        }

        protected new ObjectCollection Items
        {
            get { return base.Items; }
        }

        private Enum _Enum;

        [Description("Множество")]
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Enum Enum
        {
            get
            {
                return _Enum;
            }
            set
            {
                var oldVal = Enum;
                
                _Enum = value;

                if (GetEnumType(oldVal) != GetEnumType(Enum))
                {
                    UpdateItems(Enum);
                    if (EnumChanged != null) EnumChanged(this, new EventArgs());
                }
            }
        }

        private static Type GetEnumType(Enum e)
        {
            return e == null ? null : e.GetType();
        }

        public event EventHandler EnumChanged;


        private void UpdateItems(Enum e)
        {
            Items.Clear();
            if (e != null)
            {
                foreach (var enumValues in Enum.GetValues(e.GetType()))
                {
                    Items.Add(enumValues);
                }
                SelectedItem = e;
            }
            
        }

        void EnumEditor_SelectedValueChanged(object sender, EventArgs e)
        {
            Enum = (Enum)SelectedItem;
            var enumBinding = DataBindings["Enum"];
            if (enumBinding != null)
            {
                enumBinding.WriteValue();
            }
        }

        
    }
}
