namespace SerializationUtilities
{
	public interface ISerializationContext
	{
		object Context { get; set; }

		void PushContext();
		void PopContext();
	}
}