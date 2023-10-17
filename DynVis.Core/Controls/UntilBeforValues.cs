using System.ComponentModel;

namespace DynVis.Core
{
    public partial class UntilBeforValues : BaseControl
    {
        public UntilBeforValues()
        {
            InitializeComponent();
        }

        [DefaultValue(true)]
        [Description("Значение \"От\" введено правильно")]
        [Category("Input")]
        public bool UntilIsDecimal
        {
            get
            {
                double Temp;
                return double.TryParse(textBoxUntil.Text, out Temp);
            }
        }

        [DefaultValue(0.0)]
        [Description("Значение числовой переменной \"От\"")]
        [Category("Input")]
        public double Until
        {
            get
            {
                double Temp;
                if (double.TryParse(textBoxUntil.Text, out Temp))
                {
                    return Temp;
                }
                return 0;
            }
            set
            {
                textBoxUntil.Text = value.ToString();
            }
        }

        [DefaultValue(true)]
        [Description("Значение \"До\" введено правильно")]
        [Category("Input")]
        public bool BeforIsDecimal
        {
            get
            {
                double Temp;
                return double.TryParse(textBoxBefor.Text, out Temp);
            }
        }

        [DefaultValue(0.0)]
        [Description("Значение числовой переменной \"До\"")]
        [Category("Input")]
        public double Befor
        {
            get
            {
                double Temp;
                if (double.TryParse(textBoxBefor.Text, out Temp))
                {
                    return Temp;
                }
                return 0;
            }
            set
            {
                textBoxBefor.Text = value.ToString();
            }
        }

        public bool ValidateValues()
        {
            return UntilIsDecimal && BeforIsDecimal;
        }
    }
}
