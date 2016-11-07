using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SerializationUtilities;
using SerializationUtilities.SerializationGraph;
using WellFormedNames;
using WellFormedNames.Collections;
using Utilities;
using IQueryable = WellFormedNames.IQueryable;
#if PORTABLE
using SerializationUtilities.Attributes;
#endif

namespace KnowledgeBase
{
	using BeliefPair = Pair<Name, IEnumerable<SubstitutionSet>>;

	[Serializable]
	public partial class KB : IQueryable, ICustomSerialization
	{
		private const int MAX_TOM_LVL = 2;

		private sealed class KnowledgeEntry
		{
			private Name m_universal = null;
			private Dictionary<Name, Name> m_perspectives;

			public Name GetValueFor(Name perspective)
			{
				Name value;
				if ((m_perspectives != null) && m_perspectives.TryGetValue(perspective, out value))
					return value;

				return m_universal;
			}

			public void TellValueFor(Name perspective, Name value)
			{
				if (perspective.IsUniversal)
				{
					m_universal = value;
					return;
				}

				if (m_perspectives == null)
				{
					if(value==null)
						return;

					m_perspectives = new Dictionary<Name, Name>();
				}

				if (value == null)
				{
					m_perspectives.Remove(perspective);
					if (m_perspectives.Count == 0)
						m_perspectives = null;
				}
				else
					m_perspectives[perspective] = value;
			}

			public bool IsEmpty()
			{
				return (m_perspectives == null) && (m_universal == null);
			}

			public IEnumerable<KeyValuePair<Name, Name>> GetPerspectives()
			{
				if (m_perspectives != null)
				{
					foreach (var p in m_perspectives)
						yield return p;
				}

				if(m_universal!=null)
					yield return new KeyValuePair<Name, Name>(Name.UNIVERSAL_SYMBOL, m_universal);
			}

			public IEnumerable<Name> GetAllStoredPerspectives()
			{
				return m_perspectives?.Keys.SelectMany(Key2ToMList).Distinct() ?? Enumerable.Empty<Name>();
			}
		}

		private NameSearchTree<KnowledgeEntry> m_knowledgeStorage;
		private NameSearchTree<DynamicKnowledgeEntry> m_dynamicProperties;

		/// <summary>
		/// Indicates the default mapping of "SELF"
		/// </summary>
		public Name Perspective { get; private set; }

		private KB()
		{
			m_knowledgeStorage = new NameSearchTree<KnowledgeEntry>();
			m_dynamicProperties = new NameSearchTree<DynamicKnowledgeEntry>();
			RegistNativeDynamicProperties(this);
		}

		public KB(Name perspective) : this()
		{
			SetPerspective(perspective);
		}

#region Native Dynamic Properties

		private static void RegistNativeDynamicProperties(IDynamicPropertiesRegister kb)
		{
			kb.RegistDynamicProperty(COUNT_TEMPLATE_NEW, CountPropertyCalculator_new, "The number of substitutions found for [x]");
		}

		//Count
		private static readonly Name COUNT_TEMPLATE_NEW = Name.BuildName("Count");
		private static IEnumerable<DynamicPropertyResult> CountPropertyCalculator_new(IQueryContext context, Name x)
		{
			var set = context.AskPossibleProperties(x).ToList();
			Name count = Name.BuildName(set.Count);
			IEnumerable<SubstitutionSet> sets;
			if (set.Count == 0)
				sets = context.Constraints;
			else
				sets = set.SelectMany(s => s.Item2).Distinct();

			foreach (var d in sets)
				yield return new DynamicPropertyResult(count, d);
		}

#endregion

		public void SetPerspective(Name newPerspective)
		{
			if(!newPerspective.IsPrimitive)
				throw new ArgumentException("Only primitive symbols are allowed to be perspectives.");

			if(newPerspective == Name.NIL_SYMBOL)
				throw new ArgumentException("NIL symbol cannot be used as a perspective.");

			if (newPerspective == Name.SELF_SYMBOL)
				throw new ArgumentException("SELF symbol cannot be set as a default a perspectives.");

			if (newPerspective==Perspective)
				return;

			if(GetAllPerspectives().Contains(newPerspective))
				throw new ArgumentException($"The is already beliefs containing perspectives for {newPerspective}. Changing to the requested perspective would result in belief conflicts.");

			//Modify believes to reflect perspective changes
			var newStorage = new NameSearchTree<KnowledgeEntry>();
			foreach (var entry in m_knowledgeStorage)
			{
				var newProperty = entry.Key.SwapTerms(Perspective, newPerspective);
				newStorage.Add(newProperty,entry.Value);
			}
			m_knowledgeStorage.Clear();
			m_knowledgeStorage = newStorage;
			Perspective = newPerspective;
		}

