using System;
using Conditions;
using SocialImportance.DTOs;
using WellFormedNames;

namespace SocialImportance
{
	[Serializable]
	internal class AttributionRule
	{
		[NonSerialized]
		public Guid GUID;
		public string RuleName { get; private set; }
        public Name Target { get; private set; }
		public int Value { get; private set; }
		public ConditionSet Conditions { get; private set; }

		protected AttributionRule()
		{
			GUID = Guid.NewGuid();
		}

		public AttributionRule(AttributionRuleDTO dto) : this()
		{
			SetData(dto);
		}

		public void SetData(AttributionRuleDTO dto)
		{
			RuleName = dto.RuleName;
			Target = (Name)dto.Target;
			Value = dto.Value;
			Conditions = new ConditionSet(dto.Conditions);
		}

		public AttributionRuleDTO ToDTO()
		{
			return new AttributionRuleDTO() {
                Id = GUID,
                RuleName = RuleName,
                Target = Target.ToString(),
                Value = Value,
                Conditions = Conditions.ToDTO()};
		}
	}
}