using System;
using System.ComponentModel;

namespace DynVis.Core.CurrentPointInformation
{
    /// <summary>
    /// Контрол описания текущей точки поверхности
    /// </summary>
    public partial class CurrentPointInformation : BaseControl
    {
        public CurrentPointInformation()
        {
            InitializeComponent();

            //Скрываем дополнительную область
            VisibleAdditional = false;
        }

        private ISurface3D _Surface;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ISurface3D Surface
        {
            get { return _Surface; }
            set
            {
                if (Surface != null)
                {
                    Surface.CurrentPointChanged -= Surface_CurrentPointChanged;
                }
                _Surface = value;
                if (Surface != null)
                {
                    Surface.CurrentPointChanged += Surface_CurrentPointChanged;

                    physicalValueEditor1.DimensionType = Surface.Axes1.ScaleDimension.Dimension.Type;
                    physicalValueEditor1.Dimansion = Surface.Axes1.ScaleDimension.Dimension;
                    physicalValueEditor1.ScaleCoeff = Surface.Axes1.ScaleDimension.ScaleCoeff;
                    physicalValueEditor1.ViewDimension = Surface.Axes1.ScaleDimension.Dimension;
                    physicalValueEditor1.ViewScaleCoeff = Surface.Axes1.ScaleDimension.ScaleCoeff;

                    physicalValueEditor2.DimensionType = Surface.Axes2.ScaleDimension.Dimension.Type;
                    physicalValueEditor2.Dimansion = Surface.Axes2.ScaleDimension.Dimension;
                    physicalValueEditor2.ScaleCoeff = Surface.Axes2.ScaleDimension.ScaleCoeff;
                    physicalValueEditor2.ViewDimension = Surface.Axes2.ScaleDimension.Dimension;
                    physicalValueEditor2.ViewScaleCoeff = Surface.Axes2.ScaleDimension.ScaleCoeff;

                    physicalValueEditorEnergy.Dimansion = Surface.EnergyDimension.Dimension;
                    physicalValueEditorEnergy.ScaleCoeff = Surface.EnergyDimension.ScaleCoeff;
                    physicalValueEditorEnergy.ViewDimension = Surface.EnergyDimension.Dimension;
                    physicalValueEditorEnergy.ViewScaleCoeff = Surface.EnergyDimension.ScaleCoeff;
                }
            }
        }

        void Surface_CurrentPointChanged(object sender, EventArgs e)
        {
            UpdateView();
        }

        public void UpdateView()
        {
            EnableSetCurrentPoint = false;
            physicalValueEditor1.Value = Surface.CurrentPoint.X;
            physicalValueEditor2.Value = Surface.CurrentPoint.Y;

            physicalValueEditorEnergy.Value = Surface.CurrentE;
            textBoxQ1dev.Text = Surface.CurrentdEdq1.ToString();
            textBoxQ2Dev.Text = Surface.CurrentdEdq2.ToString();
            textBoxQ1Dev2.Text = Surface.Currentd2Edq1dq1.ToString();
            textBoxQ2Dev2.Text = Surface.Currentd2Edq2dq2.ToString();
            textBoxQ1Q2Dev2.Text = Surface.Currentd2Edq1dq2.ToString();

            if (VisibleAdditional) CalcAddition();

            EnableSetCurrentPoint = true;
        }

        /// <summary>
        /// Включает/Выключает обновление текущей точки по изменению текста контролов.
        /// Нужно для предотварщения зацыкливания
        /// </summary>
        private bool EnableSetCurrentPoint = true;

        private void physicalValueEditor_ValueChanged(object sender, EventArgs e)
        {
            if (EnableSetCurrentPoint) Surface.SetPoint(physicalValueEditor1.Value, physicalValueEditor2.Value);
        }

        #region Addition Information


        private bool _VisibleAdditional = true;
        
        [DefaultValue(false)]
        [Description("Показывать дополнительные параметры")]
        [Category("Visible")]
        public bool VisibleAdditional
        {
            get
            {
                return _VisibleAdditional;
            }
            set
            {
                if (_VisibleAdditional != value)
                {
                    _VisibleAdditional = value;
                    SetVisibleAdditional(VisibleAdditional);
                }
            }
        }

        /// <summary>
        /// Включае/Выключает дополнительные параметры
        /// </summary>
        /// <param name="visible"></param>
        private void SetVisibleAdditional(bool visible)
        {
            groupBoxEigensystem.Visible = visible;

            const int Inc = 20;

            if (visible)
            {
                Height += (groupBoxEigensystem.Height + Inc);
                buttonAdditionInformation.Text = "^";

                CalcAddition();
            } 
            else
            {
                Height -= (groupBoxEigensystem.Height + Inc);
                buttonAdditionInformation.Text = "v";
            }
        }

        private const string DecimalFormat = "F9";

        /// <summary>
        /// Вычисляет дополнительные параметры
        /// </summary>
        private void CalcAddition()
        {
            double[] vals;
            double[,] vecs;
            Surface.GetHessEigensystem(Surface.CurrentPoint.X, Surface.CurrentPoint.Y, out vals, out vecs);

            textBoxE1.Text = vals[0].ToString();
            textBoxE2.Text = vals[1].ToString();

            textBoxE1V1.Text = vecs[0, 0].ToString(DecimalFormat);
            textBoxE1V2.Text = vecs[0, 1].ToString(DecimalFormat);
            textBoxE2V1.Text = vecs[1, 0].ToString(DecimalFormat);
            textBoxE2V2.Text = vecs[1, 1].ToString(DecimalFormat);
        }

        private void buttonAdditionInformation_Click(object sender, EventArgs e)
        {
            VisibleAdditional = !VisibleAdditional;
        }

        #endregion
    }
}