		private IEnumerable<Name> GetAllPerspectives()
		{
			return m_knowledgeStorage.Values.SelectMany(e => e.GetAllStoredPerspectives()).Distinct();
		}

		public Name AssertPerspective(Name perspective)
		{
			return ToMList2Key(AssertPerspective(perspective, nameof(perspective)));
		}

		public IEnumerable<Belief> GetAllBeliefs()
	    {
			foreach (var entry in m_knowledgeStorage)
			{
				foreach (var perspective in entry.Value.GetPerspectives())
				{
					yield return new Belief()
					{
						Name = entry.Key,
						Perspective = perspective.Key,
						Value = perspective.Value
					};
				}
			}
	    }

		public Name AskProperty(Name property)
		{
			return AskProperty(property, Name.SELF_SYMBOL);
		}

		public Name AskProperty(Name property, Name perspective)
		{
			if (!property.IsGrounded)
				throw new ArgumentException("The given Well Formed Name must be grounded",nameof(property));

			var results = AskPossibleProperties(property, perspective, null).Select(p => p.Item1).ToArray();
			if (results.Length==0)
				return null;
			if (results.Length == 1)
				return results[0];

			throw new Exception("More the 1 property found");
		}

		public IEnumerable<BeliefPair> AskPossibleProperties(Name property, Name perspective, IEnumerable<SubstitutionSet> constraints)
		{
			if (constraints == null)
				constraints = new[] { new SubstitutionSet() };

			if (property.IsPrimitive)
			{
				if (property == Name.SELF_SYMBOL)
					property = Perspective;

				return new[] { Tuples.Create(property, constraints) };
			}

			var ToMList = AssertPerspective(perspective, nameof(perspective));

			return internal_AskPossibleProperties(property, ToMList, constraints);
		}

		private IEnumerable<BeliefPair> internal_AskPossibleProperties(Name property, List<Name> ToMList, IEnumerable<SubstitutionSet> constraints)
		{
			property = RemovePropertyPerspective(property, ToMList);

			//ToM property shift
			property = ExtractPropertyFromToM(property, ToMList, nameof(property));

			var mind_key = ToMList2Key(ToMList);
			if (!property.IsVariable)
			{
				bool dynamicFound = false;
				foreach (var r in AskDynamicProperties(property, mind_key, constraints))
				{
					dynamicFound = true;
					yield return r;
				}
				if (dynamicFound)
					yield break;
			}

			var group = constraints.GroupBy(property.MakeGround);

			foreach (var g in group)
			{
				if (g.Key.IsPrimitive)
				{
					yield return Tuples.Create(g.Key, (IEnumerable<SubstitutionSet>)g);
					continue;
				}

				Name fact;
				try
				{
					fact = property.ApplyToTerms(p => SimplifyProperty(p, ToMList));
				}
				catch (Exception)
				{
					continue;
				}

				var g2 = g.SelectMany(c => m_knowledgeStorage.Unify(fact, c)).GroupBy(r => r.Item1, r => r.Item2);
				foreach (var r in g2)
				{
					var value = r.Key.GetValueFor(mind_key);
					if (value == null)
						continue;
					yield return Tuples.Create(value, r.Distinct());
				}
			}
		}

		public bool BeliefExists(Name name)
	    {
	        return m_knowledgeStorage.ContainsKey(name);
	    }
		
		public void Tell(Name property, Name value)
		{
			Tell(property,value,Name.SELF_SYMBOL);
		}

		public void Tell(Name property, Name value, Name perspective)
		{
			if (property.IsPrimitive)
				throw new ArgumentException("The given property name cannot be a primitive value.",nameof(property));

			if (!property.IsConstant)
				throw new ArgumentException("The given property name is not constant. Only constant names can be stored",nameof(property));

			var ToMList = AssertPerspective(perspective, nameof(perspective));
			property = RemovePropertyPerspective(property, ToMList);

			//ToM property shift
			property = ExtractPropertyFromToM(property, ToMList, nameof(property));

			if (m_dynamicProperties.Unify(property).Any())
				throw new ArgumentException("The given name will be objuscated by a dynamic property", nameof(property));

			var fact = property.ApplyToTerms(p => SimplifyProperty(p, ToMList));

			KnowledgeEntry entry;
			if (!m_knowledgeStorage.TryGetValue(fact, out entry))
			{
				entry = new KnowledgeEntry();
				m_knowledgeStorage[fact] = entry;
			}

			var mind_key = ToMList2Key(ToMList);
			entry.TellValueFor(mind_key,value);
			if (entry.IsEmpty())
				m_knowledgeStorage.Remove(fact);
		}

