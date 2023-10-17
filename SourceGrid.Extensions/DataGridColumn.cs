#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using SourceGrid.Cells;
using SourceGrid.Cells.Editors;
using SourceGrid.Conditions;
using Cell=SourceGrid.Cells.DataGrid.Cell;
using ColumnHeader=SourceGrid.Cells.DataGrid.ColumnHeader;
using Header=SourceGrid.Cells.DataGrid.Header;
using RowHeader=SourceGrid.Cells.DataGrid.RowHeader;

#endregion

namespace SourceGrid
{
    public class DataGridColumns : ColumnInfoCollection
    {
        public DataGridColumns(DataGrid grid)
            : base(grid)
        {
        }

        public new DataGrid Grid
        {
            get { return (DataGrid) base.Grid; }
        }

        public new DataGridColumn this[int index]
        {
            get { return (DataGridColumn) base[index]; }
        }

        public void Insert(int index, DataGridColumn dataGridColumn)
        {
            base.InsertRange(index, new ColumnInfo[] {dataGridColumn});
        }

        /// <summary>
        /// Return the DataColumn object for a given grid column index. Return null if not applicable, for example if the column index requested is a FixedColumns of an unbound column
        /// </summary>
        /// <param name="gridColumnIndex"></param>
        /// <returns></returns>
        public PropertyDescriptor IndexToPropertyColumn(int gridColumnIndex)
        {
            return Grid.Columns[gridColumnIndex].PropertyColumn;
        }

        /// <summary>
        /// Returns the index for a given DataColumn. -1 if not valid.
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public int DataSourceColumnToIndex(PropertyDescriptor propertyColumn)
        {
            for (int i = 0; i < Grid.Columns.Count; i++)
            {
                if (Grid.Columns[i].PropertyColumn == propertyColumn)
                    return i;
            }

            return -1;
        }

        #region Add Helper methods

        public DataGridColumn Add(string property,
                                  string caption,
                                  Type propertyType)
        {
            ICellVirtual cell = Cell.Create(propertyType, true);

            return Add(property, caption, cell);
        }

        public DataGridColumn Add(string property,
                                  string caption,
                                  EditorBase editor)
        {
            var cell = new Cell();
            cell.Editor = editor;

            return Add(property, caption, cell);
        }

        public DataGridColumn Add(string property,
                                  string caption,
                                  ICellVirtual cell)
        {
            var col = new DataGridColumn(Grid,
                                         new ColumnHeader(caption),
                                         cell,
                                         property);
            Insert(Count, col);

            return col;
        }

        #endregion
    }

    /// <summary>
    /// A ColumnInfo derived class used to store column informations for a DataGrid control. 
    /// Mantains the cell used on this grid and manage the binding to the DataSource using a DataGridValueModel class.
    /// </summary>
    public class DataGridColumn : ColumnInfo
    {
        private readonly Dictionary<ICondition, ICellVirtual> mConditionalCells =
            new Dictionary<ICondition, ICellVirtual>();

        private readonly List<ICondition> mConditions = new List<ICondition>();
        private PropertyDescriptor mPropertyColumn;
        private string mPropertyName;

        /// <summary>
        /// Constructor. Create a DataGridColumn class.
        /// </summary>
        /// <param name="grid"></param>
        public DataGridColumn(DataGrid grid)
            : base(grid)
        {
            HeaderCell = new ColumnHeader(string.Empty);
            DataCell = new Cell();
        }

        /// <summary>
        /// Constructor. Create a DataGridColumn class.
        /// </summary>
        public DataGridColumn(DataGrid grid,
                              ICellVirtual headerCell,
                              ICellVirtual dataCell,
                              string propertyName)
            : base(grid)
        {
            mPropertyName = propertyName;
            HeaderCell = headerCell;
            DataCell = dataCell;
        }

        public new DataGrid Grid
        {
            get { return (DataGrid) base.Grid; }
        }

        public string PropertyName
        {
            get { return mPropertyName; }
            set
            {
                mPropertyName = value;
                mPropertyColumn = null;
            }
        }

        /// <summary>
        /// Gets the property column. Can be null if not bound to a datasource Column.
        /// This field is used for example to support sorting.
        /// </summary>
        public PropertyDescriptor PropertyColumn
        {
            get
            {
                if (mPropertyColumn == null && Grid.DataSource != null)
                    mPropertyColumn = Grid.DataSource.GetItemProperty(PropertyName,
                                                                      StringComparison.InvariantCultureIgnoreCase);

                return mPropertyColumn;
            }
        }

        /// <summary>
        /// Gets or sets the header cell for this column.
        /// Typically is an instance of SourceGrid.Cells.DataGrid.ColumnHeader
        /// </summary>
        public ICellVirtual HeaderCell { get; set; }

        /// <summary>
        /// Gets or sets the cell used for this column for all the rows to disply the data
        /// Typically is an instance of SourceGrid.Cells.DataGrid.Cell or other classes of the same namespace
        /// </summary>
        public ICellVirtual DataCell { get; set; }

        /// <summary>
        /// Gets the conditions used to returns different cell based on the data of the row.
        /// </summary>
        public List<ICondition> Conditions
        {
            get { return mConditions; }
        }

        /// <summary>
        /// Create a DataGridColumn with special cells used for RowHeader, usually used when FixedColumns is 1 for the first column.
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static DataGridColumn CreateRowHeader(DataGrid grid)
        {
            return new DataGridColumn(grid,
                                      new Header(),
                                      new RowHeader(),
                                      null);
        }


        /// <summary>
        /// Gets the ICellVirtual for the current column and the specified row.
        /// Override this method to provide custom cells, based on the row informations.
        /// </summary>
        /// <param name="gridRow"></param>
        /// <returns></returns>
        public virtual ICellVirtual GetDataCell(int gridRow)
        {
            object itemRow = Grid.Rows.IndexToDataSourceRow(gridRow);

            foreach (ICondition con in Conditions)
            {
                if (con.Evaluate(this, gridRow, itemRow))
                {
                    ICellVirtual cell;
                    if (mConditionalCells.TryGetValue(con, out cell) == false)
                    {
                        cell = con.ApplyCondition(DataCell);
                        mConditionalCells.Add(con, cell);
                    }

                    return cell;
                }
            }

            return DataCell;
        }
    }
}