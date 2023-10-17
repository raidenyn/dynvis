using System;
using DynVis.Forms;

namespace DynVis.Points
{
    public partial class FormPoints : FormBase
    {
        public FormPoints()
        {
            InitializeComponent();
        }

        protected override void SetEvents()
        {
            ReactionData.PointsChanged += ReactionData_PointsChanged;
            ReactionData.SurfaceChanged += ReactionData_SurfaceChanged;
        }

        protected override void UnsetEvents()
        {
            ReactionData.PointsChanged -= ReactionData_PointsChanged;
            ReactionData.SurfaceChanged -= ReactionData_SurfaceChanged;
        }

        void ReactionData_PointsChanged(object sender, EventArgs e)
        {
            SetPoints();
        }

        void ReactionData_SurfaceChanged(object sender, EventArgs e)
        {
            surfacePointsView.Surface = ReactionData.Surface;
        }

        void SetPoints()
        {
            if (Visible && ReactionData != null)
            {
                surfacePointsView.SurfacePoints = ReactionData.Points;
            }
            else
            {
                surfacePointsView.SurfacePoints = null;
                Hide();
            }
        }

        private void FormPoints_VisibleChanged(object sender, EventArgs e)
        {
            SetPoints();
        }
    }
}
