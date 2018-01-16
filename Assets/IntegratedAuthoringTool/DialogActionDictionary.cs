using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Utilities;
using WellFormedNames;
using WellFormedNames.Collections;

namespace IntegratedAuthoringTool
{
	internal class DialogActionDictionary : IEnumerable<DialogStateAction>
	{
		private NameSearchTree<HashSet<DialogStateAction>> m_nameToDialogues = new NameSearchTree<HashSet<DialogStateAction>>();
		private Dictionary<Guid, DialogStateAction> m_dialogDictionary = new Dictionary<Guid, DialogStateAction>();

		public int Count => m_dialogDictionary.Count;

		public void AddDialog(DialogStateAction action)
		{
			if(m_dialogDictionary.ContainsKey(action.Id))
				throw new Exception($"Duplicate Dialog State Action with the same id \"{action.Id}\"");

			var key = action.BuildSpeakAction();
			HashSet<DialogStateAction> set;
			if (!m_nameToDialogues.TryGetValue(key, out set))
			{
				set = new HashSet<DialogStateAction>();
				m_nameToDialogues.Add(key, set);
			}
			set.Add(action);
			m_dialogDictionary[action.Id] = action;
		}

        public bool RemoveDialog(Guid id)
		{
			DialogStateAction d;
			if (!m_dialogDictionary.TryGetValue(id, out d))
				return false;

			var key = d.BuildSpeakAction();
			var set = m_nameToDialogues[key];
			if (set.Remove(d))
			{
				if (set.Count == 0)
					m_nameToDialogues.Remove(key);
			}
			m_dialogDictionary.Remove(id);
			return true;
		}

		public IEnumerable<Pair<DialogStateAction,SubstitutionSet>> GetAllDialogsForKey(Name key, SubstitutionSet bindings = null)
		{
			var a = m_nameToDialogues.Unify(key, bindings);
			foreach (var pair in a)
			{
				foreach (var d in pair.Item1)
				{
					yield return Tuples.Create(d, pair.Item2);
				}
			}
		}

		public DialogStateAction GetDialogById(Guid id)
		{
			return m_dialogDictionary[id];
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// An enumerator that can be used to iterate through the collection.
		/// </returns>
		public IEnumerator<DialogStateAction> GetEnumerator()
		{
			return ((IEnumerable<DialogStateAction>)m_dialogDictionary.Values.ToArray()).GetEnumerator();
		}

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return m_dialogDictionary.Values.ToArray().GetEnumerator();
		}
	}
}