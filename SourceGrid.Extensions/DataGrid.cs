#region

using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Windows.Forms;
using DevAge.ComponentModel;
using SourceGrid.Cells;
using SourceGrid.Cells.Controllers;
using SourceGrid.Cells.Models;
using SourceGrid.Selection;
using Cell=SourceGrid.Cells.DataGrid.Cell;

#endregion

namespace SourceGrid
{
    /// <summary>
    /// A grid control that support load from a System.Data.DataView class, usually used for data binding.
    /// </summary>
    [ToolboxItem(true)]
    public class DataGrid : GridVirtual
    {
        private IBoundList mBoundList;
        private bool mCancelEditingWithEscapeKey = true;
        private string mDeleteQuestionMessage = "Are you sure to delete all the selected rows?";
        private bool mDeleteRowsWithDeleteKey = true;
        private int? mEditingRow;
        private bool mEndEditingRowOnValidate = true;

        public DataGrid()
        {
            FixedRows = 1;
            FixedColumns = 0;

            Controller.AddController(new DataGridCellController());

            SelectionMode = GridSelectionMode.Row;
        }

        /// <summary>
        /// Gets or sets the IBoundList used for data binding.
        /// It can be any class that implements the IBoundList interface, usually can be BoundList 
        ///  (that can be used to bind to a generic List) or BoundDataView (that can be used to bind to a DataView).
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IBoundList DataSource
        {
            get { return mBoundList; }
            set
            {
                Unbind();
                mBoundList = value;
                if (mBoundList != null)
                    Bind();
            }
        }

        /// <summary>
        /// Gets the rows information as a DataGridRows object.
        /// </summary>
        public new DataGridRows Rows
        {
            get { return (DataGridRows) base.Rows; }
        }

        /// <summary>
        /// Gets the columns informations as a DataGridColumns object.
        /// </summary>
        public new DataGridColumns Columns
        {
            get { return (DataGridColumns) base.Columns; }
        }

