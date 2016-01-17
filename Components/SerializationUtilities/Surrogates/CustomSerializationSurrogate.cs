using GAIPS.Serialization.Attributes;
using GAIPS.Serialization.SerializationGraph;
using System;
using System.Collections.Generic;

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

		private class SerializationData : ISerializationData
		{
			private IObjectGraphNode _holder;

			public SerializationData(IObjectGraphNode holder)
			{
				_holder = holder;
			}

			public void SetValue(string name, object value)
			{
				SetValue(name,value,null);
			}

			public void SetValue<T>(string name, T value)
			{
				SetValue(name,value,typeof(T));
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
				return (T) GetValue(name, typeof (T));
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

			public ISerializationFieldEnumerator GetEnumerator()
			{
				return new Enumerator(_holder.GetEnumerator());
			}

			private class Enumerator : ISerializationFieldEnumerator
			{
				private IEnumerator<FieldEnty> _it;

				public Enumerator(IEnumerator<FieldEnty> it)
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
		}
	}
}