using System;
using System.ComponentModel;
using System.Windows.Forms;
using DynVis.Core;
using DynVis.Data.Geometry;
using DynVis.Data.Properties;
using SourceGrid;
using SourceGrid.Cells;
using SourceGrid.Cells.Models;

namespace DynVis.Data.GridSurface
{
    internal partial class GeometryEditor : BaseControl
    {
        public GeometryEditor()
        {
            InitializeComponent();

            gridCoordinate.RowsCount = CoordHeaderRowCount;
            gridCoordinate.ColumnsCount = CoordColCount;
            gridCoordinate[CoordHeaderRowPos, CoordColQ1Position] = new Header(LangResource.Q1);
            gridCoordinate[CoordHeaderRowPos, CoordColQ2Position] = new Header(LangResource.Q2);

            gridStructure.RowsCount = StructHeaderRowCount;
            gridStructure.ColumnsCount = StructColCount;
            gridStructure[StructHeaderRowPos, StructColElementPosition] = new Header(LangResource.Element);
            gridStructure[StructHeaderRowPos, StructColXPosition] = new Header(LangResource.X);
            gridStructure[StructHeaderRowPos, StructColYPosition] = new Header(LangResource.Y);
            gridStructure[StructHeaderRowPos, StructColZPosition] = new Header(LangResource.Z);

            gridCoordinate.Selection.SelectionChanged += Selection_SelectionChanged;
            gridCoordinate.Selection.EnableMultiSelection = false;
        }

        void Selection_SelectionChanged(object sender, RangeRegionChangedEventArgs e)
        {
            gridStructure.Invalidate();
        }

        private const int CoordHeaderRowCount = 1;
        private const int CoordHeaderRowPos = 0;
        private const int CoordColCount = 2;
        private const int CoordColQ1Position = 0;
        private const int CoordColQ2Position = 1;

        private const int StructHeaderRowCount = 1;
        private const int StructHeaderRowPos = 0;
        private const int StructColCount = 4;
        private const int StructColElementPosition = 0;
        private const int StructColXPosition = 1;
        private const int StructColYPosition = 2;
        private const int StructColZPosition = 3;

        private void CreateCoordTable()
        {
            gridCoordinate.RowsCount = CoordHeaderRowCount + Q1.Length * Q2.Length;
            CoordValue.Owner = this;
            var row = CoordHeaderRowCount;
            for (var q1=0;q1<Q1.Length;q1++)
            {
                for (var q2 = 0; q2 < Q2.Length; q2++)
                {
                    var CellQ1 = new Cell();
                    var CellQ2 = new Cell();

                    CellQ1.Model.ValueModel = CellQ2.Model.ValueModel = CoordValue;

                    CellQ1.Tag = q1;
                    CellQ2.Tag = q2;

                    gridCoordinate[row, CoordColQ1Position] = CellQ1;
                    gridCoordinate[row, CoordColQ2Position] = CellQ2;

                    row++;
                }
            }

            
        }

        private void CreateStructTable()
        {
            gridStructure.RowsCount = CoordHeaderRowCount + Elements.Length;

            EValue.Owner = XValue.Owner = YValue.Owner = ZValue.Owner = this;

            for (var e = 0; e < Elements.Length; e++)
            {
                var CellE = new Header();
                var CellX = new Cell(0.0, typeof (double));
                var CellY = new Cell(0.0, typeof(double));
                var CellZ = new Cell(0.0, typeof(double));

                CellE.Model.ValueModel = EValue;
                CellX.Model.ValueModel = XValue;
                CellY.Model.ValueModel = YValue;
                CellZ.Model.ValueModel = ZValue;

                gridStructure[e + StructHeaderRowCount, StructColElementPosition] = CellE;
                gridStructure[e + StructHeaderRowCount, StructColXPosition] = CellX;
                gridStructure[e + StructHeaderRowCount, StructColYPosition] = CellY;
                gridStructure[e + StructHeaderRowCount, StructColZPosition] = CellZ;
            }
        }

        private void CreateStructures()
        {
            if (Structures == null)
            {
                Structures = new Structure[Q1.Length, Q2.Length];
                for (var q1 = 0; q1 < Q1.Length; q1++)
                {
                    for (var q2 = 0; q2 < Q2.Length; q2++)
                    {
                        Structures[q1, q2] = new Structure(Elements.Length);
                    }
                }
            } else
            {
                var oldStructures = Structures;
                Structures = new Structure[Q1.Length, Q2.Length];
                for (var q1 = 0; q1 < Q1.Length; q1++)
                {
                    for (var q2 = 0; q2 < Q2.Length; q2++)
                    {
                        if (q1 < oldStructures.GetLength(0) && q2 < oldStructures.GetLength(1))
                        {
                            Structures[q1, q2] = new Structure(oldStructures[q1,q2], Elements.Length);
                        } 
                        else
                        {
                            Structures[q1, q2] = new Structure(Elements.Length);
                        }
                    }
                }
            }

        }

        private int GetQ1Index(int rowIndex)
        {
            return (int)gridCoordinate[rowIndex, CoordColQ1Position].Tag;
        }
        private int GetQ2Index(int rowIndex)
        {
            return (int)gridCoordinate[rowIndex, CoordColQ2Position].Tag;
        }

