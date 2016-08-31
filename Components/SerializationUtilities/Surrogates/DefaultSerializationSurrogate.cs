using SerializationUtilities.Attributes;
using SerializationUtilities.SerializationGraph;

namespace SerializationUtilities.Surrogates
{
	[DefaultSerializationSystem(typeof(object),true)]
	public sealed class DefaultSerializationSurrogate : ISerializationSurrogate
	{
		public void GetObjectData(object obj, IObjectGraphNode holder)
		{
			var data = holder.ToSerializationData();
			SerializationServices.PopulateWithFieldData(data, obj, true,false);
		}

		public void SetObjectData(ref object obj, IObjectGraphNode node)
		{
			var data = node.ToSerializationData();
			SerializationServices.ExtractFromFieldData(data,ref obj,true);
		}
	}
}