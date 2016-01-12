using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KnowledgeBase.WellFormedNames;

namespace KnowledgeBase
{
    public class Belief 
    {
        public Name Name { get; set; }
        public PrimitiveValue Value { get; set; }
        public KnowledgeVisibility Visibility { get; set; }
        public bool IsPersistent { get; set; }
     }
}
