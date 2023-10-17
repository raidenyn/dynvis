using System;
using System.Windows.Forms;
using DynVis.Forms;
using DynVis.Properties;

namespace DynVis.Geometry
{
    public partial class FormGeometry : FormBase
    {
        public FormGeometry()
        {
            InitializeComponent();

            VisibleScroles = false;
            AddVisibleScrolesChanger();
        }

        protected override void SetEvents()
        {
            ReactionData.GeometryChanged += ReactionData_GeometryChanged;
        }

        protected override void UnsetEvents()
        {
            ReactionData.GeometryChanged -= ReactionData_GeometryChanged;
        }

        private bool _VisibleScroles = true;
        public bool VisibleScroles
        {
            get
            {
                return _VisibleScroles;
            }
            set
            {
                _VisibleScroles = value;
                SetVisibleScroles(VisibleScroles);
            }
        }


        void ReactionData_GeometryChanged(object sender, EventArgs e)
        {
            SetAtomStructure();
        }

        private void trackBarQ_Scroll(object sender, EventArgs e)
        {
            var Q1Pos = ReactionData.Surface.Axes1.MinValue + (trackBarQ1.Value / (float)trackBarQ2.Maximum * (ReactionData.Surface.Axes1.MaxValue - ReactionData.Surface.Axes1.MinValue));
            var Q2Pos = ReactionData.Surface.Axes2.MinValue + ((trackBarQ2.Value) / (float)trackBarQ2.Maximum * (ReactionData.Surface.Axes2.MaxValue - ReactionData.Surface.Axes2.MinValue));

            ReactionData.Geometry.SetPoint(Q1Pos, Q2Pos);
        }

        private void FormGeometry_Load(object sender, EventArgs e)
        {
            geometryDraw.UpdateSize();
            geometryDraw.Draw();
        }

        private void geometryDraw_GeometryChanged(object sender, EventArgs e)
        {
            if (ReactionData.Surface != null)
            {
                trackBarQ1.Value = (int)((ReactionData.Geometry.Q1 - ReactionData.Surface.Axes1.MinValue) * trackBarQ1.Maximum / (ReactionData.Surface.Axes1.MaxValue - ReactionData.Surface.Axes1.MinValue));
                trackBarQ2.Value = (int)((ReactionData.Geometry.Q2 - ReactionData.Surface.Axes2.MinValue) * trackBarQ2.Maximum / (ReactionData.Surface.Axes2.MaxValue - ReactionData.Surface.Axes2.MinValue));
            }
        }

        private void FormGeometry_VisibleChanged(object sender, EventArgs e)
        {
            SetAtomStructure();
        }

        private void SetAtomStructure()
        {
            if (Visible && ReactionData != null && ReactionData.Geometry != null)
            {
                geometryDraw.AtomStructure = ReactionData.Geometry.AtomStructure;
            }
            else
            {
                geometryDraw.AtomStructure = null;
                Hide();
            }
        }

        private void SetVisibleScroles(bool scrolesVisible)
        {
            trackBarQ1.Visible = trackBarQ2.Visible = scrolesVisible;
            if (scrolesVisible)
            {
                geometryDraw.Width -= trackBarQ2.Width;
                geometryDraw.Height -= trackBarQ1.Height;
                geometryDraw.Left = trackBarQ2.Width;
                geometryDraw.Top = trackBarQ1.Height;
            } 
            else
            {
                geometryDraw.Width += trackBarQ2.Width;
                geometryDraw.Height += trackBarQ1.Height;
                geometryDraw.Left = geometryDraw.Top = 0; 
            }
        }

        private void ChangeVisibleScroles(object sender, EventArgs e)
        {
            VisibleScroles = !VisibleScroles;
            VisibleScrolesChanger.Text = VisibleScrolesChangerCurrentText;
        }

        private ToolStripMenuItem VisibleScrolesChanger;
        private string VisibleScrolesChangerCurrentText
        {
            get
            {
                if (VisibleScroles)
                {
                    return Resources.HideCoord;
                }
                return Resources.ShowCoord;
            }
        }

        private void AddVisibleScrolesChanger()
        {
            VisibleScrolesChanger = new ToolStripMenuItem(VisibleScrolesChangerCurrentText, null, ChangeVisibleScroles);

            geometryDraw.AddToContextMenu(new ToolStripSeparator());
            geometryDraw.AddToContextMenu(VisibleScrolesChanger);
        }
    }
}
