using System;
using System.Globalization;

public sealed class PrimitiveGraphNode : SerializationGraphNode{
	public ValueType Value;
	
	public PrimitiveGraphNode(ValueType v){
		this.Value = v;
	}
	
	public object RebuildObject (SerializedGraph graph, Type knowType)
	{
		if(knowType==null || (Value.GetType() == knowType))
			return Value;
		
		return Convert.ChangeType(Value, knowType,  CultureInfo.InvariantCulture);
	}
}