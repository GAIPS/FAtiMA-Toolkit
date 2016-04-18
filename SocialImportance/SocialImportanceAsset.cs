using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ActionLibrary;
using AssetPackage;
using EmotionalAppraisal;
using GAIPS.Serialization;
using KnowledgeBase;
using KnowledgeBase.WellFormedNames;
using KnowledgeBase.WellFormedNames.Collections;
using SocialImportance.DTOs;
using Utilities;

namespace SocialImportance
{
	public sealed class SocialImportanceAsset: BaseAsset
	{
		public static SocialImportanceAsset LoadFromFile(string filename)
		{
			var asset = new SocialImportanceAsset();
			using (var f = File.Open(filename, FileMode.Open, FileAccess.Read))
			{
				var serializer = new JSONSerializer();
				asset.LoadFromDTO(serializer.Deserialize<SocialImportanceDTO>(f));
			}
			return asset;
		}

		private EmotionalAppraisalAsset m_ea = null;
		private AttributionRule[] m_attributionRules = new AttributionRule[0];
		private NameSearchTree<float> m_claimTree = new NameSearchTree<float>();

		//Volatile Statements
		private NameSearchTree<NameSearchTree<float>> m_cachedSI = new NameSearchTree<NameSearchTree<float>>();

		public SocialImportanceAsset()
		{
			m_ea = null;
			m_attributionRules = new AttributionRule[0];
			m_claimTree = new NameSearchTree<float>();
			m_cachedSI = new NameSearchTree<NameSearchTree<float>>();
		}

		public void RegisterEmotionalAppraisalAsset(EmotionalAppraisalAsset eaa)
		{
			if (m_ea != null)
			{
				//Unregist bindings
				RemoveKBBindings();
				m_ea = null;
			}

			m_ea = eaa;
			PerformKBBindings();
		}

		private void PerformKBBindings()
		{
			m_ea.Kb.RegistDynamicProperty(SI_DYNAMIC_PROPERTY_TEMPLATE,SIPropertyCalculator);
		}

		private void RemoveKBBindings()
		{
			m_ea.Kb.UnregistDynamicProperty(SI_DYNAMIC_PROPERTY_TEMPLATE);
		}

		public void LoadFromDTO(SocialImportanceDTO dto)
		{
			m_attributionRules = dto.AttributionRules.Select(adto => new AttributionRule(adto)).ToArray();

			m_claimTree.Clear();
			if(dto.Claims!=null)
			{
				foreach (var c in dto.Claims)
				{
					var n = Name.BuildName(c.ActionTemplate);
					m_claimTree.Add(n,c.ClaimSI);
				}
			}

			//TODO load the rest
		}

		public SocialImportanceDTO GetDTO()
		{
			throw new NotImplementedException();
		}

		public void SaveToFile(Stream file)
		{
			var serializer = new JSONSerializer();
			serializer.Serialize(file, GetDTO());
		}

		private void ValidateEALink()
		{
			if(m_ea==null)
				throw new InvalidOperationException($"Cannot execute operation as an instance of {nameof(EmotionalAppraisalAsset)} was not registed in this asset.");
		}

		public float GetSocialImportance(Name target, Name perspective)
		{
			ValidateEALink();
			if(!target.IsPrimitive)
				throw new ArgumentException("must be a primitive name",nameof(target));

			var p = m_ea.Kb.AssertPerspective(perspective);

			NameSearchTree<float> targetDict;
			if (!m_cachedSI.TryGetValue(p, out targetDict))
			{
				targetDict = new NameSearchTree<float>();
				m_cachedSI[p] = targetDict;
			}

			float value;
			if (!targetDict.TryGetValue(target, out value))
			{
				value = CalculateSI(target, p);
				targetDict[target] = value;
			}
			return value;
		}

		public void InvalidateCachedSI()
		{
			m_cachedSI.Clear();
		}

		public IEnumerable<IAction> CalculateConferals(Name perspective)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<IAction> FilterActions(Name perspective, IEnumerable<IAction> actionsToFilter)
		{
			foreach (var a in actionsToFilter)
			{
				float minSI;
				if(m_claimTree.TryGetValue(a.ToNameRepresentation(),out minSI))
					if (GetSocialImportance(a.Target, perspective) < minSI)
						continue;

				yield return a;
			}
		}

		private float CalculateSI(Name target, Name perspective)
		{
			float value = 0;
			foreach (var a in m_attributionRules)
			{
				var sub = new Substitution(a.Target, target);
				if (a.Conditions.Evaluate(m_ea.Kb, perspective, new[] { new SubstitutionSet(sub) }))
					value += a.Value;
			}
			return value;
		}

		#region Dynamic Properties

		private static readonly Name SI_DYNAMIC_PROPERTY_TEMPLATE = Name.BuildName("SI([target])");

		private IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> SIPropertyCalculator(KB kb, Name perspective, IDictionary<string, Name> args, IEnumerable<SubstitutionSet> constraints)
		{
			Name target;
			if(!args.TryGetValue("target",out target))
				yield break;

			foreach (var t in kb.AskPossibleProperties(target,perspective,constraints))
			{
				var si = GetSocialImportance(Name.BuildName(t.Item1), perspective);
				foreach (var s in t.Item2)
					yield return new Pair<PrimitiveValue, SubstitutionSet>(si,s);
			}
		}

		#endregion
	}
}