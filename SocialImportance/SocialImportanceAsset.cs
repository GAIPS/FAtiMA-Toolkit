using System;
using System.Linq;
using AssetPackage;
using EmotionalAppraisal;
using KnowledgeBase;
using KnowledgeBase.WellFormedNames;
using SocialImportance.DTOs;

namespace SocialImportance
{
	public sealed class SocialImportanceAsset: BaseAsset
	{
		private KB m_kb;
		private AttributionRule[] m_attributionRules = new AttributionRule[0];

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

		public float GetSocialImportance(Name perspective, Name target)
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