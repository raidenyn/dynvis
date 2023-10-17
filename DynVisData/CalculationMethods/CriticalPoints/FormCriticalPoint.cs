using System;
using System.Windows.Forms;
using DynVis.Core;

namespace DynVis.Data.CalculationMethods.CriticalPoints
{
    internal partial class FormCriticalPoint : Form
    {
        public FormCriticalPoint()
        {
            InitializeComponent();


        }

        private CriticalPoints _CriticalPoints;
        public CriticalPoints CriticalPoints
        {
            get { return _CriticalPoints; }
            set
            {
                _CriticalPoints = value;
                UpdateView();
            }
        }

        public ISurface3D Surface{get;set;}

        private void UpdateView()
        {
            decimalEditorScanStepCount.Value = CriticalPoints.stepCount;
            decimalEditorEplsilon.Value = CriticalPoints.Epsilon;
            decimalEditorOPtimizationMaxStepCount.Value = CriticalPoints.MaxStepForOptimization;

            if (Surface != null)
            {
                decimalEditorQ1.Value = Surface.CurrentPoint.X;
                decimalEditorQ2.Value = Surface.CurrentPoint.Y;
            }

            switch (CriticalPoints.CalcMode)
            {
                case CriticalPoints.CalcModeType.ScanSurface:
                    radioButtonScanSurface.Checked = true; break;
                case CriticalPoints.CalcModeType.OnePoint:
                    radioButtonPoint.Checked = true; break;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (!decimalEditorScanStepCount.IsValidValue) decimalEditorScanStepCount.Select();
            if (!decimalEditorEplsilon.IsValidValue) decimalEditorEplsilon.Select();
            if (!decimalEditorScanStepCount.IsValidValue) decimalEditorScanStepCount.Select();

            CriticalPoints.stepCount = (int)decimalEditorScanStepCount.Value  ;
            CriticalPoints.Epsilon = decimalEditorEplsilon.Value  ;
            CriticalPoints.MaxStepForOptimization = (int)decimalEditorOPtimizationMaxStepCount.Value;

            if (!decimalEditorQ1.IsValidValue) decimalEditorQ1.Select();
            if (!decimalEditorQ2.IsValidValue) decimalEditorQ2.Select();

            CriticalPoints.Q1 = decimalEditorQ1.Value;
            CriticalPoints.Q2 = decimalEditorQ2.Value;

            DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void radioButtonScanSurface_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxScan.Enabled = true;
            groupBoxPoint.Enabled = false;

            CriticalPoints.CalcMode = CriticalPoints.CalcModeType.ScanSurface;
        }

        private void radioButtonPoint_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxScan.Enabled = false;
            groupBoxPoint.Enabled = true;

            CriticalPoints.CalcMode = CriticalPoints.CalcModeType.OnePoint;
        }
    }
}
