#region

using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DevAge.Drawing;
using SourceGrid.Selection;

#endregion

namespace SourceGrid
{
    public class CellCancelEventArgs : CellContextEventArgs
    {
        public CellCancelEventArgs(CellContext pCellContext) : base(pCellContext)
        {
        }

        public bool Cancel { get; set; }
    }

    public delegate void CellCancelEventHandler(object sender, CellCancelEventArgs e);

    public class ValidatingCellEventArgs : CellCancelEventArgs
    {
        public ValidatingCellEventArgs(CellContext pCellContext, object p_NewValue) : base(pCellContext)
        {
            NewValue = p_NewValue;
        }

        public object NewValue { get; set; }
    }

    public delegate void ValidatingCellEventHandler(object sender, ValidatingCellEventArgs e);

    public class ScrollPositionChangedEventArgs : EventArgs
    {
        private readonly int m_NewValue;
        private readonly int m_OldValue;

        public ScrollPositionChangedEventArgs(int p_NewValue, int p_OldValue)
        {
            m_NewValue = p_NewValue;
            m_OldValue = p_OldValue;
        }

        public int NewValue
        {
            get { return m_NewValue; }
        }

        public int OldValue
        {
            get { return m_OldValue; }
        }

        public int Delta
        {
            get { return m_OldValue - m_NewValue; }
        }
    }

    public delegate void ScrollPositionChangedEventHandler(object sender, ScrollPositionChangedEventArgs e);

    /// <summary>
    /// EventArgs used by the FocusRowEnter
    /// </summary>
    public class RowEventArgs : EventArgs
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pRow"></param>
        public RowEventArgs(int pRow)
        {
            Row = pRow;
        }

        /// <summary>
        /// Row
        /// </summary>
        public int Row { get; set; }
    }

    /// <summary>
    /// EventHandler used by the FocusRowEnter
    /// </summary>
    public delegate void RowEventHandler(object sender, RowEventArgs e);

    /// <summary>
    /// EventArgs used by the FocusRowLeaving
    /// </summary>
    public class RowCancelEventArgs : RowEventArgs
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pRow"></param>
        public RowCancelEventArgs(int pRow) : base(pRow)
        {
        }

        /// <summary>
        /// Row
        /// </summary>
        public bool Cancel { get; set; }
    }

    /// <summary>
    /// EventHandler used by the FocusRowLeaving
    /// </summary>
    public delegate void RowCancelEventHandler(object sender, RowCancelEventArgs e);

    /// <summary>
    /// EventArgs used by the FocusColumnEnter
    /// </summary>
    public class ColumnEventArgs : EventArgs
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pColumn"></param>
        public ColumnEventArgs(int pColumn)
        {
            Column = pColumn;
        }

        /// <summary>
        /// Column
        /// </summary>
        public int Column { get; set; }
    }

    /// <summary>
    /// EventHandled used by the FocusColumnEnter
    /// </summary>
    public delegate void ColumnEventHandler(object sender, ColumnEventArgs e);

    /// <summary>
    /// EventArgs used by the FocusColumnLeaving
    /// </summary>
    public class ColumnCancelEventArgs : ColumnEventArgs
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pColumn"></param>
        public ColumnCancelEventArgs(int pColumn) : base(pColumn)
        {
        }

        /// <summary>
        /// Column
        /// </summary>
        public bool Cancel { get; set; }
    }

    /// <summary>
    /// EventHandled used by the FocusColumnLeave and FocusColumnEnter
    /// </summary>
    public delegate void ColumnCancelEventHandler(object sender, ColumnCancelEventArgs e);

