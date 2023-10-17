#region

using System.Collections;
using SourceGrid.Cells;

#endregion

namespace SourceGrid
{
    /// <summary>
    /// A collection of elements of type Cells.ICellVirtual
    /// </summary>
    public class CellCollection : CollectionBase
    {
        /// <summary>
        /// Initializes a new empty instance of the CellBaseCollection class.
        /// </summary>
        public CellCollection()
        {
            // empty
        }

        /// <summary>
        /// Initializes a new instance of the CellBaseCollection class, containing elements
        /// copied from an array.
        /// </summary>
        /// <param name="items">
        /// The array whose elements are to be added to the new CellBaseCollection.
        /// </param>
        public CellCollection(ICellVirtual[] items)
        {
            AddRange(items);
        }

        /// <summary>
        /// Initializes a new instance of the CellBaseCollection class, containing elements
        /// copied from another instance of CellBaseCollection
        /// </summary>
        /// <param name="items">
        /// The CellBaseCollection whose elements are to be added to the new CellBaseCollection.
        /// </param>
        public CellCollection(CellCollection items)
        {
            AddRange(items);
        }

        /// <summary>
        /// Gets or sets the Cells.ICellVirtual at the given index in this CellBaseCollection.
        /// </summary>
        public virtual ICellVirtual this[int index]
        {
            get { return (ICellVirtual) List[index]; }
            set { List[index] = value; }
        }

        /// <summary>
        /// Adds the elements of an array to the end of this CellBaseCollection.
        /// </summary>
        /// <param name="items">
        /// The array whose elements are to be added to the end of this CellBaseCollection.
        /// </param>
        public virtual void AddRange(ICellVirtual[] items)
        {
            foreach (ICellVirtual item in items)
            {
                List.Add(item);
            }
        }

        /// <summary>
        /// Adds the elements of another CellBaseCollection to the end of this CellBaseCollection.
        /// </summary>
        /// <param name="items">
        /// The CellBaseCollection whose elements are to be added to the end of this CellBaseCollection.
        /// </param>
        public virtual void AddRange(CellCollection items)
        {
            foreach (ICellVirtual item in items)
            {
                List.Add(item);
            }
        }

        /// <summary>
        /// Adds an instance of type Cells.ICellVirtual to the end of this CellBaseCollection.
        /// </summary>
        /// <param name="value">
        /// The Cells.ICellVirtual to be added to the end of this CellBaseCollection.
        /// </param>
        public virtual void Add(ICellVirtual value)
        {
            List.Add(value);
        }

        /// <summary>
        /// Determines whether a specfic Cells.ICellVirtual value is in this CellBaseCollection.
        /// </summary>
        /// <param name="value">
        /// The Cells.ICellVirtual value to locate in this CellBaseCollection.
        /// </param>
        /// <returns>
        /// true if value is found in this CellBaseCollection;
        /// false otherwise.
        /// </returns>
        public virtual bool Contains(ICellVirtual value)
        {
            return List.Contains(value);
        }

        /// <summary>
        /// Return the zero-based index of the first occurrence of a specific value
        /// in this CellBaseCollection
        /// </summary>
        /// <param name="value">
        /// The Cells.ICellVirtual value to locate in the CellBaseCollection.
        /// </param>
        /// <returns>
        /// The zero-based index of the first occurrence of the _ELEMENT value if found;
        /// -1 otherwise.
        /// </returns>
        public virtual int IndexOf(ICellVirtual value)
        {
            return List.IndexOf(value);
        }

        /// <summary>
        /// Inserts an element into the CellBaseCollection at the specified index
        /// </summary>
        /// <param name="index">
        /// The index at which the Cells.ICellVirtual is to be inserted.
        /// </param>
        /// <param name="value">
        /// The Cells.ICellVirtual to insert.
        /// </param>
        public virtual void Insert(int index, ICellVirtual value)
        {
            List.Insert(index, value);
        }

        /// <summary>
        /// Removes the first occurrence of a specific Cells.ICellVirtual from this CellBaseCollection.
        /// </summary>
        /// <param name="value">
        /// The Cells.ICellVirtual value to remove from this CellBaseCollection.
        /// </param>
        public virtual void Remove(ICellVirtual value)
        {
            List.Remove(value);
        }

        /// <summary>
        /// Returns an enumerator that can iterate through the elements of this CellBaseCollection.
        /// </summary>
        /// <returns>
        /// An object that implements System.Collections.IEnumerator.
        /// </returns>        
        public new virtual Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        #region Nested type: Enumerator

        /// <summary>
        /// Type-specific enumeration class, used by CellBaseCollection.GetEnumerator.
        /// </summary>
        public class Enumerator : IEnumerator
        {
            private readonly IEnumerator wrapped;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="collection"></param>
            public Enumerator(CellCollection collection)
            {
                wrapped = ((CollectionBase) collection).GetEnumerator();
            }

            /// <summary>
            /// 
            /// </summary>
            public ICellVirtual Current
            {
                get { return (ICellVirtual) (wrapped.Current); }
            }

            #region IEnumerator Members

            /// <summary>
            /// 
            /// </summary>
            object IEnumerator.Current
            {
                get { return (wrapped.Current); }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public bool MoveNext()
            {
                return wrapped.MoveNext();
            }

            /// <summary>
            /// 
            /// </summary>
            public void Reset()
            {
                wrapped.Reset();
            }

            #endregion
        }

        #endregion
    }
}