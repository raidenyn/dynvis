using System.ComponentModel;
using System.Windows.Forms;

namespace DynVis.Draw
{
    public class DrawBox : Panel
    {
        public DrawBox()
        {
            UpdateStyle();
        }

        private void UpdateStyle()
        {
            if (!DesignMode)
            {
                //Устанавливаем стили чтобы канва не моргала при перерисовке.
                SetStyle(ControlStyles.UserPaint, true);
                SetStyle(ControlStyles.AllPaintingInWmPaint, true);
                SetStyle(ControlStyles.DoubleBuffer, !IsOpenGL);
                SetStyle(ControlStyles.Opaque, true); // No Need To Draw Form Background
                SetStyle(ControlStyles.ResizeRedraw, true); // Redraw On Resize

                UpdateStyles();
            }
        }

        private bool _IsOpenGL;

        [DefaultValue(false)]
        public bool IsOpenGL
        {
            get { return _IsOpenGL; }
            set
            {
                _IsOpenGL = value;
                UpdateStyle();
            }
        }
    }
}
