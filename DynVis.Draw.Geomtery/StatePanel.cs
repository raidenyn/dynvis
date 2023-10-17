using System;
using System.ComponentModel;
using System.Windows.Forms;
using DynVis.Core.Elements;
using DynVis.Draw.Geometry;

namespace DynVis.Draw.Geomtery
{
    internal partial class StatePanel : UserControl
    {
        public StatePanel()
        {
            InitializeComponent();
        }

        private GeometryDrawProvider _GeometryDrawProvider;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GeometryDrawProvider GeometryDrawProvider
        {
            get
            {
                return _GeometryDrawProvider;
            }
            set
            {
                if (GeometryDrawProvider != null) GeometryDrawProvider.SelectionChanged -= GeometryDrawProvider_SelectionChanged;
                _GeometryDrawProvider = value;
                if (GeometryDrawProvider != null) GeometryDrawProvider.SelectionChanged += GeometryDrawProvider_SelectionChanged;
            }

        }

        void GeometryDrawProvider_SelectionChanged(object sender, EventArgs e)
        {
            UpdateSelection();
        }


        public void SetBond()
        {
            GeometryDrawProvider.SetBondOnCurrentAtom();
            GeometryDrawProvider.Draw();
        }

        public void UnselectAll()
        {
            GeometryDrawProvider.UnselectAllAtom();
            UpdateSelection();
            GeometryDrawProvider.Draw();
        }

        public void UpdateSelection()
        {
            UpdateSelectionValue();
            ChangeTextBoxValueSize();

            labelSelectionAtomList.Text = String.Empty;
            foreach (var atom in GeometryDrawProvider.SelectionAtoms)
            {
                labelSelectionAtomList.Text += String.Format("{0}{1}; ", atom.Symbol(), atom.Number + 1);
            }

            buttonBond.Visible = GeometryDrawProvider.SelectionAtoms.Count == 2;
        }

        public void UpdateSelectionValue()
        {
            textBoxValue.Text = GeometryDrawProvider.SelectedAtomsValuesString;
        }

        private void ChangeTextBoxValueSize()
        {
            var G = textBoxValue.CreateGraphics();
            var width = (int)G.MeasureString(textBoxValue.Text, textBoxValue.Font).Width + 10;
            textBoxValue.Left -= width - textBoxValue.Width;
            textBoxValue.Width = width;
        }

        private void buttonBond_Click(object sender, EventArgs e)
        {
            SetBond();
        }

        private void StatePanel_Enter(object sender, EventArgs e)
        {
            textBoxValue.Select();
        }
    }
}
