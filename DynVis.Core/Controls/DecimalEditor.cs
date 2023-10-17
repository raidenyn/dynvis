using System;
using System.ComponentModel;
using System.Windows.Forms;
using DynVis.Core.Properties;

namespace DynVis.Core
{
    /// <summary>
    /// Контрол пердназначенный для ввода чисел
    /// </summary>
    public partial class DecimalEditor : TextBox
    {
        public DecimalEditor()
        {
            InitializeComponent();

            HoldFocus = true;

            InitEvents();
        }

        /// <summary>
        /// Инитиализация событий
        /// </summary>
        private void InitEvents()
        {
            TextChanged += DecimalEditor_TextChanged;
            Validating += DecimalEditor_Validating;
        }

        /// <summary>
        /// Переменная хранит последний правильный текст
        /// </summary>
        private string LastTrueText;

        void DecimalEditor_TextChanged(object sender, EventArgs e)
        {
            //Проверяет введенный текст на соответсвие числу
            if (ForbidWrongText)
            {
                double d;
                if (double.TryParse(Text, out d) && CheckValue(d))
                {
                    LastTrueText = Text;
                }
                else
                {
                    Text = LastTrueText;
                }
            }
            HideWarning();
        }

        [DefaultValue(false)]
        [Description("Запрещает вводить неправильный текст даже в промежуточном вводе")]
        [Category("Input")]
        public bool ForbidWrongText
        { get; set;
        }

        [DefaultValue(0.0)]
        [Description("Значение числовой переменной")]
        [Category("Input")]
        public double Value
        {
            get
            {
                double d;
                if (!(double.TryParse(Text, out d) && CheckValue(d))) double.TryParse(LastTrueText, out d);
                return d;
            }
            set
            {
                LastTrueText = Text = value.ToString();
                UpdateViewValue();
            }
        }

        [DefaultValue(true)]
        [Description("Определяет правильность введенного текста")]
        [Category("Input")]
        public bool IsValidValue
        {
            get
            {
                double d;
                return double.TryParse(Text, out d) && CheckValue(d);
            }
        }

        /// <summary>
        /// Обновляет текст
        /// </summary>
        private void UpdateViewValue()
        {
            Text = Value.ToString();
        }

        /// <summary>
        /// Блокируем мультилайн
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool Multiline
        {
            get { return false; }
        }

        
        /// <summary>
        /// Закрываем текст от внешнего мира
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected new string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        [DefaultValue(true)]
        [Description("Удерживать фокус до введения корректного значения")]
        [Category("Input")]
        public bool HoldFocus
        { get;set;
        }

        [DefaultValue(false)]
        [Description("Допускать только положительные значения")]
        [Category("Input")]
        public bool PositiveOnly
        { get;set;
        }

        [DefaultValue(false)]
        [Description("Допускать нулевое значение")]
        [Category("Input")]
        public bool CanNotBeNull
        {
            get;
            set;
        }

        bool CheckValue(double value)
        {
            var result = value < 0 && PositiveOnly;
            result |= value == 0 && CanNotBeNull;

            return !result;
        }

        void DecimalEditor_Validating(object sender, CancelEventArgs e)
        {
            if (HoldFocus && !IsValidValue)
            {
                ShowWarning();
                e.Cancel = true;
            }
        }


        private readonly ErrorProvider WarningErrorProvider = new ErrorProvider();

        private void ShowWarning()
        {
            
            WarningErrorProvider.SetError(this, LangGeneral.NumberInputWrong);
        }

        private void HideWarning()
        {
            WarningErrorProvider.SetError(this, String.Empty);
        }
    }
}
