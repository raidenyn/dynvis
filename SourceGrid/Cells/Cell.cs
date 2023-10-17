#region

using System;
using SourceGrid.Cells.Editors;
using SourceGrid.Cells.Models;
using SourceGrid.Cells.Virtual;

#endregion

namespace SourceGrid.Cells
{
    /// <summary>
    /// Represents a Cell in a grid, with Cell.Value support and row/col span. Support also ToolTipText, ContextMenu and Cursor
    /// </summary>
    public class Cell : CellVirtual, ICell
    {
        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public Cell() : this(null)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cellValue"></param>
        public Cell(object cellValue)
        {
            Model = new ModelContainer();
            Model.ValueModel = new ValueModel();

            Model.AddModel(new ToolTip());
            Model.AddModel(new Models.Image());

            Value = cellValue;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cellValue"></param>
        /// <param name="pType"></param>
        public Cell(object cellValue, Type pType) : this(cellValue)
        {
            Editor = Factory.Create(pType);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cellValue"></param>
        /// <param name="pEditor"></param>
        public Cell(object cellValue, EditorBase pEditor) : this(cellValue)
        {
            Editor = pEditor;
        }

        #endregion

        #region LinkToGrid

        private Grid.GridColumn mColumn;
        private Grid mGrid;
        private Grid.GridRow mRow;

        /// <summary>
        /// Link the cell at the specified grid.
        /// For internal use only.
        /// </summary>
        /// <param name="p_grid"></param>
        /// <param name="p_Position"></param>
        public virtual void BindToGrid(Grid p_grid, Position p_Position)
        {
            mGrid = p_grid;
            mRow = Grid.Rows[p_Position.Row];
            mColumn = Grid.Columns[p_Position.Column];
        }

        /// <summary>
        /// Remove the link of the cell from the grid.
        /// For internal use only.
        /// </summary>
        public virtual void UnBindToGrid()
        {
            mGrid = null;
            mColumn = null;
            mRow = null;
        }

        /// <summary>
        /// The Grid object
        /// </summary>
        public Grid Grid
        {
            get { return mGrid; }
        }

        /// <summary>
        /// Gets the column of the specified cell
        /// </summary>
        public Grid.GridColumn Column
        {
            get { return mColumn; }
        }

        /// <summary>
        /// Gets the row of the specified cell
        /// </summary>
        public Grid.GridRow Row
        {
            get { return mRow; }
        }

        /// <summary>
        /// Gets the range of the cell
        /// </summary>
        public Range Range
        {
            get
            {
                if (Grid == null)
                    return Range.Empty;

                int col = Column.Index;
                int row = Row.Index;
                return new Range(row, col,
                                 row + RowSpan - 1, col + ColumnSpan - 1);
            }
        }

        protected CellContext GetContext()
        {
            return new CellContext(Grid, Range.Start, this);
        }

        #endregion

        #region Cell Data (Value, DisplayText, Tag)

        //ATTENTION: is reccomanded that all the actions fired by the user interface does not modify this property
        // instead call the CellEditor.ChangeCellValue to preserve data consistence

        /// <summary>
        /// The string representation of the Cell.Value property (default Value.ToString())
        /// </summary>
        public virtual string DisplayText
        {
            get { return GetContext().DisplayText; }
        }

        /// <summary>
        /// Value of the cell 
        /// </summary>
        public virtual object Value
        {
            get { return Model.ValueModel.GetValue(GetContext()); }
            set { Model.ValueModel.SetValue(GetContext(), value); }
        }

        /// <summary>
        /// Object to put additional info for this cell
        /// </summary>
        public virtual object Tag { get; set; }

        /// <summary>
        /// ToString method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return DisplayText;
        }

        #endregion

        #region Row/Col Span

        private int mColumnSpan = 1;

        private int mRowSpan = 1;

        /// <summary>
        /// ColSpan for merge operation
        /// </summary>
        public int ColumnSpan
        {
            get { return mColumnSpan; }
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException("ColumnSpan");
                if (value > Grid.MaxSpan)
                    throw new SourceGridException(
                        "ColumnSpan must be smaller than Grid.MaxSpan property. Increase the property or use a smaller column span.");

                mColumnSpan = value;
            }
        }

        /// <summary>
        /// RowSpan for merge operation
        /// </summary>
        public int RowSpan
        {
            get { return mRowSpan; }
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException("RowSpan");
                if (value > Grid.MaxSpan)
                    throw new SourceGridException(
                        "RowSpan must be smaller than Grid.MaxSpan property. Increase the property or use a smaller row span.");

                mRowSpan = value;
            }
        }

        #endregion

        #region ToolTipText

        private ToolTip ToolTipModel
        {
            get { return (ToolTip) Model.FindModel(typeof (ToolTip)); }
        }

        /// <summary>
        /// Gets or sets the tool tip text of the cell. Internally use the Models.ToolTip class.
        /// </summary>
        public string ToolTipText
        {
            get { return ToolTipModel.ToolTipText; }
            set { ToolTipModel.ToolTipText = value; }
        }

        #endregion

        #region Image

        private Models.Image ImageModel
        {
            get { return (Models.Image) Model.FindModel(typeof (Models.Image)); }
        }

        /// <summary>
        /// Gets or sets the Image associeted with the Cell. Internally use a Models.Image class.
        /// </summary>
        public System.Drawing.Image Image
        {
            get { return ImageModel.ImageValue; }
            set { ImageModel.ImageValue = value; }
        }

        #endregion
    }
}