        private int CurrentQ1Index
        {
            get
            {
               var SelReg = gridCoordinate.Selection.GetSelectionRegion();
               return GetQ1Index(SelReg.GetRowsIndex()[0]);
            }
        }
        private int CurrentQ2Index
        {
            get
            {
                var SelReg = gridCoordinate.Selection.GetSelectionRegion();
                return GetQ2Index(SelReg.GetRowsIndex()[0]);
            }
        }

        public Structure[,] Structures { get; private set; }
        public double[] Q1 { get; private set; }
        public double[] Q2 { get; private set; } 
        
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden)]
        public Structure CurrentStructure
        {
            get
            {
                return Structures[CurrentQ1Index, CurrentQ2Index];
            }
        }

        private int[] Elements;

        public void SetGrid(double[] q1, double[] q2)
        {
            Q1 = q1;
            Q2 = q2;
            
            CreateCoordTable();

            gridCoordinate.Selection.SelectRow(CoordHeaderRowPos + 1, true);
        }

        public void SetElements(int[] elements)
        {
            Elements = elements;
            CreateStructures();
            CreateStructTable();
        }

        public bool SetStructures(int[] elements, Structure[,] structures)
        {
            if (structures.GetLength(0) == Q1.Length && structures.GetLength(1) == Q2.Length && structures[0, 0].Count == elements.Length)
            {
                Elements = elements;
                Structures = structures;
                CreateStructTable();
                return true;
            }
            return false;
        }

        private readonly CoordValueProvider CoordValue = new CoordValueProvider();
        private readonly ElementProvider EValue = new ElementProvider();
        private readonly XProvider XValue = new XProvider();
        private readonly YProvider YValue = new YProvider();
        private readonly ZProvider ZValue = new ZProvider();
        
        class CoordValueProvider: IValueModel
        {
            public double[] Q1
            {
                get { return Owner.Q1; }
            }
            public double[] Q2
            {
                get { return Owner.Q2; }
            }
            
            public object GetValue(CellContext cellContext)
            {
                if (cellContext.Position.Column == CoordColQ1Position)
                {
                    return Q1[Owner.GetQ1Index(cellContext.Position.Row)];
                }
                if (cellContext.Position.Column == CoordColQ2Position)
                {
                    return Q2[Owner.GetQ2Index(cellContext.Position.Row)];
                }
                throw new Exception();
            }

            public void SetValue(CellContext cellContext, object p_Value)
            {

            }

            public GeometryEditor Owner;
        }
        class ElementProvider : IValueModel
        {
            public int[] Elements
            {
                get { return Owner.Elements; }
            }

            public object GetValue(CellContext cellContext)
            {
                return Elements[cellContext.Position.Row - StructHeaderRowCount];
            }

            public void SetValue(CellContext cellContext, object p_Value)
            {
               
            }

            public GeometryEditor Owner;
        }
        class XProvider : IValueModel
        {
            public Structure Structure
            {
                get { return Owner.CurrentStructure; }
            }

            public object GetValue(CellContext cellContext)
            {
                return Structure[cellContext.Position.Row - StructHeaderRowCount].X;
            }

            public void SetValue(CellContext cellContext, object p_Value)
            {
                Structure[cellContext.Position.Row - StructHeaderRowCount].X = (double)p_Value;
            }

            public GeometryEditor Owner;
        }
        class YProvider : IValueModel
        {
            public Structure Structure
            {
                get { return Owner.CurrentStructure; }
            }

            public object GetValue(CellContext cellContext)
            {
                return Structure[cellContext.Position.Row - StructHeaderRowCount].Y;
            }

            public void SetValue(CellContext cellContext, object p_Value)
            {
                Structure[cellContext.Position.Row - StructHeaderRowCount].Y = (double)p_Value;
            }

            public GeometryEditor Owner;
        }
        class ZProvider : IValueModel
        {
            public Structure Structure
            {
                get { return Owner.CurrentStructure; }
            }

            public object GetValue(CellContext cellContext)
            {
                return Structure[cellContext.Position.Row - StructHeaderRowCount].Z;
            }

            public void SetValue(CellContext cellContext, object p_Value)
            {
                Structure[cellContext.Position.Row - StructHeaderRowCount].Z = (double)p_Value;
            }

            public GeometryEditor Owner;
        }

        private void toolStripMenuItemPastFromText_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                GeometryInterfaceFunction.Past3LastPositionFromText(gridStructure, Clipboard.GetText(), 1, 1);
                gridStructure.Invalidate();
            }
        }

        private void buttonSetStandartOrientation_Click(object sender, EventArgs e)
        {
            var fotm = new FormSetStandartOrientation {Elements = Elements,Structure = CurrentStructure};
            if (fotm.ShowDialog(this) == DialogResult.OK)
            {
                GeometryTransformationFunctions.SetStandartOrientation(Structures, fotm.Ceneter, fotm.DirectZ, fotm.PlaneXZ);
                Refresh();
            }
            
        }
    }
}
