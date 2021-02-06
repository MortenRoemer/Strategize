using System;
using Strategize.Utility;
using Xunit;

namespace Strategize.Test
{
    public class PriorityHeapTest
    {
        [Fact]
        public void InsertHighPriorityShouldResultInTopPosition()
        {
            var heap = GivenPriorityHeap();
            heap.Insert(10, "White");
            var maxItem = heap.PeekMax();
            
            Assert.Equal(10, maxItem.Priority);
            Assert.Equal("White", maxItem.Item);
            Assert.Equal(6, heap.Count);
        }
        
        [Fact]
        public void InsertExistingItemShouldOverwritePriority()
        {
            var heap = GivenPriorityHeap();
            heap.Insert(10, "Red");
            var maxItem = heap.PeekMax();
            
            Assert.Equal(10, maxItem.Priority);
            Assert.Equal("Red", maxItem.Item);
            Assert.Equal(5, heap.Count);
        }

        [Fact]
        public void RemoveExistingShouldRemoveItem()
        {
            var heap = GivenPriorityHeap();
            heap.Remove("Yellow");
            
            Assert.Equal(4, heap.Count);
        }
        
        [Fact]
        public void RemoveMissingShouldNotDoAnything()
        {
            var heap = GivenPriorityHeap();
            heap.Remove("White");
            
            Assert.Equal(5, heap.Count);
        }

        [Fact]
        public void ClearShouldRemoveAllItems()
        {
            var heap = GivenPriorityHeap();
            heap.Clear();
            
            Assert.Equal(0, heap.Count);
        }
        
        [Fact]
        public void PeekMaxShouldGiveMaxPriorityItemWithoutChangingHeap()
        {
            var heap = GivenPriorityHeap();
            var maxItem = heap.PeekMax();
            
            Assert.Equal(5, maxItem.Priority);
            Assert.Equal("Red", maxItem.Item);
            Assert.Equal(5, heap.Count);
        }

        [Fact]
        public void RemoveMaxShouldGiveAndRemoveMaxPriorityItem()
        {
            var heap = GivenPriorityHeap();
            var maxItem = heap.RemoveMax();
            
            Assert.Equal(5, maxItem.Priority);
            Assert.Equal("Red", maxItem.Item);
            Assert.Equal(4, heap.Count);
        }

        private static IPriorityHeap<byte, string> GivenPriorityHeap()
        {
            var heap = new PriorityHeap<byte, string>();
            heap.Insert(5, "Red");
            heap.Insert(4, "Orange");
            heap.Insert(3, "Yellow");
            heap.Insert(2, "Green");
            heap.Insert(1, "Blue");
            return heap;
        }
    }
}