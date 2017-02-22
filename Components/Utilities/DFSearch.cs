using Utilities.DataStructures;
using System;
using System.Collections.Generic;

namespace Utilities
{
    public class DFSearch<T> where T : class
    {
        public SimpleUnorderedNodeList<T> Open { get; protected set; }
        public List<T> End { get; protected set; }
        public IClosedSet<T> Closed { get; protected set; }

        public T StartNode { get; protected set; }

        public Func<T, IEnumerable<T>> SuccessorFunction { get; protected set; }
        
        
        public DFSearch(Func<T,IEnumerable<T>> successorFunction)
        {
            this.Open = new SimpleUnorderedNodeList<T>();
            this.Closed = new ClosedDictionary<T>();
            this.End = new List<T>();
            this.SuccessorFunction = successorFunction;
        }

        public void InitializeSearch(T start)
        {
            this.StartNode = start;

            var initialNode = new NodeRecord<T>
            {
                node = this.StartNode
            };

            this.Open.Initialize(); 
            this.Open.AddToOpen(initialNode);
            this.Closed.Initialize();
        }

        protected virtual void ProcessChildNode(NodeRecord<T> parentNodeRecord, T childNode)
        {
            var recordChild = new NodeRecord<T>()
            {
                node = childNode,
                parent = parentNodeRecord
            };

            var closedSearch = this.Closed.SearchInClosed(recordChild);
            
            if (closedSearch != null)
            {
                return;
            }

            this.Open.AddToOpen(recordChild);
        }

        public void FullSearch()
        {
            while(this.Open.CountOpen() > 0)
            {
                var bestNodeRecord = this.Open.Pop();
                if(this.Closed.SearchInClosed(bestNodeRecord)!=null)
                {
                    continue;
                }
                this.Closed.AddToClosed(bestNodeRecord);

                var childLess = true;
                foreach(var child in this.SuccessorFunction.Invoke(bestNodeRecord.node))
                {
                    this.ProcessChildNode(bestNodeRecord, child);
                    childLess = false;
                }

                if(childLess)
                {
                    this.End.Add(bestNodeRecord.node);
                }
            }
        }
    }
}
