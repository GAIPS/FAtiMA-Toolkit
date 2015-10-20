using System;
using System.Collections.Generic;
using System.Linq;

namespace WellFormedNames
{
	using System.Text;
	using Utilities;
	//If the tree depth needs to be expanded in the future, just change the precision alias to a number type that allows more depth
	using Precision = System.Byte;

	/// <summary>
	/// 
	/// @author: Pedro Gonçalves
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class NameSearchTree<T> : IEnumerable<KeyValuePair<Name,T>>
	{
		private EvalNode root;

		public int Depth
		{
			get
			{
				return this.root.Depth(0);
			}
		}

		public NameSearchTree()
		{
			this.root = new EvalNode();
		}

		/// <summary>
		/// Clone constructor
		/// </summary>
		/// <param name="other"></param>
		public NameSearchTree(NameSearchTree<T> other)
		{
			this.root = new EvalNode(other.root);
		}

		public bool Add(Name name, T value)
		{
			return MethodWrapper(name, (s) => this.root.AddValue(s, value, 0));
		}

		public bool Remove(Name name)
		{
			return MethodWrapper(name, (s) => this.root.RemoveValue(s, 0));
		}

		public bool Contains(Name name)
		{
			return MethodWrapper(name, s => this.root.Contains(s,0));
		}

		public T Match(Name name)
		{
			T value;
			if (TryMatchValue(name, out value))
				return value;

			throw new KeyNotFoundException(string.Format(@"Could not find any match for the ""{0}""",name));
		}

		public bool TryMatchValue(Name name, out T value)
		{
			Pair<bool, T> result = MethodWrapper(name, s => this.root.Evaluate(s, 0));
			value = result.value2;
			return result.value1;
		}

		private static R MethodWrapper<R>(Name name, Func<Stack<Name>,R> func)
		{
			var stack = ObjectPool<Stack<Name>>.GetObject();
			try
			{
				stack.Push(name);
				R result = func(stack);
				return result;
			}
			finally
			{
				stack.Clear();
				ObjectPool<Stack<Name>>.Recycle(stack);
			}
		}

		public T this[Name name]
		{
			get
			{
				T v;
				this.TryMatchValue(name, out v);
				return v;
			}
			set
			{
				this.Add(name, value);
			}
		}

		public IEnumerable<T> GetValues()
		{
			return this.root.GetValues();
		}

		public IEnumerator<KeyValuePair<Name, T>> GetEnumerator()
		{
			return this.root.GetKeyValuePairs().Select(delegate(Pair<Stack<Name>,T> p){
				Name key = p.value1.Pop();
				p.value1.Clear();
				ObjectPool<Stack<Name>>.Recycle(p.value1);
				return new KeyValuePair<Name, T>(key, p.value2);
			}).GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public override string ToString()
		{
			StringBuilder builder = ObjectPool<StringBuilder>.GetObject();
			try
			{
				this.root.Write(builder, 0);
				return builder.ToString();
			}
			finally
			{
				builder.Length = 0;
				ObjectPool<StringBuilder>.Recycle(builder);
			}
		}

		#region Nested Types

		private class EvalNode
		{
			private Precision m_index;
			private SortedDictionary<string, EvalNode> m_next;
			private bool m_hasValue;
			private T m_value;

			public int Depth(int d)
			{
				if (m_next.Count > 0)
				{
					return m_next.Values.Max(n => n.Depth(d+1));
				}
				return d;
			}

			public bool IsEmpty
			{
				get
				{
					if (m_hasValue)
						return false;

					return m_next.Count == 0;
				}
			}

			public EvalNode(EvalNode other)
			{
				this.m_index = other.m_index;
				this.m_hasValue = other.m_hasValue;
				this.m_value = other.m_value;
				this.m_next = new SortedDictionary<string, EvalNode>();
				foreach (var pair in other.m_next)
					this.m_next[pair.Key] = new EvalNode(pair.Value);
			}

			public EvalNode() : this(0)
			{
			}

			private EvalNode(Precision index)
			{
				m_index = index;
				m_next = new SortedDictionary<string, EvalNode>();
				m_hasValue=false;
				m_value = default(T);
			}

			private string getKey(Name term)
			{
				if (term.NumberOfTerms == 1)
					return term.ToString();

				return string.Format("*({0})", term.NumberOfTerms);
			}

			public bool AddValue(Stack<Name> stack, T value, int processed)
			{
				if (stack.Count == 0) //End terms
				{
					if (m_index != processed)
						return false;

					if (m_hasValue)
						return false;

					m_hasValue = true;
					m_value = value;
					return true;
				}

				EvalNode nodeToAdd;
				Name term = stack.Pop();

				if (term.NumberOfTerms > 1)
				{
					using (var it = term.GetTerms().Reverse().GetEnumerator())
					{
						while (it.MoveNext())
						{
							stack.Push(it.Current);
						}
					}
				}

				string key = getKey(term);

				if (!this.m_next.TryGetValue(key, out nodeToAdd))
				{
					nodeToAdd = new EvalNode((Precision)(this.m_index + 1));
					this.m_next[key] = nodeToAdd;
				}

				return nodeToAdd.AddValue(stack, value,processed+1);
			}

			public bool RemoveValue(Stack<Name> terms, int processed)
			{
				if (terms.Count == 0) //End terms
				{
					if (m_index != processed)
						return false;

					if (m_hasValue)
					{
						m_hasValue = false;
						m_value = default(T);
						return true;
					}
					return false;
				}

				EvalNode nodeToRemove;
				Name stack = terms.Pop();

				if (stack.NumberOfTerms > 1)
				{
					using (var it = stack.GetTerms().Reverse().GetEnumerator())
					{
						while (it.MoveNext())
						{
							terms.Push(it.Current);
						}
					}
				}

				string key = getKey(stack);
				if (!this.m_next.TryGetValue(key, out nodeToRemove))
				{
					return false;
				}

				if (nodeToRemove.RemoveValue(terms, processed + 1))
				{
					if (nodeToRemove.IsEmpty)
						this.m_next.Remove(key);
					
					return true;
				}

				return false;
			}

			public bool Contains(Stack<Name> stack, int processed)
			{
				if (stack.Count == 0) //End terms
				{
					if (m_index != processed)
						return false;

					return m_hasValue;
				}

				EvalNode nodeToEvaluate;
				Name term = stack.Pop();

				if (term.NumberOfTerms > 1)
				{
					using (var it = term.GetTerms().Reverse().GetEnumerator())
					{
						while (it.MoveNext())
						{
							stack.Push(it.Current);
						}
					}
				}

				string key = getKey(term);
				if (this.m_next.TryGetValue(key, out nodeToEvaluate))
				{
					return nodeToEvaluate.Contains(stack, processed + 1);
				}
				return false;
			}

			public Pair<bool, T> Evaluate(Stack<Name> stack, int processed)
			{
				if (stack.Count == 0) //End terms
				{
					if (m_index != processed)
						return Tuple.Create(false, default(T));

					return Tuple.Create(m_hasValue, m_value);
				}

				EvalNode nodeToEvaluate;
				Name term = stack.Pop();

				string key = getKey(term);
				if (key == Symbol.UNIVERSAL_STRING)
				{
					if (this.m_next.TryGetValue(Symbol.UNIVERSAL_STRING, out nodeToEvaluate))
					{
						var res = nodeToEvaluate.Evaluate(stack, processed + 1);
						if (res.value1)
							return res;
					}

					foreach (var node in m_next.Values)
					{
						var res = node.Evaluate(stack, processed + 1);
						if (res.value1)
							return res;
					}
				}
				else
				{
					if (this.m_next.TryGetValue(key, out nodeToEvaluate))
					{
						if (term.NumberOfTerms > 1)
						{
							using (var it = term.GetTerms().Reverse().GetEnumerator())
							{
								while (it.MoveNext())
								{
									stack.Push(it.Current);
								}
							}
						}

						var res = nodeToEvaluate.Evaluate(stack, processed + 1);
						if (res.value1)
							return res;

						//Failed to evaluate, reconstruct stack
						if (term.NumberOfTerms > 1)
						{
							for (int i = 0; i < term.NumberOfTerms; i++)
								stack.Pop();
						}
					}

					if (this.m_next.TryGetValue(Symbol.UNIVERSAL_STRING, out nodeToEvaluate))
					{
						var res = nodeToEvaluate.Evaluate(stack, processed + 1);
						if(res.value1)
							return res;
					}
				}

				stack.Push(term);
				return Tuple.Create(false, default(T));
			}

			public IEnumerable<T> GetValues()
			{
				IEnumerable<T> result;
				if (m_next.Count > 0)
				{
					result = m_next.Values.SelectMany(n => n.GetValues());
				}
				else
					result = Enumerable.Empty<T>();

				if (m_hasValue)
					result = result.Prepend(m_value);

				return result;
			}

			public IEnumerable<Pair<Stack<Name>, T>> GetKeyValuePairs()
			{
				IEnumerable<Pair<Stack<Name>, T>> result = Enumerable.Empty<Pair<Stack<Name>, T>>();

				if (m_next.Count > 0)
				{
					foreach (var entry in m_next)
					{
						var entryResults = entry.Value.GetKeyValuePairs();
						string key = entry.Key;
						int termsToMerge = 0;
						if ((key[0] == '*') && (key.Length > 1))
						{
							key = key.Substring(2,key.Length-3);
							termsToMerge = int.Parse(key);
						}
						foreach (var r in entryResults)
						{
							Name term;
							if (termsToMerge > 0)
							{
								List<Name> tmp = ObjectPool<List<Name>>.GetObject();
								for (int i = 0; i < termsToMerge; i++)
									tmp.Add(r.value1.Pop());
								term = new ComposedName(tmp);
								tmp.Clear();
								ObjectPool<List<Name>>.Recycle(tmp);
							}
							else
								term = new Symbol(key);
							
							r.value1.Push(term);
						}
						result = result.Union(entryResults);
					}
				}

				if (m_hasValue)
					result = result.Append(Tuple.Create(ObjectPool<Stack<Name>>.GetObject(), m_value));

				return result;
			}

			public void Write(StringBuilder builder, int indent)
			{
				Indent(builder, indent);
				if (m_hasValue)
					builder.AppendFormat("(has value) : {0}", m_value);
				else
					builder.Append("(no value)");

				foreach (var p in m_next)
				{
					builder.AppendLine();
					Indent(builder, indent);
					builder.AppendFormat("{0} :", p.Key).AppendLine();
					p.Value.Write(builder, indent + 1);
				}
			}

			private void Indent(StringBuilder builder,int indent)
			{
				for (int i = 0; i < indent; i++)
					builder.Append("\t");
			}
		}

		#endregion
	}
}
