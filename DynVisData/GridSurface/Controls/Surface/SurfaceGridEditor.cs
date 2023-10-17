using DynVis.Core;
using DynVis.Data.Properties;
using SourceGrid.Cells;

namespace DynVis.Data.GridSurface
{
    internal sealed partial class SurfaceGridEditor : BaseControl
    {
        public SurfaceGridEditor()
        {
            InitializeComponent();
            gridSurface.RowsCount = gridSurface.ColumnsCount = 1;
            gridSurface[0, 0] = new Header(LangResource.Energy);
        }

        const int RowHeaderCount = 1;
        const int ColHeaderCount = 1;

        public void SetCoordinateGrid(double[] Q1, double[] Q2)
        {
            NewHandler newCell = NewCell;

            progressNotifier.BeginProgress();

            CreateHeaders(Q1, Q2);

            for (var q1 = 0; q1 < Q1.Length; q1++)
            {
                for (var q2 = 0; q2 < Q2.Length; q2++)
                {
                    if (gridSurface[q1 + RowHeaderCount, q2 + ColHeaderCount] == null)
                        gridSurface.Invoke(newCell, q1 + RowHeaderCount, q2 + ColHeaderCount, 0.0);
                }

                progressNotifier.ProgressChanged((int)(q1 / (double)Q1.Length * 100));
            }

            progressNotifier.CompliteProgress();

            gridSurface.Invalidate();
            gridSurface.Focus();
        }

        private void CreateHeaders(double[] Q1, double[] Q2)
        {
            NewHandler newHeader = NewHeader;
            SetCountHandler setRowCount = SetRowCount;
            SetCountHandler setColCount = SetColCount;

            gridSurface.Invoke(setRowCount, Q1.Length + RowHeaderCount);
            gridSurface.Invoke(setColCount, Q2.Length + ColHeaderCount);
            for (var q1 = 0; q1 < Q1.Length; q1++)
            {
                gridSurface.Invoke(newHeader, q1 + RowHeaderCount, 0, Q1[q1]);
            }
            for (var q2 = 0; q2 < Q2.Length; q2++)
            {
                gridSurface.Invoke(newHeader, 0, q2 + ColHeaderCount, Q2[q2]);
            }

        }

        private delegate void NewHandler(int row, int col, double value);
        private void NewHeader(int row, int col, double value)
        {
            gridSurface[row, col] = new Header(value);
        }
        private void NewCell(int row, int col, double value)
        {
            gridSurface[row, col] = new Cell(value, typeof(double));
        }
        private delegate void SetCountHandler(int count);
        private void SetRowCount(int count)
        {
            gridSurface.RowsCount = count;
        }
        private void SetColCount(int count)
        {
            gridSurface.ColumnsCount = count;
        }

        private int Q1Count
        {
            get
            {
                return gridSurface.RowsCount - RowHeaderCount;
            }
        }
        private int Q2Count
        {
            get
            {
                return gridSurface.ColumnsCount - ColHeaderCount;
            }
        }

        public double[,] GetSurface()
        {
            var surface = new double[Q1Count, Q2Count];
            for (var q1 = 0; q1 < Q1Count; q1++)
            {
                for (var q2 = 0; q2 < Q2Count; q2++)
                {
                    surface[q1, q2] = (double) gridSurface[q1 + RowHeaderCount, q2 + ColHeaderCount].Value;
                }
            }
            return surface;
        }


        public bool SetSurface(double[,] surface)
        {
            if (surface.GetLength(0) != Q1Count && surface.GetLength(1) != Q2Count)
            {
                return false;
            }

            for (var q1 = 0; q1 < Q1Count; q1++)
            {
                for (var q2 = 0; q2 < Q2Count; q2++)
                {
                    gridSurface[q1 + RowHeaderCount, q2 + ColHeaderCount].Value = surface[q1, q2];
                }
            }
            return true;
        }
    }
}
