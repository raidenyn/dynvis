#region

using System;
using System.Drawing;
using System.Windows.Forms;
using DevAge.Drawing;

#endregion

namespace SourceGrid.Cells.Controllers
{
    /// <summary>
    /// Implement the mouse resize features of a cell. This behavior can be shared between multiple cells.
    /// </summary>
    public class Resizable : ControllerBase
    {
        /// <summary>
        /// Resize both width nd height behavior
        /// </summary>
        public static readonly Resizable ResizeBoth = new Resizable(CellResizeMode.Both);

        /// <summary>
        /// Resize height behavior
        /// </summary>
        public static readonly Resizable ResizeHeight = new Resizable(CellResizeMode.Height);

        /// <summary>
        /// Resize width behavior
        /// </summary>
        public static readonly Resizable ResizeWidth = new Resizable(CellResizeMode.Width);

        private readonly CellResizeMode m_ResizeMode = CellResizeMode.Both;
        private readonly MouseCursor mHeightCursor = new MouseCursor(Cursors.HSplit, false);
        private readonly MouseCursor mWidthCursor = new MouseCursor(Cursors.VSplit, false);

        /// <summary>
        /// Border used to calculate the region where the resize is enabled.
        /// </summary>
        public RectangleBorder LogicalBorder = new RectangleBorder(new BorderLine(Color.Black, 4),
                                                                   new BorderLine(Color.Black, 4));

        private bool m_IsHeightResize;
        private bool m_IsWidthResize;
        private float mDistanceFromBorder;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="p_Mode"></param>
        public Resizable(CellResizeMode p_Mode)
        {
            m_ResizeMode = p_Mode;
        }

        /// <summary>
        /// Resize mode of the cell
        /// </summary>
        public CellResizeMode ResizeMode
        {
            get { return m_ResizeMode; }
        }

        /// <summary>
        /// Indicates if the behavior is currently resizing a cell width
        /// </summary>
        public bool IsWidthResizing
        {
            get { return m_IsWidthResize; }
        }

        /// <summary>
        /// Indicates if the behavior is currently resizing a cell height
        /// </summary>
        public bool IsHeightResizing
        {
            get { return m_IsHeightResize; }
        }

        public override void OnMouseDown(CellContext sender, MouseEventArgs e)
        {
            base.OnMouseDown(sender, e);

            m_IsHeightResize = false;
            m_IsWidthResize = false;

            Rectangle l_CellRect = sender.Grid.PositionToRectangle(sender.Position);
            var mousePoint = new Point(e.X, e.Y);

            RectanglePartType partType = LogicalBorder.GetPointPartType(l_CellRect, mousePoint, out mDistanceFromBorder);

            if (((ResizeMode & CellResizeMode.Width) == CellResizeMode.Width) &&
                partType == RectanglePartType.RightBorder)
                m_IsWidthResize = true;
            else if (((ResizeMode & CellResizeMode.Height) == CellResizeMode.Height) &&
                     partType == RectanglePartType.BottomBorder)
                m_IsHeightResize = true;
        }

        public override void OnMouseUp(CellContext sender, MouseEventArgs e)
        {
            base.OnMouseUp(sender, e);

            m_IsWidthResize = false;
            m_IsHeightResize = false;
        }

