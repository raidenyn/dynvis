using System;
using System.ComponentModel;
using System.Windows.Forms;
using DynVis.Core.Properties;

namespace DynVis.Core
{
    /// <summary>
    /// Форма ввода диапазона чисел От ... До ...
    /// </summary>
    public sealed partial class FormUntilBeforValues : Form
    {
        public FormUntilBeforValues()
        {
            InitializeComponent();
        }

        [DefaultValue(true)]
        [Description("Значение \"От\" введено правильно")]
        [Category("Input")]
        bool UntilIsDecimal
        {
            get
            {
                return untilBeforValues.UntilIsDecimal;
            }
        }
        
        [DefaultValue(0.0)]
        [Description("Значение числовой переменной \"От\"")]
        [Category("Input")]
        public double Until
        {
            get
            {
                return untilBeforValues.Until;
            }
            set
            {
                Until = value;
            }
        }

        [DefaultValue(true)]
        [Description("Значение \"До\" введено правильно")]
        [Category("Input")]
        bool BeforIsDecimal
        {
            get
            {
                return untilBeforValues.BeforIsDecimal;
            }
        }
        
        [DefaultValue(0.0)]
        [Description("Значение числовой переменной \"До\"")]
        [Category("Input")]
        public double Befor
        {
            get
            {
                return untilBeforValues.Befor;
            }
            set
            {
                Befor = value;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (BeforIsDecimal && UntilIsDecimal && Until <= Befor)
            {
                DialogResult = DialogResult.OK;
            }else
            {
                MessageBox.Show(this, LangGeneral.ValueInputWrong, LangGeneral.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
