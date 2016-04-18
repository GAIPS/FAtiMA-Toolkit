namespace GAIPS.Serialization
{
	public interface ISerializationContext
	{
		object Context { get; set; }

		void PushContext();
		void PopContext();
	}
}