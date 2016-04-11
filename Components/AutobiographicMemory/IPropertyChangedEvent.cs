using KnowledgeBase.WellFormedNames;

namespace AutobiographicMemory
{
	public interface IPropertyChangedEvent : IBaseEvent
	{
		Name Property { get;}
		string NewValue { get; }
	}
}