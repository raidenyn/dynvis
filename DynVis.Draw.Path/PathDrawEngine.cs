using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using DynVis.Core;
using DynVis.Math;

namespace DynVis.Draw.Path
{
    internal class PathDrawEngine
    {
        //------------------------------------------------------------------
        //Настройки вывода
        public Color BackgroundColor = Color.White;

        public Pen AxesPen = new Pen(Color.FromArgb(50, 50, 50)) { EndCap = LineCap.ArrowAnchor };
        public Pen PathPen = new Pen(Color.Black) { Width  = 2 };
        public Pen BackgroundGridPen = new Pen(Color.FromArgb(200, 200, 200)) { DashStyle = DashStyle.Dash };
        public int BackgroundGridStepX = 50;
        public int BackgroundGridStepY = 50;


        public Font AxesNoteFont = new Font("Times New Roman", 16);
        public Brush AxesNoteBrush = Brushes.Black;

        public Font DigitNoteFont = new Font("Times New Roman", 10);
        public Brush DigitNoteBrush = Brushes.Black;

        public Brush CurrentPointBrush = Brushes.Red;
        public int CurrentPointRadius = 5;

        public int AxesPositionLeft = 50;
        public int AxesPositionRight = 20;
        public int AxesPositionTop = 30;
        public int AxesPositionBottom = 20;

        public double ScaleX = 1.0;
        public double ScaleY = 1.0;

        public double IncY;
        //------------------------------------------------------------------


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
            SetQualityMode(G);

            ClearColor(G);

            if (Path != null)
            {
                DrawCashed(G, Path);

                DrawCurrentPoint(G, Path);
            }
        }

        public Bitmap DrawToBitmap(int width, int hight)
        {
            var result = new Bitmap(width, hight);

            var Width = ScreenWidth;
            var Height = ScreenHeight;

            SetSize(width, hight);

            using (var G = Graphics.FromImage(result))
            {
                Draw(G);
            }

            SetSize(Width, Height);

            return result;
        }

        public int ScreenWidth{ get; private set;}
        public int ScreenHeight { get; private set; }

        private IPath _Path;
        public IPath Path
        {
            get { return _Path; }
            set { SetNewPath(value); }
        }

        private void SetNewPath(IPath newPath)
        {
            _Path = newPath;

            if (Path != null)
            {
                IncY = -Path.MinEnergyPoint.Value;

                

                Rescale();
            }
        }

        private Bitmap CashedPath;

        private void Rescale()
        {
            if (Path != null)
            {
                ScaleY = (ScreenBootomCoordAxes - ScreenTopCoordAxes)/
                         (Path.MaxEnergyPoint.Value - Path.MinEnergyPoint.Value);
                ScaleX = (ScreenRightCoordAxes - ScreenLeftCoordAxes)/(double) Path.Count;
            }

            CashedPath = null;
        }

        public int ScreenBootomCoordAxes
        {
            get
            {
                return ScreenHeight - AxesPositionBottom;
            }
        }
        public int ScreenRightCoordAxes
        {
            get
            {
                return ScreenWidth - AxesPositionRight;
            }
        }
        public int ScreenTopCoordAxes
        {
            get
            {
                return AxesPositionTop;
            }
        }
        public int ScreenLeftCoordAxes
        {
            get
            {
                return AxesPositionLeft;
            }
        }

        public void SetSize(int width, int height)
        {
            ScreenWidth = width;
            ScreenHeight = height;

            Rescale();
        }

        private void ClearColor(Graphics G)
        {
            G.Clear(BackgroundColor);
        }


        private void DrawAxes(Graphics G, IPath path)
        {
            DrawBackgroundGrid(G);
            
            AxesPen.Width = 2;

            G.DrawLine(AxesPen, AxesPositionLeft, ScreenBootomCoordAxes, AxesPositionLeft, AxesPositionTop);
            G.DrawLine(AxesPen, AxesPositionLeft, ScreenBootomCoordAxes, ScreenRightCoordAxes, ScreenBootomCoordAxes);

            if (path != null)
            {
                DrawYAxesNote(G, String.Format("Энергия [{0}]", path.EnergyDimension.GetFullShortName()));
            }
        }

