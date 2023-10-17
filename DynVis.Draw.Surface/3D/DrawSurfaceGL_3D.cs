using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DynVis.Core;

namespace DynVis.Draw.Surface
{
    internal partial class DrawSurfaceGL_3D : DrawSurfaceBase
    {
        public DrawSurfaceGL_3D()
        {
            InitializeComponent();

            DrawSurfaceProvider = new DrawSurface3DProvider(drawBoxSurface);

            InitEvents();
        }

        private readonly DrawSurface3DProvider DrawSurfaceProvider;
       
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override ISurface3D Surface
        {
            get { return DrawSurfaceProvider.Surface; }
            set
            {
                if (DrawSurfaceProvider == null) return;

                if (DrawSurfaceProvider.Surface != null)
                {
                    DrawSurfaceProvider.Surface.CurrentPointChanged -= Surface_CurrentPointChanged;
                }

                DrawSurfaceProvider.Surface = value;

                if (DrawSurfaceProvider.Surface != null)
                {
                    DrawSurfaceProvider.Surface.CurrentPointChanged += Surface_CurrentPointChanged;
                }

                Refresh();
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override IPath Path
        {
            get { return DrawSurfaceProvider.Path; }
            set
            {
                if (DrawSurfaceProvider == null) return;

                if (DrawSurfaceProvider.Path != null)
                {
                    DrawSurfaceProvider.Path.CurrentPointChanged -= Surface_CurrentPointChanged;
                }

                DrawSurfaceProvider.Path = value;

                if (DrawSurfaceProvider.Path != null)
                {
                    DrawSurfaceProvider.Path.CurrentPointChanged += Surface_CurrentPointChanged;
                }

                Refresh();
            }
        }

        private void Surface_CurrentPointChanged(object sender, EventArgs e)
        {
            Draw();
        }

        private void DrawSurfaceGL_3D_Paint(object sender, PaintEventArgs e)
        {
             DrawSurfaceProvider.Draw();
        }

        private void drawBoxSurface_Click(object sender, EventArgs e)
        {
            Select();
        }

        private void InitEvents()
        {
            MouseWheel += DrawSurfaceGL_3D_MouseWheel;
        }

        private Point PrevMousePosition;

        void DrawSurfaceGL_3D_MouseWheel(object sender, MouseEventArgs e)
        {
            DrawSurfaceProvider.SetScale(e.Delta * 0.5);

            DrawSurfaceProvider.Draw();
        }

        private void DrawSurfaceGL_3D_MouseDown(object sender, MouseEventArgs e)
        {
            PrevMousePosition = e.Location;

        }

        void DrawSurfaceGL_3D_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DrawSurfaceProvider.RotateByX(e.X - PrevMousePosition.X);
                DrawSurfaceProvider.RotateByY(e.Y - PrevMousePosition.Y);

                DrawSurfaceProvider.Draw();
            }
            PrevMousePosition = e.Location;
        }

        void DrawSurfaceGL_3D_MouseUp(object sender, MouseEventArgs e)
        {

        }

        void DrawSurfaceGL_3D_SizeChanged(object sender, EventArgs e)
        {
            UpdateSize();
        }

        public void UpdateSize()
        {
            DrawSurfaceProvider.Resize(drawBoxSurface.Width, drawBoxSurface.Height);
            Draw();
        }

        public void Draw()
        {
            DrawSurfaceProvider.Draw();
        }

        private void ToolStripMenuItemViewPoint_Click(object sender, EventArgs e)
        {
            DrawSurfaceProvider.SurfaceMode = PolygonMode.Point;
            Draw();
        }

        private void ToolStripMenuItemViewLine_Click(object sender, EventArgs e)
        {
            DrawSurfaceProvider.SurfaceMode = PolygonMode.Line;
            Draw();
        }

        private void ToolStripMenuItemViewFill_Click(object sender, EventArgs e)
        {
            DrawSurfaceProvider.SurfaceMode = PolygonMode.Fill;
            Draw();
        }

        private void ToolStripMenuItemView_DropDownOpening(object sender, EventArgs e)
        {
            ToolStripMenuItemViewPoint.Checked =
                ToolStripMenuItemViewLine.Checked =
                ToolStripMenuItemViewFill.Checked = false;
            
            switch (DrawSurfaceProvider.SurfaceMode)
            {
                case PolygonMode.Point:
                    ToolStripMenuItemViewPoint.Checked = true; break;
                case PolygonMode.Line:
                    ToolStripMenuItemViewLine.Checked = true; break;
                case PolygonMode.Fill:
                    ToolStripMenuItemViewFill.Checked = true; break;
            }
        }


    }
}
