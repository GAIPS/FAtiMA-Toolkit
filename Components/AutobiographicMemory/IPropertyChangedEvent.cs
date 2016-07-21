using WellFormedNames;

namespace AutobiographicMemory
{
	public interface IPropertyChangedEvent : IBaseEvent
	{
		Name Property { get;}
		Name NewValue { get; }
	}
}