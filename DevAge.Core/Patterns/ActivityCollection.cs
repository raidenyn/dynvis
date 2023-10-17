#region

using System;
using System.Collections;

#endregion

namespace DevAge.Patterns
{
    /// <summary>
    /// A collection of elements of type Activity
    /// </summary>
    public class ActivityCollection : ICollection
    {
        private readonly ArrayList mList = new ArrayList();
        private readonly IActivity mParentActivity;

        /// <summary>
        /// Initializes a new empty instance of the ActivityCollection class.
        /// </summary>
        public ActivityCollection(IActivity parentActivity)
        {
            mParentActivity = parentActivity;
        }

        /// <summary>
        /// Gets or sets the IActivity at the given index in this ActivityCollection.
        /// </summary>
        public virtual IActivity this[int index]
        {
            get { return (IActivity) mList[index]; }
        }

        #region ICollection Members

        public int Count
        {
            get { return mList.Count; }
        }

        public bool IsSynchronized
        {
            get { return mList.IsSynchronized; }
        }

        public void CopyTo(Array array, int index)
        {
            mList.CopyTo(array, index);
        }

        public object SyncRoot
        {
            get { return mList.SyncRoot; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        private void CheckParent()
        {
            //Tolto perchè voglio permettere la modifica delle collection anche durante il calcolo
//			if (mParentActivity.Status == ActivityStatus.Running)
//				throw new DevAgeApplicationException("Cannot add or remove activities while in running status");
        }


        /// <summary>
        /// Determines whether a specfic IActivity value is in this ActivityCollection.
        /// </summary>
        /// <param name="value">
        /// The IActivity value to locate in this ActivityCollection.
        /// </param>
        /// <returns>
        /// true if value is found in this ActivityCollection;
        /// false otherwise.
        /// </returns>
        public virtual bool Contains(IActivity value)
        {
            return mList.Contains(value);
        }

        /// <summary>
        /// Return the zero-based index of the first occurrence of a specific value
        /// in this ActivityCollection
        /// </summary>
        /// <param name="value">
        /// The IActivity value to locate in the ActivityCollection.
        /// </param>
        /// <returns>
        /// The zero-based index of the first occurrence of the _ELEMENT value if found;
        /// -1 otherwise.
        /// </returns>
        public virtual int IndexOf(IActivity value)
        {
            return mList.IndexOf(value);
        }

        /// <summary>
        /// Adds an instance of type IActivity to the end of this ActivityCollection.
        /// </summary>
        /// <param name="value">
        /// The IActivity to be added to the end of this ActivityCollection.
        /// </param>
        public virtual void Add(IActivity value)
        {
            Insert(Count, value);
        }

        /// <summary>
        /// Inserts an element into the ActivityCollection at the specified index
        /// </summary>
        /// <param name="index">
        /// The index at which the IActivity is to be inserted.
        /// </param>
        /// <param name="value">
        /// The IActivity to insert.
        /// </param>
        public virtual void Insert(int index, IActivity value)
        {
            CheckParent();

            mList.Insert(index, value);

            value.Parent = mParentActivity;
        }

        /// <summary>
        /// Removes the first occurrence of a specific IActivity from this ActivityCollection.
        /// </summary>
        /// <param name="value">
        /// The IActivity value to remove from this ActivityCollection.
        /// </param>
        public virtual void Remove(IActivity value)
        {
            CheckParent();

            mList.Remove(value);

            value.Parent = null;
        }

        /// <summary>
        /// Returns an enumerator that can iterate through the elements of this ActivityCollection.
        /// </summary>
        /// <returns>
        /// An object that implements System.Collections.IEnumerator.
        /// </returns>        
        public virtual Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        #region Nested type: Enumerator

        /// <summary>
        /// Type-specific enumeration class, used by ActivityCollection.GetEnumerator.
        /// </summary>
        public class Enumerator : IEnumerator
        {
            private readonly IEnumerator wrapped;

            public Enumerator(ActivityCollection collection)
            {
                wrapped = collection.mList.GetEnumerator();
            }

            public IActivity Current
            {
                get { return (IActivity) (wrapped.Current); }
            }

            #region IEnumerator Members

            object IEnumerator.Current
            {
                get { return (wrapped.Current); }
            }

            public bool MoveNext()
            {
                return wrapped.MoveNext();
            }

            public void Reset()
            {
                wrapped.Reset();
            }

            #endregion
        }

        #endregion
    }
}