using System;
using System.Windows.Forms;
using DynVis.Core;
using DynVis.Data.Properties;
using SourceGrid.Cells;
using M=System.Math;

namespace DynVis.Data.GridSurface
{
    internal sealed partial class CoordinateEditor : BaseControl
    {
        public CoordinateEditor()
        {
            InitializeComponent();
            Initialize();
        }

        
        private const int HeaderRowCount = 1;
        private const int ColCount = 2;
        private const int ValueColumnPosition = 1;
        private const int NumberColumnPosition = 0;

        private void Initialize()
        {
            gridCoordValue.ColumnsCount = ColCount;
            gridCoordValue.RowsCount = HeaderRowCount;

            var header = new Header { Value = LangResource.Value };
            gridCoordValue[0, ValueColumnPosition] = header;
            header = new Header { Value = LangResource.NumberSymbol };
            gridCoordValue[0, NumberColumnPosition] = header;

            SetRowsCount((int)numericUpDownCount.Value + HeaderRowCount);

            textBoxName.Text = _Axe.Name;
            scaleDimensionSelector.Dimension = DimLength.Angstrom;
            coordTypeEditorMinType.CoordinateType = _Axe.TypeMinValue;
            coordTypeEditorMaxType.CoordinateType = _Axe.TypeMaxValue;
        }

        private void SetRowsCount(int rowsCount)
        {
            if (gridCoordValue.RowsCount > rowsCount)
            {
                gridCoordValue.RowsCount = rowsCount;
            }
            if (gridCoordValue.RowsCount < rowsCount)
            {
                var oldRowCount = gridCoordValue.RowsCount;
                gridCoordValue.RowsCount = rowsCount;
                for (var i = oldRowCount; i < rowsCount; i++)
                {
                    gridCoordValue[i, NumberColumnPosition] = new Header(i);
                    gridCoordValue[i, ValueColumnPosition] = new Cell(IncreaseValue(gridCoordValue[i - 1, ValueColumnPosition].Value), typeof(double));
                }
            }
        }

        public static double IncreaseValue(object prevValue)
        {
            if (!(prevValue is double))
            {
                return 0.0;
            }
            return (double) prevValue + 1.0;
        }

        public int CoordinateCount
        {
            get
            {
                return gridCoordValue.RowsCount - 1;
            }
            
        }

        private Axes _Axe = new Axes();
        
        
        public Axes Axe
        {
            get
            {
                return _Axe;
            }
            set
            {
                _Axe = value;
                UpdateViewAxe();
            }
        }

        public string CheckCoordinateValueGrid()
        {
            var NotIncreasingItem = CheckIncreasing();
            if (NotIncreasingItem > 0)
            {
                return String.Format(LangResource.CoordNotMore, NotIncreasingItem);
            }

            return String.Empty;
        }

        private int CheckIncreasing()
        {
            var prevValue = (double) gridCoordValue[1, ValueColumnPosition].Value;
            for (var i = 2; i < gridCoordValue.RowsCount; i++)
            {
                var currValue = (double)gridCoordValue[i, ValueColumnPosition].Value;
                if (currValue <= prevValue)
                {
                    return i;
                }
                prevValue = currValue;
            }
            return 0;
        }

        public double[] GetCoordinates()
        {
            var Coords = new double[CoordinateCount];

            for (var i = 0; i < CoordinateCount; i++)
            {
                Coords[i] = (double)gridCoordValue[i+1, ValueColumnPosition].Value;
            }
            return Coords;
        }

        public bool SetCoordinate(double[] coords)
        {
            if (coords.Length < numericUpDownCount.Minimum || coords.Length > numericUpDownCount.Maximum) return false;
            numericUpDownCount.Value = coords.Length;
            for (var i = 0; i < CoordinateCount; i++)
            {
                gridCoordValue[i + 1, ValueColumnPosition].Value = coords[i];
            }
            return true;
        }

        public string Title
        {
            get
            {
                return groupBox.Text;
            }
            set
            {
                groupBox.Text = value;
            }
        }

        private void numericUpDownCount_ValueChanged(object sender, EventArgs e)
        {
            SetRowsCount((int)numericUpDownCount.Value + HeaderRowCount);
        }

        private void ToolStripMenuItemRegularGrid_Click(object sender, EventArgs e)
        {
            var form = new FormUntilBeforValues();
            if (form.ShowDialog() == DialogResult.OK)
            {
                var Step = M.Round((form.Befor - form.Until) / (CoordinateCount - 1), 9);
                for (var i=0;i<CoordinateCount;i++)
                {
                    gridCoordValue[i + HeaderRowCount, ValueColumnPosition].Value = M.Round(form.Until + Step * i,9);
                }
            }
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            _Axe.Name = textBoxName.Text;
        }

        private void scaleDimensionSelector_ScaleDimensionChanged(object sender, EventArgs e)
        {
            _Axe.ScaleDimension = scaleDimensionSelector.GetScaleDimension();
        }

        private void coordTypeEditorMinType_CoordTypeChanged(object sender, EventArgs e)
        {
            _Axe.TypeMinValue = coordTypeEditorMinType.CoordinateType;
        }

        private void coordTypeEditorMaxType_CoordTypeChanged(object sender, EventArgs e)
        {
            _Axe.TypeMaxValue = coordTypeEditorMaxType.CoordinateType;
        }

        private void UpdateViewAxe()
        {
            textBoxName.Text = Axe.Name;
            coordTypeEditorMinType.CoordinateType = Axe.TypeMinValue;
            coordTypeEditorMaxType.CoordinateType = Axe.TypeMaxValue;
            scaleDimensionSelector.SetScaleDimension(Axe.ScaleDimension);
        }

        private void groupBox_Enter(object sender, EventArgs e)
        {
            
        }

        private void CoordinateEditor_Load(object sender, EventArgs e)
        {
            scaleDimensionSelector.SetAllowDimensions(new[] { DimensionType.Lenght, DimensionType.Angle });
        }


    }
}
