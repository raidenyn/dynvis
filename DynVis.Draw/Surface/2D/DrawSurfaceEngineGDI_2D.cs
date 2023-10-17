using System;
using DynVis.Math;
using M=System.Math;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;
using DynVis.Core;
using DynVis.Core.Surface;

namespace DynVis.Draw.Surface
{
    public partial class DrawSurfaceEngineGDI_2D
    {
        public DrawSurfaceEngineGDI_2D(Control control)
        {
            SetProperty();

            Control = control;
            Refresh = Control.Refresh;
        }

        public readonly Control Control;

        private ISurface3D _Surface;
        
        public ISurface3D Surface
        {
            get { return _Surface; }
            set
            {
                StopDrawSurface();
                _Surface = value;
                ReCalcCoeff();
            }
        }

        public IPath Path
        {
            get; set;
        }

        private void DrawOnEach(int screenX, int screenY, Proc<int, bool, int, bool> paint)
        {
            int startU = -(int) (M.Ceiling(TranslateScreenX/(double) ScreenWidth));
            int endU = startU + 1;
            int startV = -(int) (M.Ceiling(TranslateScreenY/(double) ScreenHeight));
            int endV = startV + 1;

            for (var u = startU; u <= endU; u++)
            {
                for (var v = startV; v <= endV; v++)
                {
                    var inverseX = false;
                    var inverseY = false;

                    var IncX = 0;
                    var IncY = 0;

                    if ((!MathExtended.Parity(u) && Surface.Axes1.TypeMinValue == CoordinateType.Mirrored && u < 0) ||
                        (!MathExtended.Parity(u) && Surface.Axes1.TypeMaxValue == CoordinateType.Mirrored && u > 0))
                    {
                        IncX = ScreenWidth;
                        inverseX = true;
                    }

                    if ((!MathExtended.Parity(v) && Surface.Axes2.TypeMinValue == CoordinateType.Mirrored && v > 0) ||
                        (!MathExtended.Parity(v) && Surface.Axes2.TypeMaxValue == CoordinateType.Mirrored && v < 0))
                    {
                        IncY = ScreenHeight;
                        inverseY = true;
                    }

                    paint( TranslateScreenX + ScreenWidth * u + IncX + screenX, inverseX,
                           TranslateScreenY + ScreenHeight * v + IncY + screenY, inverseY);
                }
            }
        }


        private void DrawOnEach(int screenX, int screenY, Proc<int, int> paint)
        {
            DrawOnEach(screenX, screenY, (x, ix, y, iy) => paint(ix ? x - 2 * (screenX - ScreenLeft) : x, iy ? y - 2 * (screenY - ScreenTop) : y));
        }

        private void DrawOnEach(Point[] points, Proc<Point[]> paint)
        {
            Proc<int, bool, int, bool> paintProc = (x, ix, y, iy) =>
                                       {
                                           var newPoints = new Point[points.Length];
                                           for (var i = 0; i < points.Length; i++)
                                           {
                                               var p = points[i];

                                               newPoints[i].X = x + (ix ? -(p.X - 2*ScreenLeft) : p.X);
                                               newPoints[i].Y = y + (iy ? -(p.Y - 2*ScreenTop) : p.Y);
                                           }
                                           paint(newPoints);
                                       };

            DrawOnEach(0, 0, paintProc);
        }


        /// <summary>
        /// Установка параметров производительности и качества
        /// </summary>
        public void SetQualityMode(Graphics G)
        {
            G.CompositingQuality = CompositingQuality.HighQuality;
            G.CompositingMode = CompositingMode.SourceOver;
            G.InterpolationMode = InterpolationMode.HighQualityBicubic;
            G.SmoothingMode = SmoothingMode.HighQuality;
            //G.PixelOffsetMode = PixelOffsetMode.HighQuality;
            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
        }

        public void Draw(Graphics G)
        {
            G.Clear(BackgrounColor);

            SetQualityMode(G);

            if (Surface != null)
            {
                DrawMainSurface(G);

                DrawFrame(G);

                DrawAxes(G, Surface);

                if (Path != null) DrawPath(G);
                DrawCurrentPoint(G);
            }
        }

