using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace KnowledgeBase.WellFormedNames.Collections
{
	//TODO optimization review
	public partial class NameSearchTree<T>
	{
		protected class TreeNode
		{
			private SortedDictionary<string, TreeNode> m_next;
			private SortedDictionary<int, TreeNode> m_nextComposed;
			private TreeNode m_universal;
			private bool m_hasValue;
			private T m_value;

			public int Depth(int d)
			{
				var nextDepth = m_next.Count > 0 ? m_next.Values.Max(n => n.Depth(d + 1)) : d;
				var nextCompDepth = m_nextComposed.Count > 0 ? m_nextComposed.Values.Max(n => n.Depth(d + 1)) : d;
				var unvDepth = m_universal != null ? m_universal.Depth(d + 1) : d;
				return Math.Max(nextDepth, Math.Max(nextCompDepth, unvDepth));
			}

			public bool IsEmpty
			{
				get
				{
					if (m_hasValue)
						return false;

					return m_next.Count == 0 && m_nextComposed.Count == 0 && m_universal==null;
				}
			}

			public TreeNode(TreeNode other)
			{
				m_hasValue = other.m_hasValue;
				m_value = other.m_value;
				m_next = new SortedDictionary<string, TreeNode>(StringComparer.InvariantCultureIgnoreCase);
				foreach (var pair in other.m_next)
					m_next[pair.Key] = new TreeNode(pair.Value);

				m_nextComposed = new SortedDictionary<int, TreeNode>();
				foreach (var pair in other.m_nextComposed)
					m_nextComposed[pair.Key] = new TreeNode(pair.Value);

				if (other.m_universal != null)
					m_universal = new TreeNode(other.m_universal);
				else
					m_universal = null;
			}

			public TreeNode()
			{
				m_next = new SortedDictionary<string, TreeNode>(StringComparer.InvariantCultureIgnoreCase);
				m_nextComposed = new SortedDictionary<int, TreeNode>();
				m_universal = null;
				m_hasValue = false;
				m_value = default(T);
			}

			public void Clear()
			{
				m_next.Clear();
				m_universal = null;
				m_hasValue = false;
				m_value = default(T);
			}

			public int Count()
			{
				int count = 0;
				if (m_hasValue)
					count++;

				if (m_next.Count > 0)
				{
					count += m_next.Values.Sum(n => n.Count());
				}

				if (m_nextComposed.Count > 0)
					count += m_nextComposed.Values.Sum(n => n.Count());

				if (m_universal != null)
					count += m_universal.Count();

				return count;
			}

			public bool AddValue(Stack<Name> stack, T value, bool overwrite)
			{
				if (stack.Count == 0) //End stack
				{
					if (m_hasValue && !overwrite)
						return false;

					m_hasValue = true;
					m_value = value;
					return true;
				}

				TreeNode nodeToAdd;
				Name term = stack.Pop();

				int numOfTerms = term.NumberOfTerms;
				if (numOfTerms == 1)
				{
					string key = term.ToString();
					if (key == Symbol.UNIVERSAL_STRING)
					{
						if (m_universal == null)
							m_universal = new TreeNode();
						nodeToAdd = m_universal;
					}
					else if (!m_next.TryGetValue(key, out nodeToAdd))
					{
						nodeToAdd = new TreeNode();
						m_next[key] = nodeToAdd;
					}
				}
				else
				{
					using (var it = term.GetTerms().Reverse().GetEnumerator())
					{
						while (it.MoveNext())
						{
							stack.Push(it.Current);
						}
					}

					if (!m_nextComposed.TryGetValue(numOfTerms, out nodeToAdd))
					{
						nodeToAdd = new TreeNode();
						m_nextComposed[numOfTerms] = nodeToAdd;
					}
				}

				return nodeToAdd.AddValue(stack, value,overwrite);
			}

			public bool RemoveValue(Stack<Name> stack)
			{
				if (stack.Count == 0) //End stack
				{
					if (m_hasValue)
					{
						m_hasValue = false;
						m_value = default(T);
						return true;
					}
					return false;
				}

				TreeNode nodeToRemove;
				Name term = stack.Pop();

				int numOfTerms = term.NumberOfTerms;
				if (numOfTerms == 1)
				{
					string key = term.ToString();
					if (key == Symbol.UNIVERSAL_STRING)
					{
						if (m_universal == null)
							return false;

						if (!m_universal.RemoveValue(stack))
							return false;

						if (m_universal.IsEmpty)
							m_universal = null;
					}
					else
					{
						if (!m_next.TryGetValue(key, out nodeToRemove))
						{
							return false;
						}

						if (!nodeToRemove.RemoveValue(stack))
							return false;

						if (nodeToRemove.IsEmpty)
							m_next.Remove(key);
					}
				}
				else
				{
					using (var it = term.GetTerms().Reverse().GetEnumerator())
					{
						while (it.MoveNext())
						{
							stack.Push(it.Current);
						}
					}

					if (!m_nextComposed.TryGetValue(numOfTerms, out nodeToRemove))
						return false;

					if (!nodeToRemove.RemoveValue(stack))
						return false;

					if (nodeToRemove.IsEmpty)
						m_nextComposed.Remove(numOfTerms);
				}

				return true;
			}

			public bool Contains(Stack<Name> stack)
			{
				if (stack.Count == 0) //End stack
					return m_hasValue;

				TreeNode nodeToEvaluate;
				Name term = stack.Pop();

				int numOfTerms = term.NumberOfTerms;
				if (numOfTerms == 1)
				{
					string key = term.ToString();
					if (key == Symbol.UNIVERSAL_STRING)
					{
						if (m_universal != null)
							return m_universal.Contains(stack);
					}
					else
					{
						if (m_next.TryGetValue(key, out nodeToEvaluate))
							return nodeToEvaluate.Contains(stack);
					}
				}
				else
				{
					using (var it = term.GetTerms().Reverse().GetEnumerator())
					{
						while (it.MoveNext())
						{
							stack.Push(it.Current);
						}
					}

					if (m_nextComposed.TryGetValue(numOfTerms, out nodeToEvaluate))
						return nodeToEvaluate.Contains(stack);
				}

				return false;
			}

			public Pair<bool, T> Match(Stack<Name> stack)
			{
				if (stack.Count == 0) //End stack
					return Tuple.Create(m_hasValue, m_value);

				Name term = stack.Pop();

				TreeNode nodeToEvaluate;
				int numOfTerms = term.NumberOfTerms;
				if (numOfTerms == 1)
				{
					string key = term.ToString();
					if (key == Symbol.UNIVERSAL_STRING)
					{
						if (m_universal != null)
						{
							var res = m_universal.Match(stack);
							if (res.Item1)
								return res;
						}

						foreach (var node in m_next.Values)
						{
							var res = node.Match(stack);
							if (res.Item1)
								return res;
						}
					}
					else
					{
						if (m_next.TryGetValue(key, out nodeToEvaluate))
						{
							var res = nodeToEvaluate.Match(stack);
							if (res.Item1)
								return res;
						}

						if (m_universal != null)
						{
							var res = m_universal.Match(stack);
							if (res.Item1)
								return res;
						}
					}
				}
				else
				{
					if (m_nextComposed.TryGetValue(numOfTerms, out nodeToEvaluate))
					{
						using (var it = term.GetTerms().Reverse().GetEnumerator())
						{
							while (it.MoveNext())
							{
								stack.Push(it.Current);
							}
						}

						var res = nodeToEvaluate.Match(stack);
						if (res.Item1)
							return res;

						//Failed to evaluate, reconstruct stack
						for (int i = 0; i < term.NumberOfTerms; i++)
							stack.Pop();
					}

					if (m_universal != null)
					{
						var res = m_universal.Match(stack);
						if (res.Item1)
							return res;
					}
				}
				
				stack.Push(term);

				return Tuple.Create(false, default(T));
			}

			public int MatchAll(Stack<Name> stack, List<T> results)
			{
				if (stack.Count == 0) //End stack
				{
					if (!m_hasValue)
						return 0;
					results.Add(m_value);
					return 1;
				}

				int found = 0;
				Name term = stack.Pop();

				TreeNode nodeToEvaluate;
				int numOfTerms = term.NumberOfTerms;
				if (numOfTerms == 1)
				{
					string key = term.ToString();
					if (key == Symbol.UNIVERSAL_STRING)
					{
						if (m_universal != null)
							found += m_universal.MatchAll(stack, results);

						foreach (var node in GetNextLevel().SelectMany(t => t.Item2))
							found += node.MatchAll(stack, results);
					}
					else
					{
						if (m_next.TryGetValue(key, out nodeToEvaluate))
							found += nodeToEvaluate.MatchAll(stack, results);

						if (m_universal != null)
							found += m_universal.MatchAll(stack, results);
					}
				}
				else
				{
					if (m_nextComposed.TryGetValue(numOfTerms, out nodeToEvaluate))
					{
						using (var it = term.GetTerms().Reverse().GetEnumerator())
						{
							while (it.MoveNext())
							{
								stack.Push(it.Current);
							}
						}

						found += nodeToEvaluate.MatchAll(stack, results);

						for (int i = 0; i < numOfTerms; i++)
							stack.Pop();
					}

					if (m_universal != null)
						found += m_universal.MatchAll(stack, results);
				}
				
				stack.Push(term);

				return found;
			}

			public Pair<bool, T> Retrive(Stack<Name> stack)
			{
				if (stack.Count == 0) //End stack
					return Tuple.Create(m_hasValue, m_value);

				TreeNode nodeToEvaluate;
				var term = stack.Pop();
				int numOfTerms = term.NumberOfTerms;

				if (numOfTerms == 1)
				{
					string key = term.ToString();
					if (key == Symbol.UNIVERSAL_STRING)
					{
						if (m_universal != null)
						{
							var res = m_universal.Retrive(stack);
							if (res.Item1)
								return res;
						}
					}
					else
					{
						if (m_next.TryGetValue(key, out nodeToEvaluate))
						{
							var res = nodeToEvaluate.Retrive(stack);
							if (res.Item1)
								return res;
						}
					}
				}
				else
				{
					if (m_nextComposed.TryGetValue(numOfTerms, out nodeToEvaluate))
					{
						using (var it = term.GetTerms().Reverse().GetEnumerator())
						{
							while (it.MoveNext())
							{
								stack.Push(it.Current);
							}
						}

						var res = nodeToEvaluate.Retrive(stack);
						if (res.Item1)
							return res;

						//Failed to evaluate, reconstruct stack
						for (int i = 0; i < numOfTerms; i++)
							stack.Pop();
					}
				}
				
				stack.Push(term);
				return Tuple.Create(false, default(T));
			}

			public IEnumerable<T> GetValues()
			{
				if (m_hasValue)
					yield return m_value;

				var set = m_next.Values.Union(m_nextComposed.Values);
				if (m_universal != null)
					set = set.Append(m_universal);

				foreach (var t in set.SelectMany(n => n.GetValues()))
					yield return t;
			}

			public IEnumerable<Stack<Name>> GetKeys()
			{
				if (m_hasValue)
					yield return ObjectPool<Stack<Name>>.GetObject();

				foreach (var entry in m_next)
				{
					var term = new Symbol(entry.Key);
					var entryResults = entry.Value.GetKeys();
					foreach (var r in entryResults)
					{
						r.Push(term);
						yield return r;
					}
				}

				foreach (var entry in m_nextComposed)
				{
					int termsToMerge = entry.Key;
					var entryResults = entry.Value.GetKeys();
					foreach (var r in entryResults)
					{
						List<Name> tmp = ObjectPool<List<Name>>.GetObject();
						for (int i = 0; i < termsToMerge; i++)
							tmp.Add(r.Pop());
						Name term = new ComposedName(tmp);
						tmp.Clear();
						ObjectPool<List<Name>>.Recycle(tmp);

						r.Push(term);
						yield return r;
					}
				}

				if (m_universal != null)
				{
					var entryResults = m_universal.GetKeys();
					foreach (var r in entryResults)
					{
						r.Push(Symbol.UNIVERSAL_SYMBOL);
						yield return r;
					}
				}
			}

			public IEnumerable<Pair<Stack<Name>, T>> GetKeyValuePairs()
			{
				if (m_hasValue)
					yield return Tuple.Create(ObjectPool<Stack<Name>>.GetObject(), m_value);

				foreach (var entry in m_next)
				{
					var entryResults = entry.Value.GetKeyValuePairs();
					Name term = new Symbol(entry.Key);
					foreach (var r in entryResults)
					{
						r.Item1.Push(term);
						yield return r;
					}
				}

				foreach (var entry in m_nextComposed)
				{
					var entryResults = entry.Value.GetKeyValuePairs();
					int termsToMerge = entry.Key;
					foreach (var r in entryResults)
					{
						List<Name> tmp = ObjectPool<List<Name>>.GetObject();
						for (int i = 0; i < termsToMerge; i++)
							tmp.Add(r.Item1.Pop());
						Name term = new ComposedName(tmp);
						tmp.Clear();
						ObjectPool<List<Name>>.Recycle(tmp);

						r.Item1.Push(term);
						yield return r;
					}
				}

				if (m_universal != null)
				{
					var entryResults = m_universal.GetKeyValuePairs();
					foreach (var r in entryResults)
					{
						r.Item1.Push(Symbol.UNIVERSAL_SYMBOL);
						yield return r;
					}
				}
			}

			private IEnumerable<Pair<Stack<Name>, IEnumerable<TreeNode>>> CollectNextLevel(int level)
			{
				foreach (var entry in m_next)
				{
					var key = entry.Key;
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

				foreach (var entry in m_nextComposed)
				{
					var nextLevel = entry.Key;
					Dictionary<Name, IEnumerable<TreeNode>> group = new Dictionary<Name, IEnumerable<TreeNode>>();
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
						yield return Tuple.Create(s, pair.Value);
					}
				}

				if (m_universal != null)
				{
					if (level > 1)
					{
						foreach (var pair in m_universal.CollectNextLevel(level - 1))
						{
							pair.Item1.Push(Symbol.UNIVERSAL_SYMBOL);
							yield return pair;
						}
					}
					else
					{
						var s = ObjectPool<Stack<Name>>.GetObject();
						s.Push(Symbol.UNIVERSAL_SYMBOL);
						yield return Tuple.Create(s, (IEnumerable<TreeNode>)new[] { m_universal });
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

			public bool Bind(Stack<Name> stack, List<SubstitutionSet> resultsFound)
			{
				if (stack.Count == 0) //End stack
					return true;

				TreeNode nodeToEvaluate;
				Name term = stack.Pop();

				if (!term.IsVariable)
				{
					int numOfTerms = term.NumberOfTerms;
					if (numOfTerms == 1)
					{
						string key = term.ToString();
						if (key == Symbol.UNIVERSAL_STRING)
						{
							if (m_universal != null)
								return m_universal.Bind(stack, resultsFound);
						}
						else
						{
							if (m_next.TryGetValue(key, out nodeToEvaluate))
								return nodeToEvaluate.Bind(stack, resultsFound);
						}
					}
					else
					{
						if (m_nextComposed.TryGetValue(numOfTerms, out nodeToEvaluate))
						{
							using (var it = term.GetTerms().Reverse().GetEnumerator())
							{
								while (it.MoveNext())
								{
									stack.Push(it.Current);
								}
							}

							return nodeToEvaluate.Bind(stack, resultsFound);
						}
					}

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
						if (!node.Bind(stackClone, childResults))
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

				if (m_universal != null)
				{
					builder.AppendLine();
					Indent(builder, indent);
					builder.AppendLine("* :");
					m_universal.Write(builder, indent + 1);
				}
			}

			private static void Indent(StringBuilder builder, int indent)
			{
				for (int i = 0; i < indent; i++)
					builder.Append("\t");
			}
		}
	}
}
