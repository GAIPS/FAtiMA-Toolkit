/*
 * This code implements priority queue which uses min-heap as underlying storage
 * 
 * Copyright (C) 2010 Alexey Kurakin
 * www.avk.name
 * alexey[ at ]kurakin.me
 */

// joao dias - Simplification of the original file for the IAJ course
// Instead of using an explicit comparer and store a KeyValuePair with priority (like it was done in the original version),
// we just enforce that the element Type stored in the Heap is Comparable
// this saves memory and simplifies the code (at the expense of less flexibility)

using System;
using System.Collections;
using System.Collections.Generic;

namespace Utilities.DataStructures
{
    /// <summary>
    /// Priority queue based on binary heap,
    /// Elements with minimum priority dequeued first
    /// </summary>
    /// <typeparam name="TValue">Type of values</typeparam>
    public class PriorityHeap<TValue> : ICollection<TValue> where TValue : IComparable<TValue>
    {
        private List<TValue> _baseHeap;
        #region Constructors
        public PriorityHeap()
        {
            this._baseHeap = new List<TValue>();
        }

        /// <summary>
        /// Initializes a new instance of priority queue with specified initial capacity and default priority comparer
        /// </summary>
        /// <param name="capacity">initial capacity</param>
        public PriorityHeap(int capacity)
        {
            this._baseHeap = new List<TValue>(capacity);
        }
        #endregion

        #region Priority queue operations

        /// <summary>
        /// Enqueues element into priority queue
        /// </summary>
        /// <param name="value">element value</param>
        public void Enqueue(TValue value)
        {
            Insert(value);
        }

        /// <summary>
        /// Dequeues element with minimum priority and return it/> 
        /// </summary>
        /// <returns>dequeued element</returns>
        /// <remarks>
        /// Method throws <see cref="InvalidOperationException"/> if priority queue is empty
        /// </remarks>
        public TValue Dequeue()
        {
            if (!IsEmpty)
            {
                var result = _baseHeap[0];
                DeleteRoot();
                return result;
            }
            else
                throw new InvalidOperationException("Priority queue is empty");
        }

      

        /// <summary>
        /// Returns priority and value of the element with minimun priority, without removing it from the queue
        /// </summary>
        /// <returns>priority and value of the element with minimum priority</returns>
        /// <remarks>
        /// Method throws <see cref="InvalidOperationException"/> if priority queue is empty
        /// </remarks>
        public TValue Peek()
        {
            if (!IsEmpty)
                return _baseHeap[0];
            else
                throw new InvalidOperationException("Priority queue is empty");
        }

        /// <summary>
        /// Gets whether priority queue is empty
        /// </summary>
        public bool IsEmpty
        {
            get { return _baseHeap.Count == 0; }
        }

        #endregion

        #region Heap operations

        private void ExchangeElements(int pos1, int pos2)
        {
            var val = _baseHeap[pos1];
            _baseHeap[pos1] = _baseHeap[pos2];
            _baseHeap[pos2] = val;
        }

        private void Insert(TValue value)
        {
            _baseHeap.Add(value);

            // heap[i] have children heap[2*i + 1] and heap[2*i + 2] and parent heap[(i-1)/ 2];

            // heapify after insert, from end to beginning
            HeapifyFromEndToBeginning(_baseHeap.Count - 1);
        }


        private int HeapifyFromEndToBeginning(int pos)
        {
            if (pos >= _baseHeap.Count) return -1;

            while (pos > 0)
            {
                int parentPos = (pos - 1) / 2;
                if (_baseHeap[parentPos].CompareTo(_baseHeap[pos]) > 0)
                {
                    ExchangeElements(parentPos, pos);
                    pos = parentPos;
                }
                else break;
            }
            return pos;
        }


        private void DeleteRoot()
        {
            if (_baseHeap.Count <= 1)
            {
                _baseHeap.Clear();
                return;
            }

            _baseHeap[0] = _baseHeap[_baseHeap.Count - 1];
            _baseHeap.RemoveAt(_baseHeap.Count - 1);

            // heapify
            HeapifyFromBeginningToEnd(0);
        }

