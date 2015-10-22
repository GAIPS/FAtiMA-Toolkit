public interface ISerializer
{
	void Serialize(System.IO.Stream stream, object graph);
	object Deserialize(System.IO.Stream stream);
}

