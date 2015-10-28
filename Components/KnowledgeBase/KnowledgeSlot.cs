using System.Collections.Generic;
using System.Linq;
using System.Text;
using KnowledgeBase.WellFormedNames;
using Utilities;

namespace KnowledgeBase
{
	/// <summary>
	///     Class used to store knowledge in the KnowledgeBase. A KnowledgeSlot can store
	///     an object, but also any number of children KnowledgeSlots. This hierarchical
	///     composition of KnowledgeSlots builds up the KnowledgeBase and allows fast indexing
	///     and search of properties.
	/// </summary>
	/// @author João Dias
	/// @author Pedro Gonçalves (C# version)
	public sealed class KnowledgeSlot
	{
		private readonly Dictionary<string, KnowledgeSlot> _children;

		public KnowledgeSlot(string visibility, bool persistent, string name, string displayName)
		{
			DisplayName = displayName;
			Name = name;
			_children = new Dictionary<string, KnowledgeSlot>();
			Persistent = persistent;
			Visibility = visibility;
		}

		/// <summary>
		///     Creates an empty KnowledgeSpot, identified by the received name
		/// </summary>
		/// <param name="name">the name that identifies the KnowledgeSlot</param>
		/// <param name="displayName"></param>
		public KnowledgeSlot(string name, string displayName)
			: this(Symbol.UNIVERSAL_STRING, false, name, displayName)
		{
		}

		/// <summary>
		///     Creates an empty KnowledgeSpot, identified by the received name
		/// </summary>
		/// <param name="persistent"></param>
		/// <param name="name">the name that identifies the KnowledgeSlot</param>
		/// <param name="displayName"></param>
		public KnowledgeSlot(bool persistent, string name, string displayName)
			: this(Symbol.UNIVERSAL_STRING, persistent, name, displayName)
		{
		}

		/// <summary>
		///     The KnowledgeSlot identifier
		/// </summary>
		/// <remarks>read only</remarks>
		public string Name { get; private set; }

		/// <summary>
		///     the KnowledgeSlot display Name
		/// </summary>
		/// <remarks>read only</remarks>
		public string DisplayName { get; private set; }

		/// <summary>
		///     The object stored in the KnowledgeSlot
		/// </summary>
		public object Value { get; set; }

		/// <summary>
		///     Whether the KnowledgeSlot is persistent (i.e. it will be saved and reused after migration) or not
		/// </summary>
		public bool Persistent { get; set; }

		public string Visibility { get; set; }

		/// <summary>
		///     Clears all the information stored in the
		///     KnowledgeSlot (including all its children)
		/// </summary>
		public void Clear()
		{
			Value = null;
			_children.Clear();
			Persistent = false;
		}

		/// <summary>
		///     Determines if this KnowledgeSlot has a child with a specific key
		/// </summary>
		/// <param name="key">the key of the child to search</param>
		/// <returns>true if the KnowledgeSlot contains a child with the received key</returns>
		public bool ContainsKey(string key)
		{
			return _children.ContainsKey(key);
		}

		/// <summary>
		///     Gets the child KnowledgeSlot identified by the given key
		/// </summary>
		/// <param name="key">the key of the child to get</param>
		/// <returns>the child KnowledgeSlot</returns>
		public KnowledgeSlot Get(string key)
		{
			return _children[key];
		}

		public IEnumerable<string> GetKeys()
		{
			return _children.Keys;
		}

		public IEnumerable<KnowledgeSlot> GetLeafs()
		{
			var leafs = _children.Values.SelectMany(ks => ks.GetLeafs());
			if (Value != null)
			{
				leafs = leafs.Prepend(this);
			}

			return leafs;
		}

		/// <summary>
		///     Counts the number of valid elements (ones that have a value stored in them)
		///     stored within this KnowledgeSlot (may include children, grandchildren,
		///     grandgrandchildren, etc).
		/// </summary>
		/// <returns>the number of elements stored inside the KnowledgeSlot</returns>
		public int CountElements()
		{
			var number = 0;
			if (Value != null)
				number++;

			number += _children.Values.Select(ks => ks.CountElements()).Sum();
			return number;
		}

		/// <summary>
		///     Adds a KS child to the KnowledgeSlot
		/// </summary>
		/// <param name="key">the Key of the KnowledgeSlot to ADD</param>
		/// <param name="kSlot">the KnowledgeSlot to ADD</param>
		public void Put(string key, KnowledgeSlot kSlot)
		{
			_children.Add(key, kSlot);
		}

		/// <summary>
		///     Removes a child from the KnowledgeSlot
		/// </summary>
		/// <param name="key">the key of the child to remove</param>
		public void Remove(string key)
		{
			_children.Remove(key);
		}

		private void WriteToStringBuilder(StringBuilder builder)
		{
			builder.Append(Name).Append(':');
			if (Value != null)
				builder.Append(Value);

			var count = _children.Count;
			if (count == 0)
				return;

			builder.Append('{');
			foreach (var it in _children.Values)
			{
				it.WriteToStringBuilder(builder);
				count--;
				if (count > 0)
					builder.Append(',');
			}
			builder.Append('}');
		}

		public override string ToString()
		{
			var builder = ObjectPool<StringBuilder>.GetObject();
			try
			{
				WriteToStringBuilder(builder);
				return builder.ToString();
			}
			finally
			{
				builder.Length = 0;
				ObjectPool<StringBuilder>.Recycle(builder);
			}
		}

		/// <summary>
		///     Remove all non persistent properties
		/// </summary>
		public void RemoveNonPersitent()
		{
			foreach (var key in _children.Keys.ToArray())
			{
				var ks = _children[key];
				ks.RemoveNonPersitent();
				if (!ks.Persistent && ks._children.Count == 0)
					_children.Remove(key);
			}
		}
	}
}