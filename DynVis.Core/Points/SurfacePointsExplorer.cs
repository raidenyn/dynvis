using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevAge.Drawing;
using DynVis.Core.IO;
using DynVis.Core.Properties;
using DynVis.Math;
using SourceGrid;
using SourceGrid.Cells;
using SourceGrid.Selection;

namespace DynVis.Core.Points
{
    public partial class SurfacePointsExplorer : BaseControl
    {
        public SurfacePointsExplorer()
        {
            InitializeComponent();

            InitializeGrid();
        }

        

        private const int FixedColCount = 1;
        private const int FixedRowCount = 1;
        private const int HeaderRow = 0;
        private const int ColCount = 5;
        private const int NumberCol = 0;
        private const int NameCol = 1;
        private const int Q1Col = 2;
        private const int Q2Col = 3;
        private const int AdditionalInformationCol = 4;

        private void InitializeGrid()
        {
            gridPoints.ColumnsCount = ColCount;
            gridPoints.RowsCount = FixedRowCount;

            gridPoints.FixedColumns = FixedColCount;
            gridPoints.FixedRows = FixedRowCount;

            gridPoints[HeaderRow, NumberCol] = Header(LangGeneral.Number);
            gridPoints[HeaderRow, NameCol] = Header(LangGeneral.Description);
            gridPoints[HeaderRow, Q1Col] = Header(LangGeneral.Q1);
            gridPoints[HeaderRow, Q2Col] = Header(LangGeneral.Q2);
            gridPoints[HeaderRow, AdditionalInformationCol] = Header(LangGeneral.Addition);

            gridPoints.Selection.SelectionChanged += Selection_SelectionChanged;
        }

        void Selection_SelectionChanged(object sender, RangeRegionChangedEventArgs e)
        {
            if (SurfacePoints != null)
            {
                SurfacePoints.CurrentItem = CurrentPoint;
            }
        }

        private static Header Header(string text)
        {
            return new Header(text)
                       {
                           View = {TextAlignment = ContentAlignment.MiddleCenter}
                       };
        }

        private static Header Header(string text, ISurfacePoint point)
        {
            return new Header(text)
            {
                View = { TextAlignment = ContentAlignment.MiddleCenter },
                Tag = point
            };
        }

        private static Cell Cell(object value)
        {
            return new Cell(value, value.GetType()) { Editor = { EnableEdit = false } };
        }

        private ISurfacePoints _SurfacePoints;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ISurfacePoints SurfacePoints
        {
            get { return _SurfacePoints; }
            set
            {
                _SurfacePoints = value;
                UpdateView();
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ISurface3D Surface
        {
            get; set;
        }

        private void UpdateView()
        {
            gridPoints.RowsCount = FixedRowCount;
            if (SurfacePoints != null)
            {
                gridPoints.RowsCount += SurfacePoints.Count;

                var counter = FixedRowCount;
                foreach (var point in SurfacePoints)
                {
                    gridPoints[counter, NumberCol] = Header(counter.ToString(), point);
                    gridPoints[counter, NameCol] = Cell(point.Name);
                    gridPoints[counter, Q1Col] = Cell(point.Point.X.ToString("F8"));
                    gridPoints[counter, Q2Col] = Cell(point.Point.Y.ToString("F8"));
                    gridPoints[counter, AdditionalInformationCol] = Cell(point.AdditionalInformation);

                    counter++;
                }
            } 
        }

        private IEnumerable<ISurfacePoint> GetSelectionPoints()
        {
            var rows = gridPoints.Selection.GetSelectionRegion().GetRowsIndex();

            var result = from row in rows
                         where gridPoints[row, NumberCol].Tag is ISurfacePoint
                         select gridPoints[row, NumberCol].Tag as ISurfacePoint;

            return result;
        }

        private ISurfacePoint CurrentPoint
        {
            get
            {
                return GetSelectionPoints().FirstOrDefault();
            }
        }

        private void DeleteSelection()
        {
            var selectedPoints = GetSelectionPoints();
            foreach (var point in selectedPoints)
            {
                SurfacePoints.Remove(point);
            }
            UpdateView();
        }

        private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            DeleteSelection();
        }

        private void gridPoints_DoubleClick(object sender, EventArgs e)
        {
            var currPoint = CurrentPoint;

            if (currPoint != null)
            {
                var formPointEditor = new FormPointEditor { SurfacePoint = currPoint };

                if (formPointEditor.ShowDialog(this) == DialogResult.OK)
                {
                    UpdateView();
                }
            }
        }

        private void ToolStripMenuItemAdd_Click(object sender, EventArgs e)
        {
            var surfacePoint = new SurfacePoint();

            if (Surface != null)
            {
                surfacePoint.PointD = new PointD(Surface.CurrentPoint);
            }

            var formPointEditor = new FormPointEditor { SurfacePoint = surfacePoint };

            if (formPointEditor.ShowDialog(this) == DialogResult.OK)
            {
                SurfacePoints.Add(surfacePoint);

                UpdateView();
            }
        }

        private void ToolStripMenuItemOpenFile_Click(object sender, EventArgs e)
        {
            IOFileDialog.OpenFromFile(SurfacePoints);
            UpdateView();
        }
    }
}
