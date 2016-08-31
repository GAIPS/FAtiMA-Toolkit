using SerializationUtilities.Attributes;
using SerializationUtilities.SerializationGraph;

namespace SerializationUtilities.Surrogates
{
	[DefaultSerializationSystem(typeof(ICustomSerialization),true)]
	public class CustomSerializationSurrogate : ISerializationSurrogate
	{
		public void GetObjectData(object obj, IObjectGraphNode holder)
		{
			((ICustomSerialization)obj).GetObjectData(holder.ToSerializationData(),holder.ParentGraph.Context);
		}

		public void SetObjectData(ref object obj, IObjectGraphNode node)
		{
			var data = node.ToSerializationData();
			((ICustomSerialization)obj).SetObjectData(data, node.ParentGraph.Context);
		}
	}
}