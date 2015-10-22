public interface ICustomSerialization
{
	void SerializeData(ISerializationData dataHolder);
	void DeserializeData(ISerializationData dataHolder);
}