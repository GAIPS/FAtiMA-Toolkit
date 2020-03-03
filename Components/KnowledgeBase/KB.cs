using System;
using System.Collections.Generic;
using System.Linq;
using SerializationUtilities;
using SerializationUtilities.SerializationGraph;
using WellFormedNames;
using WellFormedNames.Collections;
using Utilities;
using IQueryable = WellFormedNames.IQueryable;
using System.Diagnostics;
using System.Globalization;

namespace KnowledgeBase
{
	using BeliefValueSubsPair = Pair<ComplexValue, IEnumerable<SubstitutionSet>>;
        
	[Serializable]
	public partial class KB : IQueryable, ICustomSerialization
	{
		private const int MAX_TOM_LVL = 2;

        private sealed class KnowledgeEntry
		{
			private ComplexValue m_universal = null;
			private Dictionary<Name, ComplexValue> m_perspectives;

			public ComplexValue GetValueFor(Name mindKey, Name finalPerspective)
			{
				ComplexValue belief;
				if ((m_perspectives != null) && m_perspectives.TryGetValue(mindKey, out belief))
                {
                    return new ComplexValue(belief.Value.SwapTerms(Name.SELF_SYMBOL, finalPerspective),
                        belief.Certainty);
                }

                return m_universal;
			}

			public void TellValueFor(Name perspective, Name value, float certainty)
			{
				if (perspective.IsUniversal)
				{
					m_universal = new ComplexValue(value);
					return;
				}

				if (m_perspectives == null)
				{
					if(value==null)
						return;

					m_perspectives = new Dictionary<Name, ComplexValue>();
				}

				if (value == null)
				{
					m_perspectives.Remove(perspective);
					if (m_perspectives.Count == 0)
						m_perspectives = null;
				}
                else
                {
                    var bValue = new ComplexValue(value,certainty);
                    m_perspectives[perspective] = bValue;
                }
			}

			public bool IsEmpty()
			{
				return (m_perspectives == null) && (m_universal == null);
			}

			public IEnumerable<KeyValuePair<Name, ComplexValue>> GetPerspectives()
			{
				if (m_perspectives != null)
				{
					foreach (var p in m_perspectives)
						yield return p;
				}

				if(m_universal!=null)
					yield return new KeyValuePair<Name, ComplexValue>(Name.UNIVERSAL_SYMBOL, m_universal);
			}

			public IEnumerable<Name> GetAllStoredPerspectives()
			{
				return m_perspectives?.Keys.SelectMany(Key2ToMList).Distinct() ?? Enumerable.Empty<Name>();
			}
		}

		private NameSearchTree<KnowledgeEntry> m_knowledgeStorage;
 

        /// <summary>
        /// Indicates the default mapping of "SELF"
        /// </summary>
        public Name Perspective { get; private set; }

		private KB()
		{
			m_knowledgeStorage = new NameSearchTree<KnowledgeEntry>();
            CreateRegistry();
			BindToRegistry(this);
		}

		public KB(Name perspective) : this()
		{
			SetPerspective(perspective);
		}


		private void BindToRegistry(IDynamicPropertiesRegistry registry)
		{
			registry.RegistDynamicProperty(COUNT_DP_NAME, COUNT_DP_DESC, CountPropertyCalculator_new);
            registry.RegistDynamicProperty(HAS_LITERAL_DP_NAME, HAS_LITERAL_DP_DESC, HasLiteralPropertyCalculator);
            registry.RegistDynamicProperty(MATH_DP_NAME, MATH_DP_DESC, MathPropertyCalculator);
            registry.RegistDynamicProperty(SQUARE_DISTANCE_NAME, SQUARE_DISTANCE_DESC, SquareDistanceCalculator);
            registry.RegistDynamicProperty(ASK_DP_NAME, ASK_DP_DESC, AskDynamicProperty);
            registry.RegistDynamicProperty(TELL_DP_NAME, TELL_DP_DESC, TellDynamicProperty);
            registry.RegistDynamicProperty(EXISTS_DP_NAME, EXISTS_DP_DESC, ExistsDynamicProperty);
        }

		#region Native Dynamic Properties