//
//	public class CellArrayEventArgs : EventArgs
//	{
//		private Cell[] m_Cell;
//		public CellArrayEventArgs(Cell[] p_Cell)
//		{
//			m_Cell = p_Cell;
//		}
//
//		public Cell[] Cells
//		{
//			get{return m_Cell;}
//			set{m_Cell = value;}
//		}
//
//	}

    public class RowInfoEventArgs : EventArgs
    {
        private readonly RowInfo m_RowInfo;

        public RowInfoEventArgs(RowInfo p_RowInfo)
        {
            m_RowInfo = p_RowInfo;
        }

        public RowInfo Row
        {
            get { return m_RowInfo; }
        }
    }

    public delegate void RowInfoEventHandler(object sender, RowInfoEventArgs e);

    public class ColumnInfoEventArgs : EventArgs
    {
        private readonly ColumnInfo m_ColumnInfo;

        public ColumnInfoEventArgs(ColumnInfo p_ColumnInfo)
        {
            m_ColumnInfo = p_ColumnInfo;
        }

        public ColumnInfo Column
        {
            get { return m_ColumnInfo; }
        }
    }

    public delegate void ColumnInfoEventHandler(object sender, ColumnInfoEventArgs e);

    public class IndexRangeEventArgs : EventArgs
    {
        private readonly int m_iCount;
        private readonly int m_iStartIndex;

        public IndexRangeEventArgs(int p_iStartIndex, int p_iCount)
        {
            m_iStartIndex = p_iStartIndex;
            m_iCount = p_iCount;
        }

        public int StartIndex
        {
            get { return m_iStartIndex; }
        }

        public int Count
        {
            get { return m_iCount; }
        }
    }

    public delegate void IndexRangeEventHandler(object sender, IndexRangeEventArgs e);

    public class CellContextEventArgs : EventArgs
    {
        private readonly CellContext mCellContext;

        public CellContextEventArgs(CellContext pCellContext)
        {
            mCellContext = pCellContext;
        }

        public CellContext CellContext
        {
            get { return mCellContext; }
        }
    }

    public delegate void CellContextEventHandler(object sender, CellContextEventArgs e);

    public class RangePaintEventArgs : EventArgs
    {
        private readonly GridVirtual mGrid;

        public RangePaintEventArgs(GridVirtual grid,
                                   GraphicsCache graphicsCache,
                                   Range drawingRange)
        {
            mGrid = grid;
            GraphicsCache = graphicsCache;
            DrawingRange = drawingRange;
        }

        public GridVirtual Grid
        {
            get { return mGrid; }
        }

        public GraphicsCache GraphicsCache { get; set; }

        public Range DrawingRange { get; set; }
    }

    public delegate void RangePaintEventHandler(GridVirtual sender, RangePaintEventArgs e);

    public class RangeEventArgs : EventArgs
    {
        private readonly Range m_GridRange;

        public RangeEventArgs(Range p_GridRange)
        {
            m_GridRange = p_GridRange;
        }

        public Range Range
        {
            get { return m_GridRange; }
        }
    }

    public delegate void RangeEventHandler(object sender, RangeEventArgs e);

    public class RangeCancelEventArgs : RangeEventArgs
    {
        public RangeCancelEventArgs(Range p_GridRange) : base(p_GridRange)
        {
        }

        public bool Cancel { get; set; }
    }

    public delegate void RangeCancelEventHandler(object sender, RangeCancelEventArgs e);

    public class RangeRegionEventArgs : EventArgs
    {
        private readonly RangeRegion m_GridRangeRegion;

        public RangeRegionEventArgs(RangeRegion p_GridRangeRegion)
        {
            m_GridRangeRegion = p_GridRangeRegion;
        }

        public RangeRegion RangeRegion
        {
            get { return m_GridRangeRegion; }
        }
    }

    public delegate void RangeRegionEventHandler(object sender, RangeRegionEventArgs e);

    public class RangeRegionChangingEventArgs : EventArgs
    {
        private readonly RangeRegion mCurrentRegion;
        private readonly RangeRegion mRegionToExclude;
        private readonly RangeRegion mRegionToInclude;

        public RangeRegionChangingEventArgs(RangeRegion pCurrentRegion, RangeRegion pRangeToExclude,
                                            RangeRegion pRangeToInclude)
        {
            mRegionToExclude = pRangeToExclude;
            mCurrentRegion = pCurrentRegion;
            mRegionToInclude = pRangeToInclude;
        }

        public RangeRegion CurrentRegion
        {
            get { return mCurrentRegion; }
        }

        public RangeRegion RegionToInclude
        {
            get { return mRegionToInclude; }
        }

        public RangeRegion RegionToExclude
        {
            get { return mRegionToExclude; }
        }
    }

    public delegate void RangeRegionChangingEventHandler(object sender, RangeRegionChangingEventArgs e);

    public class RangeRegionCancelEventArgs : RangeRegionEventArgs
    {
        public RangeRegionCancelEventArgs(RangeRegion p_GridRangeRegion) : base(p_GridRangeRegion)
        {
        }

        public bool Cancel { get; set; }
    }

    public delegate void RangeRegionCancelEventHandler(object sender, RangeRegionCancelEventArgs e);

    public class SelectionChangeEventArgs : EventArgs
    {
        private readonly Range m_Range;
        private readonly SelectionChangeEventType m_Type;

        public SelectionChangeEventArgs(SelectionChangeEventType p_Type, Range p_Range)
        {
            m_Type = p_Type;
            m_Range = p_Range;
        }

        public Range Range
        {
            get { return m_Range; }
        }

        public SelectionChangeEventType EventType
        {
            get { return m_Type; }
        }
    }

    public delegate void SelectionChangeEventHandler(object sender, SelectionChangeEventArgs e);

    public class ExceptionEventArgs : EventArgs
    {
        private readonly Exception m_Exception;

        public ExceptionEventArgs(Exception p_Exception)
        {
            m_Exception = p_Exception;
        }

        public Exception Exception
        {
            get { return m_Exception; }
        }

        public bool Handled { get; set; }
    }

    public delegate void ExceptionEventHandler(object sender, ExceptionEventArgs e);

    public class SortRangeRowsEventArgs : EventArgs
    {
        private readonly bool m_bAscending;
        private readonly IComparer m_CellComparer;
        private readonly Range m_Range;
        private readonly int mKeyColumn;

        public SortRangeRowsEventArgs(Range p_Range,
                                      int keyColumn,
                                      bool p_bAscending,
                                      IComparer p_CellComparer)
        {
            m_Range = p_Range;
            mKeyColumn = keyColumn;
            m_bAscending = p_bAscending;
            m_CellComparer = p_CellComparer;
        }

        public Range Range
        {
            get { return m_Range; }
        }

        public int KeyColumn
        {
            get { return mKeyColumn; }
        }

        public bool Ascending
        {
            get { return m_bAscending; }
        }

        public IComparer CellComparer
        {
            get { return m_CellComparer; }
        }
    }

    public delegate void SortRangeRowsEventHandler(object sender, SortRangeRowsEventArgs e);


    public delegate void GridMouseEventHandler(GridVirtual sender, MouseEventArgs e);

    public delegate void GridEventHandler(GridVirtual sender, EventArgs e);

    public delegate void GridDragEventHandler(GridVirtual sender, DragEventArgs e);

    public delegate void GridGiveFeedbackEventHandler(GridVirtual sender, GiveFeedbackEventArgs e);

    public delegate void GridKeyEventHandler(GridVirtual sender, KeyEventArgs e);

    public delegate void GridKeyPressEventHandler(GridVirtual sender, KeyPressEventArgs e);

    /// <summary>
    /// Cell Lost Focus event arguments with the old position and the new position. Extends PositionCancelEventArgs.
    /// </summary>
    public class ChangeActivePositionEventArgs : CancelEventArgs
    {
        private readonly Position mNewFocusPosition;
        private readonly Position mOldFocusPosition;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pOldFocusPosition"></param>
        /// <param name="pNewFocusPosition">If Empty there isn't a cell that will receive the focus.</param>
        public ChangeActivePositionEventArgs(Position pOldFocusPosition, Position pNewFocusPosition) : base(false)
        {
            mOldFocusPosition = pOldFocusPosition;
            mNewFocusPosition = pNewFocusPosition;
        }

        /// <summary>
        /// Position that had the focus
        /// </summary>
        public Position OldFocusPosition
        {
            get { return mOldFocusPosition; }
        }

        /// <summary>
        /// Position that will receive the focus. If Empty there isn't a cell that will receive the focus.
        /// </summary>
        public Position NewFocusPosition
        {
            get { return mNewFocusPosition; }
        }
    }

    public delegate void ChangeActivePositionEventHandler(SelectionBase sender, ChangeActivePositionEventArgs e);

    /// <summary>
    /// Represents the event arguments used when changing a RangeRegion class (like the selection class).
    /// </summary>
    public class RangeRegionChangedEventArgs : EventArgs
    {
        private readonly RangeRegion addedRange;
        private readonly RangeRegion removedRange;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="addedRange"></param>
        /// <param name="removedRange"></param>
        public RangeRegionChangedEventArgs(Range addedRange, Range removedRange)
        {
            if (addedRange.IsEmpty() == false)
                this.addedRange = new RangeRegion(addedRange);
            if (removedRange.IsEmpty() == false)
                this.removedRange = new RangeRegion(removedRange);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="addedRange">Use null if the added range is empty</param>
        /// <param name="removedRange">Use null if the removed range is empty</param>
        public RangeRegionChangedEventArgs(RangeRegion addedRange, RangeRegion removedRange)
        {
            this.addedRange = addedRange;
            this.removedRange = removedRange;
        }

        /// <summary>
        /// Null if the added range is empty
        /// </summary>
        public RangeRegion AddedRange
        {
            get { return addedRange; }
        }

        /// <summary>
        /// Null if the removed range is empty
        /// </summary>
        public RangeRegion RemovedRange
        {
            get { return removedRange; }
        }
    }

    public delegate void RangeRegionChangedEventHandler(object sender, RangeRegionChangedEventArgs e);
}