using System;
using System.Windows.Forms;
using DynVis.Core.IO;
using DynVis.Data.Properties;
using M=System.Math;

namespace DynVis.Data.CalculationMethods.LEPS
{
    internal partial class FormLEPSEditor : Form
    {
        public FormLEPSEditor()
        {
            InitializeComponent();
            
        }

        private bool CreateSurface()
        {
            if (GetParams())
            {
                SurfaceLEPS = new SurfaceLEPS(ParamsLEPS);
                return true;
            }
            return false;
        }

        bool GetParams()
        {
            ParamsLEPS.AB = AB.GetAtomAtomProperty();
            if (ParamsLEPS.AB == null) return false;
            ParamsLEPS.AC = AC.GetAtomAtomProperty();
            if (ParamsLEPS.AC == null) return false;
            ParamsLEPS.BC = BC.GetAtomAtomProperty();
            if (ParamsLEPS.BC == null) return false;
            var d = coordinateParamsLEPS.GetConst();
            if (d == null) return false;
            ParamsLEPS.ConstQValue = d.Value;

            ParamsLEPS.Axes1 = coordinateParamsLEPS.GetAxes1();
            if (ParamsLEPS.Axes1 == null) return false;
            ParamsLEPS.Axes2 = coordinateParamsLEPS.GetAxes2();
            if (ParamsLEPS.Axes2 == null) return false;

            ParamsLEPS.ElementA = elementSelectorA.ElementNumber;
            ParamsLEPS.ElementB = elementSelectorB.ElementNumber;
            ParamsLEPS.ElementC = elementSelectorC.ElementNumber;

            return true;
        }

        public SurfaceLEPS GetSurface()
        {
            return SurfaceLEPS;
        }

        private SurfaceLEPS SurfaceLEPS;
        private ParamsLEPS ParamsLEPS = new ParamsLEPS { ConstQValue = M.PI };


        private void FormLEPSEditor_Load(object sender, EventArgs e)
        {
            SetParams();
        }

        private void SetParams()
        {
            elementSelectorA.ElementNumber = ParamsLEPS.ElementA;
            elementSelectorB.ElementNumber = ParamsLEPS.ElementB;
            elementSelectorC.ElementNumber = ParamsLEPS.ElementC;

            AB.SetAtomAtomProperty(ParamsLEPS.AB);
            AC.SetAtomAtomProperty(ParamsLEPS.AC);
            BC.SetAtomAtomProperty(ParamsLEPS.BC);

            coordinateParamsLEPS.SetAxes1(ParamsLEPS.Axes1);
            coordinateParamsLEPS.SetAxes2(ParamsLEPS.Axes2);
            coordinateParamsLEPS.SetConst(ParamsLEPS.ConstQValue);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (CreateSurface())
            {
                DialogResult = DialogResult.OK;
            } else
            {
                MessageBox.Show(this, LangResource.ErrorInInputData, LangResource.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ToolStripMenuItemSave_Click(object sender, EventArgs e)
        {
            if (GetParams())
            {
                IOFileDialog.SaveToFile(ParamsLEPS);
            }
            
        }

        private void ToolStripMenuItemOpen_Click(object sender, EventArgs e)
        {
            var temp = IOFileDialog.OpenObjectFromFile<ParamsLEPS>(ParamsLEPS.SaveFilters, ParamsLEPS.OpenFromFile);
            if (temp != null)
            {
                ParamsLEPS = temp;
                SetParams();
            }
        }
    }
}
