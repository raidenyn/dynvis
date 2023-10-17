using System;
using System.Windows.Forms;
using DynVis.Data.Geometry;
using DynVis.Data.Properties;

namespace DynVis.Data.GridSurface
{
    internal partial class FormGridGeometry : Form
    {
        public FormGridGeometry(double[] q1, double[] q2)
        {
            InitializeComponent();

            geometryEditor.SetGrid(q1, q2);
        }

        private GeometryGrid Geometry;

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabPageElementList)
            {
                if (CheckElementList()) SwitchToStructures();
                return;
            }
        }

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabPageStructures)
            {
                SwitchToElementList();
                return;
            }
        }

        private void SwitchToStructures()
        {
            buttonPrev.Enabled = true;
            buttonNext.Enabled = false;
            buttonOK.Enabled = true;

            tabControl.SelectedTab = tabPageStructures;

            geometryEditor.SetElements(elementListEditor.GetElements());
        }

        private void SwitchToElementList()
        {
            buttonPrev.Enabled = false;
            buttonNext.Enabled = true;
            buttonOK.Enabled = false;

            tabControl.SelectedTab = tabPageElementList;
        }

        private static bool CheckElementList()
        {
            return true;
        }

        private static bool CheckStructures()
        {
            return true;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabPageStructures)
            {
                if (CheckStructures()) CreateGeometry();
            }
        }

        private void CreateGeometry()
        {
            Geometry = new GeometryGrid(elementListEditor.GetElements(), geometryEditor.Q1, geometryEditor.Q2, geometryEditor.Structures);
            DialogResult = DialogResult.OK;
        }

        public GeometryGrid GetGeometry()
        {
            return Geometry;
        }

        private void FormImportSurface_Load(object sender, EventArgs e)
        {
            SwitchToElementList();
        }

        private void toolStripMenuItemLoadFromFile_Click(object sender, EventArgs e)
        {
            int[] elements;
            Structure[,] structures;
            var Result = GeometryInterfaceFunction.OpenGeometryFromText(out elements, geometryEditor.Q1.Length,
                                                                        geometryEditor.Q2.Length,
                                                                        out structures);

            if (Result == null)
            {
                if (!elementListEditor.SetElements(elements) || !geometryEditor.SetStructures(elements, structures))
                {
                    Result = LangResource.UbposibleSetReadData;
                }
                else
                {
                    Result = String.Empty;
                }
            }

            if (Result != String.Empty)
            {
                MessageBox.Show(this, Result, LangResource.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