        private void HeapifyFromBeginningToEnd(int pos)
        {
            if (pos >= _baseHeap.Count) return;

            // heap[i] have children heap[2*i + 1] and heap[2*i + 2] and parent heap[(i-1)/ 2];

            while (true)
            {
                // on each iteration exchange element with its smallest child
                int smallest = pos;
                int left = 2 * pos + 1;
                int right = 2 * pos + 2;
                if (left < _baseHeap.Count && _baseHeap[smallest].CompareTo(_baseHeap[left]) > 0)
                    smallest = left;
                if (right < _baseHeap.Count && _baseHeap[smallest].CompareTo(_baseHeap[right]) > 0)
                    smallest = right;

                if (smallest != pos)
                {
                    ExchangeElements(smallest, pos);
                    pos = smallest;
                }
                else break;
            }
        }

        #endregion

        #region ICollection<KeyValuePair<TPriority, TValue>> implementation

        /// <summary>
        /// Enqueus element into priority queue
        /// </summary>
        /// <param name="item">element to add</param>
        public void Add(TValue item)
        {
            Enqueue(item);
        }

        /// <summary>
        /// Clears the collection
        /// </summary>
        public void Clear()
        {
            _baseHeap.Clear();
        }

        /// <summary>
        /// Determines whether the priority queue contains a specific element
        /// </summary>
        /// <param name="item">The object to locate in the priority queue</param>
        /// <returns><c>true</c> if item is found in the priority queue; otherwise, <c>false.</c> </returns>
        public bool Contains(TValue item)
        {
            return _baseHeap.Contains(item);
        }

        /// <summary>
        /// Gets number of elements in the priority queue
        /// </summary>
        public int Count
        {
            get { return _baseHeap.Count; }
        }

        /// <summary>
        /// Copies the elements of the priority queue to an Array, starting at a particular Array index. 
        /// </summary>
        /// <param name="array">The one-dimensional Array that is the destination of the elements copied from the priority queue. The Array must have zero-based indexing. </param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <remarks>
        /// It is not guaranteed that items will be copied in the sorted order.
        /// </remarks>
        public void CopyTo(TValue[] array, int arrayIndex)
        {
            _baseHeap.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets a value indicating whether the collection is read-only. 
        /// </summary>
        /// <remarks>
        /// For priority queue this property returns <c>false</c>.
        /// </remarks>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the priority queue. 
        /// </summary>
        /// <param name="item">The object to remove from the ICollection <(Of <(T >)>). </param>
        /// <returns><c>true</c> if item was successfully removed from the priority queue.
        /// This method returns false if item is not found in the collection. </returns>
        public bool Remove(TValue item)
        {
            // find element in the collection and remove it
            int elementIdx = _baseHeap.IndexOf(item);
            if (elementIdx < 0) return false;

            //remove element
            _baseHeap[elementIdx] = _baseHeap[_baseHeap.Count - 1];
            _baseHeap.RemoveAt(_baseHeap.Count - 1);

            // heapify
            int newPos = HeapifyFromEndToBeginning(elementIdx);
            if (newPos == elementIdx)
                HeapifyFromBeginningToEnd(elementIdx);

            return true;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>Enumerator</returns>
        /// <remarks>
        /// Returned enumerator does not iterate elements in sorted order.</remarks>
        public IEnumerator<TValue> GetEnumerator()
        {
            return _baseHeap.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>Enumerator</returns>
        /// <remarks>
        /// Returned enumerator does not iterate elements in sorted order.</remarks>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public TValue SearchForEqual(TValue item)
        {
            var count = this._baseHeap.Count;
            for (int i = 0; i < count; i++)
            {
                if (this._baseHeap[i].Equals(item))
                {
                    
                    return this._baseHeap[i];
                }
            }
            return default(TValue);
        }

        #endregion
    }
}

