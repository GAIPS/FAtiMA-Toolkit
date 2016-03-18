using System;
using System.Linq;
using AssetPackage;
using EmotionalAppraisal;
using KnowledgeBase;
using KnowledgeBase.WellFormedNames;
using KnowledgeBase.WellFormedNames.Collections;
using SocialImportance.DTOs;

namespace SocialImportance
{
	public sealed class SocialImportanceAsset: BaseAsset
	{
		private KB m_kb;
		private AttributionRule[] m_attributionRules = new AttributionRule[0];

		//Volatile Statements
		private NameSearchTree<NameSearchTree<float>> m_cachedSI = new NameSearchTree<NameSearchTree<float>>();

		public SocialImportanceAsset(EmotionalAppraisalAsset EA)
		{
			m_kb = EA.Kb;
			PerformKBBindings();
		}

		private void PerformKBBindings()
		{
			//Regist dynamic properties here
		}

		public void LoadFromDTO(SocialImportanceDTO dto)
		{
			m_attributionRules = dto.AttributionRules.Select(adto => new AttributionRule(adto)).ToArray();

			//TODO load the rest
		}

		public float GetSocialImportance(Name target, Name perspective)
		{
			if(!target.IsPrimitive)
				throw new ArgumentException("must be a primitive name",nameof(target));

			var p = m_kb.AssertPerspective(perspective);

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

		private float CalculateSI(Name target, Name perspective)
		{
			float value = 0;
			foreach (var a in m_attributionRules)
			{
				var sub = new Substitution(a.Target, target);
				if (a.Conditions.Evaluate(m_kb, perspective, new[] { new SubstitutionSet(sub) }))
					value += a.Value;
			}
			return value;
		}
	}
}