        /// <summary>
        /// Gets or sets the selected DataRowView.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object[] SelectedDataRows
        {
            get
            {
                if (mBoundList == null)
                    return new DataRowView[0];

                int[] rowsSel = Selection.GetSelectionRegion().GetRowsIndex();

                int count = 0;
                for (int i = 0; i < rowsSel.Length; i++)
                {
                    object objRow = Rows.IndexToDataSourceRow(rowsSel[i]);
                    if (objRow != null)
                        count++;
                }

                var dataRows = new object[count];
                int indexRows = 0;
                for (int i = 0; i < rowsSel.Length; i++)
                {
                    object objRow = Rows.IndexToDataSourceRow(rowsSel[i]);
                    if (objRow != null)
                    {
                        dataRows[indexRows] = objRow;
                        indexRows++;
                    }
                }
                return dataRows;
            }
            set
            {
                Selection.ResetSelection(false);

                if (mBoundList != null && value != null)
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        for (int r = FixedRows; r < Rows.Count; r++)
                        {
                            object objRow = Rows.IndexToDataSourceRow(r);

                            if (ReferenceEquals(objRow, value[i]))
                            {
                                Selection.SelectRow(r, true);
                                break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a property to force an End Editing when the control loose the focus
        /// </summary>
        [DefaultValue(true)]
        public bool EndEditingRowOnValidate
        {
            get { return mEndEditingRowOnValidate; }
            set { mEndEditingRowOnValidate = value; }
        }

        /// <summary>
        /// Gets or sets if enable the delete of the selected rows when pressing Delete key.
        /// </summary>
        [DefaultValue(true)]
        public bool DeleteRowsWithDeleteKey
        {
            get { return mDeleteRowsWithDeleteKey; }
            set { mDeleteRowsWithDeleteKey = value; }
        }

        /// <summary>
        /// Gets or sets if enable the Cancel Editing feature when pressing escape key
        /// </summary>
        [DefaultValue(true)]
        public bool CancelEditingWithEscapeKey
        {
            get { return mCancelEditingWithEscapeKey; }
            set { mCancelEditingWithEscapeKey = value; }
        }

        /// <summary>
        /// Message showed with the DeleteSelectedRows method. Set to null to not show any message.
        /// </summary>
        public string DeleteQuestionMessage
        {
            get { return mDeleteQuestionMessage; }
            set { mDeleteQuestionMessage = value; }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        /// <summary>
        /// Method used to create the rows object, in this class of type DataGridRows.
        /// </summary>
        protected override RowsBase CreateRowsObject()
        {
            return new DataGridRows(this);
        }

        /// <summary>
        /// Method used to create the columns object, in this class of type DataGridColumns.
        /// </summary>
        protected override ColumnsBase CreateColumnsObject()
        {
            return new DataGridColumns(this);
        }

        protected override SelectionBase CreateSelectionObject()
        {
            SelectionBase selObj = base.CreateSelectionObject();

            selObj.EnableMultiSelection = false;
            selObj.FocusStyle = FocusStyle.RemoveFocusCellOnLeave;
            selObj.FocusRowLeaving += Selection_FocusRowLeaving;

            return selObj;
        }

        protected virtual void Unbind()
        {
            if (mBoundList != null)
                mBoundList.ListChanged -= mBoundList_ListChanged;

            Rows.RowsChanged();
        }

        protected virtual void Bind()
        {
            if (Columns.Count == 0)
                CreateColumns();

            mBoundList.ListChanged += mBoundList_ListChanged;
            Rows.RowsChanged();
        }

        protected virtual void mBoundList_ListChanged(object sender, ListChangedEventArgs e)
        {
            Rows.RowsChanged();
            Invalidate(true);
        }

        /// <summary>
        /// Gets a specified Cell by its row and column.
        /// </summary>
        /// <param name="p_iRow"></param>
        /// <param name="p_iCol"></param>
        /// <returns></returns>
        public override ICellVirtual GetCell(int p_iRow, int p_iCol)
        {
            try
            {
                if (mBoundList != null)
                {
                    if (p_iRow < FixedRows)
                        return Columns[p_iCol].HeaderCell;
                    else
                        return Columns[p_iCol].GetDataCell(p_iRow);
                }
                else
                    return null;
            }
            catch (Exception err)
            {
                Debug.Assert(false, err.Message);
                return null;
            }
        }

        protected override void OnSortingRangeRows(SortRangeRowsEventArgs e)
        {
            base.OnSortingRangeRows(e);

            if (DataSource == null || DataSource.AllowSort == false)
                return;

            PropertyDescriptor propertyCol = Columns[e.KeyColumn].PropertyColumn;

            if (propertyCol != null)
            {
                ListSortDirection direction;
                if (e.Ascending)
                    direction = ListSortDirection.Ascending;
                else
                    direction = ListSortDirection.Descending;
                var sortsArray = new ListSortDescription[1];
                sortsArray[0] = new ListSortDescription(propertyCol, direction);

                DataSource.ApplySort(new ListSortDescriptionCollection(sortsArray));
            }
            else
                DataSource.ApplySort(null);
        }

        /// <summary>
        /// Automatic create the columns classes based on the specified DataSource.
        /// </summary>
        public void CreateColumns()
        {
            Columns.Clear();
            if (DataSource != null)
            {
                int i = 0;

                if (FixedColumns > 0)
                {
                    Columns.Insert(i, DataGridColumn.CreateRowHeader(this));
                    i++;
                }

                foreach (PropertyDescriptor prop in DataSource.GetItemProperties())
                {
                    DataGridColumn col = Columns.Add(prop.Name,
                                                     prop.DisplayName,
                                                     Cell.Create(prop.PropertyType, !prop.IsReadOnly));
                }
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Delete &&
                mBoundList != null &&
                mBoundList.AllowDelete &&
                e.Handled == false &&
                mDeleteRowsWithDeleteKey)
            {
                object[] rows = SelectedDataRows;
                if (rows != null && rows.Length > 0)
                    DeleteSelectedRows();

                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape &&
                     e.Handled == false &&
                     mCancelEditingWithEscapeKey)
            {
                EndEditingRow(true);

                e.Handled = true;
            }
        }


        protected override void OnValidating(CancelEventArgs e)
        {
            base.OnValidating(e);

            try
            {
                if (EndEditingRowOnValidate)
                {
                    EndEditingRow(false);
                }
            }
            catch (Exception ex)
            {
                OnUserException(new ExceptionEventArgs(ex));
            }
        }

        /// <summary>
        /// Delete all the selected rows.
        /// </summary>
        /// <returns>Returns true if one or more row is deleted otherwise false.</returns>
        public virtual bool DeleteSelectedRows()
        {
            if (string.IsNullOrEmpty(mDeleteQuestionMessage) ||
                MessageBox.Show(this, mDeleteQuestionMessage, Application.ProductName, MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (int gridRow in Selection.GetSelectionRegion().GetRowsIndex())
                {
                    int dataIndex = Rows.IndexToDataSourceIndex(gridRow);
                    if (dataIndex < DataSource.Count)
                        DataSource.RemoveAt(dataIndex);
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// AutoSize the columns based on the visible range and autosize te rows based on the first row. (because there is only one height available)
        /// </summary>
        public override void AutoSizeCells()
        {
            Columns.AutoSizeView();
            if (Rows.Count > 1)
                Rows.AutoSizeRow(1);
            else if (Rows.Count > 0)
                Rows.AutoSizeRow(0);
        }

        private void Selection_FocusRowLeaving(object sender, RowCancelEventArgs e)
        {
            try
            {
                EndEditingRow(false);
            }
            catch (Exception exc)
            {
                OnUserException(new ExceptionEventArgs(new EndEditingException(exc)));

                e.Cancel = true;
            }
        }

        /// <summary>
        /// Check if the specified row is the active row (focused), return false if it is not the active row. Then call the BeginEdit on the associated DataRowView. Add a row to the DataView if required. Returns true if the method sucesfully call the BeginEdit and set the EditingRow property.
        /// </summary>
        /// <param name="gridRow"></param>
        /// <returns></returns>
        public bool BeginEditRow(int gridRow)
        {
            if (mEditingRow != null && mEditingRow.Value == gridRow)
                return true;

            EndEditingRow(false); //Terminate the old edit if present

            if (DataSource != null)
            {
                int dataIndex = Rows.IndexToDataSourceIndex(gridRow);

                // add this here to check if we have permission for edition
                if (!DataSource.AllowEdit)
                    return false;

                if (dataIndex == DataSource.Count && DataSource.AllowNew) //Last Row
                {
                    DataSource.BeginAddNew();
                }
                else if (dataIndex < DataSource.Count)
                {
                    DataSource.BeginEdit(dataIndex);
                }
            }

            mEditingRow = gridRow;

            return true;
        }

        /// <summary>
        /// Calls the CancelEdit or the EndEdit on the editing Row and set to null the editing row.
        /// </summary>
        /// <param name="cancel"></param>
        public void EndEditingRow(bool cancel)
        {
            if (mBoundList != null)
                mBoundList.EndEdit(cancel);

            mEditingRow = null;
        }
    }

    #region Models

    /// <summary>
    /// A Model of type IValueModel used for binding the value to a specified property of the bound object. 
    /// Used for the DataGrid control.
    /// </summary>
    public class DataGridValueModel : IValueModel
    {
        #region IValueModel Members

        public object GetValue(CellContext cellContext)
        {
            var grid = (DataGrid) cellContext.Grid;

            PropertyDescriptor prop = grid.Columns[cellContext.Position.Column].PropertyColumn;

            //Convert DbNull to null
            int dataIndex = grid.Rows.IndexToDataSourceIndex(cellContext.Position.Row);

            //Check if the row is not outside the valid range (for example to handle the new row)
            if (dataIndex >= grid.DataSource.Count)
                return null;
            else
            {
                object tmp = grid.DataSource.GetItemValue(dataIndex, prop);
                if (DBNull.Value == tmp)
                    return null;
                else
                    return tmp;
            }
        }

        public void SetValue(CellContext cellContext, object p_Value)
        {
            //Convert the null value to DbNull
            if (p_Value == null)
                p_Value = DBNull.Value;

            var valArgs = new ValueEventArgs(p_Value);
            if (cellContext.Grid != null)
                cellContext.Grid.Controller.OnValueChanging(cellContext, valArgs);

            var grid = (DataGrid) cellContext.Grid;

            if (grid != null)
            {
                PropertyDescriptor prop = grid.Columns[cellContext.Position.Column].PropertyColumn;

                grid.DataSource.SetEditValue(prop, valArgs.Value);
            }

            if (cellContext.Grid != null)
                cellContext.Grid.Controller.OnValueChanged(cellContext, EventArgs.Empty);
        }

        #endregion
    }

    ///<summary>
    ///</summary>
    public class DataGridRowHeaderModel : IValueModel
    {
        #region IValueModel Members

        public object GetValue(CellContext cellContext)
        {
            var dataGrid = (DataGrid) cellContext.Grid;
            if (dataGrid.DataSource != null &&
                dataGrid.DataSource.AllowNew &&
                cellContext.Position.Row == (dataGrid.Rows.Count - 1))
                return "*";
            return null;
        }

        public void SetValue(CellContext cellContext, object p_Value)
        {
            throw new ApplicationException("Not supported");
        }

        #endregion
    }

    #endregion

    #region Controller

    ///<summary>
    ///</summary>
    public class DataGridCellController : ControllerBase
    {
        public override void OnValueChanging(CellContext sender, ValueEventArgs e)
        {
            base.OnValueChanging(sender, e);

            //BeginEdit on the row, set the Cancel = true if failed to start editing.
            bool success = ((DataGrid) sender.Grid).BeginEditRow(sender.Position.Row);
            if (success == false)
                throw new SourceGridException("Failed to editing row " + sender.Position.Row);
        }

        public override void OnEditStarting(CellContext sender, CancelEventArgs e)
        {
            base.OnEditStarting(sender, e);

            //BeginEdit on the row, set the Cancel = true if failed to start editing.
            bool success = ((DataGrid) sender.Grid).BeginEditRow(sender.Position.Row);
            e.Cancel = !success;
        }
    }

    #endregion

    ///<summary>
    ///</summary>
    [Serializable]
    public class EndEditingException : SourceGridException
    {
        ///<summary>
        ///</summary>
        ///<param name="innerException"></param>
        public EndEditingException(Exception innerException) :
            base(innerException.Message, innerException)
        {
        }

        protected EndEditingException(SerializationInfo p_Info, StreamingContext p_StreamingContext) :
            base(p_Info, p_StreamingContext)
        {
        }
    }
}