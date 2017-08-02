namespace SerializationUtilities
{
	public interface ICustomSerialization
	{
		void GetObjectData(ISerializationData dataHolder, ISerializationContext context);
		void SetObjectData(ISerializationData dataHolder, ISerializationContext context); 
	}
}