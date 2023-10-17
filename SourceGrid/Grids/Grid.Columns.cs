#region

using System.Collections.Generic;
using SourceGrid.Cells;

#endregion

namespace SourceGrid
{
    public partial class Grid
    {
        #region Nested type: GridColumn

        public class GridColumn : ColumnInfo
        {
            private readonly Dictionary<GridRow, ICell> mCells = new Dictionary<GridRow, ICell>();

            public GridColumn(Grid grid)
                : base(grid)
            {
            }

            public ICell this[GridRow row]
            {
                get
                {
                    ICell cell;
                    if (mCells.TryGetValue(row, out cell))
                        return cell;
                    else
                        return null;
                }
                set { mCells[row] = value; }
            }
        }

        #endregion

        #region Nested type: GridColumns

        public class GridColumns : ColumnInfoCollection
        {
            public GridColumns(Grid grid)
                : base(grid)
            {
            }

            public new GridColumn this[int index]
            {
                get { return (GridColumn) base[index]; }
            }

            /// <summary>
            /// Insert a column at the specified position
            /// </summary>
            /// <param name="p_Index"></param>
            public void Insert(int p_Index)
            {
                InsertRange(p_Index, 1);
            }

            /// <summary>
            /// Insert the specified number of Columns at the specified position
            /// </summary>
            /// <param name="p_StartIndex"></param>
            /// <param name="p_Count"></param>
            public void InsertRange(int p_StartIndex, int p_Count)
            {
                var columns = new ColumnInfo[p_Count];
                for (int i = 0; i < columns.Length; i++)
                    columns[i] = CreateColumn();

                InsertRange(p_StartIndex, columns);
            }

            protected GridColumn CreateColumn()
            {
                return new GridColumn((Grid) Grid);
            }

            public void SetCount(int value)
            {
                if (Count < value)
                    InsertRange(Count, value - Count);
                else if (Count > value)
                    RemoveRange(value, Count - value);
            }
        }

        #endregion
    }
}