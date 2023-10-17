using System.ComponentModel;
using System.Windows.Forms;
using DynVis.Core;

namespace DynVis.Draw.Surface
{
    public partial class DrawSurface : BaseControl
    {
        public DrawSurface()
        {
            InitializeComponent();

            Set2D();

            drawSurfaceGDI_2D.SetSize = SetSizeControl;
        }

        private DrawSurfaceBase CurrentDrawControl
        { get; set;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ISurface3D Surface
        {
            get { return CurrentDrawControl.Surface; }
            set
            {
                drawSurfaceGDI_2D.Surface = value;
                drawSurfaceGL_3D.Surface = value;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IPath Path
        {
            get { return CurrentDrawControl.Path; }
            set
            {
                drawSurfaceGDI_2D.Path = value;
                drawSurfaceGL_3D.Path = value;
            }
        }

        void Set2D()
        {
            if (CurrentDrawControl != null)
            {
                drawSurfaceGDI_2D.Surface = CurrentDrawControl.Surface;
                drawSurfaceGDI_2D.Path = CurrentDrawControl.Path;
            }


            drawSurfaceGL_3D.Surface = null;
            drawSurfaceGL_3D.Path = null;

            drawSurfaceGL_3D.Visible = false;
            drawSurfaceGDI_2D.Visible = true;


            CurrentDrawControl = drawSurfaceGDI_2D;

            splitContainer.Panel1Collapsed = false;
            splitContainer.Panel2Collapsed = true;

            button3D.Enabled = buttonSplit.Enabled = true;
            button2D.Enabled = false;

            SetProportion();
        }

        void Set3D()
        {
            if (CurrentDrawControl != null)
            {
                drawSurfaceGL_3D.Surface = CurrentDrawControl.Surface;
                drawSurfaceGL_3D.Path = CurrentDrawControl.Path;
            }

            drawSurfaceGDI_2D.Surface = null;
            drawSurfaceGDI_2D.Path = null;

            drawSurfaceGDI_2D.Visible = false;
            drawSurfaceGL_3D.Visible = true;

            CurrentDrawControl = drawSurfaceGL_3D;

            splitContainer.Panel2Collapsed = false;
            splitContainer.Panel1Collapsed = true;

            button2D.Enabled = buttonSplit.Enabled = true;
            button3D.Enabled = false;
        }

        void SetSplit()
        {
            if (CurrentDrawControl != null)
            {
                drawSurfaceGL_3D.Surface = CurrentDrawControl.Surface;
                drawSurfaceGL_3D.Path = CurrentDrawControl.Path;

                drawSurfaceGDI_2D.Surface = CurrentDrawControl.Surface;
                drawSurfaceGDI_2D.Path = CurrentDrawControl.Path;
            }

            drawSurfaceGDI_2D.Visible = true;
            drawSurfaceGL_3D.Visible = true;


            splitContainer.Panel2Collapsed = false;
            splitContainer.Panel1Collapsed = false;

            button2D.Enabled = button3D.Enabled = true;
            buttonSplit.Enabled = false;

            SetProportion();
        }


        private void button2D_Click(object sender, System.EventArgs e)
        {
            Set2D();
        }

        private void button3D_Click(object sender, System.EventArgs e)
        {
            Set3D();
        }

        private void buttonSplit_Click(object sender, System.EventArgs e)
        {
            SetSplit();
        }

        private void SetSizeControl(int width, int height)
        {
            
            var controlWidth = width * (splitContainer.Panel2Collapsed ? 1 : 2) + splitContainer.SplitterWidth - 1;
            var controlHeight = height + Height - drawSurfaceGDI_2D.Height;

            if (SetSize == null)
            {
                Width = controlWidth;
                Height = controlHeight;
            }
            else
            {
                SetSize(controlWidth, controlHeight);
                splitContainer.SplitterDistance = width;
            }
        }

        public Proc<int, int> SetSize;

        public void SetProportion()
        {
            drawSurfaceGDI_2D.SetProportion();
        }
    }
}
