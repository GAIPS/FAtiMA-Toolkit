using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace KnowledgeBase.WellFormedNames.Collections
{
	//If the tree depth needs to be expanded in the future, just change the precision alias to a number type that allows more depth
	using Precision = Byte;

	//TODO optimization review
	public partial class NameSearchTree<T>
	{
		protected class TreeNode
		{
			private readonly Precision m_index;
			private SortedDictionary<string, TreeNode> m_next;
			private bool m_hasValue;
			private T m_value;

			public int Depth(int d)
			{
				return m_next.Count > 0 ? m_next.Values.Max(n => n.Depth(d + 1)) : d;
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

			public TreeNode(TreeNode other)
			{
				m_index = other.m_index;
				m_hasValue = other.m_hasValue;
				m_value = other.m_value;
				m_next = new SortedDictionary<string, TreeNode>();
				foreach (var pair in other.m_next)
					m_next[pair.Key] = new TreeNode(pair.Value);
			}

			public TreeNode()
				: this(0)
			{
			}

			public void Clear()
			{
				m_next.Clear();
				m_hasValue = false;
				m_value = default(T);
			}

			public int Count()
			{
				int count = 0;
				if (m_next.Count > 0)
				{
					count += m_next.Values.Sum(n => n.Count());
				}

				if (m_hasValue)
					count++;

				return count;
			}

			private TreeNode(Precision index)
			{
				m_index = index;
				m_next = new SortedDictionary<string, TreeNode>();
				m_hasValue = false;
				m_value = default(T);
			}

			private string getKey(Name term)
			{
				if (term.NumberOfTerms == 1)
					return term.ToString().ToUpperInvariant();

				return string.Format("*({0})", term.NumberOfTerms);
			}

			public bool AddValue(Stack<Name> stack, T value, bool overwrite)
			{
				if (stack.Count == 0) //End terms
				{
					if (m_hasValue && !overwrite)
						return false;

					m_hasValue = true;
					m_value = value;
					return true;
				}

				TreeNode nodeToAdd;
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

				if (!m_next.TryGetValue(key, out nodeToAdd))
				{
					nodeToAdd = new TreeNode((Precision)(m_index + 1));
					m_next[key] = nodeToAdd;
				}

				return nodeToAdd.AddValue(stack, value,overwrite);
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

				TreeNode nodeToRemove;
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
				if (!m_next.TryGetValue(key, out nodeToRemove))
				{
					return false;
				}

				if (!nodeToRemove.RemoveValue(terms, processed + 1))
					return false;

				if (nodeToRemove.IsEmpty)
					m_next.Remove(key);

				return true;
			}

			public bool Contains(Stack<Name> stack, int processed)
			{
				if (stack.Count == 0) //End terms
				{
					if (m_index != processed)
						return false;

					return m_hasValue;
				}

				TreeNode nodeToEvaluate;
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
				if (m_next.TryGetValue(key, out nodeToEvaluate))
				{
					return nodeToEvaluate.Contains(stack, processed + 1);
				}
				return false;
			}

			public Pair<bool, T> Match(Stack<Name> stack, int processed)
			{
				if (stack.Count == 0) //End terms
				{
					if (m_index != processed)
						return Tuple.Create(false, default(T));

					return Tuple.Create(m_hasValue, m_value);
				}

				TreeNode nodeToEvaluate;
				Name term = stack.Pop();

				string key = getKey(term);
				if (key == Symbol.UNIVERSAL_STRING)
				{
					if (m_next.TryGetValue(Symbol.UNIVERSAL_STRING, out nodeToEvaluate))
					{
						var res = nodeToEvaluate.Match(stack, processed + 1);
						if (res.Item1)
							return res;
					}

					foreach (var node in m_next.Values)
					{
						var res = node.Match(stack, processed + 1);
						if (res.Item1)
							return res;
					}
				}
				else
				{
					if (m_next.TryGetValue(key, out nodeToEvaluate))
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

						var res = nodeToEvaluate.Match(stack, processed + 1);
						if (res.Item1)
							return res;

						//Failed to evaluate, reconstruct stack
						if (term.NumberOfTerms > 1)
						{
							for (int i = 0; i < term.NumberOfTerms; i++)
								stack.Pop();
						}
					}

					if (m_next.TryGetValue(Symbol.UNIVERSAL_STRING, out nodeToEvaluate))
					{
						var res = nodeToEvaluate.Match(stack, processed + 1);
						if (res.Item1)
							return res;
					}
				}

				stack.Push(term);
				return Tuple.Create(false, default(T));
			}

			public Pair<bool, T> Retrive(Stack<Name> stack, int processed)
			{
				if (stack.Count == 0) //End terms
				{
					if (m_index != processed)
						return Tuple.Create(false, default(T));

					return Tuple.Create(m_hasValue, m_value);
				}

				TreeNode nodeToEvaluate;
				var term = stack.Pop();

				var key = getKey(term);
				if (m_next.TryGetValue(key, out nodeToEvaluate))
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

					var res = nodeToEvaluate.Match(stack, processed + 1);
					if (res.Item1)
						return res;

					//Failed to evaluate, reconstruct stack
					if (term.NumberOfTerms > 1)
					{
						for (int i = 0; i < term.NumberOfTerms; i++)
							stack.Pop();
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

			public IEnumerable<Stack<Name>> GetKeys()
			{
				IEnumerable<Stack<Name>> result = Enumerable.Empty<Stack<Name>>();

				if (m_next.Count > 0)
				{
					foreach (var entry in m_next)
					{
						var entryResults = entry.Value.GetKeys().ToList();
						string key = entry.Key;
						int termsToMerge = 0;
						if ((key[0] == '*') && (key.Length > 1))
						{
							key = key.Substring(2, key.Length - 3);
							termsToMerge = int.Parse(key);
						}
						foreach (var r in entryResults)
						{
							Name term;
							if (termsToMerge > 0)
							{
								List<Name> tmp = ObjectPool<List<Name>>.GetObject();
								for (int i = 0; i < termsToMerge; i++)
									tmp.Add(r.Pop());
								term = new ComposedName(tmp);
								tmp.Clear();
								ObjectPool<List<Name>>.Recycle(tmp);
							}
							else
								term = new Symbol(key);

							r.Push(term);
						}
						result = result.Union(entryResults);
					}
				}

				if (m_hasValue)
					result = result.Append(ObjectPool<Stack<Name>>.GetObject());

				return result;
			}

			public IEnumerable<Pair<Stack<Name>, T>> GetKeyValuePairs()
			{
				IEnumerable<Pair<Stack<Name>, T>> result = Enumerable.Empty<Pair<Stack<Name>, T>>();

				if (m_next.Count > 0)
				{
					foreach (var entry in m_next)
					{
						var entryResults = entry.Value.GetKeyValuePairs().ToList();
						string key = entry.Key;
						int termsToMerge = 0;
						if ((key[0] == '*') && (key.Length > 1))
						{
							key = key.Substring(2, key.Length - 3);
							termsToMerge = int.Parse(key);
						}
						foreach (var r in entryResults)
						{
							Name term;
							if (termsToMerge > 0)
							{
								List<Name> tmp = ObjectPool<List<Name>>.GetObject();
								for (int i = 0; i < termsToMerge; i++)
									tmp.Add(r.Item1.Pop());
								term = new ComposedName(tmp);
								tmp.Clear();
								ObjectPool<List<Name>>.Recycle(tmp);
							}
							else
								term = new Symbol(key);

							r.Item1.Push(term);
						}
						result = result.Union(entryResults);
					}
				}

				if (m_hasValue)
					result = result.Append(Tuple.Create(ObjectPool<Stack<Name>>.GetObject(), m_value));

				return result;
			}

			private IEnumerable<Pair<Stack<Name>, IEnumerable<TreeNode>>> CollectNextLevel(int level)
			{
				foreach (var entry in m_next)
				{
					var key = entry.Key;
					if ((key[0] == '*') && (key.Length > 1))
					{
						key = key.Substring(2, key.Length - 3);
						var nextLevel = int.Parse(key);
						Dictionary<Name,IEnumerable<TreeNode>> group = new Dictionary<Name, IEnumerable<TreeNode>>();
						foreach (var subEntry in entry.Value.CollectNextLevel(nextLevel))
						{
							Name newName;
							var r = subEntry.Item1;
							List<Name> tmp = ObjectPool<List<Name>>.GetObject();
							for (int i = 0; i < nextLevel; i++)
								tmp.Add(r.Pop());

							newName = new ComposedName(tmp);
							tmp.Clear();
							ObjectPool<List<Name>>.Recycle(tmp);
							subEntry.Item1.Clear();
							ObjectPool<Stack<Name>>.Recycle(subEntry.Item1);

							IEnumerable<TreeNode> collection;
							if (group.TryGetValue(newName, out collection))
								collection = collection.Union(subEntry.Item2);
							else
								collection = subEntry.Item2;

							group[newName] = collection;
						}

						foreach (var pair in group)
						{
							var s = ObjectPool<Stack<Name>>.GetObject();
							s.Push(pair.Key);
							yield return Tuple.Create(s,pair.Value);
						}
					}
					else
					{
						if (level > 1)
						{
							foreach (var pair in entry.Value.CollectNextLevel(level - 1))
							{
								pair.Item1.Push(new Symbol(key));
								yield return pair;
							}
						}
						else
						{
							var s = ObjectPool<Stack<Name>>.GetObject();
							s.Push(new Symbol(key));
							yield return Tuple.Create(s, (IEnumerable<TreeNode>)new[] { entry.Value });
						}
					}
				}
			}

			private IEnumerable<Pair<Name, IEnumerable<TreeNode>>> GetNextLevel()
			{
				foreach (var pair in CollectNextLevel(1))
				{
					var stack = pair.Item1;
					var name = stack.Pop();
					stack.Clear();
					ObjectPool<Stack<Name>>.Recycle(stack);
					yield return Tuple.Create(name, pair.Item2);
				}
			}

			public bool Bind(Stack<Name> stack, int processed, List<SubstitutionSet> resultsFound)
			{
				if (stack.Count == 0) //End terms
					return true;

				TreeNode nodeToEvaluate;
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

				if (!term.IsVariable)
				{
					string key = getKey(term);
					if (m_next.TryGetValue(key, out nodeToEvaluate))
						return nodeToEvaluate.Bind(stack, processed + 1, resultsFound);

					return false;
				}

				//Find bindings
				foreach (var pair in GetNextLevel())
				{
					var newSubstitution = new Substitution((Symbol)term, pair.Item1);
					foreach (var node in pair.Item2)
					{
						List<SubstitutionSet> childResults = new List<SubstitutionSet>();
						Stack<Name> stackClone = new Stack<Name>(stack);
						if (!node.Bind(stackClone, processed + 1, childResults))
							continue;

						if (childResults.Count > 0)
						{
							foreach (var set in childResults)
							{
								if (set.Conflicts(newSubstitution))
									return false;
								set.AddSubstitution(newSubstitution);
							}
							resultsFound.AddRange(childResults);
						}
						else
						{
							var newSet = new SubstitutionSet();
							newSet.AddSubstitution(newSubstitution);
							resultsFound.Add(newSet);
						}
					}
				}

				//foreach (var entry in m_next)
				//{
				//	List<SubstitutionSet> childResults = new List<SubstitutionSet>();
				//	Stack<Name> stackClone = new Stack<Name>(stack);
				//	if(!entry.Value.Bind(stackClone,processed+1,childResults))
				//		continue;
					
				//	string key = entry.Key;
				//	int termsToMerge = 0;
				//	if ((key[0] == '*') && (key.Length > 1))
				//	{
				//		key = key.Substring(2, key.Length - 3);
				//		termsToMerge = int.Parse(key);
				//	}

				//	Name name;
				//	if (termsToMerge > 0)
				//	{
				//		throw new NotImplementedException();
				//	}
				//	else
				//		name = new Symbol(key);

				//	var newSubstitution = new Substitution((Symbol)term, name);
				//	if (childResults.Count > 0)
				//	{
				//		foreach (var set in childResults)
				//		{
				//			if (set.Conflicts(newSubstitution))
				//				return false;
				//			set.AddSubstitution(newSubstitution);
				//		}
				//		resultsFound.AddRange(childResults);
				//	}
				//	else
				//	{
				//		var newSet = new SubstitutionSet();
				//		newSet.AddSubstitution(newSubstitution);
				//		resultsFound.Add(newSet);
				//	}
				//}

				return true;
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

			private void Indent(StringBuilder builder, int indent)
			{
				for (int i = 0; i < indent; i++)
					builder.Append("\t");
			}
		}
	}
}
