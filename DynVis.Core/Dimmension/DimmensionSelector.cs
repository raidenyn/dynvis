using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DynVis.Core
{
    public partial class DimensionSelector : BaseControl
    {
        public DimensionSelector()
        {
            InitializeComponent();

            ScaleFactorItem = GetScaleFactorItem();
            Changed();
            UpdateViewDimensionList();
        }

        private void linkLabelDimension_Resize(object sender, EventArgs e)
        {
            Width = linkLabelDimension.Width + linkLabelDimension.Left * 2;
        }

        private DimensionType _DimensionType = DimensionType.Lenght;
        [DefaultValue(DimensionType.Lenght)]
        public DimensionType DimensionType
        {
            get { return _DimensionType; }
            set
            {
                if (_DimensionType != value)
                {
                    _DimensionType = value;
                    UpdateViewDimensionList();
                    Dimension = Dimensions.GetDimensions(DimensionType).StandartDimension;
                    RiseDimensionTypeChanged();
                }
            }
        }

        public Dimension Dimension
        {
            get
            {
                return DimensionVarlible.CurrentDimension;
            }
            set
            {
                DimensionVarlible.CurrentDimension = value;
                Changed();
                DimensionVarlible.CurrentDimension = value;
            }
        }

        [DefaultValue(ScaleCoeff.normal)]
        public ScaleCoeff ScaleCoeff
        {
            get
            {
                return DimensionVarlible.CurrentScaleCoeff;
            }
            set
            {
                DimensionVarlible.CurrentScaleCoeff = value;
                Changed();
            }
        }

        private readonly DimensionVarlible DimensionVarlible = new DimensionVarlible(DimLength.Metr, ScaleCoeff.normal);

        [DefaultValue(0)]
        public double Value
        {
            get { return DimensionVarlible.Value; }
            set { DimensionVarlible.Value = value; }
        }

        private void Changed()
        {
            DimensionType = Dimensions.GetDimensionType(DimensionVarlible.CurrentDimension);

            linkLabelDimension.Text = DimensionVarlible.GetFullShortName();
            toolTipFullDimensionName.SetToolTip(linkLabelDimension, DimensionVarlible.GetFullName());
            RiseDimensionChanged();
        }

        private void UpdateViewDimensionList()
        {
            ViewDimensionList.Items.Clear();

            var DimType = Dimensions.GetDimensions(DimensionType);

            foreach (var Dim in DimType.DimensionTypes)
            {
                var item = new ToolStripMenuItem(Dim.Name, null, OnClickViewDimensionListItem) {Tag = Dim};
                ViewDimensionList.Items.Add(item);
            }


            if (DimType.Type != DimensionType.None)
            {
                ViewDimensionList.Items.Add(new ToolStripSeparator());
                ViewDimensionList.Items.Add(ScaleFactorItem);
            } 
            else
            {
                ScaleCoeff = ScaleCoeff.normal;
            }

        }

        private void OnClickViewDimensionListItem(object sender, EventArgs e)
        {
            var dim = sender is ToolStripMenuItem ? ((ToolStripMenuItem)sender).Tag as Dimension : null;
            if (dim != null) Dimension = dim;
        }

        private void OnClickScaleItem(object sender, EventArgs e)
        {
            var coeff = sender is ToolStripMenuItem ? ((ToolStripMenuItem)sender).Tag as DimensionCoeff : null;
            if (coeff != null) ScaleCoeff = coeff.ScaleCoeff;
        }

        private const string ScaleItemName = "Масштаб";

        private readonly ToolStripMenuItem ScaleFactorItem;
        private ToolStripMenuItem GetScaleFactorItem()
        {
            var ScaleItem = new ToolStripMenuItem(ScaleItemName);

            foreach (var Coeff in DimensionCoeff.Coeffs)
            {
                var item = new ToolStripMenuItem(Coeff.Name, null, OnClickScaleItem) {Tag = Coeff};
                ScaleItem.DropDownItems.Add(item);
            }
            return ScaleItem;
        }

        private void linkLabelDimension_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ViewDimensionList.Show(this, Width, 0);
        }

        private void DimensionSelector_MouseHover(object sender, EventArgs e)
        {
        }

        public event EventHandler OnDimensionChanged;
        private void RiseDimensionChanged()
        {
            if (OnDimensionChanged != null) OnDimensionChanged(this, new EventArgs());
        }
        public event EventHandler OnDimensionTypeChanged;
        private void RiseDimensionTypeChanged()
        {
            if (OnDimensionTypeChanged != null) OnDimensionTypeChanged(this, new EventArgs());
        }

        public double ConvertTo(Dimension destinationDimension, ScaleCoeff destinationScaleCoeff, Dimension currentDimension, ScaleCoeff currentScaleCoeff, double v)
        {
            return DimensionVarlible.ConvertTo(destinationDimension, destinationScaleCoeff, currentDimension,
                                               currentScaleCoeff, v);
        }

        public ScaleDimension GetScaleDimension()
        {
            return DimensionVarlible.GetScaleDimension();
        }

        public void SetScaleDimension(ScaleDimension sd)
        {
            DimensionVarlible.SetScaleDimension(sd);
            Changed();
        }
    }
    
}
