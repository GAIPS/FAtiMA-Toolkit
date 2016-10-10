using System;
using System.Collections.Generic;
using System.Linq;

namespace SerializationUtilities
{
	internal class TypeSelector<T> where T : class
	{
		private readonly List<Node> m_roots = new List<Node>();

		private static bool Match(Type baseType, Type type)
		{
			var r = TypeTools.IsAssignableFrom(baseType, type);
			if (r)
				return true;

			if (!baseType.IsGenericType())
				return false;

			if (baseType.IsInterface())
				return TypeTools.GetInterfaces(type).Select(GetUnderlinedGenericType).Any(t => baseType == t);

			if (type.IsInterface())
				return false;

			do
			{
				type = GetUnderlinedGenericType(type);
				if (baseType == type)
					return true;
				type = type.GetBaseType();
			} while (type != null);
			return false;
		}

		private static Type GetUnderlinedGenericType(Type t)
		{
			return t.IsGenericType() ? t.GetGenericTypeDefinition() : t;
		}

		public void AddValue(Type type, bool useForChildren, T value)
		{
			if (m_roots.Count == 0)
			{
				m_roots.Add(new Node(type, useForChildren, value));
				return;
			}

			for (var i = 0; i < m_roots.Count; i++)
			{
				var r = m_roots[i];
				var node = r.AddValue(type, useForChildren, value);
				if (node == null)
					continue;
				m_roots[i] = node;
				return;
			}

			var newNode = new Node(type, useForChildren, value);
			for (var i = 0; i < m_roots.Count; i++)
			{
				var child = m_roots[i];
				if (!Match(type, child.type))
					continue;

				newNode.childs.Add(child);
				m_roots.RemoveAt(i);
				i--;
			}
			m_roots.Add(newNode);
		}

		public T GetValue(Type type)
		{
			return m_roots.Select(n => n.GetValue(type)).FirstOrDefault(v => v != null);
		}

		public void Clear()
		{
			m_roots.Clear();
		}

		private class Node
		{
			public readonly List<Node> childs;
			public readonly Type type;
			private readonly bool m_useForChildren;
			private readonly T m_value;

			public Node(Type type, bool useForChildren, T value)
			{
				childs = new List<Node>();
				this.type = type;
				m_useForChildren = useForChildren;
				m_value = value;
			}

			public Node AddValue(Type type, bool useForChildren, T value)
			{
				if (this.type == type)
					throw new Exception("Duplicated surrogate type chain: " + type);

				if (!Match(this.type, type))
					return null;

				for (var i = 0; i < childs.Count; i++)
				{
					var child = childs[i];
					var r = child.AddValue(type, useForChildren, value);
					if (r != null)
					{
						childs[i] = child;
						return this;
					}
				}

				var newNode = new Node(type, useForChildren, value);
				for (var i = 0; i < childs.Count; i++)
				{
					var child = childs[i];
					if (!Match(type, child.type))
						continue;

					newNode.childs.Add(child);
					childs.RemoveAt(i);
					i--;
				}
				childs.Add(newNode);
				return this;
			}

			public T GetValue(Type type)
			{
				if (!Match(this.type, type))
					return null;

				foreach (var child in childs)
				{
					var value = child.GetValue(type);
					if (value != null)
						return value;
				}

				if (m_useForChildren)
					return m_value;

				if (this.type.IsGenericTypeDefinition() && type.IsGenericType())
				{
					type = type.GetGenericTypeDefinition();
					if (this.type == type)
						return m_value;
				}
				else if (this.type == type)
					return m_value;

				return null;
			}
		}
	}
}