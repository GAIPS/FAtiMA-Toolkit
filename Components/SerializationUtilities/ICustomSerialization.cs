namespace GAIPS.Serialization
{
	public interface ICustomSerialization
	{
		void GetObjectData(ISerializationData dataHolder);
		void SetObjectData(ISerializationData dataHolder); 
	}
}