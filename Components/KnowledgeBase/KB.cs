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

	[Serializable]
	public class KB : ICustomSerialization
	{
		private sealed class KnowledgeEntry
		{
			public readonly object Value;
			public readonly bool IsPersistent;
			public readonly KnowledgeVisibility Visibility;

			public KnowledgeEntry(object value, bool isPersistent, KnowledgeVisibility visibility)
			{
				this.Value = value;
				this.IsPersistent = isPersistent;
				this.Visibility = visibility;
			}
		}

		private readonly NameSearchTree<KnowledgeEntry> m_knowledgeStorage = new NameSearchTree<KnowledgeEntry>();

		/// <summary>
		/// Asks the KB the Truth value of the received predicate
		/// </summary>
		/// <param name="predicate">The predicate to search in the KB</param>
		/// <returns>
		/// Under the Closed World Assumption, the predicate is considered 
		/// true if it is stored in the KB and false otherwise.
		/// </returns>
		public bool AskPredicate(Name predicate, SubstitutionSet constraints = null)
		{
			var value = AskProperty(predicate,constraints);
			return (value is bool) && (bool)value;
		}

		/// <summary>
		/// Asks the KB the value of a given property
		/// </summary>
		/// <param name="property">the property to search in the KB</param>
		/// <returns>the value stored inside the property, if the property exists. If the property does not exist, it returns null</returns>
		public object AskProperty(Name property, SubstitutionSet constraints = null)
		{
			if (property.IsPrimitive)
				return ConvertToPrimitiveValue((Symbol) property);

			if (constraints != null && !property.IsGrounded)
				property = property.MakeGround(constraints);

			if (!property.IsConstant)
				throw new Exception("The given name is not constant. Only constant names can be used for queries");	//TODO add a better exception

			SubstitutionSet set;
			var fact = property.Unfold(out set);
			if (set != null)
				fact = GroundName(fact, set);

			return SimpleAskProperty(fact, constraints);
		}

		private object ConvertToPrimitiveValue(Symbol name)
		{
			string str = name.Name;

			if (str.IndexOf('.') < 0)
			{
				bool b;
				if (bool.TryParse(str, out b))
					return b;

				int i;
				if (int.TryParse(str, out i))
					return i;

				long l;
				if (long.TryParse(str, out l))
					return l;
			}

			float f;
			if (float.TryParse(str, out f))
				return f;

			double d;
			if (double.TryParse(str, out d))
				return d;

			throw new ArgumentException("Unable to parse \""+str+"\" to a primitive value.");
		}

		private object SimpleAskProperty(Name property, SubstitutionSet constraints)
		{
			KnowledgeEntry result;
			if (constraints == null)
			{
				if (!m_knowledgeStorage.TryGetValue(property, out result))
					return null;
			}
			else
			{
				var u = m_knowledgeStorage.Unify(property, constraints).FirstOrDefault();
				if (u == null)
					return null;
				result = u.Item1;
			}

			return result.Value;
		}

		public IEnumerable<Pair<object, SubstitutionSet>> AskPossibleValues(Name property, SubstitutionSet constraints)
		{
			if(constraints==null)
				constraints = new SubstitutionSet();

			return m_knowledgeStorage.Unify(property, constraints).Select(p => Tuple.Create(p.Item1.Value, p.Item2));
		}

		/// <summary>
		/// Removes a predicate from the Semantic KB
		/// </summary>
		/// <param name="predicate">the predicate to be removed</param>
		public void Retract(Name predicate)
		{
			m_knowledgeStorage.Remove(predicate);
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
		/// <param name="name">a name (that correspond to a predicate or property)</param>
		/// <returns>a set of SubstitutionSets that make the received name to match predicates or properties that do exist in the KB</returns>
		public IEnumerable<SubstitutionSet> Unify(Name name, SubstitutionSet constraints = null)
		{
			List<SubstitutionSet> result = null;
			foreach (var v in m_knowledgeStorage.Unify(name,constraints))
			{
				if(result==null)
					result=new List<SubstitutionSet>();
				if(v.Item2.Count()==0)
					continue;

				result.Add(v.Item2);
			}
			return result;
		}

		public void Tell(Name property, object value, bool persistent=false, KnowledgeVisibility visibility=KnowledgeVisibility.Universal)
		{
			if(!value.GetType().IsPrimitiveData())
				throw new ArgumentException("Only primitive values are allowed to be told in the KB","value");

			if (!property.IsConstant)
				throw new Exception("The given name is not constant. Only constant names can be stored");	//TODO add a better exception

			if(property.IsPrimitive)
				throw new Exception("The given name is a primitive. Primitive values cannot be changed.");	//TODO add a better exception

			SubstitutionSet set;
			var fact = property.Unfold(out set);
			if (set != null)
			{
				fact = GroundName(fact, set);
			}

			m_knowledgeStorage[fact] = new KnowledgeEntry(value, persistent, visibility);
		}

		private static Name ConvertValueToName(object value)
		{
			Name v = value as Name;
			if (v == null)
			{
				if (!value.GetType().IsPrimitiveData())
					throw new Exception("Can only convert primitive types to Well Formed Names");

				v = new Symbol(value.ToString());
			}
			return v;
		}

		private Name GroundName(Name name, SubstitutionSet bindings)
		{
			var subs = new SubstitutionSet();
			foreach (var v in name.GetVariableList())
			{
				var value = bindings[v];
				if (!value.IsGrounded)
					value = GroundName(value, bindings);

				var prop = SimpleAskProperty(value, bindings);
				if(prop==null)
					throw new Exception(string.Format("Knowledge Base could not find a property for {0}",value));
				var s = new Substitution(v,ConvertValueToName(prop));
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

				IGraphNode node;
				if (e.Value.Value is string)
					node = dataHolder.ParentGraph.BuildStringNode((string) e.Value.Value);
				else
					node = dataHolder.ParentGraph.BuildPrimitiveNode((ValueType) e.Value.Value);

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
					object value = ToValue(e.FieldNode);
					if (value == null)
						continue;
					Tell(Name.Parse(e.FieldName), value, true, KnowledgeVisibility.Self);
				}
			}

			var universal = (IObjectGraphNode)dataHolder.GetValueGraphNode("universal");
			if (universal != null)
			{
				foreach (var e in universal)
				{
					object value = ToValue(e.FieldNode);
					if (value == null)
						continue;
					Tell(Name.Parse(e.FieldName), value, true, KnowledgeVisibility.Universal);
				}
			}
		}

		private static object ToValue(IGraphNode node)
		{
			switch (node.DataType)
			{
				case SerializedDataType.Boolean:
				case SerializedDataType.Number:
					return ((IPrimitiveGraphNode) node).Value;
				case SerializedDataType.String:
					return ((IStringGraphNode) node).Value;
			}
			return null;
		}
	}
}
