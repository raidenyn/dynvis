using System;
using DynVis.Forms;

namespace DynVis.Surface
{
    public partial class FormSurface : FormBase
    {
        public FormSurface()
        {
            InitializeComponent();

            drawSurface.SetSize = SetSize;
        }

        protected override void SetEvents()
        {
            ReactionData.SurfaceChanged += ReactionData_SurfaceChanged;
            ReactionData.ReactionPathChanged += ReactionData_ReactionPathChanged;
        }

        protected override void UnsetEvents()
        {
            ReactionData.SurfaceChanged -= ReactionData_SurfaceChanged;
            ReactionData.ReactionPathChanged -= ReactionData_ReactionPathChanged;
        }

        void ReactionData_SurfaceChanged(object sender, EventArgs e)
        {
            SetSurface();
        }

        void SetSurface()
        {
            if (Visible && ReactionData != null && ReactionData.Surface != null)
            {
                drawSurface.Surface = ReactionData.Surface;

            }
            else
            {
                drawSurface.Surface = null;
                Hide();
            }
        }

        void ReactionData_ReactionPathChanged(object sender, EventArgs e)
        {
            SetPath();
        }

        void SetPath()
        {
            if (Visible && ReactionData != null && ReactionData.CurrentReactionPath != null)
            {
                drawSurface.Path = ReactionData.CurrentReactionPath;
            }
            else
            {
                drawSurface.Path = null;
            }
        }

        
        private void FormDrawSurface_VisibleChanged(object sender, EventArgs e)
        {
            SetSurface();
            SetPath();
        }

        private void SetSize(int width, int height)
        {
            var controlWidth = width + Width - drawSurface.Width;
            var controlHeight = height + Height - drawSurface.Height;


            Width = controlWidth;
            Height = controlHeight;
        }

        public void SetProportion()
        {
            drawSurface.SetProportion();
        }
    }
}
