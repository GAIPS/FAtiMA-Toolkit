using System.Collections.Generic;

namespace Utilities.DataStructures
{
    public class ClosedDictionary<T> : IClosedSet<T> where T : class
    {
        private Dictionary<T,NodeRecord<T>> Closed { get; set; }

        public ClosedDictionary()
        {
            this.Closed = new Dictionary<T, NodeRecord<T>>();
        }
        public void Initialize()
        {
            this.Closed.Clear();
        }

        public void AddToClosed(NodeRecord<T> nodeRecord)
        {
            this.Closed.Add(nodeRecord.node,nodeRecord);
        }

        public void RemoveFromClosed(NodeRecord<T> nodeRecord)
        {
            this.Closed.Remove(nodeRecord.node);
        }

        public NodeRecord<T> SearchInClosed(NodeRecord<T> nodeRecord)
        {
            if (this.Closed.ContainsKey(nodeRecord.node))
            {
                return this.Closed[nodeRecord.node];
            }
            else return null;
        }

        public ICollection<NodeRecord<T>> All()
        {
            return this.Closed.Values;
        }
    }
}
