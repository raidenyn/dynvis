using System.Drawing;
using System.Drawing.Drawing2D;
using DynVis.Core.PropertyEditor;

namespace DynVis.Draw.Surface
{
    partial class DrawSurfaceEngineGDI_2D
    {
        [TransactingEditable]
        [SavableProperty]
        public Pen FramePen { get; set; }
        [TransactingEditable]
        [SavableProperty]
        public Pen PathPen { get; set; }
        [TransactingEditable]
        [SavableProperty]
        public Pen AxesPen { get; set; }
        [TransactingEditable]
        [SavableProperty]
        public int AxesMargin { get; set; }
        [TransactingEditable]
        [SavableProperty]
        public Font AxesFont { get; set; }
        [TransactingEditable]
        [SavableProperty]
        public Brush AxesFontBrush { get; set; }
        [TransactingEditable]
        [SavableProperty]
        public Font AxesNoteFont { get; set; }
        [TransactingEditable]
        [SavableProperty]
        public Brush AxesNoteFontBrush { get; set; }

        [TransactingEditable]
        [SavableProperty]
        public Pen GridPen { get; set; }
        [TransactingEditable]
        [SavableProperty]
        public int GridStep { get; set; }
        [TransactingEditable]
        [SavableProperty]
        public Brush CurrentPointBrush { get; set; }
        [TransactingEditable]
        [SavableProperty]
        public int CurrentPointSize { get; set; }

        [TransactingEditable]
        [SavableProperty]
        public int ContourCount { get; set; }

        [TransactingEditable]
        [SavableProperty]
        public Color BackgrounColor { get; set; }

        public SurfaceDrawModeType DrawMode { get; set; }

        public int Top = 25;
        public int Left = 60;
        public int Right = 5;
        public int Bottom = 45;

        [TransactingEditable]
        [SavableProperty]
        public bool DrawGridLines { get; set; }

        private void SetProperty()
        {
            DrawGridLines = true;
            DrawMode = SurfaceDrawModeType.ContourColor;
            BackgrounColor = Color.White;
            CurrentPointSize = 5;
            CurrentPointBrush = Brushes.White;
            GridStep = 100;
            ContourCount = 20;
            GridPen = new Pen(Color.FromArgb(155, Color.Gray)) { DashStyle = DashStyle.Dash };
            FramePen = new Pen(Color.Black);
            PathPen = new Pen(Color.White);
            AxesPen = new Pen(Color.Black, 2) {EndCap = LineCap.ArrowAnchor};
            AxesMargin = 5;
            AxesFont = new Font("Courier New", 10);
            AxesFontBrush = Brushes.Black;
            AxesNoteFont = new Font("Courier New", 10);
            AxesNoteFontBrush = Brushes.Black;
        }
    }
}
