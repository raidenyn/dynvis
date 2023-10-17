#region

using SourceGrid.Cells.Controllers;

#endregion

namespace SourceGrid.Cells.Virtual
{
    /// <summary>
    /// A cell that rappresent a header of a table, with 3D effect. This cell override IsSelectable to false. Default use VisualModels.VisualModelHeader.Style1
    /// </summary>
    public class RowHeader : CellVirtual
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RowHeader()
        {
            View = Views.RowHeader.Default;
            AddController(Unselectable.Default);
            AddController(MouseInvalidate.Default);
            ResizeEnabled = true;
        }

        /// <summary>
        /// Gets or sets if enable the resize of the height, using a Resizable controller. Default is true.
        /// </summary>
        public bool ResizeEnabled
        {
            get { return FindController(typeof (Resizable)) == Resizable.ResizeHeight; }
            set
            {
                if (value == ResizeEnabled)
                    return;

                if (value)
                    AddController(Resizable.ResizeHeight);
                else
                    RemoveController(Resizable.ResizeHeight);
            }
        }
    }
}

namespace SourceGrid.Cells
{
    /// <summary>
    /// A cell that rappresent a header of a table, with 3D effect. This cell override IsSelectable to false. Default use VisualModels.VisualModelHeader.Style1
    /// </summary>
    public class RowHeader : Cell
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RowHeader() : this(null)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cellValue"></param>
        public RowHeader(object cellValue) : base(cellValue)
        {
            View = Views.RowHeader.Default;
            AddController(Unselectable.Default);
            AddController(MouseInvalidate.Default);
            ResizeEnabled = true;
        }

        /// <summary>
        /// Gets or sets if enable the resize of the height, using a Resizable controller. Default is true.
        /// </summary>
        public bool ResizeEnabled
        {
            get { return FindController(typeof (Resizable)) == Resizable.ResizeHeight; }
            set
            {
                if (value == ResizeEnabled)
                    return;

                if (value)
                    AddController(Resizable.ResizeHeight);
                else
                    RemoveController(Resizable.ResizeHeight);
            }
        }
    }
}