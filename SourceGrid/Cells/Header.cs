#region

using SourceGrid.Cells.Controllers;

#endregion

namespace SourceGrid.Cells.Virtual
{
    /// <summary>
    /// A cell that rappresent a header of a table, with 3D effect. This cell override IsSelectable to false. Default use VisualModels.VisualModelHeader.Style1
    /// </summary>
    public abstract class Header : CellVirtual
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Header()
        {
            View = Views.Header.Default;
            AddController(Unselectable.Default);
            AddController(MouseInvalidate.Default);
            ResizeEnabled = true;
        }

        public bool ResizeEnabled
        {
            get { return FindController(typeof (Resizable)) == Resizable.ResizeBoth; }
            set
            {
                if (value == ResizeEnabled)
                    return;

                if (value)
                    AddController(Resizable.ResizeBoth);
                else
                    RemoveController(Resizable.ResizeBoth);
            }
        }
    }
}

namespace SourceGrid.Cells
{
    /// <summary>
    /// A cell that rappresent a header of a table, with 3D effect. This cell override IsSelectable to false. Default use VisualModels.VisualModelHeader.Style1
    /// </summary>
    public class Header : Cell
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Header() : this(null)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cellValue"></param>
        public Header(object cellValue) : base(cellValue)
        {
            View = Views.Header.Default;
            AddController(Unselectable.Default);
            AddController(MouseInvalidate.Default);
            ResizeEnabled = true;
        }

        public bool ResizeEnabled
        {
            get { return FindController(typeof (Resizable)) == Resizable.ResizeBoth; }
            set
            {
                if (value == ResizeEnabled)
                    return;

                if (value)
                    AddController(Resizable.ResizeBoth);
                else
                    RemoveController(Resizable.ResizeBoth);
            }
        }
    }
}