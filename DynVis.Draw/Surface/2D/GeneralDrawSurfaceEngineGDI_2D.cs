using M=System.Math;
using DynVis.Core.Surface;

namespace DynVis.Draw.Surface
{
    partial class DrawSurfaceEngineGDI_2D
    {
        public int Width { get; private set; }
        public int Height { get; private set; }


        public int ScreenWidth
        {
            get
            {
                return ScreenRight - ScreenLeft;
            }
        }
        public int ScreenHeight
        {
            get
            {
                return ScreenBottom - ScreenTop;
            }
        }

        public double NativeWidth
        {
            get
            {
                return Surface.Axes1.MaxValue - Surface.Axes1.MinValue;
            }
        }
        public double NativeHeight
        {
            get
            {
                return Surface.Axes2.MaxValue - Surface.Axes2.MinValue;
            }
        }

        private double CoeffScreenToNativeX;
        private double CoeffScreenToNativeY;

        private double CoeffNativeToScreenX;
        private double CoeffNativeToScreenY;

        private int ScreenLeft
        {
            get
            {
                return Left;
            }
        }
        private int ScreenTop
        {
            get
            {
                return Top;
            }
        }
        private int ScreenRight
        {
            get
            {
                return Width - Right;
            }
        }
        private int ScreenBottom
        {
            get
            {
                return Height - Bottom;
            }
        }

        public void SetSize(int width, int height)
        {
            Width = width;
            Height = height;

            ReCalcCoeff();
        }

        private void ReCalcCoeff()
        {
            if (Surface != null)
            {
                CoeffScreenToNativeX = (Surface.Axes1.MaxValue - Surface.Axes1.MinValue) / ScreenWidth;
                CoeffScreenToNativeY = (Surface.Axes2.MaxValue - Surface.Axes2.MinValue) / ScreenHeight;

                CoeffNativeToScreenX = 1 / CoeffScreenToNativeX;
                CoeffNativeToScreenY = 1 / CoeffScreenToNativeY;

                CreateChasheSurfaceImage();
            }
        }

        public double ScreenToNativeX(int screenX)
        {
            return ScreenToNativeXStandart(screenX - TranslateScreenX);
        }

        public double ScreenToNativeY(int screenY)
        {
            return ScreenToNativeYStandart(screenY - TranslateScreenY);
        }

        public int NativeToScreenX(double nativeX)
        {
            return TranslateScreenX + NativeToScreenXStandart(nativeX);
        }

        public int NativeToScreenY(double nativeY)
        {
            return NativeToScreenYStandart(nativeY) + TranslateScreenY;
        }

        public double ScreenToNativeXStandart(int screenX)
        {
            return Surface.Axes1.MinValue + (screenX - ScreenLeft) * CoeffScreenToNativeX;
        }

        public double ScreenToNativeYStandart(int screenY)
        {
            return Surface.Axes2.MinValue + (ScreenHeight - (screenY - ScreenTop)) * CoeffScreenToNativeY;
        }

        public int NativeToScreenXStandart(double nativeX)
        {
            return ScreenLeft + (int)((nativeX - Surface.Axes1.MinValue) * CoeffNativeToScreenX);
        }

        public int NativeToScreenYStandart(double nativeY)
        {
            return ScreenHeight - ((int)((nativeY - Surface.Axes2.MinValue) * CoeffNativeToScreenY) - ScreenTop);
        }

        public double ScreenToNativeWidth(int screenWidth)
        {
            return (screenWidth) * CoeffScreenToNativeX;
        }

        public double ScreenToNativeHeight(int screenHeight)
        {
            return (screenHeight) * CoeffScreenToNativeY;
        }

        public int NativeToScreenWidth(double nativeWidth)
        {
            return (int)((nativeWidth) * CoeffNativeToScreenX);
        }

        public int NativeToScreenHeight(double nativeHeight)
        {
            return ((int)(nativeHeight * CoeffNativeToScreenY));
        }

        public int TranslateScreenX
        {
            get
            {
                return NativeToScreenWidth(TranslateNativeX);
            }
            set
            {
                TranslateNativeX = ScreenToNativeWidth(value);
            }
        }
        public int TranslateScreenY
        {
            get
            {
                return NativeToScreenHeight(TranslateNativeY);
            }
            set
            {
                TranslateNativeY = ScreenToNativeHeight(value);
            }
        }

        private double _TranslateNativeX;
        private double _TranslateNativeY;

        public double TranslateNativeX
        {
            get
            {
                return _TranslateNativeX;
            }
            set
            {
                var width = Surface.Axes1.MaxValue - Surface.Axes1.MinValue;

                if (Surface.Axes1.TypeMinValue == CoordinateType.Ended)
                {
                    if (value < 0) value = 0;
                    if (value > width) value = width;
                }
                if (Surface.Axes1.TypeMaxValue == CoordinateType.Ended)
                {
                    if (value > 0) value = 0;
                    if (value < -width) value = -width;
                }

                _TranslateNativeX = value;
            }
        }
        public double TranslateNativeY
        {
            get
            {
                return _TranslateNativeY;
            }
            set
            {
                var height = Surface.Axes2.MaxValue - Surface.Axes2.MinValue;

                if (Surface.Axes2.TypeMinValue == CoordinateType.Ended)
                {
                    if (value < 0) value = 0;
                    if (value > height) value = height;
                }
                if (Surface.Axes2.TypeMaxValue == CoordinateType.Ended)
                {
                    if (value > 0) value = 0;
                    if (value < -height) value = -height;
                }

                _TranslateNativeY = value;
            }
        }
    }
}
