using System;
using System.Drawing;
using DynVis.Forms;

namespace DynVis.CurrentPointInformation
{
    public partial class FormCurrentPointInformation : FormBase
    {
        public FormCurrentPointInformation()
        {
            InitializeComponent();
        }

        protected override void SetEvents()
        {
            ReactionData.SurfaceChanged -= ReactionData_SurfaceChanged;
        }

        protected override void UnsetEvents()
        {
            ReactionData.SurfaceChanged += ReactionData_SurfaceChanged;
        }

        void ReactionData_SurfaceChanged(object sender, EventArgs e)
        {
            SetSurface();
        }

        void SetSurface()
        {
            if (Visible && ReactionData != null && ReactionData.Surface != null)
            {
                currentPointInformation.Surface = ReactionData.Surface;
            }
            else
            {
                currentPointInformation.Surface = null;
                Hide();
            }
        }

        private void FormCurrentPointInformation_VisibleChanged(object sender, EventArgs e)
        {
            SetSurface();
        }

        private void currentPointInformation_SizeChanged(object sender, EventArgs e)
        {
            ClientSize = new Size(currentPointInformation.Width, currentPointInformation.Height);
        }

        private void FormCurrentPointInformation_Activated(object sender, EventArgs e)
        {
            currentPointInformation.UpdateView();
        }

        private void FormCurrentPointInformation_Shown(object sender, EventArgs e)
        {
            
        }
    }
}
