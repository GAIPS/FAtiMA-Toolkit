using System;
using System.Collections.Generic;
using System.Linq;
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

	public class KnowledgeBase
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
		/// Asks the Memory the Truth value of the received predicate
		/// </summary>
		/// <param name="predicate">The predicate to search in the Memory</param>
		/// <returns>
		/// Under the Closed World Assumption, the predicate is considered 
		/// true if it is stored in the Memory and false otherwise.
		/// </returns>
		public bool AskPredicate(Name predicate)
		{
			KnowledgeEntry result;
			if (!m_knowledgeStorage.TryGetValue(predicate, out result))
				return true;

			return (result.Value is bool) && (bool)result.Value;
		}

		/// <summary>
		/// Asks the Memory the value of a given property
		/// </summary>
		/// <param name="property">the property to search in the Memory</param>
		/// <returns>the value stored inside the property, if the property exists. If the property does not exist, it returns null</returns>
		public object AskProperty(Name property) {
			KnowledgeEntry result;
			if (!m_knowledgeStorage.TryGetValue(property, out result))
				return null;
			return result.Value;
		}
		
		/// <summary>
		/// Removes a predicate from the Semantic Memory
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
		/// <returns>a set of SubstitutionSets that make the received name to match predicates or properties that do exist in the KnowledgeBase</returns>
		public IEnumerable<SubstitutionSet> GetPossibleBindings(Name name)
		{
			return m_knowledgeStorage.GetPosibleBindings(name);
		}

		public void Tell(Name property, object value, bool persistent, KnowledgeVisibility visibility)
		{
			if (!property.IsConstant)
				throw new Exception("The given name is not constant. Only constant names can be stored");	//TODO add a better exception

			if(!(value is Name || value is bool || value.GetType().IsNumeric()))
				throw new Exception("invalid value type to be stored");	//TODO add a better exception

			m_knowledgeStorage[property] = new KnowledgeEntry(value, persistent, visibility);
		}

		public void RemoveNonPersistent()
		{
			var nonPersistentEntries = m_knowledgeStorage.Where(p => !p.Value.IsPersistent).Select(p => p.Key).ToList();
			foreach (var entry in nonPersistentEntries)
			{
				m_knowledgeStorage.Remove(entry);
			}
		}
	}
}
