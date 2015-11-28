using KnowledgeBase.WellFormedNames;

namespace KnowledgeBase.Conditions
{
	public class PropertyCondition : ICondition
	{
		public Name Property { get; private set; }
		public object Value { get; set; }

		public PropertyCondition(Name property, object value)
		{
			this.Property = property;
			this.Value = value;
		}

		public bool Evaluate(Memory kb)
		{
			object result = kb.AskProperty(Property);
			if (result == null)
				return Value == null;
			return result.Equals(Value);
		}
	}
}