using System;
using System.Collections;
using System.Collections.Generic;

namespace Strategize.Utility
{
    public class PriorityHeap<TPriority, TItem> : IPriorityHeap<TPriority,TItem>
        where TPriority : IComparable<TPriority>
    {
        public void Clear()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<PrioritizedItem<TPriority, TItem>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count { get; }

        public PrioritizedItem<TPriority, TItem> this[int index] => throw new NotImplementedException();
    }
}