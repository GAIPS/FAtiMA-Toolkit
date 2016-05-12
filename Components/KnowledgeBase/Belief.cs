using KnowledgeBase.WellFormedNames;

namespace KnowledgeBase
{
    public class Belief 
    {
		//public Guid Id { get; set; }
        public Name Name { get; set; }
		public Name Perspective { get; set; }
		public PrimitiveValue Value { get; set; }
     }
}
