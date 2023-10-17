using System;
using System.Windows.Forms;
using DynVis.Core;
using DynVis.Data.Properties;

namespace DynVis.Data.GridSurface
{
    internal partial class FormGridSurface : Form
    {
        public FormGridSurface()
        {
            InitializeComponent();
        }

        private GridSurface Surface;

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabPageCoordinate)
            {
                if (CheckCoordinate()) SwitchToEnergy();
                return;
            }
        }

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabPageEnergy)
            {
                SwitchToCoordinate();
                return;
            }
        }

        private void SwitchToEnergy()
        {
            buttonPrev.Enabled = true;
            buttonNext.Enabled = false;
            buttonOK.Enabled = true;

            tabControl.SelectedTab = tabPageEnergy;

            surfaceGridEditor.SetCoordinateGrid(coordinateEditorQ1.GetCoordinates(), coordinateEditorQ2.GetCoordinates());
        }

        private void SwitchToCoordinate()
        {
            buttonPrev.Enabled = false;
            buttonNext.Enabled = true;
            buttonOK.Enabled = false;

            tabControl.SelectedTab = tabPageCoordinate;
        }



        private bool CheckCoordinate()
        {
            var CoordinateCheckErrorMessage = coordinateEditorQ1.CheckCoordinateValueGrid();
            if (!String.IsNullOrEmpty(CoordinateCheckErrorMessage))
            {
                MessageBox.Show(this,
                                String.Format("{0}:\n    {1}", coordinateEditorQ1.Title, CoordinateCheckErrorMessage),
                                LangResource.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            CoordinateCheckErrorMessage = coordinateEditorQ2.CheckCoordinateValueGrid();
            if (!String.IsNullOrEmpty(CoordinateCheckErrorMessage))
            {
                MessageBox.Show(this,
                                String.Format("{0}:\n    {1}", coordinateEditorQ2.Title, CoordinateCheckErrorMessage),
                                LangResource.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private static bool CheckEnergy()
        {
            return true;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabPageEnergy)
            {
                if (CheckEnergy()) CreateSurface();
            }
        }

        private void CreateSurface()
        {
            Surface = new GridSurface(coordinateEditorQ1.GetCoordinates(), coordinateEditorQ2.GetCoordinates(), surfaceGridEditor.GetSurface(), coordinateEditorQ1.Axe, coordinateEditorQ2.Axe);
            DialogResult = DialogResult.OK;
        }

        public GridSurface GetSurface()
        {
            return Surface;
        }

        private void FormImportSurface_Load(object sender, EventArgs e)
        {
            SwitchToCoordinate();
        }

        private void ToolStripMenuItemOpen_Click(object sender, EventArgs e)
        {
            double[] Q1, Q2;
            double[,] Energy;
            Axes axes1, axes2;

            var resultString = OpenFile.FromFile(out Q1, out Q2, out Energy, out axes1, out axes2);
            if (resultString != null)
            if (String.IsNullOrEmpty(resultString))
            {
                coordinateEditorQ1.SetCoordinate(Q1);
                coordinateEditorQ1.Axe = axes1;
                coordinateEditorQ2.SetCoordinate(Q2);
                coordinateEditorQ2.Axe = axes2;

                if (CheckCoordinate()) SwitchToEnergy();

                surfaceGridEditor.SetSurface(Energy);
            } 
            else
            {
                MessageBox.Show(this, resultString, LangResource.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);    
            }
        }
    }
}
