using System;
using System.Collections.Generic;
using System.Linq;
using GAIPS.Serialization;
using GAIPS.Serialization.SerializationGraph;
using KnowledgeBase.WellFormedNames;
using KnowledgeBase.WellFormedNames.Collections;
using Utilities;

namespace KnowledgeBase
{
	public enum KnowledgeVisibility
	{
		Universal,
		Self
	}

	public delegate IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> DynamicPropertyCalculator(KB kb, IDictionary<string,Name> args, SubstitutionSet constraints);

	[Serializable]
	public class KB : ICustomSerialization
	{
		private sealed class KnowledgeEntry
		{
			public readonly PrimitiveValue Value;
			public readonly bool IsPersistent;
			public readonly KnowledgeVisibility Visibility;

			public KnowledgeEntry(PrimitiveValue value, bool isPersistent, KnowledgeVisibility visibility)
			{
				this.Value = value;
				this.IsPersistent = isPersistent;
				this.Visibility = visibility;
			}
		}

		private sealed class DynamicKnowledgeEntry
		{
			public readonly DynamicPropertyCalculator surogate;
			
			public DynamicKnowledgeEntry(DynamicPropertyCalculator surogate)
			{
				this.surogate = surogate;
			}
		}

		private readonly NameSearchTree<KnowledgeEntry> m_knowledgeStorage = new NameSearchTree<KnowledgeEntry>();
		private readonly NameSearchTree<DynamicKnowledgeEntry> m_dynamicProperties = new NameSearchTree<DynamicKnowledgeEntry>();

		public KB()
		{
			RegistNativeDynamicProperties(this);
		}

#region Native Dynamic Properties

		private static void RegistNativeDynamicProperties(KB kb)
		{
			kb.RegistDynamicProperty(COUNT_TEMPLATE,CountPropertyCalculator);
		}

		//Count
		private static readonly Name COUNT_TEMPLATE = Name.BuildName("Count([x])");
		private static IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> CountPropertyCalculator(KB kb, IDictionary<string,Name> args, SubstitutionSet constraints)
		{
			var arg = args["x"];
			var r = kb.AskPossibleProperties(arg, constraints).ToList();
			PrimitiveValue c = r.Count;
			return new[] { Tuples.Create(c, constraints) };
		}

#endregion

		public void RegistDynamicProperty(Name propertyTemplate, DynamicPropertyCalculator surogate)
		{
			if(surogate==null)
				throw new ArgumentNullException("surogate");

			if(propertyTemplate.IsGrounded)
				throw new ArgumentException("Grounded names cannot be used as dynamic properties", "propertyTemplate");

			var r = m_dynamicProperties.Unify(propertyTemplate).FirstOrDefault();
			if (r != null)
			{
				throw new ArgumentException(string.Format("The given template {0} will collide with already registed {1} dynamic property", propertyTemplate, propertyTemplate.MakeGround(r.Item2)), "propertyTemplate");
			}

			if(m_knowledgeStorage.Unify(propertyTemplate).Any())
				throw new ArgumentException(string.Format("The given template {0} will collide with stored constant properties", propertyTemplate), "propertyTemplate");

			m_dynamicProperties.Add(propertyTemplate,new DynamicKnowledgeEntry(surogate));
		}

		/// <summary>
		/// Asks the KB the Truth value of the received predicate
		/// </summary>
		/// <param name="predicate">The predicate to search in the KB</param>
		/// <returns>
		/// Under the Closed World Assumption, the predicate is considered 
		/// true if it is stored in the KB and false otherwise.
		/// </returns>
		/*
		public bool AskPredicate(Name predicate, SubstitutionSet constraints = null)
		{
			var value = AskProperty(predicate,constraints);
			if (value == null)
				return false;
			if (value.GetTypeCode() != TypeCode.Boolean)
				return false;
			return value;
		}
		*/
		/// <summary>
		/// Asks the KB the value of a given predicate
		/// </summary>
		/// <param name="property">the predicate to search in the KB</param>
		/// <returns>the value stored inside the predicate, if the predicate exists. If the predicate does not exist, it returns null</returns>
		/*
		public PrimitiveValue AskProperty(Name property, SubstitutionSet constraints = null)
		{
			var r = AskPossibleProperties(property, constraints).ToList();
			if (r.Count==0)
				return null;
			if(r.Count>1)
				throw new Exception("Multiple results found for "+property);

			return r[0].Item1;
		}
		*/
		public IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> AskPossibleProperties(Name property, SubstitutionSet constraints)
		{
			if (property.IsPrimitive)
				return new[] {Tuples.Create(property.GetPrimitiveValue(), constraints)};

			if (constraints != null && !property.IsGrounded)
				property = property.MakeGround(constraints);

			var dynamicResult = AskDynamicProperties(property, constraints);
			if (dynamicResult != null)
				return dynamicResult;

			SubstitutionSet set;
			var fact = property.Unfold(out set);
			if (set != null)
				fact = FindConstantLeveledName(fact, set,false);
			if (fact == null)
				return Enumerable.Empty<Pair<PrimitiveValue, SubstitutionSet>>();

			return m_knowledgeStorage.Unify(fact, constraints).Select(p => Tuples.Create(p.Item1.Value, p.Item2));
		}

