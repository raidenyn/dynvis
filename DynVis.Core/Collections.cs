using System;
using System.Collections;
using System.Collections.Generic;
using IEnumerator=System.Collections.IEnumerator;

namespace DynVis.Core
{
    public interface IStaticCollection<T>: IEnumerable<T>
    {
        int Count { get; }

        T this[int index] { get; }

        event EventHandler CountItemChanged;
    }

    public interface ICurrentItems<T>
    {
        T CurrentItem { get; set; }

        event EventHandler CurrentItemChanged;
    }

    public interface IStaticCollectionWithCurrentItems<T> : IStaticCollection<T>, ICurrentItems<T>
    {

    }

    public interface ICollectionWithCurrentItems<T> : ICollection<T>, ICurrentItems<T>
    {
        T this[int index] { get; }

        event EventHandler CountItemChanged;
    }

    public class CollectionWithCurrentItems<T> : ICollection<T>, IStaticCollectionWithCurrentItems<T> 
    {
        private readonly List<T> Paths = new List<T>();

        private T _CurrentItem;
        public T CurrentItem
        {
            get { return _CurrentItem; }
            set
            {
                if (value == null || Contains(value))
                {
                    _CurrentItem = value;
                    RiseCurrentItemChanged();
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        public T this[int index]
        {
            get { return Paths[index]; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Paths.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            Paths.Add(item);
            RiseCountItemChanged();
        }

        public void Clear()
        {
            Paths.Clear();
            CurrentItem = default(T);
            RiseCountItemChanged();
        }

        public bool Contains(T item)
        {
            return Paths.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Paths.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            var index = -1;
            if (item.Equals(CurrentItem))
            {
                index = Paths.IndexOf(item);
                
            }
            var result = Paths.Remove(item);

            if (index >= 0)
            {
                if (index >= Count) index = Count - 1;
                CurrentItem = index >= 0 ? Paths[index] : default(T);
            }

            if (result) RiseCountItemChanged();
            return result;
        }

        public int Count
        {
            get { return Paths.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public event EventHandler CurrentItemChanged;
        private void RiseCurrentItemChanged()
        {
            if (CurrentItemChanged != null) CurrentItemChanged(this, new EventArgs());
        }

        public event EventHandler CountItemChanged;
        private void RiseCountItemChanged()
        {
            if (CountItemChanged != null) CountItemChanged(this, new EventArgs());
        }
    }
}