        private void DrawBackgroundGrid(Graphics G)
        {
            for (var x = ScreenLeftCoordAxes + BackgroundGridStepX; x <= ScreenRightCoordAxes; x += BackgroundGridStepX)
            {
                G.DrawLine(BackgroundGridPen, x, ScreenBootomCoordAxes, x, ScreenTopCoordAxes);
                DrawXAxesDigit(G, x, ((x - ScreenLeftCoordAxes) / (double)(ScreenRightCoordAxes - ScreenLeftCoordAxes)).ToString("F4"));
            }

            for (var y = ScreenBootomCoordAxes; y >= ScreenTopCoordAxes; y -= BackgroundGridStepY)
            {
                G.DrawLine(BackgroundGridPen, ScreenLeftCoordAxes, y, ScreenRightCoordAxes, y);
                DrawYAxesDigit(G, y, ScreentToNativeY(y).ToString("F2"));
            }
        }

        private void DrawYAxesNote(Graphics G, string note)
        {
            var stringSize = G.MeasureString(note, AxesNoteFont);

            var x = 5;
            var y = AxesPositionTop - 8 - stringSize.Height;

            G.DrawString(note, AxesNoteFont, AxesNoteBrush, x, y);
        }

        private void DrawXAxesDigit(Graphics G, int screenX, string note)
        {
            var screenY = ScreenBootomCoordAxes;

            var stringSize = G.MeasureString(note, DigitNoteFont);

            screenX -= (int)stringSize.Width / 2;

            G.DrawString(note, DigitNoteFont, DigitNoteBrush, screenX, screenY);
        }

        private void DrawYAxesDigit(Graphics G, int screenY, string note)
        {
            var screenX = ScreenLeftCoordAxes - 2;

            var stringSize = G.MeasureString(note, DigitNoteFont);

            screenX -= (int) (stringSize.Width*0.9);
            screenY -= (int)(stringSize.Height / 2);

            G.DrawString(note, DigitNoteFont, DigitNoteBrush, screenX, screenY);
        }

        private void DrawCashed(Graphics G, IPath path)
        {
            if (CashedPath == null)
            {
                CashedPath = new Bitmap(ScreenWidth, ScreenHeight);

                using (var CashedPathGraphics = Graphics.FromImage(CashedPath))
                {
                    SetQualityMode(CashedPathGraphics);


                    DrawAxes(CashedPathGraphics, path);
                    DrawEnergyPath(CashedPathGraphics, path);
                }
            }

            G.DrawImage(CashedPath, 0, 0);
        }
        
        private void DrawEnergyPath(Graphics G, IPath path)
        {
            if (path.Count > 1)
            {
                G.DrawLines(PathPen, GetPathArrayPoint(path));
            }
        }

        private void DrawCurrentPoint(Graphics G, IPath path)
        {
            G.FillEllipse(CurrentPointBrush, NativeToScreen(path.CurrentPointIndex, path.CurrentPoint.Value, CurrentPointRadius));
        }

        private Point[] GetPathArrayPoint(IPath path)
        {
            var ImagePointCount = ScreenRightCoordAxes - ScreenLeftCoordAxes;
            var PointList = new List<Point>(ImagePointCount);
            var step = path.Count / (double)ImagePointCount;
            var prevY = 0;
            for (var q = 0; q < ImagePointCount; q++)
            {
                var x = ScreenLeftCoordAxes + q;
                var y = NativeToScreenY(path[(int)(q * step)].Value);

                if (prevY != y)
                {
                    PointList.Add(new Point(x, y));
                }

                prevY = y;
            }
            return PointList.ToArray();
        }

        public int NativeToScreenX(double nativeX)
        {
            return (int)(nativeX * ScaleX) + AxesPositionLeft;
        }

        public int NativeToScreenY(double nativeY)
        {
            return ScreenBootomCoordAxes - (int) ((nativeY + IncY)*(ScaleY));
        }

        public double ScreentToNativeX(int screenX)
        {
            return (screenX - AxesPositionLeft)/ScaleX;
        }

        public double ScreentToNativeY(int screenY)
        {
            return (ScreenBootomCoordAxes - screenY) / (ScaleY) - IncY;
        }

        public Point NativeToScreen(IPointD NativeP)
        {
            return NativeToScreen(NativeP.X, NativeP.Y);
        }

        public Point NativeToScreen(double x, double y)
        {
            return new Point(NativeToScreenX(x), NativeToScreenY(y));
        }

        public Rectangle NativeToScreen(double centerX, double centerY, int width, int height)
        {
            var screenX = NativeToScreenX(centerX);
            var screenY = NativeToScreenY(centerY);
            return new Rectangle(screenX, screenY, width / 2, height / 2);
        }

        public Rectangle NativeToScreen(double centerX, double centerY, int radius)
        {
            var screenX = NativeToScreenX(centerX);
            var screenY = NativeToScreenY(centerY);
            return new Rectangle(screenX - radius, screenY - radius, radius * 2, radius * 2);
        }
    }
}
