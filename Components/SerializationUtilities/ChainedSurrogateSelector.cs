using GAIPS.Serialization.Surrogates;
using System;
using System.Collections.Generic;
using Utilities;

namespace GAIPS.Serialization
{
	public sealed class ChainedSurrogateSelector
	{
		private class ChainNode
		{
			private List<ChainNode> m_childs;
			private Type m_serializationType;
			private ISerializationSurrogate m_surrogate;

			public ChainNode(Type serializationType, ISerializationSurrogate surrogate)
			{
				m_childs = new List<ChainNode>();
				m_serializationType = serializationType;
				m_surrogate = surrogate;
			}

			private static bool Match(Type baseType, Type type)
			{
				bool r = baseType.IsAssignableFrom(type);
				if (r)
					return true;

				if (!baseType.IsGenericType)
					return false;

				if (baseType.IsInterface)
				{
					foreach (var i in type.GetInterfaces())
					{
						var t = i.IsGenericType ? i.GetGenericTypeDefinition() : i;
						if (baseType == t)
							return true;
					}
				}
				else
				{
					if (type.IsInterface)
						return false;

					do
					{
						type = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
						if (baseType == type)
							return true;
						type = type.BaseType;
					}
					while (type != null);
				}
				return false;
			}

			public bool AddNewSurrogate(Type type, ISerializationSurrogate surrogate)
			{
				if (m_serializationType == type)
					throw new Exception("Duplicated surrogate type chain: " + type);

				if (Match(m_serializationType,type))
				{
					for (int i = 0; i < m_childs.Count; i++)
					{
						var child = m_childs[i];
						bool r = child.AddNewSurrogate(type, surrogate);
						if (r)
							return true;
					}

					ChainNode newNode = new ChainNode(type, surrogate);
					for (int i = 0; i < m_childs.Count; i++)
					{
						var child = m_childs[i];
						if (Match(type,child.m_serializationType))
						{
							newNode.m_childs.Add(child);
							m_childs.RemoveAt(i);
							i--;
						}
					}
					m_childs.Add(newNode);
					return true;
				}

				return false;
			}

			public ISerializationSurrogate GetSurrogate(Type type)
			{
				if (Match(m_serializationType, type))
				{
					foreach (ChainNode child in m_childs)
					{
						ISerializationSurrogate surrogate = child.GetSurrogate(type);
						if (surrogate != null)
							return surrogate;
					}
					return m_surrogate;
				}
				return null;
			}
		}

		private ChainNode m_root = null;
		
		public ChainedSurrogateSelector()
		{
			m_root = new ChainNode(typeof(object), new DefaultSerializationSurrogate());
		}

		public void AddSurrogate(Type type, ISerializationSurrogate surrogate)
		{
			m_root.AddNewSurrogate(type, surrogate);
		}

		public ISerializationSurrogate GetSurrogate(Type type)
		{
			return m_root.GetSurrogate(type);
		}
	}
}