		//Count
		private static readonly Name COUNT_DP_NAME = Name.BuildName("CountSubs");
        private static readonly string COUNT_DP_DESC = "The number of substitutions found for [x].";
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
				yield return new DynamicPropertyResult(new ComplexValue(count,1.0f), d);
		}

        //Math
        private static readonly Name MATH_DP_NAME = Name.BuildName("Math");
        private static readonly string MATH_DP_DESC = "Applies the mathematical [op] to [x] and "+
            "[y] and return the result. The [op] can be either 'Plus', 'Minus', 'Times', 'Div'.";
        private static IEnumerable<DynamicPropertyResult> MathPropertyCalculator(IQueryContext context, Name x, Name op, Name y)
        {
            if (op.IsVariable || op.IsComposed)
                yield break;

            //TODO: Make sure that every dynamic property checks for empty constraints set
            var constraintsSets = context.Constraints.Any() ? context.Constraints : new[] { new SubstitutionSet() }; 

            foreach (var subSet in constraintsSets)
            {
                foreach (var xSubs in context.AskPossibleProperties(x).ToList())
                {
                    foreach(var ySubs in context.AskPossibleProperties(y).ToList())
                    {
                        if(xSubs.Item1.Value == Name.NIL_SYMBOL || ySubs.Item1.Value == Name.NIL_SYMBOL)
                            throw new Exception("Trying to perform a MATH operation on a Nil value");

                        float i = 0;

                        if(!float.TryParse(xSubs.Item1.Value.ToString(), out i) || !float.TryParse(ySubs.Item1.Value.ToString(), out i))
                            yield break;
                        

                        var xValue = float.Parse(xSubs.Item1.Value.ToString(), CultureInfo.InvariantCulture);
                        var yValue = float.Parse(ySubs.Item1.Value.ToString(), CultureInfo.InvariantCulture);

                        if (op.ToString().EqualsIgnoreCase("Plus"))
                        {
                            var res = xValue + yValue;
                            yield return new DynamicPropertyResult(new ComplexValue(Name.BuildName(res)), subSet);
                        }

                        if (op.ToString().EqualsIgnoreCase("Minus"))
                        {
                            var res = xValue - yValue;
                            yield return new DynamicPropertyResult(new ComplexValue(Name.BuildName(res)), subSet);
                        }

                        if (op.ToString().EqualsIgnoreCase("Times"))
                        {
                            var res = xValue * yValue;
                            yield return new DynamicPropertyResult(new ComplexValue(Name.BuildName(res)), subSet);
                        }

                        if (op.ToString().EqualsIgnoreCase("Div"))
                        {
                            var res = xValue / yValue;
                            yield return new DynamicPropertyResult(new ComplexValue(Name.BuildName(res)), subSet);
                        }
                    }
                }
            }
        }

        //Square Distance
        private static readonly Name SQUARE_DISTANCE_NAME = Name.BuildName("SquareDistance");
        private static readonly string SQUARE_DISTANCE_DESC = "Returns the square distance between point (x1, y1) and (x1, y2)";
        private static IEnumerable<DynamicPropertyResult> SquareDistanceCalculator(IQueryContext context, Name x1, Name y1, Name x2, Name y2)
        {
            foreach (var subSet in context.Constraints)
            {
                foreach (var x1Subs in context.AskPossibleProperties(x1).ToList())
                {
                    foreach (var y1Subs in context.AskPossibleProperties(y1).ToList())
                    {
                        foreach(var x2Subs in context.AskPossibleProperties(x2).ToList())
                        {
                            foreach(var y2Subs in context.AskPossibleProperties(y2).ToList())
                            {
                                var x1Value = float.Parse(x1Subs.Item1.Value.ToString(), CultureInfo.InvariantCulture);
                                var y1Value = float.Parse(y1Subs.Item1.Value.ToString(), CultureInfo.InvariantCulture);
                                var x2Value = float.Parse(x2Subs.Item1.Value.ToString(), CultureInfo.InvariantCulture);
                                var y2Value = float.Parse(y2Subs.Item1.Value.ToString(), CultureInfo.InvariantCulture);

                                var res = Math.Pow((x2Value - x1Value), 2) + Math.Pow((y2Value - y1Value), 2);

                                yield return new DynamicPropertyResult(new ComplexValue(Name.BuildName(res)), subSet);
                            }
                        }
                    }
                }
            }
        }


