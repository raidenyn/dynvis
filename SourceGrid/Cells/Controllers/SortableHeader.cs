#region

using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using DevAge.Drawing;
using SourceGrid.Cells.Models;

#endregion

namespace SourceGrid.Cells.Controllers
{
    /// <summary>
    /// A behavior that support sort and resize. Once created cannot be modified. When calculated automatically the range to sort is all the grid range without the rows minor of the current row and the range header is all the grid range with the rows minor or equal of the current row
    /// </summary>
    public class SortableHeader : ControllerBase
    {
        /// <summary>
        /// Column header behavior with sort and resize support, same as SortResizeHeader.
        /// </summary>
        public static readonly SortableHeader Default;

        /// <summary>
        /// Border used to calculate the region where the sort is enabled.
        /// </summary>
        public RectangleBorder LogicalBorder = new RectangleBorder(new BorderLine(Color.Black, 4),
                                                                   new BorderLine(Color.Black, 4));

        #region Constructor

        /// <summary>
        /// Static constructor
        /// </summary>
        static SortableHeader()
        {
            Default = new SortableHeader();
        }


        public SortableHeader() : this(null, null)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="p_RangeToSort">If null then the range is automatically calculated.</param>
        /// <param name="p_HeaderRange">If null then the range is automatically calculated.</param>
        public SortableHeader(IRangeLoader p_RangeToSort, IRangeLoader p_HeaderRange)
        {
            m_HeaderRange = p_HeaderRange;
            m_RangeToSort = p_RangeToSort;
        }

        #endregion

        public override void OnMouseUp(CellContext sender, MouseEventArgs e)
        {
            base.OnMouseUp(sender, e);

            //Note: I can't use the click event because I don't have Button information (Control.MouseButtons returns always None inside the Click event)

            Point currentPoint = sender.Grid.PointToClient(Control.MousePosition);
            Rectangle cellRect = sender.Grid.PositionToRectangle(sender.Position);
            float distance;
            RectanglePartType partType = LogicalBorder.GetPointPartType(cellRect, currentPoint, out distance);

            //eseguo il sort solo se non sono attualmente in resize
            if (IsSortEnable(sender) &&
                partType == RectanglePartType.ContentArea &&
                e.Button == MouseButtons.Left)
            {
                var sortHeader = (ISortableHeader) sender.Cell.Model.FindModel(typeof (ISortableHeader));
                SortStatus l_Status = sortHeader.GetSortStatus(sender);
                if (l_Status.Style == HeaderSortStyle.Ascending)
                    SortColumn(sender, false, l_Status.Comparer);
                else
                    SortColumn(sender, true, l_Status.Comparer);
            }
        }

        #region Sort Methods

        #region Status Properties

        /// <summary>
        /// Header range (can be null).
        /// </summary>
        private readonly IRangeLoader m_HeaderRange;

        /// <summary>
        /// Range to sort
        /// </summary>
        private readonly IRangeLoader m_RangeToSort;

        /// <summary>
        /// Range to sort. If null and EnableSort is true the range is automatically calculated.
        /// </summary>
        public IRangeLoader RangeToSort
        {
            get { return m_RangeToSort; }
        }

        /// <summary>
        /// Header range. If null and EnableSort is true the range is automatically calculated.
        /// </summary>
        public IRangeLoader RangeHeader
        {
            get { return m_HeaderRange; }
        }

        #endregion

        #region Support Function

        /// <summary>
        /// Indicates if for the specified cell the sort is enabled.
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public bool IsSortEnable(CellContext sender)
        {
            if (sender.Cell.Model.FindModel(typeof (ISortableHeader)) != null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Sort the current column. If the range contains all the columns this method move directly the row object otherwise move each cell.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="p_bAscending"></param>
        /// <param name="p_Comparer"></param>
        public void SortColumn(CellContext sender, bool p_bAscending, IComparer p_Comparer)
        {
            //verifico che il sort sia abilitato e che ci sia almeno una riga da ordinare oltra a quella corrente
            if (IsSortEnable(sender) && sender.Position.Row < (sender.Grid.Rows.Count) && sender.Grid.Columns.Count > 0)
            {
                Range l_RangeToSort;
                Range l_RangeHeader;
                if (m_RangeToSort != null)
                    l_RangeToSort = m_RangeToSort.GetRange(sender.Grid);
                else
                    //the range to sort is all the grid range without the rows < of the current row
                    l_RangeToSort = new Range(sender.Position.Row + 1, 0, sender.Grid.Rows.Count - 1,
                                              sender.Grid.Columns.Count - 1);

                if (m_HeaderRange != null)
                    l_RangeHeader = m_HeaderRange.GetRange(sender.Grid);
                else
                    //the range header is all the grid range with the rows <= of the current row
                    l_RangeHeader = new Range(0, 0, sender.Position.Row, sender.Grid.Columns.Count - 1);

                var modelSortable = (ISortableHeader) sender.Cell.Model.FindModel(typeof (ISortableHeader));

                if (sender.Grid.Rows.Count > (sender.Position.Row + 1) &&
                    sender.Grid.Columns.Count > sender.Grid.FixedColumns)
                {
                    //Sort
                    sender.Grid.SortRangeRows(l_RangeToSort, sender.Position.Column, p_bAscending, p_Comparer);
                    if (p_bAscending)
                        modelSortable.SetSortMode(sender, HeaderSortStyle.Ascending);
                    else
                        modelSortable.SetSortMode(sender, HeaderSortStyle.Descending);

                    //Remove the image from others ColHeaderSort
                    for (int r = l_RangeHeader.Start.Row; r <= l_RangeHeader.End.Row; r++)
                    {
                        for (int c = l_RangeHeader.Start.Column; c <= l_RangeHeader.End.Column; c++)
                        {
                            ICellVirtual tmpCell = sender.Grid.GetCell(r, c);

                            if (tmpCell != sender.Cell &&
                                tmpCell != null &&
                                tmpCell.Model.FindModel(typeof (ISortableHeader)) != null)
                            {
                                var header = (ISortableHeader) tmpCell.Model.FindModel(typeof (ISortableHeader));

                                header.SetSortMode(new CellContext(sender.Grid, new Position(r, c), tmpCell),
                                                   HeaderSortStyle.None);
                            }
                        }
                    }

                    sender.Grid.InvalidateRange(l_RangeHeader);
                }
            }
        }

        #endregion

        #endregion
    }
}