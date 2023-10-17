using System;
using System.Windows.Forms;
using DynVis.Core;
using DynVis.Core.IO;

namespace DynVis.Data.CalculationMethods.Dynamic
{
    internal partial class FormDynamicParamsEditor : Form
    {
        public FormDynamicParamsEditor()
        {
            InitializeComponent();
        }

        public DialogResult ShowDialog(IWin32Window owner, ISurface3D pes, ref DynamicPath dynamicPath)
        {
            DynamicPath = (DynamicPath)dynamicPath.Clone();

            if (pes.CurrentPoint != null)
            {
                DynamicPath.Q1 = pes.CurrentPoint.X;
                DynamicPath.Q2 = pes.CurrentPoint.Y;
            }


            PES = pes;
            var Result =  ShowDialog(owner);
            if (Result == DialogResult.OK) dynamicPath = DynamicPath;
            return Result;
        }

        private DynamicPath DynamicPath;
        private ISurface3D PES;


        private void UpdateViewValues(DynamicPath dp, ISurface3D pes)
        {
            physicalValueEditorQ1.SetValue(dp.Q1, pes.Axes1.ScaleDimension);
            physicalValueEditorQ2.SetValue(dp.Q2, pes.Axes2.ScaleDimension);

            decimalEditorV1.Value = dp.v1;
            decimalEditorV2.Value = dp.v2;

            physicalValueEditorM1.SetValue(dp.M1, DimMass.Dalton);
            physicalValueEditorM2.SetValue(dp.M2, DimMass.Dalton);

            physicalValueEditorTimeStep.SetValue(dp.dt, DimTime.Second, ScaleCoeff.femto);
            decimalEditorMaxStepCount.Value = dp.MaxStep;
        }

        private void UpdateParams(DynamicPath dp, ISurface3D pes)
        {
            dp.Q1 = physicalValueEditorQ1.GetValue(pes.Axes1.ScaleDimension);
            dp.Q2 = physicalValueEditorQ2.GetValue(pes.Axes2.ScaleDimension);

            dp.v1 = decimalEditorV1.Value;
            dp.v2 = decimalEditorV2.Value;

            dp.M1 = physicalValueEditorM1.GetValue(DimMass.Dalton);
            dp.M2 = physicalValueEditorM2.GetValue(DimMass.Dalton);

            dp.dt = physicalValueEditorTimeStep.GetValue(DimTime.Second, ScaleCoeff.femto);
            dp.MaxStep = (int)decimalEditorMaxStepCount.Value;
            
        }

        private void FormDynamicParamsEditor_Load(object sender, EventArgs e)
        {
            UpdateViewValues(DynamicPath, PES);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            UpdateParams(DynamicPath, PES);
            DialogResult = DialogResult.OK;
        }

        private void ToolStripMenuItemSave_Click(object sender, EventArgs e)
        {
            UpdateParams(DynamicPath, PES);
            IOFileDialog.SaveToFile(DynamicPath);
        }

        private void ToolStripMenuItemOpen_Click(object sender, EventArgs e)
        {
            var temp = IOFileDialog.OpenObjectFromFile<DynamicPath>(DynamicPath.SaveFilters, DynamicPath.OpenFromFile);
            if (temp != null)
            {
                DynamicPath = temp;
                UpdateViewValues(DynamicPath, PES);
            }
        }

    }
}
