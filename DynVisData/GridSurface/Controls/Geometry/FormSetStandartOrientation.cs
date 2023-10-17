using System;
using System.Windows.Forms;
using DynVis.Core;
using DynVis.Data.Geometry;
using DynVis.Data.Properties;

namespace DynVis.Data.GridSurface
{
    internal partial class FormSetStandartOrientation : Form
    {
        public FormSetStandartOrientation()
        {
            InitializeComponent();
        }

        private int[] _Elements;

        public int[] Elements
        {
            get
            {
                return _Elements;
            }
            set
            {
                _Elements = value;
                UpdateLists();
            }
        }

        private Structure _Structure;

        public Structure Structure
        {
            get
            {
                return _Structure;
            }
            set
            {
                _Structure = value;
                CreateGeometryGrid();
            }
        }

        private void CreateGeometryGrid()
        {
            var st = new IStructure[1, 1];
            st[0, 0] = Structure;
            GeometryGrid = new GeometryGrid(Elements, new[] { 1.0 }, new[] { 1.0 }, st);

            GeometryGrid.SetPoint(1.0, 1.0);

            geometryDraw.AtomStructure = GeometryGrid.AtomStructure;
        }

        private GeometryGrid GeometryGrid;

        class VisibleElement
        {
            public readonly int Element;

            public readonly int Number;

            public VisibleElement(int element, int number)
            {
                Element = element;
                Number = number;
            }

            public override string ToString()
            {
                return String.Format("{0}: {1}", Number, Core.Elements.Elements.GetElementSymbol(Element));
            }

        }

        private void UpdateLists()
        {
            comboBoxCenter.Items.Clear();
            comboBoxDirectZ.Items.Clear();
            comboBoxPlaneXZ.Items.Clear();
            if (Elements != null)
            {
                var counter = 0;
                foreach (var element in Elements)
                {
                    var ve = new VisibleElement(element, counter++);
                    comboBoxCenter.Items.Add(ve);
                    comboBoxDirectZ.Items.Add(ve);
                    comboBoxPlaneXZ.Items.Add(ve);
                }


            }
        }

        public int Ceneter
        {
            get
            {
                return comboBoxCenter.SelectedIndex >= 0 ? ((VisibleElement)comboBoxCenter.Items[comboBoxCenter.SelectedIndex]).Number : -1;
            }
        }

        public int DirectZ
        {
            get
            {
                return comboBoxCenter.SelectedIndex >= 0 ? ((VisibleElement)comboBoxDirectZ.Items[comboBoxDirectZ.SelectedIndex]).Number : -1;
            }
        }

        public int PlaneXZ
        {
            get
            {
                return comboBoxCenter.SelectedIndex >= 0 ? ((VisibleElement)comboBoxPlaneXZ.Items[comboBoxPlaneXZ.SelectedIndex]).Number : -1;
            }
        }

        private void buttonОК_Click(object sender, EventArgs e)
        {
            if (Ceneter == DirectZ || DirectZ == PlaneXZ || PlaneXZ == Ceneter)
            {
                MessageBox.Show(this, LangResource.AtomsCouldNotBeRepeated, LangResource.Error, MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            else
            {
                if (Ceneter == -1 || DirectZ == -1 || PlaneXZ == -1)
                {
                    MessageBox.Show(this, LangResource.AtomNotSelected, LangResource.Error, MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                }
                else
                {
                    DialogResult = DialogResult.OK;
                }
            }
        }

        private void FormSetStandartOrientation_Load(object sender, EventArgs e)
        {
            geometryDraw.UpdateSize();
            geometryDraw.Draw();
            
            if (Elements != null)
            {
                comboBoxCenter.SelectedIndex = 0;
                comboBoxDirectZ.SelectedIndex = 1;
                comboBoxPlaneXZ.SelectedIndex = 2;
            }
        }

        private bool FreezUpdate;

        private void geometryDraw_SelectionChanged(object sender, EventArgs e)
        {
            FreezUpdate = true;
            if (geometryDraw.SelectedAtoms.Count > 3)
            {
                geometryDraw.SelectedAtoms.RemoveAt(0);
            }
            comboBoxCenter.SelectedIndex = geometryDraw.SelectedAtoms.Count > 0 ? geometryDraw.SelectedAtoms[0].Number : -1;
            comboBoxDirectZ.SelectedIndex = geometryDraw.SelectedAtoms.Count > 1 ? geometryDraw.SelectedAtoms[1].Number : -1;
            comboBoxPlaneXZ.SelectedIndex = geometryDraw.SelectedAtoms.Count > 2 ? geometryDraw.SelectedAtoms[2].Number : -1;
            FreezUpdate = false;
        }

        private void comboBoxCenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!FreezUpdate && Ceneter != -1 && GeometryGrid != null)
            {
                var atom = GeometryGrid.AtomStructure[Ceneter];
                if (geometryDraw.SelectedAtoms.Contains(atom))
                {
                    geometryDraw.SelectedAtoms.Remove(atom);
                }
                if (geometryDraw.SelectedAtoms.Count > 1) geometryDraw.SelectedAtoms.RemoveAt(0);
                if (geometryDraw.SelectedAtoms.Count > 1) geometryDraw.SelectedAtoms.Insert(0, atom);
                else geometryDraw.SelectedAtoms.Add(atom);

                geometryDraw.Refresh();
            }

        }

        private void comboBoxDirectZ_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!FreezUpdate && DirectZ != -1 && GeometryGrid != null)
            {
                var atom = GeometryGrid.AtomStructure[DirectZ];
                if (geometryDraw.SelectedAtoms.Contains(atom))
                {
                    geometryDraw.SelectedAtoms.Remove(atom);
                }
                if (geometryDraw.SelectedAtoms.Count > 2) geometryDraw.SelectedAtoms.RemoveAt(1);
                if (geometryDraw.SelectedAtoms.Count > 2) geometryDraw.SelectedAtoms.Insert(1, atom);
                else geometryDraw.SelectedAtoms.Add(atom);

                geometryDraw.Refresh();
            }
        }

        private void comboBoxPlaneXZ_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!FreezUpdate && PlaneXZ != -1 && GeometryGrid != null)
            {
                var atom = GeometryGrid.AtomStructure[PlaneXZ];
                if (geometryDraw.SelectedAtoms.Contains(atom))
                {
                    geometryDraw.SelectedAtoms.Remove(atom);
                }
                if (geometryDraw.SelectedAtoms.Count > 3) geometryDraw.SelectedAtoms.RemoveAt(2);
                if (geometryDraw.SelectedAtoms.Count > 3) geometryDraw.SelectedAtoms.Insert(2, atom);
                else geometryDraw.SelectedAtoms.Add(atom);

                geometryDraw.Refresh();
            }
        }
    }
}