		private Name RemovePropertyPerspective(Name property, List<Name> ToMList)
		{
			var self = ToMList.Last();
			if (self.IsUniversal && property.HasSelf())
				throw new InvalidOperationException($"Cannot evaluate a property containing SELF in the Universal context of the {nameof(KB)}");

			if (self == Name.SELF_SYMBOL)
				self = Perspective;

			var p = property.RemovePerspective(self);
			return p;
		}

		private static Name ToMList2Key(IEnumerable<Name> ToMList)
		{
			Name current = null;
			foreach (var n in ToMList.Reverse())
			{
				if(!(n.IsPrimitive||n.IsUniversal))
					throw new ArgumentException("The name list can only have primitive or universal symbols",nameof(ToMList));
				if (current == null)
					current = n;
				else
					current = Name.BuildName(n, current);
			}
			return current;
		}

		private static IEnumerable<Name> Key2ToMList(Name key)
		{
			while (key.IsComposed)
			{
				var c = key.GetFirstTerm();
				yield return c;
				key = key.GetNTerm(1);
			}
			yield return key;
		}

		private Name SimplifyProperty(Name property,List<Name> ToMList)
		{
			if (!property.IsComposed)
				return property;

			var tmpList = ObjectPool<List<Name>>.GetObject();
			var r = ObjectPool<List<BeliefPair>>.GetObject();
			try
			{
				tmpList.AddRange(ToMList);
				r.AddRange(internal_AskPossibleProperties(property, tmpList, new[] {new SubstitutionSet()}));
				if (r.Count != 1)
				{
					if (r.Count == 0)
						throw new Exception($"{nameof(KB)} could not find property value for {property}");
					if (r.Count > 1)
						throw new Exception($"{nameof(KB)} found multiple valid values for {property}");
				}

				return r[0].Item1;
			}
			finally
			{
				r.Clear();
				ObjectPool<List<BeliefPair>>.Recycle(r);
				tmpList.Clear();
				ObjectPool<List<Name>>.Recycle(tmpList);
			}
		}

#region Auxiliary Methods

		/// <summary>
		/// 
		/// </summary>
		/// <param name="perspective">The perspective to evaluate</param>
		/// <returns>The ToM sequenceList</returns>
		private List<Name> AssertPerspective(Name perspective, string argumentName)
		{
			if(perspective == Name.NIL_SYMBOL)
				throw new ArgumentException("Perspectives cannot contain NIL symbol",argumentName);

			perspective = perspective.ApplyPerspective(Perspective);

			List<Name> ToMList = new List<Name>();
			if (perspective.IsUniversal)
			{
				ToMList.Add(Name.UNIVERSAL_SYMBOL);
				return ToMList;
			}

			ToMList.Add(Name.SELF_SYMBOL);
			if (perspective.IsPrimitive)
			{
				if(perspective != Name.SELF_SYMBOL)
					ToMList.Add(perspective);

				return ToMList;
			}

			if (!perspective.IsConstant)
				throw new ArgumentException("The given Theory of the Mind perspective needs to be constant", argumentName);

			//Validate ToM definition and ToM level
			var eval = perspective;

			while (eval.IsComposed)
			{
				if (eval.NumberOfTerms > 2)
					throw new ArgumentException($"Invalid perspective format {perspective}", argumentName);

				var f = eval.GetNTerm(0);

				AssetToMList(ToMList, f, argumentName);

				eval = eval.GetNTerm(1);
			}
			AssetToMList(ToMList, eval, argumentName);

			return ToMList;
		}

		private static void AssetToMList(List<Name> ToMList, Name current, string argName)
		{
			if(current==Name.NIL_SYMBOL)
				throw new ArgumentException("Perspectives cannot contain NIL symbol.", argName);

			if (current == Name.UNIVERSAL_SYMBOL)
				throw new ArgumentException("Perspectives cannot contain Universal symbol.", argName);

			var last = ToMList.LastOrDefault();
			if((last==null && current == Name.SELF_SYMBOL) || current!=last)
				ToMList.Add(current);

			if ((ToMList.Count - 1) > MAX_TOM_LVL)
				throw new ArgumentException($"Invalid Theory of the Mind level. Max ToM level {MAX_TOM_LVL}.", argName);
		}

