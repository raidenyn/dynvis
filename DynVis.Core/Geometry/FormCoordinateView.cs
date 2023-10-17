using System;
using System.Windows.Forms;
using DynVis.Core;
using DynVis.Core.Geometry.Extension;
using DynVis.Core.IO;
using DynVis.Core.Properties;
using SourceGrid;
using SourceGrid.Cells;
using SourceGrid.Cells.Models;

namespace DynVis.Geometry
{
    public partial class FormCoordinateView : Form
    {
        public FormCoordinateView()
        {
            InitializeComponent();

            InitGrid();
        }

        private static readonly FileFilter CoordinateTextFileFilter = new FileFilter(LangGeneral.CoordFile, "*.xyz");

        private const int HeaderRow = 0;

        private const int ColumnsCount = 5;
        private const int NumberPosition = 0;
        private const int ElementPosition = 1;
        private const int XPosition = 2;
        private const int YPosition = 3;
        private const int ZPosition = 4;

        private void InitGrid()
        {
            gridCoordinate.ColumnsCount = ColumnsCount;
            gridCoordinate.RowsCount = 1;

            gridCoordinate[HeaderRow, NumberPosition] = CreatHeader(LangGeneral.NumberSymbol);
            gridCoordinate[HeaderRow, ElementPosition] = CreatHeader(LangGeneral.Element);
            gridCoordinate[HeaderRow, XPosition] = CreatHeader(LangGeneral.X);
            gridCoordinate[HeaderRow, YPosition] = CreatHeader(LangGeneral.Y);
            gridCoordinate[HeaderRow, ZPosition] = CreatHeader(LangGeneral.Z);
        }


        private Cell CreatCell()
        {
            return new Cell { Model = { ValueModel = CoordValueModel } };
        }

        private Header CreatHeader(object value)
        {
            return new Header(value);
        }

        private void UpdateViewCoordinate()
        {
            if (AtomStructure != null)
            {
                gridCoordinate.RowsCount = AtomStructure.Count + 1;
                for (var i = 0; i < AtomStructure.Count; i++)
                {
                    var rowNumber = i + 1;
                    gridCoordinate[rowNumber, NumberPosition] = CreatHeader(rowNumber);
                    gridCoordinate[rowNumber, ElementPosition] = CreatCell();
                    gridCoordinate[rowNumber, XPosition] = CreatCell();
                    gridCoordinate[rowNumber, YPosition] = CreatCell();
                    gridCoordinate[rowNumber, ZPosition] = CreatCell();
                }

                gridCoordinate.Columns[NumberPosition].Width = 30;
                gridCoordinate.Columns[ElementPosition].Width = 60;
                gridCoordinate.Columns[XPosition].Width = 130;
                gridCoordinate.Columns[YPosition].Width = 130;
                gridCoordinate.Columns[ZPosition].Width = 130;

                gridCoordinate.AutoStretchColumnsToFitWidth = true;
                gridCoordinate.Width -= 1;
            }
        }

        public IAtomStructure AtomStructure
        {
            get { return CoordValueModel.AtomStructure; }
            set
            {
                CoordValueModel.AtomStructure = value;
                UpdateViewCoordinate();
            }
        }

        private readonly CoordProvider CoordValueModel = new CoordProvider();
        class CoordProvider : IValueModel
        {
            public IAtomStructure AtomStructure;

            public object GetValue(CellContext cellContext)
            {
                switch (cellContext.Position.Column)
                {
                    case ElementPosition:
                        return AtomStructure[cellContext.Position.Row - 1].Element;
                    case XPosition:
                        return AtomStructure[cellContext.Position.Row - 1].Coordinate.X.ToString("F9");
                    case YPosition:
                        return AtomStructure[cellContext.Position.Row - 1].Coordinate.Y.ToString("F9");
                    case ZPosition:
                        return AtomStructure[cellContext.Position.Row - 1].Coordinate.Z.ToString("F9");
                    default: return 0;
                }
            }

            public void SetValue(CellContext cellContext, object p_Value)
            {
                
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Hide();
        }

        

        private void ToolStripMenuItemSaveToFile_Click(object sender, EventArgs e)
        {
            if (AtomStructure != null)
            {
                IOFileDialog.SaveTextFile(CoordinateTextFileFilter, AtomStructure.ToString());
            }
        }

        private void ToolStripMenuItemSaveToClipboard_Click(object sender, EventArgs e)
        {
            if (AtomStructure != null)
            {
                Clipboard.SetText(AtomStructure.ToString());
            }
        }

        private void квадратToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormGeometryExtension {AtomStructure = AtomStructure};
            form.Show(this);
        }
    }
}