		public IEnumerable<SubstitutionSet> AskPossiblePredicates(Name predicate, SubstitutionSet constraints)
		{
			return AskPossibleProperties(predicate, constraints)
				.Where(p => p.Item1.GetTypeCode() == TypeCode.Boolean)
				.Select(p => p.Item2);
		}

		private IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> AskDynamicProperties(Name property, SubstitutionSet constraints)
		{
			const string tmpMarker = "_arg";
			if (m_dynamicProperties.Count == 0)
				return null;

			Name tmpPropertyName = property.ReplaceUnboundVariables(tmpMarker);

			var d = m_dynamicProperties.Unify(tmpPropertyName).ToList();
			if (d.Count == 0)
				return null;

			var results = d.SelectMany(p =>
			{
				var args = new Dictionary<string, Name>();
				foreach (var s in p.Item2)
				{
					var paramName = s.Variable.ToString();
					paramName = paramName.Substring(1, paramName.Length - 2);
					args[paramName] = s.Value.RemoveBoundedVariables(tmpMarker);
					if (s.Value.IsVariable)
					{
						//Unify can mix parameter Name with it's value, if the value is a variable.
						//In this case, flip a duplicate of the argument entry
						paramName = s.Value.ToString();
						paramName = paramName.Substring(1, paramName.Length - 2);
						args[paramName] = s.Variable.RemoveBoundedVariables(tmpMarker);
					}
				}
				return p.Item1.surogate(this, args, constraints);
			});

			var final = results.ToList();
			if (final.Count == 0)
				return Enumerable.Empty<Pair<PrimitiveValue, SubstitutionSet>>();

			return final.Select(p =>
			{
				if (p.Item2 == null)
					p.Item2 = new SubstitutionSet();
				//else
				//	p.Item2 = p.Item2.RemoveBoundedVariables(tmpMarker);
				return p;
			});
		}

		/// <summary>
		/// Removes a predicate from the Semantic KB
		/// </summary>
		/// <param name="predicate">the predicate to be removed</param>
		public void Retract(Name predicate)
		{
			if(predicate.IsPrimitive)
				throw new ArgumentException("Unable to retract primitive value","predicate");

			if (!predicate.IsConstant)
				throw new ArgumentException("The given name is not constant. Only constant names can be retracted","predicate");

			if(m_dynamicProperties.Unify(predicate).Any())
				throw new ArgumentException("The given name cannot be retracted as it is a dynamic property.", "predicate");

			SubstitutionSet set;
			var fact = predicate.Unfold(out set);
			if (set != null)
			{
				fact = FindConstantLeveledName(fact, set,true);
			}
			m_knowledgeStorage.Remove(fact);
		}

		/// <summary>
		/// This method provides a way to search for properties/predicates in the WorkingMemory 
		/// that match with a specified name with unbound variables.
		/// 
		/// In order to understand this method, let’s examine the following example. Suppose that 
		/// the memory only contains properties about two characters: Luke and John.
		/// Furthermore, it only stores two properties: their name and strength. So the KB will 
		/// only store the following objects:
		/// - Luke(Name) : Luke
		/// - Luke(Strength) : 8
		/// - John(Name) : John 
		/// - John(Strength) : 4
		/// 
		/// The next table shows the result of calling the method with several distinct names. 
		/// The function works by finding substitutions for the unbound variables, which make 
		/// the received name equal to the name of an object stored in memory.
		/// 
		/// Name			Substitutions returned
		/// Luke([x])		{{[x]/Name},{[x]/Strength}}
		/// [x](Strength)	{{[x]/John},{[x]/Luke}}
		/// [x]([y])		{{[x]/John,[y]/Name},{[x]/John,[y]/Strength},{[x]/Luke,[y]/Name},{[x]/Luke,[y]/Strength}}
		/// John(Name)	    {{}}
		/// John(Height)	null
		/// Paul([x])	    null
		/// 
		/// In the first example, there are two possible substitutions that make “Luke([x])”
		/// equal to the objects stored above. The third example has two unbound variables,
		/// so the returned set contains all possible combinations of variable attributions.
		/// 
		/// If this method receives a ground name, as seen on examples 4 and 5, it checks
		/// if the received name exists in memory. If so, a set with the empty substitution is
		/// returned, i.e. the empty substitution makes the received name equal to some object
		/// in memory. Otherwise, the function returns null, i.e. there is no substitution
		/// that applied to the name will make it equal to an object in memory. This same result
		/// is returned in the last example, since there is no object named Paul, and therefore no 
		/// substitution of [x] will match the received name with an existing object.
		/// </summary>
		/// <param name="name">a name (that correspond to a predicate or predicate)</param>
		/// <returns>a set of SubstitutionSets that make the received name to match predicates or properties that do exist in the KB</returns>
		public IEnumerable<SubstitutionSet> Unify(Name name, SubstitutionSet constraints = null)
		{
			return m_knowledgeStorage.Unify(name, constraints).Select(p => p.Item2);
		}

