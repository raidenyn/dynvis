#region

using System;
using SourceGrid.Cells.Editors;
using SourceGrid.Cells.Models;
using SourceGrid.Cells.Virtual;

#endregion

namespace SourceGrid.Cells.DataGrid
{
    public class Cell : CellVirtual
    {
        public Cell()
        {
            Model.AddModel(new DataGridValueModel());
        }

        public static ICellVirtual Create(Type type, bool editable)
        {
            ICellVirtual cell;

            if (type == typeof (bool))
                cell = new CheckBox();
            else
            {
                cell = new Cell();
                cell.Editor = Factory.Create(type);
            }

            if (cell.Editor != null) //Can be null for special DataType like Object
            {
                //The columns now support always DbNull values because the validation is done at row level by the DataTable itself.
                cell.Editor.AllowNull = true;
                cell.Editor.EnableEdit = editable;
            }

            return cell;
        }
    }

    public class CheckBox : Virtual.CheckBox
    {
        public CheckBox()
        {
            Model.AddModel(new DataGridValueModel());
        }
    }

    public class Image : Virtual.Image
    {
        public Image()
        {
            Model.AddModel(new DataGridValueModel());
        }
    }

    public class Link : Virtual.Link
    {
        public Link()
        {
            Model.AddModel(new DataGridValueModel());
        }
    }

    /// <summary>
    /// A cell header used for the columns. Usually used in the HeaderCell property of a DataGridColumn.
    /// </summary>
    public class ColumnHeader : Virtual.ColumnHeader
    {
        public ColumnHeader(string pCaption)
        {
            Model.AddModel(new ValueModel(pCaption));
        }
    }

    /// <summary>
    /// A cell used as left row selector. Usually used in the DataCell property of a DataGridColumn. If FixedColumns is grater than 0 and the columns are automatically created then the first column is created of this type.
    /// </summary>
    public class RowHeader : Virtual.RowHeader
    {
        public RowHeader()
        {
            Model.AddModel(new DataGridRowHeaderModel());
            ResizeEnabled = false;
        }
    }

    /// <summary>
    /// A cell used for the top/left cell when using DataGridRowHeader.
    /// </summary>
    public class Header : Virtual.Header
    {
        public Header()
        {
            Model.AddModel(new NullValueModel());
        }
    }
}