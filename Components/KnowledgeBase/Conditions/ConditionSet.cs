using System.Collections.Generic;
using System.Linq;

namespace KnowledgeBase.Conditions
{
	public class ConditionSet : HashSet<ICondition>, ICondition
	{
		public enum Operation : byte
		{
			And,
			Or
		}

		public Operation operation { get; set; }

		public ConditionSet(Operation operation, IEnumerable<ICondition> conditions) : base(conditions)
		{
			this.operation = operation;
		}

		public bool Evaluate(Memory kb)
		{
			return operation == Operation.Or ? this.Any(c => c.Evaluate(kb)) : this.All(c => c.Evaluate(kb));
		}
	}
}