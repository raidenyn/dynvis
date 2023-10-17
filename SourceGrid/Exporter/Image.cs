#region

using System;
using System.Drawing;
using DevAge.Drawing;
using SourceGrid.Cells;

#endregion

namespace SourceGrid.Exporter
{
    /// <summary>
    /// An utility class to export a grid to a csv delimited format file.
    /// </summary>
    public class Image
    {
        public virtual Bitmap Export(GridVirtual grid, Range rangeToExport)
        {
            Bitmap bitmap = null;

            try
            {
                Size size = grid.RangeToSize(rangeToExport);

                bitmap = new Bitmap(size.Width, size.Height);

                using (Graphics graphic = Graphics.FromImage(bitmap))
                {
                    Export(grid, graphic, rangeToExport, new Point(0, 0));
                }
            }
            catch (Exception)
            {
                if (bitmap != null)
                {
                    bitmap.Dispose();
                    bitmap = null;
                }

                throw;
            }

            return bitmap;
        }

        public virtual void Export(GridVirtual grid, Graphics graphics,
                                   Range rangeToExport, Point destinationLocation)
        {
            if (rangeToExport.IsEmpty())
                return;

            Point cellPoint = destinationLocation;

            Point deltaPoint = destinationLocation;

            using (var graphicsCache = new GraphicsCache(graphics))
            {
                for (int r = rangeToExport.Start.Row; r <= rangeToExport.End.Row; r++)
                {
                    int rowHeight = grid.Rows.GetHeight(r);

                    for (int c = rangeToExport.Start.Column; c <= rangeToExport.End.Column; c++)
                    {
                        Rectangle cellRectangle;
                        var pos = new Position(r, c);

                        var cellSize = new Size(grid.Columns.GetWidth(c), rowHeight);

                        Range range = grid.PositionToCellRange(pos);

                        //support for RowSpan or ColSpan 
                        //Note: for now I draw only the merged cell at the first position 
                        // (this can cause a problem if you export partial range that contains a partial merged cells)
                        if (range.ColumnsCount > 1 || range.RowsCount > 1)
                        {
                            //Is the first position
                            if (range.Start == pos)
                            {
                                Size rangeSize = grid.RangeToSize(range);

                                cellRectangle = new Rectangle(cellPoint,
                                                              rangeSize);
                            }
                            else
                                cellRectangle = Rectangle.Empty;
                        }
                        else
                        {
                            cellRectangle = new Rectangle(cellPoint, cellSize);
                        }

                        if (cellRectangle.IsEmpty == false)
                        {
                            ICellVirtual cell = grid.GetCell(pos);
                            var context = new CellContext(grid, pos, cell);
                            ExportCell(context, graphicsCache, cellRectangle);
                        }

                        cellPoint = new Point(cellPoint.X + cellSize.Width, cellPoint.Y);
                    }

                    cellPoint = new Point(destinationLocation.X, cellPoint.Y + rowHeight);
                }
            }
        }

        protected virtual void ExportCell(CellContext context, GraphicsCache graphics, Rectangle rectangle)
        {
            if (context.Cell != null)
            {
                context.Cell.View.DrawCell(context, graphics, rectangle);
            }
        }
    }
}