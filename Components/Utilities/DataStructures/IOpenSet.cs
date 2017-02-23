using System.Collections.Generic;

namespace Utilities.DataStructures
{
    public interface IOpenSet<T> where T : class
    {
        void Initialize();
        void Replace(NodeRecord<T> nodeToBeReplaced, NodeRecord<T> nodeToReplace);
        NodeRecord<T> GetBestAndRemove();
        NodeRecord<T> PeekBest();
        void AddToOpen(NodeRecord<T> nodeRecord);
        void RemoveFromOpen(NodeRecord<T> nodeRecord);
        //should return null if the node is not found
        NodeRecord<T> SearchInOpen(NodeRecord<T> nodeRecord);
        ICollection<NodeRecord<T>> All();
        int CountOpen();
    }
}
