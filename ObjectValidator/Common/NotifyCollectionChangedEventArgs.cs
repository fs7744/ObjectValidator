using System;
using System.Collections.Generic;

namespace ObjectValidator.Common
{
    public class NotifyCollectionChangedEventArgs<T> : EventArgs
    {
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList<T> newItems, IList<T> oldItems)
        {
            Action = action;
            NewItems = newItems;
            OldItems = oldItems;
        }

        public NotifyCollectionChangedAction Action { get; protected set; }

        public IList<T> NewItems { get; internal set; }

        public IList<T> OldItems { get; internal set; }
    }
}