using System.Collections.Generic;

namespace Utilities.DataStructures
{
    public interface IClosedSet<T>  where T : class
    {
        void Initialize();
        void AddToClosed(NodeRecord<T> nodeRecord);
        void RemoveFromClosed(NodeRecord<T> nodeRecord);
        //should return null if the node is not found
        NodeRecord<T> SearchInClosed(NodeRecord<T> nodeRecord);
        ICollection<NodeRecord<T>> All();
    }
}
