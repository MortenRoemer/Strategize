using System;
using System.Collections;
using System.Collections.Generic;

namespace Strategize.Utility
{
    public class PriorityHeap<TPriority, TItem> : IPriorityHeap<TPriority,TItem>
        where TPriority : IComparable<TPriority>
    {
        #region Exposed

        public PriorityHeap(int capacity = DEFAULT_CAPACITY)
        {
            _buffer = new PrioritizedItem<TPriority, TItem>[capacity];
            _enumerator = new Enumerator();
            Clear();
        }
        
        public IEnumerator<PrioritizedItem<TPriority, TItem>> GetEnumerator()
        {
            _enumerator.Prepare(_buffer, Count);
            return _enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count { get; private set; }

        public void Insert(TPriority priority, TItem item)
        {
            EnsureCapacity();

            for (var index = 0; index < Count; index++)
            {
                var heapItem = _buffer[index];

                if (!heapItem.Item.Equals(item))
                    continue;
                
                _buffer[index] = new PrioritizedItem<TPriority, TItem>(priority, item);
                return;
            }

            _buffer[Count++] = new PrioritizedItem<TPriority, TItem>(priority, item);
            _maxPriorityIndex = -1;
        }

        public void Remove(TItem item)
        {
            for (var index = 0; index < Count; index++)
            {
                var heapItem = _buffer[index];
                
                if (!heapItem.Item.Equals(item))
                    continue;
                
                RemoveAt(index);

                if (index >= _maxPriorityIndex)
                    _maxPriorityIndex = -1;

                return;
            }
        }

        public void Clear()
        {
            Count = 0;
            _maxPriorityIndex = -1;
        }

        public PrioritizedItem<TPriority, TItem> PeekMax()
        {
            var index = FindMaxPriority();
            return index >= 0 ? _buffer[index] : default;
        }

        public PrioritizedItem<TPriority, TItem> RemoveMax()
        {
            var index = FindMaxPriority();

            if (index < 0)
                return default;

            var result = _buffer[index];
            RemoveAt(index);
            return result;
        }

        #endregion

        #region Internal

        private const int DEFAULT_CAPACITY = 10;
        
        private PrioritizedItem<TPriority, TItem>[] _buffer;
        private int _maxPriorityIndex;
        private readonly Enumerator _enumerator;

        private void EnsureCapacity()
        {
            if (Count < _buffer.Length)
                return;

            var newBuffer = new PrioritizedItem<TPriority, TItem>[_buffer.Length + DEFAULT_CAPACITY];
            Array.Copy(_buffer, newBuffer, Count);
            _buffer = newBuffer;
        }

        private int FindMaxPriority()
        {
            if (_maxPriorityIndex >= 0)
                return _maxPriorityIndex;

            TPriority maxPriority = default;
            var maxIndex = -1;
            
            for (var index = 0; index < Count; index++)
            {
                var heapItem = _buffer[index];

                if (heapItem.Priority.CompareTo(maxPriority) > 0)
                {
                    maxPriority = heapItem.Priority;
                    maxIndex = index;
                }
            }

            _maxPriorityIndex = maxIndex;
            return _maxPriorityIndex;
        }

        private void RemoveAt(int index)
        {
            Array.Copy(_buffer, index + 1, _buffer, index, Count - index - 1);
            Count--;
        }

        private class Enumerator : IEnumerator<PrioritizedItem<TPriority, TItem>>
        {
            public bool MoveNext()
            {
                return ++_count < _count;
            }

            public void Reset()
            {
                _currentIndex = -1;
            }

            public void Prepare(PrioritizedItem<TPriority, TItem>[] heap, int count)
            {
                _heap = heap;
                _currentIndex = -1;
                _count = count;
            }

            public PrioritizedItem<TPriority, TItem> Current => _heap[_currentIndex];
            object IEnumerator.Current => Current;
            private PrioritizedItem<TPriority, TItem>[] _heap;
            private int _currentIndex;
            private int _count;

            public void Dispose()
            {
                // There is nothing to dispose
            }
        }

        #endregion
    }
}