        private void DrawMainSurface(Graphics G)
        {
            if (CashedSurfaceImage != null)
            {
                var clip = G.Clip;
                G.Clip = new Region(new Rectangle(ScreenLeft, ScreenTop, ScreenWidth, ScreenHeight));

                Proc<int, bool, int, bool> paint = (x, ix, y, iy) => G.DrawImage(CashedSurfaceImage,
                                                                                 x,
                                                                                 y,
                                                                                 ix ? -ScreenWidth : ScreenWidth, iy ? -ScreenHeight : ScreenHeight);

                DrawOnEach(ScreenLeft, ScreenTop, paint);
                
                G.Clip = clip;
            }
        }

        private void DrawFrame(Graphics G)
        {
            G.DrawRectangle(FramePen, ScreenLeft, ScreenTop, ScreenWidth, ScreenHeight);
        }


        #region CreatingCashe

        private Image CashedSurfaceImage;
        private Thread ThreadCreatingChasheImage;
        private delegate void RefreshFunc();
        private readonly RefreshFunc Refresh;


        private void StopDrawSurface()
        {
            if (ThreadCreatingChasheImage != null && ThreadCreatingChasheImage.IsAlive)
            {
                ThreadCreatingChasheImage.Abort();
            }
        }

        public void CreateChasheSurfaceImage()
        {
            StopDrawSurface();
            ThreadCreatingChasheImage = new Thread(DrawChasheSurfaceImage) { IsBackground = true };
            ThreadCreatingChasheImage.Start();
        }

        private void DrawChasheSurfaceImage()
        {
            if (Surface != null && ScreenWidth > 0 && ScreenHeight > 0)
            {
                var image = new Bitmap(ScreenWidth, ScreenHeight);

                SetPixelHandler setPixel = (color, x, y) => image.SetPixel(x - Left, y - Top, color);

                switch (DrawMode)
                {
                    case SurfaceDrawModeType.Gradient:
                        DrawGradientSurface2D(setPixel);
                        break;
                    case SurfaceDrawModeType.ContourColor:
                        DrawContourColorSurface2D(setPixel);
                        break;
                    case SurfaceDrawModeType.ContourGradient:
                        DrawContourGradientSurface2D(setPixel);
                        break;
                    case SurfaceDrawModeType.Contour:
                        DrawContourSurface2D(setPixel);
                        break;
                }


                CashedSurfaceImage = image;

                Control.Invoke(Refresh);
            }
        }

        #endregion

        #region DrawAxesFunction

        private void DrawAxes(Graphics G, ISurface3D surface)
        {
            G.DrawLine(AxesPen, ScreenLeft, ScreenBottom, ScreenLeft, ScreenTop - AxesMargin);
            G.DrawLine(AxesPen, ScreenLeft, ScreenBottom, ScreenRight + AxesMargin, ScreenBottom);

            for (var q1 = ScreenLeft; q1 < ScreenRight; q1 += GridStep)
            {
                if (DrawGridLines) G.DrawLine(GridPen, q1, ScreenBottom, q1, ScreenTop);
                DrawQ1AxesDigits(G, ScreenToNativeX(q1).ToString("F2"), q1);
            }

            for (var q2 = ScreenBottom; q2 > ScreenTop; q2 -= GridStep)
            {
                if (DrawGridLines) G.DrawLine(GridPen, ScreenLeft, q2, ScreenRight, q2);
                DrawQ2AxesDigits(G, ScreenToNativeY(q2).ToString("F2"), q2);
            }


            DrawQ1AxesNote(G, String.Format("{0} [{1}]", surface.Axes1.Name, surface.Axes1.ScaleDimension.GetFullShortName()));
            DrawQ2AxesNote(G, String.Format("{0} [{1}]", surface.Axes2.Name, surface.Axes2.ScaleDimension.GetFullShortName()));
        }