        public override void OnMouseMove(CellContext sender, MouseEventArgs e)
        {
            base.OnMouseMove(sender, e);

            Rectangle cellRect = sender.Grid.PositionToRectangle(sender.Position);
            if (cellRect.IsEmpty)
                return;

            var mousePoint = new Point(e.X, e.Y);

            float dummy;
            RectanglePartType partType = LogicalBorder.GetPointPartType(cellRect, mousePoint, out dummy);

            //sono già in fase di resizing
            if (sender.Grid.MouseDownPosition == sender.Position)
            {
                if (m_IsWidthResize)
                {
                    int newWidth = mousePoint.X - cellRect.Left;

                    if (newWidth > 0)
                        SetWidth(sender.Grid, sender.Position, (int) (newWidth + mDistanceFromBorder));

                    mWidthCursor.ApplyCursor(sender, e);
                    mHeightCursor.ResetCursor(sender, e);
                }
                else if (m_IsHeightResize)
                {
                    int newHeight = mousePoint.Y - cellRect.Top;

                    if (newHeight > 0)
                        SetHeight(sender.Grid, sender.Position, (int) (newHeight + mDistanceFromBorder));

                    mHeightCursor.ApplyCursor(sender, e);
                    mWidthCursor.ResetCursor(sender, e);
                }
            }
            else
            {
                if (partType == RectanglePartType.RightBorder &&
                    (ResizeMode & CellResizeMode.Width) == CellResizeMode.Width)
                    mWidthCursor.ApplyCursor(sender, e);
                else if (partType == RectanglePartType.BottomBorder &&
                         (ResizeMode & CellResizeMode.Height) == CellResizeMode.Height)
                    mHeightCursor.ApplyCursor(sender, e);
                else
                {
                    mWidthCursor.ResetCursor(sender, e);
                    mHeightCursor.ResetCursor(sender, e);
                }
            }
        }

        private void SetWidth(GridVirtual grid, Position position, int width)
        {
            Range range = grid.PositionToCellRange(position);
            int widthForCol = width/range.ColumnsCount;
            for (int c = range.Start.Column; c <= range.End.Column; c++)
                grid.Columns.SetWidth(c, widthForCol);
        }

        private void SetHeight(GridVirtual grid, Position position, int height)
        {
            Range range = grid.PositionToCellRange(position);
            int heightForCol = height/range.RowsCount;
            for (int r = range.Start.Row; r <= range.End.Row; r++)
                grid.Rows.SetHeight(r, heightForCol);
        }

        public override void OnMouseLeave(CellContext sender, EventArgs e)
        {
            base.OnMouseLeave(sender, e);

            mWidthCursor.ResetCursor(sender, e);
            mHeightCursor.ResetCursor(sender, e);
            m_IsWidthResize = false;
            m_IsHeightResize = false;
        }

        public override void OnDoubleClick(CellContext sender, EventArgs e)
        {
            base.OnDoubleClick(sender, e);

            Point currentPoint = sender.Grid.PointToClient(Control.MousePosition);
            Rectangle cellRect = sender.Grid.PositionToRectangle(sender.Position);

            float distance;
            RectanglePartType partType = LogicalBorder.GetPointPartType(cellRect, currentPoint, out distance);

            if ((ResizeMode & CellResizeMode.Width) == CellResizeMode.Width &&
                partType == RectanglePartType.RightBorder)
            {
                sender.Grid.Columns.AutoSizeColumn(sender.Position.Column);
            }
            else if ((ResizeMode & CellResizeMode.Height) == CellResizeMode.Height &&
                     partType == RectanglePartType.BottomBorder)
            {
                sender.Grid.Rows.AutoSizeRow(sender.Position.Row);
            }
        }

        //		#region Support Functions
        //		/// <summary>
        //		/// 
        //		/// </summary>
        //		/// <param name="p_CellRectangle">A grid relative rectangle</param>
        //		/// <param name="p"></param>
        //		/// <returns></returns>
        //		public static bool IsInResizeHorRegion(Rectangle p_CellRectangle, Point p)
        //		{
        //			if (p.X >= p_CellRectangle.Right-c_MouseDelta && p.X <= p_CellRectangle.Right)
        //				return true;
        //			else
        //				return false;
        //		}
        //
        //		/// <summary>
        //		/// 
        //		/// </summary>
        //		/// <param name="p_CellRectangle">A grid relative rectangle</param>
        //		/// <param name="p"></param>
        //		/// <returns></returns>
        //		public static bool IsInResizeVerRegion(Rectangle p_CellRectangle, Point p)
        //		{
        //			if (p.Y >= p_CellRectangle.Bottom-c_MouseDelta && p.Y <= p_CellRectangle.Bottom)
        //				return true;
        //			else
        //				return false;
        //		}
        //
        //		private const int c_MouseDelta = 4;
        //
        //		#endregion
    }
}