using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GAIPS.Serialization;
using Utilities;

namespace KnowledgeBase.WellFormedNames.Collections
{
	//TODO optimiza count. It should not iterate all elements. Just modify a variable of contained elements at each add/remove
	[Serializable]
	public partial class NameSearchTree<T> : IDictionary<Name, T>, ICustomSerialization
	{
		private readonly TreeNode Root;

		public NameSearchTree()
		{
			Root = new TreeNode();
		}

		/// <summary>
		///     Clone constructor
		/// </summary>
		/// <param name="other"></param>
		public NameSearchTree(NameSearchTree<T> other)
		{
			Root = new TreeNode(other.Root);
		}

		public int Depth
		{
			get { return Root.Depth(0); }
		}

		public void Add(Name name, T value)
		{
			if (!MethodWrapper(name, s => Root.AddValue(s, value,false)))
				throw new Exception("Duplicate key"); //TODO better exception
		}

		public bool Remove(Name name)
		{
			return MethodWrapper(name, s => Root.RemoveValue(s));
		}

		public IEnumerator<KeyValuePair<Name, T>> GetEnumerator()
		{
			return Root.GetKeyValuePairs().Select(delegate(Pair<Stack<Name>, T> p)
			{
				var key = p.Item1.Pop();
				p.Item1.Clear();
				ObjectPool<Stack<Name>>.Recycle(p.Item1);
				return new KeyValuePair<Name, T>(key, p.Item2);
			}).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public bool ContainsKey(Name key)
		{
			return MethodWrapper(key, s => Root.Contains(s));
		}

		public ICollection<Name> Keys
		{
			get
			{
				return Root.GetKeys().Select(delegate(Stack<Name> s)
				{
					var key = s.Pop();
					s.Clear();
					ObjectPool<Stack<Name>>.Recycle(s);
					return key;
				}).ToList();
			}
		}

		public bool TryGetValue(Name key, out T value)
		{
			var result = MethodWrapper(key, s => Root.Retrive(s));
			value = result.Item2;
			return result.Item1;
		}

		public ICollection<T> Values
		{
			get { return Root.GetValues().ToList(); }
		}

		public T this[Name key]
		{
			get
			{
				T v;
				TryGetValue(key, out v);
				return v;
			}
			set
			{
				MethodWrapper(key, s => Root.AddValue(s, value, true));
			}
		}

		public void Add(KeyValuePair<Name, T> item)
		{
			Add(item.Key, item.Value);
		}

		public void Clear()
		{
			Root.Clear();
		}

		public bool Contains(KeyValuePair<Name, T> item)
		{
			T value;
			if (!TryGetValue(item.Key, out value))
				return false;

			return value.Equals(item.Value);
		}

		public void CopyTo(KeyValuePair<Name, T>[] array, int arrayIndex)
		{
			using (var it = GetEnumerator())
			{
				while (it.MoveNext())
				{
					array[arrayIndex++] = it.Current;
				}
			}
		}

		public int Count
		{
			get { return Root.Count(); }
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		public bool Remove(KeyValuePair<Name, T> item)
		{
			if (!Contains(item))
				return false;

			Remove(item.Key);
			return true;
		}
		/*
		public T Match(Name name)
		{
			T value;
			if (TryMatchValue(name, out value))
				return value;

			throw new KeyNotFoundException(string.Format(@"Could not find any match for the ""{0}""", name));
		}

		public bool TryMatchValue(Name name, out T value)
		{
			foreach (var m in MatchAll(name))
			{
				value = m;
				return true;
			}
			value = default(T);
			return false;
		}
		
		public IEnumerable<T> MatchAll(Name name)
		{
			var stack = ObjectPool<Stack<Name>>.GetObject();
			try
			{
				stack.Push(name);
				foreach (var pair in Root.Unify(stack, null))
					yield return pair.Item1;
			}
			finally
			{
				stack.Clear();
				ObjectPool<Stack<Name>>.Recycle(stack);
			}
		}
		*/
		public IEnumerable<Pair<T, SubstitutionSet>> Unify(Name predicate, SubstitutionSet bindings=null)
		{
			var stack = ObjectPool<Stack<Name>>.GetObject();
			try
			{
				stack.Push(predicate);
				foreach (var pair in Root.Unify(stack, bindings ?? new SubstitutionSet()))
					yield return pair;
			}
			finally
			{
				stack.Clear();
				ObjectPool<Stack<Name>>.Recycle(stack);
			}
		}

		private static TReturn MethodWrapper<TReturn>(Name name, Func<Stack<Name>, TReturn> func)
		{
			var stack = ObjectPool<Stack<Name>>.GetObject();
			try
			{
				stack.Push(name);
				var result = func(stack);
				return result;
			}
			finally
			{
				stack.Clear();
				ObjectPool<Stack<Name>>.Recycle(stack);
			}
		}

		public override int GetHashCode()
		{
			const int BASE_HASH = 0x27b895b3;
			int hash = BASE_HASH;
			var it = this.Select(p => p.Key.GetHashCode() ^ p.Value.GetHashCode()).GetEnumerator();
			while (it.MoveNext())
				hash ^= it.Current;

			return hash;
		}

		public override bool Equals(object obj)
		{
			NameSearchTree<T> dic = obj as NameSearchTree<T>;
			if (dic == null)
				return false;

			if (Count != dic.Count)
				return false;

			return this.All(pair => dic.Contains(pair));
		}

		public void GetObjectData(ISerializationData dataHolder)
		{
			var t = typeof (T);
			foreach (var pairs in this)
			{
				var name = pairs.Key.ToString();
				dataHolder.SetValue(name,pairs.Value,t);
			}
		}

		public void SetObjectData(ISerializationData dataHolder)
		{
			using (var it = dataHolder.GetEnumerator())
			{
				while (it.MoveNext())
				{
					Name n = Name.BuildName(it.FieldName);
					var value = it.BuildValue<T>();
					Add(n,value);
				}
			}
		}
	}
}