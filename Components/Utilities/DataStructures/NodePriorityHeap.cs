using System.Collections.Generic;

namespace Utilities.DataStructures
{
    public class NodePriorityHeap<T> : IOpenSet<T> where T : class
    {
        PriorityHeap<NodeRecord<T>> OpenHeap { get; set; }

        public NodePriorityHeap()
        {
            this.OpenHeap = new PriorityHeap<NodeRecord<T>>();
        }
 
        public void Initialize()
        {
            this.OpenHeap.Clear();
        }

        public void Replace(NodeRecord<T> nodeToBeReplaced, NodeRecord<T> nodeToReplace)
        {
            this.OpenHeap.Remove(nodeToBeReplaced);
            this.OpenHeap.Enqueue(nodeToReplace);
        }

        public NodeRecord<T> GetBestAndRemove()
        {
            return this.OpenHeap.Dequeue();
        }

        public NodeRecord<T> PeekBest()
        {
            return this.OpenHeap.Peek();
        }

        public void AddToOpen(NodeRecord<T> nodeRecord)
        {
            this.OpenHeap.Enqueue(nodeRecord);
        }

        public void RemoveFromOpen(NodeRecord<T> nodeRecord)
        {
            this.OpenHeap.Remove(nodeRecord);
        }

        public NodeRecord<T> SearchInOpen(NodeRecord<T> nodeRecord)
        {
            return this.OpenHeap.SearchForEqual(nodeRecord);
        }

        public ICollection<NodeRecord<T>> All()
        {
            return this.OpenHeap;
        }

        public int CountOpen()
        {
            return this.OpenHeap.Count;
        }
    }
}
