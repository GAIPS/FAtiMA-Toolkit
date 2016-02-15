using GAIPS.Serialization.Attributes;
using GAIPS.Serialization.SerializationGraph;

namespace GAIPS.Serialization.Surrogates
{
	[DefaultSerializationSystem(typeof(ICustomSerialization),true)]
	public class CustomSerializationSurrogate : ISerializationSurrogate
	{
		public void GetObjectData(object obj, IObjectGraphNode holder)
		{
			((ICustomSerialization)obj).GetObjectData(new SerializationData(holder));
		}

		public void SetObjectData(ref object obj, IObjectGraphNode node)
		{
			var data = new SerializationData(node);
			((ICustomSerialization)obj).SetObjectData(data);
		}
	}
}