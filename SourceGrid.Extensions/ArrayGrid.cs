#region

using System;
using System.ComponentModel;
using DevAge.ComponentModel;
using SourceGrid.Cells;
using SourceGrid.Cells.Editors;
using SourceGrid.Cells.Models;
using SourceGrid.Cells.Virtual;
using ColumnHeader=SourceGrid.Cells.Virtual.ColumnHeader;
using Header=SourceGrid.Cells.Virtual.Header;
using RowHeader=SourceGrid.Cells.Virtual.RowHeader;

#endregion

namespace SourceGrid
{
    /// <summary>
    /// This class derive from GridVirtual and create a grid bound to an array.
    /// </summary>
    [ToolboxItem(true)]
    public class ArrayGrid : GridVirtual
    {
        private ICellVirtual mColumnHeader = new ArrayColumnHeader();
        private Array mDataSource;
        private ICellVirtual mHeader = new ArrayHeader();
        private ICellVirtual mRowHeader = new ArrayRowHeader();
        private ICellVirtual mValueCell;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ArrayRows Rows
        {
            get { return (ArrayRows) base.Rows; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ArrayColumns Columns
        {
            get { return (ArrayColumns) base.Columns; }
        }

        /// <summary>
        /// Gets or sets the data source array used to bind the grid.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Array DataSource
        {
            get { return mDataSource; }
            set
            {
                if (value != null && value.Rank != 2)
                    throw new SourceGridException("Array dimension not valid, must be an array with 2 dimensions");

                mDataSource = value;
                Bind();
            }
        }

        /// <summary>
        /// Gets or sets the cell used for the column headers.  Only used when FixedRows is greater than 0.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ICellVirtual ColumnHeader
        {
            get { return mColumnHeader; }
            set { mColumnHeader = value; }
        }

        /// <summary>
        /// Gets or sets the cell used for the row headers. Only used when FixedColumns is greater than 0.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ICellVirtual RowHeader
        {
            get { return mRowHeader; }
            set { mRowHeader = value; }
        }

        /// <summary>
        /// Gets or sets the cell used for the left top position header. Only used when FixedRows and FixedColumns are greater than 0.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ICellVirtual Header
        {
            get { return mHeader; }
            set { mHeader = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ICellVirtual ValueCell
        {
            get { return mValueCell; }
            set { mValueCell = value; }
        }

        protected override RowsBase CreateRowsObject()
        {
            return new ArrayRows(this);
        }

        protected override ColumnsBase CreateColumnsObject()
        {
            return new ArrayColumns(this);
        }

        public override ICellVirtual GetCell(int p_iRow, int p_iCol)
        {
            if (p_iRow < FixedRows &&
                p_iCol < FixedColumns)
                return mHeader;
            else if (p_iRow < FixedRows)
                return mColumnHeader;
            else if (p_iCol < FixedColumns)
                return mRowHeader;
            else
                return mValueCell;
        }

        protected virtual void Bind()
        {
            ValueCell = null;
            if (mDataSource != null)
            {
                mValueCell = new CellVirtual();
                mValueCell.Model.AddModel(new ArrayValueModel());
                mValueCell.Editor = Factory.Create(mDataSource.GetType().GetElementType());
            }

            Rows.RowsChanged();
            Columns.ColumnsChanged();
        }
    }

    #region Models

    public class ArrayValueModel : IValueModel
    {
        #region IValueModel Members

        public virtual object GetValue(CellContext cellContext)
        {
            Array array = ((ArrayGrid) cellContext.Grid).DataSource;
            return array.GetValue(cellContext.Position.Row - cellContext.Grid.FixedRows,
                                  cellContext.Position.Column - cellContext.Grid.FixedColumns);
        }

        public virtual void SetValue(CellContext cellContext, object p_Value)
        {
            var valArgs = new ValueEventArgs(p_Value);
            if (cellContext.Grid != null)
                cellContext.Grid.Controller.OnValueChanging(cellContext, valArgs);

            Array array = ((ArrayGrid) cellContext.Grid).DataSource;
            array.SetValue(valArgs.Value, cellContext.Position.Row - cellContext.Grid.FixedRows,
                           cellContext.Position.Column - cellContext.Grid.FixedColumns);

            if (cellContext.Grid != null)
                cellContext.Grid.Controller.OnValueChanged(cellContext, EventArgs.Empty);
        }

        #endregion
    }

    public class ArrayRowHeaderModel : IValueModel
    {
        #region IValueModel Members

        public virtual object GetValue(CellContext cellContext)
        {
            return cellContext.Position.Row - cellContext.Grid.FixedRows;
        }

        public virtual void SetValue(CellContext cellContext, object p_Value)
        {
            throw new ApplicationException("Not supported");
        }

        #endregion
    }

    public class ArrayColumnHeaderModel : IValueModel
    {
        #region IValueModel Members

        public virtual object GetValue(CellContext cellContext)
        {
            return cellContext.Position.Column - cellContext.Grid.FixedColumns;
        }

        public virtual void SetValue(CellContext cellContext, object p_Value)
        {
            throw new ApplicationException("Not supported");
        }

        #endregion
    }

    #endregion

    #region Cells

    /// <summary>
    /// A cell header used for the columns. Usually used in the HeaderCell property of a DataGridColumn.
    /// </summary>
    public class ArrayColumnHeader : ColumnHeader
    {
        public ArrayColumnHeader()
        {
            Model.AddModel(new ArrayColumnHeaderModel());
            AutomaticSortEnabled = false;
            ResizeEnabled = false;

//            ColumnSelectorEnabled = true;
//            ColumnFocusEnabled = true;
        }
    }

    /// <summary>
    /// A cell used as left row selector. Usually used in the DataCell property of a DataGridColumn. If FixedColumns is grater than 0 and the columns are automatically created then the first column is created of this type.
    /// </summary>
    public class ArrayRowHeader : RowHeader
    {
        public ArrayRowHeader()
        {
            Model.AddModel(new ArrayRowHeaderModel());
            ResizeEnabled = false;

            //RowSelectorEnabled = true;
        }
    }

    /// <summary>
    /// A cell used for the top/left cell when using DataGridRowHeader.
    /// </summary>
    public class ArrayHeader : Header
    {
        public ArrayHeader()
        {
            Model.AddModel(new NullValueModel());
        }
    }

    #endregion

    #region Rows and Columns

    public class ArrayRows : RowsSimpleBase
    {
        private AutoSizeMode mAutoSizeMode = AutoSizeMode.Default;

        public ArrayRows(ArrayGrid grid) : base(grid)
        {
        }

        public new ArrayGrid Grid
        {
            get { return (ArrayGrid) base.Grid; }
        }

        public override int Count
        {
            get
            {
                if (Grid.DataSource == null)
                    return Grid.FixedRows;
                else
                {
                    return Grid.DataSource.GetLength(0) + Grid.FixedRows;
                }
            }
        }

        public AutoSizeMode AutoSizeMode
        {
            get { return mAutoSizeMode; }
            set { mAutoSizeMode = value; }
        }

        public override AutoSizeMode GetAutoSizeMode(int row)
        {
            return mAutoSizeMode;
        }
    }

    public class ArrayColumns : ColumnsSimpleBase
    {
        private AutoSizeMode mAutoSizeMode = AutoSizeMode.Default;

        public ArrayColumns(ArrayGrid grid) : base(grid)
        {
        }

        public new ArrayGrid Grid
        {
            get { return (ArrayGrid) base.Grid; }
        }

        public override int Count
        {
            get
            {
                if (Grid.DataSource == null)
                    return Grid.FixedColumns;
                else
                {
                    return Grid.DataSource.GetLength(1) + Grid.FixedColumns;
                }
            }
        }

        public AutoSizeMode AutoSizeMode
        {
            get { return mAutoSizeMode; }
            set { mAutoSizeMode = value; }
        }

        public override AutoSizeMode GetAutoSizeMode(int row)
        {
            return mAutoSizeMode;
        }
    }

    #endregion
}