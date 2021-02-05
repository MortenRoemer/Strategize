using System;
using System.Collections.Generic;

namespace Strategize.Utility
{
    public interface IPriorityHeap<TPriority, TItem> : IReadOnlyList<PrioritizedItem<TPriority, TItem>>
        where TPriority : IComparable<TPriority>
    {
        void Clear();
        
        
    }
}