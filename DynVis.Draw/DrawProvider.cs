using System;
using System.Windows.Forms;


namespace DynVis.Draw
{
    public abstract class DrawProvider : IDisposable
    {
        protected abstract BaseEngine BaseEngine{get;}

        public Control OwnerControl
        {
            get
            {
                return BaseEngine.OwnerControl;
            }
        }

        public void Dispose()
        {
            BaseEngine.Dispose();
        }

        private static double MouseTranslateToRotateAngle(double mouseTranslate)
        {
            return mouseTranslate;
        }

        public void RotateByX(double x)
        {
            BaseEngine.RotateAlongScreenAxesY(MouseTranslateToRotateAngle(x));
        }

        public void RotateByY(double y)
        {
            BaseEngine.RotateAlongScreenAxesX(MouseTranslateToRotateAngle(y));
        }

        public void SetScale(double mouseWheel)
        {
            BaseEngine.CenterPoint.Z += mouseWheel / 100;
        }

        public bool AutoUpdateRotateCenter = true;

        public void Resize(int Width, int Height)
        {
            BaseEngine.Resize(Width, Height);
        }
    }
}
