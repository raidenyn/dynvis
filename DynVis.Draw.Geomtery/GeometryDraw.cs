using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DynVis.Core;
using DynVis.Math;
using DynVis.Draw.Geometry;


namespace DynVis.Geometry.Draw
{
    public partial class GeometryDraw : BaseControl
    {
        public GeometryDraw()
        {
            InitializeComponent();

            GeometryDrawProvider = new GeometryDrawProvider(drawBox);

            GeometryDrawProvider.SelectionChanged += GeometryDrawProvider_SelectionChanged;

            statePanel.GeometryDrawProvider = GeometryDrawProvider;
        }

        private readonly GeometryDrawProvider GeometryDrawProvider;

       

        #region Events
        private void GeometryDraw_Load(object sender, EventArgs e)
        {
            InitEvents();
        }

        private void InitEvents()
        {
            FindForm().MouseWheel += GeometryDraw_MouseWheel;
        }

        private Point PrevMousePosition;
        private Point StartMousePosition;

        private void GeometryDraw_MouseWheel(object sender, MouseEventArgs e)
        {
            GeometryDrawProvider.SetScale(e.Delta * 0.5);

            GeometryDrawProvider.Draw();
        }

        private void GeometryDraw_MouseDown(object sender, MouseEventArgs e)
        {
            StartMousePosition = e.Location;
            PrevMousePosition = e.Location;

            if (e.Button == MouseButtons.Left)
            {
                GeometryDrawProvider.SelectAtoms(e.X, e.Y);
            }
        }

        private void GeometryDraw_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                GeometryDrawProvider.RotateByX(e.X - PrevMousePosition.X);
                GeometryDrawProvider.RotateByY(e.Y - PrevMousePosition.Y);

                GeometryDrawProvider.Draw();
            }
            PrevMousePosition = e.Location;
        }

        private void GeometryDraw_MouseUp(object sender, MouseEventArgs e)
        {
            if (EnableMenu && e.Button == MouseButtons.Right && StartMousePosition.SquearDistanceTo(e.Location) < 2)
                contextMenuStripGeneral.Show(PointToScreen(e.Location));
        }

        private void drawBox_Click(object sender, EventArgs e)
        {
            statePanel.Select();
        }

        private void GeometryDraw_Paint(object sender, PaintEventArgs e)
        {
#if DEBUG
            if (!DesignMode)
            {
#endif
            Draw();
#if DEBUG
            }
#endif
        }

        private void GeometryDraw_SizeChanged(object sender, EventArgs e)
        {
#if DEBUG
            if (!DesignMode)
            {
#endif
            UpdateSize();
#if DEBUG
            }
#endif
        }
        #endregion
        
        
        public void UpdateSize()
        {
            GeometryDrawProvider.Resize(drawBox.Width, drawBox.Height);
            Draw();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IAtomStructure AtomStructure
        {
            get
            {
                return GeometryDrawProvider.CurrentAtomStructure;
            }
            set
            {
                AlocateEvents(GeometryDrawProvider.CurrentAtomStructure, value);

                GeometryDrawProvider.CurrentAtomStructure = value;
                FormCoordinateView.AtomStructure = value;
            }
        }

        private void AlocateEvents(IAtomStructure oldAtomStructure, IAtomStructure newAtomStructure)
        {
            if (oldAtomStructure != null)
            {
                oldAtomStructure.GeometryChanged -= AtomStructure_GeomentryChanged;
                oldAtomStructure.GeometryChanged -= GeometryChanged;
            }
            if (newAtomStructure != null)
            {
                newAtomStructure.GeometryChanged += AtomStructure_GeomentryChanged;
                newAtomStructure.GeometryChanged += GeometryChanged;
            }
        }

        private void AtomStructure_GeomentryChanged(object sender, EventArgs e)
        {
            Draw();
            statePanel.UpdateSelectionValue();
            FormCoordinateView.Refresh();
        }

        public void Draw()
        {
            GeometryDrawProvider.Draw();
        }


        private void drawBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.B)
            {
                statePanel.SetBond();
            }
        }


        #region Menu
        public bool EnableMenu
        { get; set;
        }

        private void GeometryDraw_DoubleClick(object sender, EventArgs e)
        {
            GeometryDrawProvider.UnselectAllAtom();
            GeometryDrawProvider.Draw();
        }

        public event EventHandler GeometryChanged;

        public void AddToContextMenu(ToolStripItem item)
        {
            contextMenuStripGeneral.Items.Add(item);
        }

        private void ToolStripMenuItemProperties_Click(object sender, EventArgs e)
        {
            GeometryDrawProvider.ShowPropertiesDialog();
        }


        private void GeneralContextMenu_Opening(object sender, CancelEventArgs e)
        {

            contextMenuStripGeneral.Items[0].Enabled = AtomStructure != null && !FormCoordinateView.Visible;
        }

        private readonly FormCoordinateView FormCoordinateView = new FormCoordinateView();

        private void viewGeometryItem_Click(object sender, EventArgs e)
        {
            if (AtomStructure != null)
            {
                FormCoordinateView.Show(this);
            }

        }
        #endregion

        void GeometryDrawProvider_SelectionChanged(object sender, EventArgs e)
        {
            if (SelectionChanged != null) SelectionChanged(this, e);
        }

        public List<IAtom> SelectedAtoms
        {
            get
            {
                return GeometryDrawProvider.SelectionAtoms;
            }
        }

        public event EventHandler SelectionChanged;
    }
}
