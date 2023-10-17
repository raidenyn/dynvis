using System;
using System.ComponentModel;
using DynVis.Core.Properties;

namespace DynVis.Core.Dimmension
{
    public partial class ScaleDimensionSelector : BaseControl
    {
        public ScaleDimensionSelector()
        {
            InitializeComponent();

            GenerateAllDimensionTypeList();
        }

        private void TypeDimensionSelector_Load(object sender, EventArgs e)
        {
            
        }

        private void GenerateAllDimensionTypeList()
        {
            foreach (var dimType in Dimensions.DimensionsList)
            {
                comboBoxDimensionType.Items.Add(dimType);
            }
            DimensionType = DimensionType.Lenght;
        }

        public void SetAllowDimensions(DimensionType[] allowDimensionTypes)
        {
            if (allowDimensionTypes == null)
            {
                throw new ArgumentNullException("allowDimensionTypes");
            }

            if (allowDimensionTypes.Length == 0)
            {
                throw new ArgumentException(LangDimension.ArrayLengthCanntBeNull);
            }

            comboBoxDimensionType.Items.Clear();

            AllowDimensionTypes = new DimensionType[allowDimensionTypes.Length];
            var i = 0;
            foreach (var dimType in allowDimensionTypes)
            {
                comboBoxDimensionType.Items.Add(Dimensions.GetDimensions(dimType));
                AllowDimensionTypes[i++] = dimType;
            }

            DimensionType = allowDimensionTypes[0];
        }


        private void comboBoxDimensionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dimType = comboBoxDimensionType.SelectedItem as Dimensions;

            if (dimType != null)
            {
                dimensionSelector.DimensionType = dimType.Type;
                RiseDimensionTypeChanged();
            }
           
        }

        [DefaultValue(DimensionType.Lenght)]
        public DimensionType DimensionType
        {
            get { return ((Dimensions) comboBoxDimensionType.SelectedItem).Type; }
            set
            {
                comboBoxDimensionType.SelectedItem = Dimensions.GetDimensions(value);
                RiseDimensionTypeChanged();
            }
        }

        public ScaleDimension GetScaleDimension()
        {
            return dimensionSelector.GetScaleDimension();
        }

        public void SetScaleDimension(ScaleDimension sd)
        {
            dimensionSelector.SetScaleDimension(sd);
        }

        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden)]
        public Dimension Dimension
        {
            get
            {
                return dimensionSelector.Dimension;
            }
            set
            {
                dimensionSelector.Dimension = value;
                RiseScaleDimensionChanged();
            }
        }

        [DefaultValue(ScaleCoeff.normal)]
        public ScaleCoeff ScaleCoeff
        {
            get
            {
                return dimensionSelector.ScaleCoeff;
            }
            set
            {
                dimensionSelector.ScaleCoeff = value;
                RiseScaleDimensionChanged();
            }
        }

        public event EventHandler DimensionTypeChanged;
        public event EventHandler ScaleDimensionChanged;

        private void RiseDimensionTypeChanged()
        {
            if (DimensionTypeChanged != null) DimensionTypeChanged(this, new EventArgs());
        }

        private void RiseScaleDimensionChanged()
        {
            if (ScaleDimensionChanged != null) ScaleDimensionChanged(this, new EventArgs());
        }

        private void dimensionSelector_OnDimensionChanged(object sender, EventArgs e)
        {
            RiseScaleDimensionChanged();
        }

        private void dimensionSelector_OnDimensionTypeChanged(object sender, EventArgs e)
        {
            DimensionType = dimensionSelector.DimensionType;
        }


        public DimensionType[] AllowDimensionTypes
        { get; private set;
        }
    }
}
