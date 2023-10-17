using System;
using System.Windows.Forms;
using DynVis.Core;
using DynVis.Data.Properties;
using SourceGrid.Cells;

namespace DynVis.Data.GridSurface
{
    internal partial class ElementListEditor : BaseControl
    {
        public ElementListEditor()
        {
            InitializeComponent();

            gridElements.ColumnsCount = ColCount;
            SetRowsCount((int)(numericUpDownAtomCount.Value) + HeaderRowCount);

            gridElements[0, 0] = new Header(LangResource.NumberSymbol);
            gridElements[0, 1] = new Header(LangResource.Element);
        }

        private const int ColCount = 2;
        private const int HeaderRowCount = 1;
        private const int NumberColumnPosition = 0;
        private const int ValueColumnPosition = 1;

        private void SetRowsCount(int rowsCount)
        {
            if (gridElements.RowsCount > rowsCount)
            {
                gridElements.RowsCount = rowsCount;
            }
            if (gridElements.RowsCount < rowsCount)
            {
                var oldRowCount = gridElements.RowsCount;
                gridElements.RowsCount = rowsCount;
                for (var i = oldRowCount; i < rowsCount; i++)
                {
                    gridElements[i, NumberColumnPosition] = new Header(i);
                    gridElements[i, ValueColumnPosition] = new Cell(1, typeof(int));
                }
            }
        }

        private void numericUpDownAtomCount_ValueChanged(object sender, EventArgs e)
        {
            SetRowsCount((int)(numericUpDownAtomCount.Value) + HeaderRowCount);
        }

        public int[] GetElements()
        {
            var Result = new int[gridElements.RowsCount - HeaderRowCount];
            for (var i = 1; i < gridElements.RowsCount; i++)
            {
                Result[i - 1] = (int)gridElements[i, ValueColumnPosition].Value;
            }
            return Result;
        }

        public bool SetElements(int[] elementsList)
        {
            if (elementsList.Length < numericUpDownAtomCount.Minimum || elementsList.Length > numericUpDownAtomCount.Maximum)
            {
                return false;
            }
            
            numericUpDownAtomCount.Value = elementsList.Length;

            for (var i = HeaderRowCount; i < gridElements.RowsCount; i++)
            {
                gridElements[i, ValueColumnPosition].Value = elementsList[i - HeaderRowCount];
            }

            return true;
        }

        private void toolStripMenuItemPastFromText_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                GeometryInterfaceFunction.Past1FirstPositionFromText(gridElements, Clipboard.GetText(), 1, 1);
            }
        }
    }
}
