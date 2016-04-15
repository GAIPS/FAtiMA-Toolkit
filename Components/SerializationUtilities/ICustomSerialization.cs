namespace GAIPS.Serialization
{
	public interface ICustomSerialization
	{
		void GetObjectData(ISerializationData dataHolder, ISerializationContext context);
		void SetObjectData(ISerializationData dataHolder, ISerializationContext context); 
	}
}