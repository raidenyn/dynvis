using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DynVis.Core;
using DynVis.Core.PropertyEditor;

namespace DynVis.Draw.Surface
{
    internal partial class DrawSurfaceGDI_2D : DrawSurfaceBase
    {
        public DrawSurfaceGDI_2D()
        {
            InitializeComponent();

            DrawSurfaceEngine = new DrawSurfaceEngineGDI_2D(drawBox);

            GlobalProperiesEditor.RegisterEditableObject(new SurfacePropertyProvider(DrawSurfaceEngine, Refresh));
        }

        private readonly DrawSurfaceEngineGDI_2D DrawSurfaceEngine;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override ISurface3D Surface
        {
            get { return DrawSurfaceEngine.Surface; }
            set
            {
                if (DrawSurfaceEngine.Surface != null)
                {
                    DrawSurfaceEngine.Surface.CurrentPointChanged -= Surface_CurrentPointChanged;
                }
               
                DrawSurfaceEngine.Surface = value;

                if (DrawSurfaceEngine.Surface != null)
                {
                    DrawSurfaceEngine.Surface.CurrentPointChanged += Surface_CurrentPointChanged;
                }

                SetProportion();

                Refresh();
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override IPath Path
        {
            get { return DrawSurfaceEngine.Path; }
            set
            {
                if (DrawSurfaceEngine.Path != null)
                {
                    DrawSurfaceEngine.Path.CurrentPointChanged -= Surface_CurrentPointChanged;
                }

                DrawSurfaceEngine.Path = value;

                if (DrawSurfaceEngine.Path != null)
                {
                    DrawSurfaceEngine.Path.CurrentPointChanged += Surface_CurrentPointChanged;
                }

                Refresh();
            }
        }

        void Surface_CurrentPointChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        private void drawBox_Paint(object sender, PaintEventArgs e)
        {
            RefreshSurface(e.Graphics);
        }

        public void RefreshSurface()
        {
            using (var G = drawBox.CreateGraphics())
            {
                RefreshSurface(G);
            }
        }

        public void RefreshSurface(Graphics G)
        {
            DrawSurfaceEngine.Draw(G);
        }

        private void drawBox_SizeChanged(object sender, EventArgs e)
        {
            DrawSurfaceEngine.SetSize(drawBox.Width, drawBox.Height);
        }

        private Point PrevPoint;
        private void drawBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (Surface != null && e.Button == MouseButtons.Left)
            {
                var x = DrawSurfaceEngine.ScreenToNativeX(e.X);
                var y = DrawSurfaceEngine.ScreenToNativeY(e.Y);

                Surface.SetPoint(x, y);
            }
        }

        private void drawBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DrawSurfaceEngine.TranslateScreenX += e.X - PrevPoint.X;
                DrawSurfaceEngine.TranslateScreenY += e.Y - PrevPoint.Y;

                Refresh();
            }
            PrevPoint = e.Location;
        }

        private void drawBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DrawSurfaceEngine.TranslateScreenX = 
                DrawSurfaceEngine.TranslateScreenY = 0;

                Refresh();
            }
        }

        private void contextMenuStrip_Opening(object sender, EventArgs e)
        {
            toolStripTextBoxCounterCount.Text = DrawSurfaceEngine.ContourCount.ToString();


            ToolStripMenuItemContuor.Checked =
                ToolStripMenuItemContuorColor.Checked =
                    ToolStripMenuItemContuorGradient.Checked = 
                        ToolStripMenuItemGradient.Checked = false;

            switch (DrawSurfaceEngine.DrawMode)
            {
                case SurfaceDrawModeType.Contour:
                    ToolStripMenuItemContuor.Checked = true;
                    break;
                case SurfaceDrawModeType.ContourColor:
                    ToolStripMenuItemContuorColor.Checked = true;
                    break;
                case SurfaceDrawModeType.ContourGradient:
                    ToolStripMenuItemContuorGradient.Checked = true;
                    break;
                case SurfaceDrawModeType.Gradient:
                    ToolStripMenuItemGradient.Checked = true;
                    break;
            }
        }

        private void toolStripTextBoxCounterCount_TextChanged(object sender, EventArgs e)
        {
            int i;
            if (int.TryParse(toolStripTextBoxCounterCount.Text, out i))
            {
                DrawSurfaceEngine.ContourCount = i;
                DrawSurfaceEngine.CreateChasheSurfaceImage();
            }
        }

        private void ToolStripMenuItemContuor_Click(object sender, EventArgs e)
        {
            DrawSurfaceEngine.DrawMode = SurfaceDrawModeType.Contour;
            DrawSurfaceEngine.CreateChasheSurfaceImage();
        }

        private void ToolStripMenuItemContuorColor_Click(object sender, EventArgs e)
        {
            DrawSurfaceEngine.DrawMode = SurfaceDrawModeType.ContourColor;
            DrawSurfaceEngine.CreateChasheSurfaceImage();
        }

        private void ToolStripMenuItemContuorGradient_Click(object sender, EventArgs e)
        {
            DrawSurfaceEngine.DrawMode = SurfaceDrawModeType.ContourGradient;
            DrawSurfaceEngine.CreateChasheSurfaceImage();
        }

        private void ToolStripMenuItemGradient_Click(object sender, EventArgs e)
        {
            DrawSurfaceEngine.DrawMode = SurfaceDrawModeType.Gradient;
            DrawSurfaceEngine.CreateChasheSurfaceImage();
        }

        public void SetProportion()
        {
            if (Surface != null)
            {
                var proportion = DrawSurfaceEngine.NativeWidth/DrawSurfaceEngine.NativeHeight;
                var width = (int)(DrawSurfaceEngine.ScreenHeight * proportion) + (Width - DrawSurfaceEngine.ScreenWidth);
                

                if (SetSize == null)
                {
                    Width = width;
                } 
                else
                {
                    SetSize(width, Height);
                }
            }
        }

        public Proc<int,int> SetSize;

        private void ToolStripMenuItemSetProportion_Click(object sender, EventArgs e)
        {
            SetProportion();
        }

    }
}