		public void Tell(Name property, PrimitiveValue value, bool persistent=false, KnowledgeVisibility visibility=KnowledgeVisibility.Universal)
		{
			if (property.IsPrimitive)
				throw new Exception("The given name is a primitive. Primitive values cannot be changed.");	//TODO add a better exception

			if (!property.IsConstant)
				throw new Exception("The given name is not constant. Only constant names can be stored");	//TODO add a better exception

			if (m_dynamicProperties.Unify(property).Any())
				throw new ArgumentException("The given name will be objuscated by a dynamic property","property");

			SubstitutionSet set;
			var fact = property.Unfold(out set);
			if (set != null)
			{
				fact = FindConstantLeveledName(fact, set,true);
			}

			m_knowledgeStorage[fact] = new KnowledgeEntry(value, persistent, visibility);
		}

		private Name FindConstantLeveledName(Name name, SubstitutionSet bindings,bool throwException)
		{
			var subs = new SubstitutionSet();
			foreach (var v in name.GetVariableList())
			{
				var value = bindings[v];
				if (!value.IsGrounded)
					value = FindConstantLeveledName(value, bindings,throwException);
				if (!throwException && value == null)
					return null;

				var r = AskPossibleProperties(value, bindings).ToList();
				if (r.Count != 1)
				{
					if (throwException)
					{
						if (r.Count == 0)
							throw new Exception(string.Format("Knowledge Base could not find property for {0}", value));
						if (r.Count > 1)
							throw new Exception(string.Format("Knowledge Base found multiple valid values for {0}", value));
					}
					else
						return null;
				}

				var s = new Substitution(v, Name.BuildName(r[0].Item1));
				subs.AddSubstitution(s);
			}
			return name.MakeGround(subs);
		}

		public void RemoveNonPersistent()
		{
			var nonPersistentEntries = m_knowledgeStorage.Where(p => !p.Value.IsPersistent).Select(p => p.Key).ToList();
			foreach (var entry in nonPersistentEntries)
			{
				m_knowledgeStorage.Remove(entry);
			}
		}

		public int NumOfEntries
		{
			get { return m_knowledgeStorage.Count; }
		}

		public void GetObjectData(ISerializationData dataHolder)
		{
			var self = dataHolder.ParentGraph.CreateObjectData();
			var universal = dataHolder.ParentGraph.CreateObjectData();

			foreach (var e in m_knowledgeStorage)
			{
				if(!e.Value.IsPersistent)
					continue;

				IGraphNode node = dataHolder.ParentGraph.BuildNode(e.Value.Value, typeof (PrimitiveValue));

				if(e.Value.Visibility == KnowledgeVisibility.Self)
					self[e.Key.ToString()]=node;
				else
					universal[e.Key.ToString()] = node;
			}

			if(self.NumOfFields>0)
				dataHolder.SetValueGraphNode("self",self);
			if(universal.NumOfFields>0)
				dataHolder.SetValueGraphNode("universal",universal);
		}

		public void SetObjectData(ISerializationData dataHolder)
		{
			var self = (IObjectGraphNode)dataHolder.GetValueGraphNode("self");
			if (self != null)
			{
				foreach (var e in self)
				{
					PrimitiveValue value = e.FieldNode.RebuildObject <PrimitiveValue>();
					if (value == null)
						continue;
					Tell(Name.BuildName(e.FieldName), value, true, KnowledgeVisibility.Self);
				}
			}

			var universal = (IObjectGraphNode)dataHolder.GetValueGraphNode("universal");
			if (universal != null)
			{
				foreach (var e in universal)
				{
					PrimitiveValue value = e.FieldNode.RebuildObject<PrimitiveValue>();
					if (value == null)
						continue;
					Tell(Name.BuildName(e.FieldName), value, true, KnowledgeVisibility.Universal);
				}
			}
		}
	}
}