        //HasLiteral
        private static readonly Name HAS_LITERAL_DP_NAME = (Name) "HasLiteral";
        private static readonly string HAS_LITERAL_DP_DESC = "Returns true if [lit] is present in [name] and false otherwise.";
		private static IEnumerable<DynamicPropertyResult> HasLiteralPropertyCalculator(IQueryContext context, Name lit, Name name)
		{
			if (!(name.IsVariable || name.IsComposed))
				return Enumerable.Empty<DynamicPropertyResult>();

			List<DynamicPropertyResult> results = ObjectPool<List<DynamicPropertyResult>>.GetObject();
			try
			{
				IEnumerable<Name> candidates;
				if (name.IsVariable)
				{
					candidates = context.Constraints.Select(s => s[name]).Where(n => n != null);
				}
				else if (!name.IsGrounded)
				{
					candidates = context.Constraints.Select(name.MakeGround).Where(c => c.IsConstant);
				}
				else
				{
					candidates = new[] { name };
				}

				foreach (var c in candidates)
				{
					foreach (var t in c.GetTerms())
					{
						if (lit.IsVariable)
						{
							var sub = new Substitution(lit,new ComplexValue(t));
							foreach (var g in context.Constraints)
							{
								if(g.Conflicts(sub))
									continue;

								var s = new SubstitutionSet(g);
								s.AddSubstitution(sub);
								results.Add(new DynamicPropertyResult(new ComplexValue(Name.BuildName(true)),s));
							}
						}
						else
						{
							foreach (var x2 in context.AskPossibleProperties(lit))
							{
                                if (x2.Item1.Value == t)
								{
									var r = Name.BuildName(true);
									foreach (var s in x2.Item2)
									{
										results.Add(new DynamicPropertyResult(new ComplexValue(r), s));
									}
								}
							}
						}
					}
				}

				return results.ToArray();
			}
			finally
			{
				results.Clear();
				ObjectPool<List<DynamicPropertyResult>>.Recycle(results);
			}
		}


        private static readonly Name ASK_DP_NAME = (Name)"Ask";
        private static readonly string ASK_DP_DESC = "Returns the value of the belief identified by [property]";
        private static IEnumerable<DynamicPropertyResult> AskDynamicProperty(IQueryContext context, Name property)
        {
            var constraintsSets = context.Constraints.Any() ? context.Constraints : new[] { new SubstitutionSet() };
            
            foreach (var c in constraintsSets)
            {
                Name propValue = null;

                if (!property.IsComposed)
                {
                    propValue = property;
                }
                else
                {
                    propValue = context.AskPossibleProperties(property).FirstOrDefault()?.Item1.Value;
                }

                if(propValue == null)
                {
                    yield return new DynamicPropertyResult(new ComplexValue(Name.NIL_SYMBOL), c);
                }
                else
                {
                    yield return new DynamicPropertyResult(new ComplexValue(propValue), c);
                }
            }
        }


        private static readonly Name TELL_DP_NAME = (Name)"Tell";
        private static readonly string TELL_DP_DESC = "Adds or updates the KB with a new belief named [property] with a given [value]";
        private IEnumerable<DynamicPropertyResult> TellDynamicProperty(IQueryContext context, Name property, Name value)
        {
            var constraintsSets = context.Constraints.Any() ? context.Constraints : new[] { new SubstitutionSet() };

            foreach (var c in constraintsSets)
            {
                if (property.IsPrimitive)
                {
                    yield return new DynamicPropertyResult(new ComplexValue(value), c);
                }
                else
                {
                    Name val;
                    if(!value.IsPrimitive)
                    {
                        val = context.AskPossibleProperties(value).FirstOrDefault()?.Item1.Value;
                        if(val == null)
                        {
                            val = Name.NIL_SYMBOL;
                        }
                        this.Tell(property, val);
                        yield return new DynamicPropertyResult(new ComplexValue(val), c);
                    }
                    else
                    {
                        this.Tell(property, value);
                        yield return new DynamicPropertyResult(new ComplexValue(value), c);
                    }
                }
            }
        }