		private static readonly Name TOM_NAME = Name.BuildName("ToM");
		private Name ExtractPropertyFromToM(Name property, List<Name> ToMList, string argumentName)
		{
			if (property.GetFirstTerm() != TOM_NAME)
				return property;

			if (property.NumberOfTerms != 3)
				throw new ArgumentException("The property name contains a invalid Theory of the Mind");

			var pers = property.GetNTerm(1);
			var prop = property.GetNTerm(2);

			AssetToMList(ToMList,pers,argumentName);
			return ExtractPropertyFromToM(prop, ToMList, argumentName);
		}

#endregion

		///// <summary>
		///// Removes a predicate from the Semantic KB
		///// </summary>
		///// <param name="predicate">the predicate to be removed</param>
		//public void Retract(Name predicate, Name perspective)
		//{
		//	if (predicate.IsPrimitive)
		//		throw new ArgumentException("Unable to retract primitive value", nameof(predicate));

		//	if (!predicate.IsConstant)
		//		throw new ArgumentException("The given name is not constant. Only constant names can be retracted", nameof(predicate));

		//	if (m_dynamicProperties.Unify(predicate).Any())
		//		throw new ArgumentException("The given name cannot be retracted as it is a dynamic property.", nameof(predicate));

		//	if (perspective.IsUniversal)
		//	{
		//		if (predicate.HasSelf())
		//			throw new Exception($"Cannot remove a property containing SELF from the Universal context of the {nameof(KB)}");
		//	}

		//	predicate = predicate.RemovePerspective(perspective.RemovePerspective(Perspective));

		//	SubstitutionSet set;
		//	var fact = predicate.Unfold(out set);
		//	if (set != null)
		//	{
		//		fact = FindConstantLeveledName(fact, perspective, set, true);
		//	}

		//	KnowledgeEntry entry;
		//	if (m_knowledgeStorage.TryGetValue(fact, out entry))
		//	{
		//		entry.TellValueFor(perspective, null);
		//		if (entry.IsEmpty())
		//			m_knowledgeStorage.Remove(fact);
		//	}
		//}

		//public int NumOfEntries
		//{
		//	get { return m_knowledgeStorage.Count; }
		//}

#region Serialization

		private string Perspective2String(Name perception)
		{
			if (perception.IsUniversal)
				return "Universal";

			return perception.ToString();
		}

		private Name String2Perspective(string str)
		{
			StringComparison c;
#if PORTABLE
			c = StringComparison.OrdinalIgnoreCase;
#else
			c = StringComparison.InvariantCultureIgnoreCase;
#endif
			if (string.Equals(str, "universal", c))
				return Name.UNIVERSAL_SYMBOL;

			return Name.BuildName(str);
		}

		public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			dataHolder.SetValue("Perspective",Perspective);
			var knowledge = dataHolder.ParentGraph.CreateObjectData();
			dataHolder.SetValueGraphNode("Knowledge",knowledge);
			foreach (var entry in m_knowledgeStorage)
			{
				foreach (var perspective in entry.Value.GetPerspectives())
				{
					var key = Perspective2String(perspective.Key);
					
					IGraphNode node;
					if (!knowledge.TryGetField(key, out node))
					{
						node = dataHolder.ParentGraph.CreateObjectData();
						knowledge[key] = node;
					}

					((IObjectGraphNode)node)[entry.Key.ToString()] = dataHolder.ParentGraph.BuildNode(perspective.Value);
				}
			}
		}

		public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			if(m_knowledgeStorage == null)
				m_knowledgeStorage = new NameSearchTree<KnowledgeEntry>();
			else
				m_knowledgeStorage.Clear();

			if(m_dynamicProperties==null)
				m_dynamicProperties = new NameSearchTree<DynamicKnowledgeEntry>();
			else
				m_dynamicProperties.Clear();
			RegistNativeDynamicProperties(this);

			Perspective = dataHolder.GetValue<Name>("Perspective");
			var knowledge = dataHolder.GetValueGraphNode("Knowledge");
			var it = ((IObjectGraphNode) knowledge).GetEnumerator();
			while (it.MoveNext())
			{
				var perspective = String2Perspective(it.Current.FieldName);
				var holder = (IObjectGraphNode) it.Current.FieldNode;
				foreach (var field in holder)
				{
					var property = (Name) field.FieldName;
					var value = field.FieldNode.RebuildObject<Name>();
					if(value==null)
						continue;
					
					Tell(property,value,perspective);
				}
			}
		}

#endregion
	}
}
