using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ObjectValidator.Common
{
    public class ObservableCollection<T> : IEnumerable<T>, IList<T>
    {
        private readonly List<T> m_List = new List<T>();

        public int Count { get { return m_List.Count; } }

        public bool IsReadOnly { get { return false; } }

        public T this[int index]
        {
            get
            {
                return m_List[index];
            }

            set
            {
                var old = m_List[index];
                m_List[index] = value;
                CallCollectionChanged(new NotifyCollectionChangedEventArgs<T>(
                    NotifyCollectionChangedAction.Replace, new List<T>() { value }, new List<T>() { old }));
            }
        }

        public virtual event NotifyCollectionChangedEventHandler<T> CollectionChanged;

        private void CallCollectionChanged(NotifyCollectionChangedEventArgs<T> args)
        {
            if (CollectionChanged != null)
                CollectionChanged(this, args);
        }

        bool ICollection<T>.Remove(T item)
        {
            Remove(item);
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return m_List.GetEnumerator();
        }

        public void Add(T item)
        {
            ParamHelper.CheckParamNull(item, "item", "item can't be null");

            m_List.Add(item);

            CallCollectionChanged(new NotifyCollectionChangedEventArgs<T>(
                NotifyCollectionChangedAction.Add, new List<T>() { item }, null));
        }

        public void Remove(T item)
        {
            m_List.Remove(item);
            CallCollectionChanged(new NotifyCollectionChangedEventArgs<T>(
                    NotifyCollectionChangedAction.Remove, null, new List<T>() { item }));
        }

        public int IndexOf(T item)
        {
            return m_List.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            m_List.Insert(index, item);
            CallCollectionChanged(new NotifyCollectionChangedEventArgs<T>(
                    NotifyCollectionChangedAction.Add, new List<T>() { item }, null));
        }

        public void RemoveAt(int index)
        {
            var old = m_List[index];
            m_List.RemoveAt(index);
            CallCollectionChanged(new NotifyCollectionChangedEventArgs<T>(
                NotifyCollectionChangedAction.Remove, null, new List<T>() { old }));
        }

        public void Clear()
        {
            var old = m_List.ToList();
            m_List.Clear();
            CallCollectionChanged(new NotifyCollectionChangedEventArgs<T>(
                    NotifyCollectionChangedAction.Reset, null, old));
        }

        public bool Contains(T item)
        {
            return m_List.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            m_List.CopyTo(array, arrayIndex);
        }
    }
}