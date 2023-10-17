using System.Windows.Forms;

namespace DynVis.Core
{
    /// <summary>
    /// Базовый контрол 
    /// </summary>
    public class BaseControl : UserControl
    {
        /// <summary>
        /// Переопределяет DesignMode, который всегда возращает правильные значения (в отличае от системного)
        /// </summary>
        protected new bool DesignMode
        {
            get
            {
                return DesignerDetector.IsComponentInDesignMode(this);
            }
        }
    }
}
