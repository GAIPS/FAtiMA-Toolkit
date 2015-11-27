using System;
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
			private SortedDictionary<string, TreeNode> m_nextSymbol;
			private SortedDictionary<string, TreeNode> m_nextVariable;
			private SortedDictionary<int, TreeNode> m_nextComposed;
			private TreeNode m_universal;
			private bool m_hasValue;
			private T m_value;

			public int Depth(int d)
			{
				var nextDepth = m_nextSymbol.Count > 0 ? m_nextSymbol.Values.Max(n => n.Depth(d + 1)) : d;
				var nextVarDepth = m_nextVariable.Count > 0 ? m_nextVariable.Values.Max(n => n.Depth(d + 1)) : d;
				var nextCompDepth = m_nextComposed.Count > 0 ? m_nextComposed.Values.Max(n => n.Depth(d + 1)) : d;
				var unvDepth = m_universal != null ? m_universal.Depth(d + 1) : d;
				return Math.Max(nextDepth, Math.Max(nextCompDepth, Math.Max(nextVarDepth, unvDepth)));
			}

			public bool IsEmpty
			{
				get
				{
					if (m_hasValue)
						return false;

					return m_nextSymbol.Count == 0 && m_nextComposed.Count == 0 && m_nextVariable.Count == 0 && m_universal==null;
				}
			}

			public TreeNode(TreeNode other)
			{
				m_hasValue = other.m_hasValue;
				m_value = other.m_value;
				m_nextSymbol = new SortedDictionary<string, TreeNode>(StringComparer.InvariantCultureIgnoreCase);
				foreach (var pair in other.m_nextSymbol)
					m_nextSymbol[pair.Key] = new TreeNode(pair.Value);

				m_nextVariable = new SortedDictionary<string, TreeNode>(StringComparer.InvariantCultureIgnoreCase);
				foreach (var pair in other.m_nextVariable)
					m_nextVariable[pair.Key]=new TreeNode(pair.Value);

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
				m_nextSymbol = new SortedDictionary<string, TreeNode>(StringComparer.InvariantCultureIgnoreCase);
				m_nextVariable = new SortedDictionary<string, TreeNode>(StringComparer.InvariantCultureIgnoreCase);
				m_nextComposed = new SortedDictionary<int, TreeNode>();
				m_universal = null;
				m_hasValue = false;
				m_value = default(T);
			}

			public void Clear()
			{
				m_nextSymbol.Clear();
				m_nextVariable.Clear();
				m_nextComposed.Clear();
				m_universal = null;
				m_hasValue = false;
				m_value = default(T);
			}

			public int Count()
			{
				int count = 0;
				if (m_hasValue)
					count++;

				if (m_nextSymbol.Count > 0)
				{
					count += m_nextSymbol.Values.Sum(n => n.Count());
				}

				if (m_nextComposed.Count > 0)
				{
					count += m_nextComposed.Values.Sum(n => n.Count());
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
					else
					{
						var set = term.IsVariable ? m_nextVariable : m_nextSymbol;
						if (!set.TryGetValue(key, out nodeToAdd))
						{
							nodeToAdd = new TreeNode();
							set[key] = nodeToAdd;
						}
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
						var set = term.IsVariable ? m_nextVariable : m_nextSymbol;

						if (!set.TryGetValue(key, out nodeToRemove))
						{
							return false;
						}

						if (!nodeToRemove.RemoveValue(stack))
							return false;

						if (nodeToRemove.IsEmpty)
							set.Remove(key);
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
						var set = term.IsVariable ? m_nextVariable : m_nextSymbol;
						if (set.TryGetValue(key, out nodeToEvaluate))
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
						var set = term.IsVariable ? m_nextVariable : m_nextSymbol;
						if (set.TryGetValue(key, out nodeToEvaluate))
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

				var set = m_nextSymbol.Values.Union(m_nextVariable.Values);
				set = set.Union(m_nextComposed.Values);
				if (m_universal != null)
					set = set.Append(m_universal);

				foreach (var t in set.SelectMany(n => n.GetValues()))
					yield return t;
			}

			public IEnumerable<Stack<Name>> GetKeys()
			{
				if (m_hasValue)
					yield return ObjectPool<Stack<Name>>.GetObject();

				foreach (var entry in m_nextSymbol.Union(m_nextVariable))
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

				foreach (var entry in m_nextSymbol.Union(m_nextVariable))
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
				foreach (var entry in m_nextSymbol.Union(m_nextVariable))
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

			public IEnumerable<Pair<T, SubstitutionSet>> Unify(Stack<Name> stack, SubstitutionSet binding)
			{
				if (stack.Count == 0) //End stack
				{
					if (m_hasValue)
						yield return Tuple.Create(m_value, binding);
					yield break;
				}

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
							{
								foreach (var pair in m_universal.Unify(stack, binding))
									yield return pair;
							}

							foreach (var node in GetNextLevel().SelectMany(p => p.Item2))
							{
								foreach (var pair in node.Unify(stack,binding))
								{
									yield return pair;
								}
							}
						}
						else
						{
							if (m_nextSymbol.TryGetValue(key, out nodeToEvaluate))
							{
								foreach (var pair in nodeToEvaluate.Unify(stack, binding))
									yield return pair;
							}

							if (m_universal != null)
							{
								foreach (var pair in m_universal.Unify(stack, binding))
									yield return pair;
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

							foreach (var pair in nodeToEvaluate.Unify(stack,binding))
								yield return pair;

							for (int i = 0; i < term.NumberOfTerms; i++)
								stack.Pop();
						}

						if (m_universal != null)
						{
							foreach (var pair in m_universal.Unify(stack,binding))
								yield return pair;
						}
					}

					if (binding != null)
					{
						//Find bindings with stored variables
						foreach (var pair in m_nextVariable)
						{
							var sub = new Substitution(new Symbol(pair.Key), term);
							if (binding.Conflicts(sub))
								continue;

							var set = new SubstitutionSet(binding);
							set.AddSubstitution(sub);
							foreach (var r in pair.Value.Unify(stack, set))
							{
								yield return r;
							}
						}
					}
				}
				else
				{
					//if binding is null, Unify behaves like a "MatchAll"
					if (binding != null)
					{
						var variable = (Symbol) term;
						//Find bindings
						foreach (var pair in GetNextLevel())
						{
							SubstitutionSet set = binding;
							if (!pair.Item1.IsVariable || pair.Item1 != variable)
							{
								var sub = new Substitution(variable, pair.Item1);
								if (binding.Conflicts(sub))
									continue;

								set = new SubstitutionSet(binding);
								set.AddSubstitution(sub);
							}

							foreach (var node in pair.Item2)
							{
								foreach (var r in node.Unify(stack, set))
								{
									yield return r;
								}
							}
						}
					}
					else
					{
						if (m_nextVariable.TryGetValue(term.ToString(), out nodeToEvaluate))
						{
							foreach (var pair in nodeToEvaluate.Unify(stack, null))
								yield return pair;
						}

						if (m_universal != null)
						{
							foreach (var pair in m_universal.Unify(stack, null))
								yield return pair;
						}
					}
				}

				stack.Push(term);
			}

			public void Write(StringBuilder builder, int indent)
			{
				Indent(builder, indent);
				if (m_hasValue)
					builder.AppendFormat("(has value) : {0}", m_value);
				else
					builder.Append("(no value)");

				foreach (var p in m_nextSymbol.Union(m_nextVariable))
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
