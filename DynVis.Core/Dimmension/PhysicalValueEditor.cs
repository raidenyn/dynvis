using System;
using System.ComponentModel;

namespace DynVis.Core
{
    public partial class PhysicalValueEditor : BaseControl
    {
        public PhysicalValueEditor()
        {
            InitializeComponent();

            Dimansion = DimLength.Metr;
            ScaleCoeff = ScaleCoeff.normal;
            ChangeText();
            AutoChangeViewDimension = true;
        }

        [DefaultValue(DimensionType.Lenght)]
        public DimensionType DimensionType
        {
            get { return dimensionSelector.DimensionType; }
            set
            {
                dimensionSelector.DimensionType = value;
                ChangeText();
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Dimension Dimansion
        { get;set;
        }

        private ScaleCoeff _ScaleCoeff;

        [DefaultValue(ScaleCoeff.normal)]
        public ScaleCoeff ScaleCoeff
        { get
        {
            return _ScaleCoeff;
        }
            set
            {
                _ScaleCoeff = value;
            }
        }

        [DefaultValue(false)]
        public bool ReadOnlyValue
        {
            get { return decimalEditor.ReadOnly; }
            set
            {
                decimalEditor.ReadOnly = value;
            }
        }

        public double Value
        {
            get { return GetValue(Dimansion, ScaleCoeff); }
            set
            {
                ViewValue = dimensionSelector.ConvertTo(ViewDimension, ViewScaleCoeff, Dimansion, ScaleCoeff, value);
            }
        }

        public double GetValue(Dimension dimansion, ScaleCoeff scaleCoeff)
        {
            return dimensionSelector.ConvertTo(dimansion, scaleCoeff, ViewDimension, ViewScaleCoeff, ViewValue);
        }

        public double GetValue(Dimension dimansion)
        {
            return GetValue(dimansion, ScaleCoeff.normal);
        }

        public double GetValue(ScaleDimension scaleDimansion)
        {
            return GetValue(scaleDimansion.Dimension, scaleDimansion.ScaleCoeff);
        }

        [DefaultValue(true)]
        public bool AutoChangeViewDimension{get;set;}

        public void SetValue( double v, Dimension dimansion, ScaleCoeff scaleCoeff)
        {
            if (AutoChangeViewDimension)
            {
                ViewDimension = dimansion;
                ViewScaleCoeff = scaleCoeff;
                ViewValue = v;
            }
            else
            {
                ViewValue = dimensionSelector.ConvertTo(ViewDimension, ViewScaleCoeff, dimansion, scaleCoeff, v);
            }
            Dimansion = dimansion;
            ScaleCoeff = scaleCoeff;
        }

        public void SetValue( double v, Dimension dimansion)
        {
            SetValue(v, dimansion, ScaleCoeff.normal);
        }

        public void SetValue(double v, ScaleDimension scaleDimansion)
        {
            SetValue(v, scaleDimansion.Dimension, scaleDimansion.ScaleCoeff);
        }

        public void SetValue(double v, Dimension dimansion, ScaleCoeff scaleCoeff, Dimension viewDimansion)
        {
            ViewDimension = viewDimansion;
            ViewValue = dimensionSelector.ConvertTo(ViewDimension, ViewScaleCoeff, dimansion, scaleCoeff, v);
            Dimansion = dimansion;
            ScaleCoeff = scaleCoeff;
        }

        public void SetValue(double v, Dimension dimansion, Dimension viewDimansion)
        {
            SetValue(v, dimansion, ScaleCoeff.normal, viewDimansion);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Dimension ViewDimension
        {
            get
            {
                return dimensionSelector.Dimension;
            }
            set { dimensionSelector.Dimension = value; }
        }

        [DefaultValue(ScaleCoeff.normal)]
        public ScaleCoeff ViewScaleCoeff
        {
            get
            {
                return dimensionSelector.ScaleCoeff;
            }
            set { dimensionSelector.ScaleCoeff = value; }
        }

        [DefaultValue(0)]
        public double ViewValue
        {
            get { return dimensionSelector.Value; }
            private set { dimensionSelector.Value = value;
                ChangeText(); }
        }

        private void ChangeText()
        {
            decimalEditor.Value = ViewValue;
        }

        private void dimensionSelector_OnDimensionChanged(object sender, EventArgs e)
        {
            ChangeText();
        }


        private void textBoxEditValue_TextChanged(object sender, EventArgs e)
        {
            if (decimalEditor.IsValidValue)
            {
                dimensionSelector.Value = decimalEditor.Value;
                RiseValueChanged();
            }
        }

        public event EventHandler ValueChanged;
        private void RiseValueChanged()
        {
            if (ValueChanged != null) ValueChanged(this, new EventArgs());
        }
    }
}