        private void DrawQ1AxesDigits(Graphics G, string Text, int ScreenX)
        {
            var size = G.MeasureString(Text, AxesFont);
            var XCoord = ScreenX - size.Width/2;
            var YCoord = ScreenBottom + 2;
            if (XCoord < ScreenLeft) XCoord = ScreenLeft;
            G.DrawString(Text, AxesFont, AxesFontBrush, XCoord, YCoord);
        }

        private void DrawQ2AxesDigits(Graphics G, string Text, int ScreenY)
        {
            var size = G.MeasureString(Text, AxesFont);
            var XCoord = ScreenLeft - size.Width - 2;

            if (ScreenY > ScreenBottom) ScreenY = ScreenBottom;

            var YCoord = ScreenY - size.Height/2;
            
            G.DrawString(Text, AxesFont, AxesFontBrush, XCoord, YCoord);
        }

        private void DrawQ1AxesNote(Graphics G, string Text)
        {
            var size = G.MeasureString(Text, AxesNoteFont);

            var XCoord = Width - size.Width - 2;
            var YCoord = Height - size.Height - 2;

            G.DrawString(Text, AxesNoteFont, AxesNoteFontBrush, XCoord, YCoord);
        }

        private void DrawQ2AxesNote(Graphics G, string Text)
        {
            var XCoord = 2;
            var YCoord = 2;
            G.DrawString(Text, AxesNoteFont, AxesNoteFontBrush, XCoord, YCoord);
        }
        #endregion

        #region DrawSurfaceFunction
        private void DrawGradientSurface2D(SetPixelHandler setPixel)
        {
            for (var q1 = ScreenLeft; q1 < ScreenRight; q1++)
            {
                for (var q2 = ScreenTop; q2 < ScreenBottom; q2++)
                {
                    var color = GradientColor.GetGradientColor(Surface.E(ScreenToNativeXStandart(q1), ScreenToNativeYStandart(q2)), Surface.MinE.Value, Surface.MaxE.Value);
                    setPixel(color, q1, q2);
                }
            }
        }

        private void DrawContourColorSurface2D(SetPixelHandler setPixel)
        {
            var ContourStep = (Surface.MaxE.Value - Surface.MinE.Value) / ContourCount;

            double PrevEColorQ2 = 0;

            var PrevEColorQ1 = new double[Height];

            PrevEColorQ1.SetAllItemsTo(i => GetStepedEnergy(ScreenLeft, i, ContourStep));

            for (var q1 = ScreenLeft; q1 < ScreenRight; q1++)
            {
                for (var q2 = ScreenTop; q2 < ScreenBottom; q2++)
                {
                    var EColor = GetStepedEnergy(q1, q2, ContourStep);

                    Color color;

                    if ((PrevEColorQ2 == EColor) && (PrevEColorQ1[q2] == EColor) || EColor > Surface.MaxE.Value)
                    {
                        color = GradientColor.GetGradientColor(EColor, Surface.MinE.Value, Surface.MaxE.Value);
                    }
                    else
                    {
                        color = Color.Black;
                    }


                    setPixel(color, q1, q2);


                    PrevEColorQ1[q2] = PrevEColorQ2 = EColor;
                }
            }
        }

        private double GetStepedEnergy(int q1, int q2, double ContourStep)
        {
            var currentCounter = (int)(Surface.E(ScreenToNativeXStandart(q1), ScreenToNativeYStandart(q2)) / ContourStep);
            return currentCounter * ContourStep;
        }

        private void DrawContourGradientSurface2D(SetPixelHandler setPixel)
        {
            var ContourStep = (Surface.MaxE.Value - Surface.MinE.Value) / ContourCount;

            double PrevEColorQ2 = 0;

            var PrevEColorQ1 = new double[Height];

            PrevEColorQ1.SetAllItemsTo(i => GetStepedEnergy(ScreenLeft, i, ContourStep));

            for (var q1 = ScreenLeft; q1 < ScreenRight; q1++)
            {
                for (var q2 = ScreenTop; q2 < ScreenBottom; q2++)
                {
                    var E = Surface.E(ScreenToNativeXStandart(q1), ScreenToNativeYStandart(q2));
                    var currentCounter = (int)(E / ContourStep);
                    var EColor = currentCounter * ContourStep;

                    Color color;

                    if ((PrevEColorQ2 == EColor) && (PrevEColorQ1[q2] == EColor) || EColor > Surface.MaxE.Value && (PrevEColorQ1[q2] == 0))
                    {
                        color = GradientColor.GetGradientColor(E, Surface.MinE.Value, Surface.MaxE.Value);
                    }
                    else
                    {
                        color = Color.Black;
                    }


                    setPixel(color, q1, q2);


                    PrevEColorQ1[q2] = PrevEColorQ2 = EColor;
                }
            }
        }

