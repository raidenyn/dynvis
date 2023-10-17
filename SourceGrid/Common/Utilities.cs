#region

using System;
using System.Collections;
using System.Runtime.Serialization;
using SourceGrid.Cells;

#endregion

namespace SourceGrid
{
    //public class Constants
    //{
    //    public const string c_ImagePath = @"SourceGrid2.Common.Icons.";
    //}

    /// <summary>
    /// A comparer for the Cell class. (Not for CellVirtual). Using the value of the cell.
    /// </summary>
    public class ValueCellComparer : IComparer
    {
        #region IComparer Members

        public virtual Int32 Compare(Object x, Object y)
        {
            //Cell object
            if (x == null && y == null)
                return 0;
            if (x == null)
                return -1;
            if (y == null)
                return 1;

            if (x is IComparable)
                return ((IComparable) x).CompareTo(y);
            if (y is IComparable)
                return (-1*((IComparable) y).CompareTo(x));

            //Cell.Value object
            object vx = ((ICell) x).Value;
            object vy = ((ICell) y).Value;
            if (vx == null && vy == null)
                return 0;
            if (vx == null)
                return -1;
            if (vy == null)
                return 1;

            if (vx is IComparable)
                return ((IComparable) vx).CompareTo(vy);
            if (vy is IComparable)
                return (-1*((IComparable) vy).CompareTo(vx));

            throw new ArgumentException("Invalid cell object, no IComparable interface found");
        }

        #endregion
    }


    /// <summary>
    /// A comparer used to sort more than one columns.
    /// </summary>
    public class MultiColumnsComparer : IComparer
    {
        private readonly IComparer m_defaultComparer = new ValueCellComparer();
        private readonly int[] m_SecondarySortColumns;

        public MultiColumnsComparer(params int[] secondarySortColumns)
        {
            m_SecondarySortColumns = secondarySortColumns;
        }

        #region IComparer Members

        public virtual Int32 Compare(Object x, Object y)
        {
            int ret = m_defaultComparer.Compare(x, y);
            if (ret == 0)
            {
                Grid grid = ((ICell) x).Grid;
                int rowX = ((ICell) x).Range.Start.Row;
                int rowY = ((ICell) y).Range.Start.Row;


                int indexOrder = 0;
                while (indexOrder < m_SecondarySortColumns.Length)
                {
                    ICellVirtual otherX = grid.GetCell(rowX, m_SecondarySortColumns[indexOrder]);
                    ICellVirtual otherY = grid.GetCell(rowY, m_SecondarySortColumns[indexOrder]);

                    int subRet = m_defaultComparer.Compare(otherX, otherY);
                    if (subRet != 0)
                        return subRet;

                    indexOrder++;
                }

                return 0;
            }
            else
                return ret;
        }

        #endregion
    }


    /// <summary>
    /// A comparer for the Cell class. (Not for CellVirtual). Using the DisplayString of the cell.
    /// </summary>
    public class DisplayStringCellComparer : IComparer
    {
        #region IComparer Members

        public virtual Int32 Compare(Object x, Object y)
        {
            //Cell object
            if (x == null && y == null)
                return 0;
            if (x == null)
                return -1;
            if (y == null)
                return 1;

            if (x is IComparable)
                return ((IComparable) x).CompareTo(y);
            if (y is IComparable)
                return (-1*((IComparable) y).CompareTo(x));

            //Cell.Value object
            string vx = ((ICell) x).DisplayText;
            string vy = ((ICell) y).DisplayText;
            if (vx == null && vy == null)
                return 0;
            if (vx == null)
                return -1;
            if (vy == null)
                return 1;

            return vx.CompareTo(vy);
        }

        #endregion
    }

    [Serializable]
    public class SourceGridException : ApplicationException
    {
        public SourceGridException(string p_strErrDescription) :
            base(p_strErrDescription)
        {
        }

        public SourceGridException(string p_strErrDescription, Exception p_InnerException) :
            base(p_strErrDescription, p_InnerException)
        {
        }

        protected SourceGridException(SerializationInfo p_Info, StreamingContext p_StreamingContext) :
            base(p_Info, p_StreamingContext)
        {
        }
    }

    [Serializable]
    public class EditingCellException : SourceGridException
    {
        public EditingCellException(Exception innerException) :
            base(innerException.Message, innerException)
        {
        }

        protected EditingCellException(SerializationInfo p_Info, StreamingContext p_StreamingContext) :
            base(p_Info, p_StreamingContext)
        {
        }
    }
}