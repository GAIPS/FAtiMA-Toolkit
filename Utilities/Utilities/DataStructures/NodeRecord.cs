using System;

namespace Utilities.DataStructures
{
    public enum NodeStatus
    {
        Unvisited,
        Open,
        Closed
    }

    public class NodeRecord<T>  : IComparable<NodeRecord<T>> where T : class
    {
        public T node;
        public NodeRecord<T> parent;
        public float gValue;
        public float hValue;
        public float fValue;
        public NodeStatus status;

        public int CompareTo(NodeRecord<T> other)
        {
            return this.fValue.CompareTo(other.fValue);
        }

        //two node records are equal if they refer to the same node
        public override bool Equals(object obj)
        {
            var target = obj as NodeRecord<T>;
            if (target != null) return this.node == target.node;
            else return false;
        }

        public override int GetHashCode()
        {
            return this.node.GetHashCode();
        }
    }
}