        private void DrawContourSurface2D(SetPixelHandler setPixel)
        {
            var ContourStep = (Surface.MaxE.Value - Surface.MinE.Value) / ContourCount;

            double PrevEColorQ2 = 0;

            var PrevEColorQ1 = new double[Height];

            var color = Color.Black;

            PrevEColorQ1.SetAllItemsTo(i => GetStepedEnergy(ScreenLeft, i, ContourStep));

            for (var q1 = ScreenLeft; q1 < ScreenRight; q1++)
            {
                for (var q2 = ScreenTop; q2 < ScreenBottom; q2++)
                {
                    var EColor = GetStepedEnergy(q1, q2, ContourStep);

                    if (!((PrevEColorQ2 == EColor) && (PrevEColorQ1[q2] == EColor) || EColor > Surface.MaxE.Value))
                    {
                        setPixel(color, q1, q2);
                    }


                    PrevEColorQ1[q2] = PrevEColorQ2 = EColor;
                }
            }
        }
        #endregion

        #region DrawPointFunction
        private void DrawCurrentPoint(Graphics G)
        {
            var HalfCurrentPointSize = CurrentPointSize / 2;

            var XCoord = NativeToScreenXStandart(Surface.CurrentPoint.X);
            var YCoord = NativeToScreenYStandart(Surface.CurrentPoint.Y);

            DrawOnEach(XCoord, YCoord, (x, y) => G.FillEllipse(CurrentPointBrush, x - HalfCurrentPointSize, y - HalfCurrentPointSize, CurrentPointSize, CurrentPointSize));
        }
        #endregion

        #region DrawPathFunction
        private void DrawPath(Graphics G)
        {
            var pointIndex = 1;
            while (pointIndex < Path.CurrentPointIndex)
            {
                var points = GetPathArray(ref pointIndex);

                if (points.Length > 1)
                {
                    DrawOnEach(points, newPoints => G.DrawLines(PathPen, newPoints));
                }
            }
        }

        private Point[] GetPathArray(ref int pointIndex)
        {
            var result = new List<Point>();

            int oldX = 0, oldY = 0;

            for (; pointIndex <= Path.CurrentPointIndex; pointIndex++)
            {
                var nativeX = Path[pointIndex].Point.X;
                var nativeY = Path[pointIndex].Point.Y;

                Surface.IsValidCoord(ref nativeX, ref nativeY);

                var screenX = NativeToScreenXStandart(nativeX);
                var screenY = NativeToScreenYStandart(nativeY);

                var prevNativeX = Path[pointIndex - 1].Point.X;
                var prevNativeY = Path[pointIndex - 1].Point.Y;

                Surface.IsValidCoord(ref prevNativeX, ref prevNativeY);

                var prevScreenX = NativeToScreenXStandart(prevNativeX);
                var prevScreenY = NativeToScreenYStandart(prevNativeY);

                if (M.Abs(oldX - screenX) > 1 ||
                    M.Abs(oldY - screenY) > 1)
                {
                    if (M.Abs(screenX - prevScreenX) > 10 ||
                        M.Abs(screenY - prevScreenY) > 10)
                    {
                        pointIndex++;
                        break;
                    }

                    oldX = screenX;
                    oldY = screenY;

                    result.Add(new Point(screenX, screenY));
                }
            }
            return result.ToArray();
        }
        #endregion

        private delegate void SetPixelHandler(Color color, int x, int y);
    }

    public enum SurfaceDrawModeType { Gradient, ContourColor, ContourGradient, Contour }
}
