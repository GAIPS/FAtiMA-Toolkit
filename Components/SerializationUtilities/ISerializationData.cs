using System;
using System.Collections.Generic;
using GAIPS.Serialization.SerializationGraph;

namespace GAIPS.Serialization
{
	public interface ISerializationData
	{
		void SetValue(string name, object value);
		void SetValue<T>(string name, T value);
		void SetValue(string name, object value, Type expectedType);
		
		object GetValue(string name);
		T GetValue<T>(string name);
		object GetValue(string name, Type expectedType);

		void SetValueGraphNode(string name, IGraphNode node);
		IGraphNode GetValueGraphNode(string name);

		ISerializationFieldEnumerator GetEnumerator();

		Graph ParentGraph { get; }
	}

	public interface ISerializationFieldEnumerator : IEnumerator<IGraphNode>
	{
		string FieldName { get; }
		object BuildValue();
		T BuildValue<T>();
		object BuildValue(Type type);
	}
}