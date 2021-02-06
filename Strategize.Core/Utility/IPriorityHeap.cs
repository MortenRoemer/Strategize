using System;
using System.Collections.Generic;

namespace Strategize.Utility
{
    public interface IPriorityHeap<TPriority, TItem> : IEnumerable<PrioritizedItem<TPriority, TItem>>
        where TPriority : IComparable<TPriority>
    {
        int Count { get; }
        
        void Insert(TPriority priority, TItem item);

        void Remove(TItem item);
        
        void Clear();

        PrioritizedItem<TPriority, TItem> PeekMax();

        PrioritizedItem<TPriority, TItem> RemoveMax();
    }
}