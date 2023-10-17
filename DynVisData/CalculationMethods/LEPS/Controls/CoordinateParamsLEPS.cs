using System;
using DynVis.Core;
using DynVis.Data.Properties;

namespace DynVis.Data.CalculationMethods.LEPS.Controls
{
    internal partial class CoordinateParamsLEPS : BaseControl
    {
        public CoordinateParamsLEPS()
        {
            InitializeComponent();
        }

        public AxesLEPS GetAxes1()
        {
            var min = physicalValueEditorQ1Min.GetValue(DimLength.Angstrom);
            var max = physicalValueEditorQ1Max.GetValue(DimLength.Angstrom);
            var name = labelQ1.Text;

            if (min >= max)
            {
                return null;
            }

            return new AxesLEPS { MinValue = min, MaxValue = max, Name = name };
        }

        public AxesLEPS GetAxes2()
        {
            var name = labelQ2.Text;
            double max, min;
            LEPSAxesType axesType;

            if (radioButtonDistanceDistance.Checked)
            {
                min = physicalValueEditorQ2Min.GetValue(DimLength.Angstrom);
                max = physicalValueEditorQ2Max.GetValue(DimLength.Angstrom);

                axesType = LEPSAxesType.Distance;
            } 
            else
            {
                min = physicalValueEditorQ2Min.GetValue(DimAngle.Radian);
                max = physicalValueEditorQ2Max.GetValue(DimAngle.Radian);

                axesType = LEPSAxesType.Angle;
            }

            if (min >= max)
            {
                return null;
            }

            return new AxesLEPS { MinValue = min, MaxValue = max, Name = name, AxesType = axesType };


        }


        private static void SetAxes(double v, PhysicalValueEditor editor, LEPSAxesType axesType)
        {
            if (axesType == LEPSAxesType.Distance)
            {
                editor.DimensionType = DimensionType.Lenght;
                editor.SetValue(v, DimLength.Angstrom);
            } 
            else
            {
                editor.DimensionType = DimensionType.Angle;
                editor.SetValue(v, DimAngle.Radian, DimAngle.Degree);
            }
        }

        public void SetAxes1(AxesLEPS a)
        {
            SetAxes(a.MinValue, physicalValueEditorQ1Min, a.AxesType);
            SetAxes(a.MaxValue, physicalValueEditorQ1Max, a.AxesType);
        }

        public void SetAxes2(AxesLEPS a)
        {
            SetAxes(a.MinValue, physicalValueEditorQ2Min, a.AxesType);
            SetAxes(a.MaxValue, physicalValueEditorQ2Max, a.AxesType);

            if (a.AxesType == LEPSAxesType.Distance)
            {
                radioButtonDistanceDistance.Checked = true;
            }
            else
            {
                radioButtonDistanceAngle.Checked = true;
            }
        }

        public double? GetConst()
        {
            if (ConstIsAngle)
            {
                return physicalValueEditorConstValue.GetValue(DimAngle.Radian);
            } 
            return physicalValueEditorConstValue.GetValue(DimLength.Angstrom);
        }

        public void SetConst(double c)
        {
            SetAxes(c, physicalValueEditorConstValue, ConstIsAngle ? LEPSAxesType.Angle : LEPSAxesType.Distance);
        }

        bool ConstIsAngle
        {
            get
            {
                return radioButtonDistanceDistance.Checked;
            }
        }

        private void radioButtonDistanceDistance_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonDistanceDistance.Checked)
            {
                labelQ2.Text = String.Format("{0} ({1})", LangResource.LengthBC, LangResource.Q1);
                labelQConst.Text = LangResource.AngleABC;
                physicalValueEditorQ2Min.DimensionType = physicalValueEditorQ2Max.DimensionType = DimensionType.Lenght;
                physicalValueEditorQ2Min.ViewDimension = physicalValueEditorQ2Max.ViewDimension = DimLength.Angstrom;
                physicalValueEditorQ2Min.SetValue(0.3, DimLength.Angstrom);
                physicalValueEditorQ2Max.SetValue(5.0, DimLength.Angstrom);
                physicalValueEditorConstValue.DimensionType = DimensionType.Angle;
                physicalValueEditorConstValue.ViewDimension = DimAngle.Degree;
                physicalValueEditorConstValue.SetValue(180.0, DimAngle.Degree);
            }
        }

        private void radioButtonDistanceAngle_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonDistanceAngle.Checked)
            {
                labelQ2.Text = String.Format("{0} ({1})", LangResource.AngleABC, LangResource.Q1);
                labelQConst.Text = LangResource.LengthBC;
                physicalValueEditorQ2Min.DimensionType = physicalValueEditorQ2Max.DimensionType = DimensionType.Angle;
                physicalValueEditorQ2Min.ViewDimension = physicalValueEditorQ2Max.ViewDimension = DimAngle.Degree;
                physicalValueEditorQ2Min.SetValue(30.0, DimAngle.Degree);
                physicalValueEditorQ2Max.SetValue(180.0, DimAngle.Degree);
                physicalValueEditorConstValue.DimensionType = DimensionType.Lenght;
                physicalValueEditorConstValue.ViewDimension = DimLength.Angstrom;
                physicalValueEditorConstValue.SetValue(0.81, DimLength.Angstrom);
            }
        }
    }
}
