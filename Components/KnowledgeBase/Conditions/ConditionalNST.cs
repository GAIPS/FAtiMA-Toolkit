using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using GAIPS.Serialization;
using GAIPS.Serialization.SerializationGraph;
using KnowledgeBase.Conditions;
using KnowledgeBase.WellFormedNames;
using KnowledgeBase.WellFormedNames.Collections;
using Utilities;

namespace KnowledgeBase
{
	[Serializable]
	public class ConditionalNST<T> : ICustomSerialization
	{
		private NameSearchTree<ConditionMapper<T>> m_dictionary=new NameSearchTree<ConditionMapper<T>>();

		public bool Add(Name name, ConditionEvaluatorSet conditionsEvaluator, T value)
		{
			ConditionMapper<T> conds;
			if (!m_dictionary.TryGetValue(name, out conds))
			{
				conds = new ConditionMapper<T>();
				m_dictionary[name] = conds;
			}

			return conds.Add(conditionsEvaluator,value);
		}

		public bool Remove(Name name, ConditionEvaluatorSet conditionsEvaluator, T value)
		{
			ConditionMapper<T> conds;
			if (!m_dictionary.TryGetValue(name, out conds))
				return false;

			if (!conds.Remove(conditionsEvaluator, value))
				return false;

			if (conds.Count == 0)
				m_dictionary.Remove(name);

			return true;
		}

		public IEnumerable<Pair<T,SubstitutionSet>> UnifyAll(Name expression, KB knowledgeBase, SubstitutionSet bindings)
		{
			if(bindings==null)
				bindings=new SubstitutionSet();

			var p1 = m_dictionary.Unify(expression, bindings);
			return p1.SelectMany(p => p.Item1.MatchConditions(knowledgeBase, p.Item2));
		}

		public ICollection<Name> Keys
		{
			get { return m_dictionary.Keys; }
		}

		public ICollection<ConditionEvaluatorSet> Conditions
		{
			get
			{
				var set =
					m_dictionary.Values.SelectMany(v => v.Select(m => m.Item1))
						.Distinct()
						.Select(c => c ?? new ConditionEvaluatorSet());
				return set.ToList();
			}
		}

		public ICollection<T> Values
		{
			get
			{
				var set = m_dictionary.Values.SelectMany(v => v.Select(m => m.Item2));
				return set.ToList();
			}
		}

		public void Clear()
		{
			m_dictionary.Clear();
		}

		public override int GetHashCode()
		{
			return m_dictionary.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			ConditionalNST<T> map = obj as ConditionalNST<T>;
			if (map == null)
				return false;

			var b = m_dictionary.Equals(map.m_dictionary);
			return b;
		}

		public void GetObjectData(ISerializationData dataHolder)
		{
			var seq = dataHolder.ParentGraph.BuildSequenceNode();

			foreach (var pair in m_dictionary)
			{
				foreach (var v in pair.Value)
				{
					var node = dataHolder.ParentGraph.CreateObjectData();
					node["key"] = dataHolder.ParentGraph.BuildNode(pair.Key);
					if (v.Item1 != null && v.Item1.Count > 0)
					{
						node["conditions"] = dataHolder.ParentGraph.BuildNode(v.Item1);
					}
					node["value"] = dataHolder.ParentGraph.BuildNode(v.Item2);
					seq.Add(node);
				}
			}

			dataHolder.SetValueGraphNode("values",seq);
		}

		public void SetObjectData(ISerializationData dataHolder)
		{
			m_dictionary.Clear();
			var seq = dataHolder.GetValueGraphNode("values") as ISequenceGraphNode;
			if(seq==null)
				throw new SerializationException("Unable to deserialize "+typeof(ConditionalNST<T>));

			foreach (var node in seq.Cast<IObjectGraphNode>())
			{
				var keyNode = node["key"];
				if(keyNode==null)
					throw new SerializationException("Unable to deserialize "+typeof(ConditionalNST<T>));

				var valueNode = node["value"];
				if(valueNode==null)
					throw new SerializationException("Unable to deserialize "+typeof(ConditionalNST<T>));

				Name key = keyNode.RebuildObject<Name>();
				T value = valueNode.RebuildObject<T>();
				ConditionEvaluatorSet cond = null;

				var condNode = node["conditions"];
				if (condNode != null)
					cond = condNode.RebuildObject<ConditionEvaluatorSet>();

				Add(key,cond,value);
			}
		}
	}
}