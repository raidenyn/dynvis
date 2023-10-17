using System.Globalization;


namespace DynVis.Core
{
    /// <summary>
    /// Класс функций работы с культурой
    /// </summary>
    public static class Culture
    {
        public const string DescimalSeparator = ".";
        
        /// <summary>
        /// Устанавливает разделитель целой и дробной части в числах на точку
        /// </summary>
        public static void SetPointAsDecimalSeparator()
        {
            if (System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator != DescimalSeparator)
            {
                var myCI = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
                myCI.NumberFormat.NumberDecimalSeparator = DescimalSeparator;
                System.Threading.Thread.CurrentThread.CurrentCulture = myCI;
            }
        }
    }
}
