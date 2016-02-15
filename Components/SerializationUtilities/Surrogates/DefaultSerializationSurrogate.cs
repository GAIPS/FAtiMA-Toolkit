using GAIPS.Serialization.Attributes;
using GAIPS.Serialization.SerializationGraph;

namespace GAIPS.Serialization.Surrogates
{
	[DefaultSerializationSystem(typeof(object),true)]
	public sealed class DefaultSerializationSurrogate : ISerializationSurrogate
	{
		public void GetObjectData(object obj, IObjectGraphNode holder)
		{
			var data = new SerializationData(holder);
			SerializationServices.PopulateWithFieldData(data, obj, true,false);
		}

		public void SetObjectData(ref object obj, IObjectGraphNode node)
		{
			var data = new SerializationData(node);
			SerializationServices.ExtractFromFieldData(data,ref obj,true);
		}
	}
}