        private static readonly Name EXISTS_DP_NAME = (Name)"Exists";
        private static readonly string EXISTS_DP_DESC = "Checks if [property] exists in the KB and returns true or false accordingly";
        private IEnumerable<DynamicPropertyResult> ExistsDynamicProperty(IQueryContext context, Name property)
        {
            var constraintsSets = context.Constraints.Any() ? context.Constraints : new[] { new SubstitutionSet() };

            foreach (var c in constraintsSets)
            {
                if (property.IsPrimitive)
                {
                    yield return new DynamicPropertyResult(new ComplexValue(Name.BuildName(false)), c);
                }
                else
                {
                    if (this.BeliefExists(property))
                    {
                        yield return new DynamicPropertyResult(new ComplexValue(Name.BuildName(true)), c);
                    }
                    else
                    {
                        yield return new DynamicPropertyResult(new ComplexValue(Name.BuildName(false)), c);
                    }
                }
            }
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
                        Value = perspective.Value.Value,
                        Certainty = perspective.Value.Certainty
					};
				}
			}
	    }

        public ComplexValue AskProperty(Name property)
		{
			return AskProperty(property, Name.SELF_SYMBOL);
		}

        
        public ComplexValue AskProperty(Name property, Name perspective)
		{
			if (!property.IsGrounded)
				throw new ArgumentException("The given Well Formed Name must be grounded",nameof(property));

			var results = AskPossibleProperties(property, perspective, null).Select(p => p.Item1).ToArray();
			if (results.Length==0)
				return new ComplexValue(Name.NIL_SYMBOL);
			if (results.Length == 1)
				return results[0];

			throw new Exception("More than 1 property found");
		}


		public IEnumerable<BeliefValueSubsPair> AskPossibleProperties(Name property, Name perspective, IEnumerable<SubstitutionSet> constraints)
		{
			if (constraints == null)
				constraints = new[] { new SubstitutionSet() };

			if (property.IsPrimitive)
			{
				if (property == Name.SELF_SYMBOL)
				{
					var p = AssertPerspective(perspective, nameof(perspective)).Last();
					if (p == Name.SELF_SYMBOL)
						p = Perspective;
					property = p;
				}

				return new[] { Tuples.Create(new ComplexValue(property), constraints) };
			}

			var ToMList = AssertPerspective(perspective, nameof(perspective));

			return internal_AskPossibleProperties(property, ToMList, constraints);
		}

		private IEnumerable<BeliefValueSubsPair> internal_AskPossibleProperties(Name property, List<Name> ToMList, IEnumerable<SubstitutionSet> constraints)
		{
			property = RemovePropertyPerspective(property, ToMList);

			//ToM property shift
			property = ExtractPropertyFromToM(property, ToMList, nameof(property));

			var mind_key = ToMList2Key(ToMList);
			if (!property.IsVariable)
			{
				bool dynamicFound = false;
				//foreach (var r in _registry.Evaluate(this, property, mind_key, constraints))
				foreach (var match in _registry.Evaluate(property))
				{
					dynamicFound = true;
					foreach (var r in match.Evaluate(this, mind_key, constraints))
					{
						yield return r;
					}
				}
				if (dynamicFound)
					yield break;
			}

			var group = constraints.GroupBy(property.MakeGround);

			foreach (var g in group)
			{
				if (g.Key.IsPrimitive)
				{
					yield return Tuples.Create(new ComplexValue(g.Key),(IEnumerable<SubstitutionSet>)g);
					continue;
				}

				Name fact;
				try
				{
					fact = property.ApplyToTerms(p => ApplyDynamic(p,mind_key));
				}
				catch (Exception)
				{
					continue;
				}

				var g2 = g.SelectMany(c => m_knowledgeStorage.Unify(fact, c)).GroupBy(r => r.Item1, r => r.Item2);
				foreach (var r in g2)
				{
					var value = r.Key.GetValueFor(mind_key,GetFinalPerspective(ToMList));
					if (value == null)
						continue;
                    var subSets = r.Distinct();
                    foreach(var set in subSets)
                    {
                        foreach(var sub in set)
                        {
                            if(sub.SubValue.Certainty == -1) //Trick
                            {
                                sub.SubValue.Certainty = value.Certainty;
                            }
                        }
                    }
					yield return Tuples.Create(new ComplexValue(value.Value, value.Certainty), subSets);
				}
			}
		}

		private Name GetFinalPerspective(IEnumerable<Name> ToMList)
		{
			var last = Perspective;
			foreach (var n in ToMList)
			{
				if (n != Name.SELF_SYMBOL)
					last = n;
			}
			return last;
		}

		public bool BeliefExists(Name name)
	    {
	        return m_knowledgeStorage.ContainsKey(name);
	    }
		
		public void Tell(Name property, Name value)
		{
			Tell(property,value,Name.SELF_SYMBOL,1.0f);
		}

        public void Tell(Name property, Name value, Name perspective)
        {
            Tell(property, value, perspective, 1.0f);
        }

        public void Tell(Name property, Name value, Name perspective, float certainty)
		{
			if (property.IsPrimitive)
				throw new ArgumentException("The given property name cannot be a primitive value.",nameof(property));

			if (!property.IsConstant)
				throw new ArgumentException("The given property name is not constant. Only constant names can be stored",nameof(property));

			var ToMList = AssertPerspective(perspective, nameof(perspective));
			property = RemovePropertyPerspective(property, ToMList);

			//ToM property shift
			property = ExtractPropertyFromToM(property, ToMList, nameof(property));

			if (_registry.WillEvaluate(property))
				throw new ArgumentException("The given name will be obfuscated by a dynamic property", nameof(property));

			var mind_key = ToMList2Key(ToMList);

			var fact = property.ApplyToTerms(p => ApplyDynamic(p,mind_key));

			KnowledgeEntry entry;
			if (!m_knowledgeStorage.TryGetValue(fact, out entry))
			{
				entry = new KnowledgeEntry();
				m_knowledgeStorage[fact] = entry;
			}

			entry.TellValueFor(mind_key,value,certainty);
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

		private Name ApplyDynamic(Name property, Name perspective)
		{
			if (!property.IsComposed)
				return property;

			Name value = null;
			bool found = false;
			using (var it = _registry.Evaluate(property).SelectMany(m => m.Evaluate(this, perspective, null)).GetEnumerator())
			{
				while (it.MoveNext())
				{
					if(found)
						throw new Exception($"{nameof(KB)} found multiple valid values for {property}");

					value = it.Current.Item1.Value;
					found = true;
				}
			}

			return found ? value : property.ApplyToTerms(p => ApplyDynamic(p,perspective));
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
			if (!property.IsComposed)
				return property;

			if (property.GetFirstTerm() != TOM_NAME)
				return property;

			if (property.NumberOfTerms != 3)
				throw new ArgumentException("The property name contains a invalid Theory of the Mind");

			var pers = property.GetNTerm(1);
			var prop = property.GetNTerm(2);

			AssetToMList(ToMList,pers,argumentName);
			return ExtractPropertyFromToM(prop, ToMList, argumentName);
		}

	    public void removeBelief(Name bName)
	    {
	        m_knowledgeStorage.Remove(bName);
	    }
		
#endregion

#region Serialization

		private string Perspective2String(Name perception)
		{
			if (perception.IsUniversal)
				return Name.UNIVERSAL_STRING;

			return perception.ToString();
		}

		private Name String2Perspective(string str)
		{
			StringComparison c;

			c = StringComparison.InvariantCultureIgnoreCase;

			if (string.Equals(str, Name.UNIVERSAL_STRING, c))
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

                    ((IObjectGraphNode)node)[entry.Key.ToString()] = dataHolder.ParentGraph.BuildNode(perspective.Value.Serialize());
				}
			}
        }

        public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			if(m_knowledgeStorage == null)
				m_knowledgeStorage = new NameSearchTree<KnowledgeEntry>();
			else
				m_knowledgeStorage.Clear();

			if (_registry == null)
				CreateRegistry();
			else
				_registry.Clear();

			BindToRegistry(this);

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
					var value = ComplexValue.Deserialize(field.FieldNode.RebuildObject<string>());
					if(value==null)
						continue;
					
					Tell(property,value.Value, perspective, value.Certainty);
				}
			}
		}

#endregion
	}
}
