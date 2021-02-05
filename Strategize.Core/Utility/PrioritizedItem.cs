using System;

namespace Strategize.Utility
{
    public readonly struct PrioritizedItem<TPriority, TItem> : IComparable<PrioritizedItem<TPriority, TItem>>
        where TPriority : IComparable<TPriority>
    {
        public PrioritizedItem(TPriority priority, TItem item)
        {
            Priority = priority;
            Item = item;
        }
        
        public TPriority Priority { get; }
        public TItem Item{ get; }

        public int CompareTo(PrioritizedItem<TPriority, TItem> other)
        {
            return Priority.CompareTo(other.Priority);
        }

        public override string ToString()
        {
            return $"[{Priority}] {Item}";
        }
    }
}