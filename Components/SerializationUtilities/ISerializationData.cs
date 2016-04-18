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

		bool ContainsField(string name);
		bool RenameField(string oldFieldName, string newFieldName);

		void SetValueGraphNode(string name, IGraphNode node);
		IGraphNode GetValueGraphNode(string name);

		ISerializationFieldEnumerator GetEnumerator();

		Graph ParentGraph { get; }
		ISerializationContext Context { get; }
	}

	public interface ISerializationFieldEnumerator : IEnumerator<IGraphNode>
	{
		string FieldName { get; }
		object BuildValue();
		T BuildValue<T>();
		object BuildValue(Type type);
	}

	internal class SerializationData : ISerializationData
	{
		private IObjectGraphNode _holder;

		public SerializationData(IObjectGraphNode holder)
		{
			_holder = holder;
		}

		public void SetValue(string name, object value)
		{
			SetValue(name, value, null);
		}

		public void SetValue<T>(string name, T value)
		{
			SetValue(name, value, typeof(T));
		}

		public void SetValue(string name, object value, Type expectedType)
		{
			_holder[name] = _holder.ParentGraph.BuildNode(value, expectedType);
		}

		public void SetValueGraphNode(string name, IGraphNode node)
		{
			_holder[name] = node;
		}

		public object GetValue(string name)
		{
			return GetValue(name, null);
		}

		public IGraphNode GetValueGraphNode(string name)
		{
			return _holder[name];
		}

		public T GetValue<T>(string name)
		{
			return (T)GetValue(name, typeof(T));
		}

		public object GetValue(string name, Type expectedType)
		{
			IGraphNode node;
			if (_holder.TryGetField(name, out node))
			{
				if (node == null)
					return null;
				return node.RebuildObject(expectedType);
			}

			return expectedType == null ? null : SerializationServices.GetDefaultValueForType(expectedType);
		}

		public bool ContainsField(string name)
		{
			return _holder.ContainsField(name);
		}

		public bool RenameField(string oldFieldName, string newFieldName)
		{
			if (_holder.ContainsField(newFieldName))
				return false;

			IGraphNode node;
			if (!_holder.TryGetField(oldFieldName, out node))
				return false;

			_holder.RemoveField(oldFieldName);
			_holder[newFieldName] = node;

			return true;
		}

		public ISerializationFieldEnumerator GetEnumerator()
		{
			return new Enumerator(_holder.GetEnumerator());
		}

		private class Enumerator : ISerializationFieldEnumerator
		{
			private IEnumerator<FieldEntry> _it;

			public Enumerator(IEnumerator<FieldEntry> it)
			{
				_it = it;
			}

			public bool MoveNext()
			{
				return _it.MoveNext();
			}

			public void Reset()
			{
				_it.Reset();
			}

			public void Dispose()
			{
				_it.Dispose();
			}

			public IGraphNode Current
			{
				get { return _it.Current.FieldNode; }
			}

			object System.Collections.IEnumerator.Current
			{
				get { return _it.Current.FieldNode; }
			}

			public string FieldName
			{
				get { return _it.Current.FieldName; }
			}

			public object BuildValue()
			{
				return BuildValue(null);
			}

			public T BuildValue<T>()
			{
				return (T)BuildValue(typeof(T));
			}

			public object BuildValue(Type type)
			{
				return _it.Current.FieldNode.RebuildObject(type);
			}
		}

		public Graph ParentGraph
		{
			get { return _holder.ParentGraph; }
		}

		public ISerializationContext Context {
			get { return _holder.ParentGraph.Context; }
		}
	}
}