#region

using System.Collections;
using DevAge.Drawing;

#endregion

namespace SourceGrid.Cells.Models
{
    /// <summary>
    /// Summary description for ICellSortableHeader.
    /// </summary>
    public interface ISortableHeader : IModel
    {
        /// <summary>
        /// Returns the current sort status
        /// </summary>
        /// <param name="cellContext"></param>
        /// <returns></returns>
        SortStatus GetSortStatus(CellContext cellContext);

        /// <summary>
        /// Set the current sort mode
        /// </summary>
        /// <param name="cellContext"></param>
        /// <param name="pStyle"></param>
        void SetSortMode(CellContext cellContext, HeaderSortStyle pStyle);
    }

    public struct SortStatus
    {
        public IComparer Comparer;
        public HeaderSortStyle Style;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="p_Style">Status of current sort.</param>
        public SortStatus(HeaderSortStyle p_Style)
        {
            Style = p_Style;
            Comparer = null;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="p_Style">Status of current sort.</param>
        /// <param name="p_Comparer">Comparer used to sort the column. The comparer will take 2 Cell. If null the default ValueCellComparer is used.</param>
        public SortStatus(HeaderSortStyle p_Style, IComparer p_Comparer)
            : this(p_Style)
        {
            Comparer = p_Comparer;
        }
    }
}