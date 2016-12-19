using System;
using CommeillFaut.DTOs;
using Conditions;
using WellFormedNames;


namespace CommeillFaut
{
    [Serializable]
    public class InfluenceRule
    {
        [NonSerialized]
        public readonly Guid GUID;

        public string RuleName { get; private set; }
        public Name Target { get; private set; }
        public int Value { get; private set; }
       public bool cond { get; private set; }
      public ConditionSet Conditions;


        protected InfluenceRule()
        {
            GUID = Guid.NewGuid();
        }

        public int Result(Name init, Name targ)
        {

            foreach (var cond in Conditions)
            {
                 return Value;
            }
          
            return 0;

        }
        public InfluenceRule(InfluenceRuleDTO dto) : this()
		{
            SetData(dto);
        }

        public void SetData(InfluenceRuleDTO dto)
        {
            RuleName = dto.RuleName;
            Target = (Name)dto.Target;
            Value = dto.Value;
            Conditions = new ConditionSet(dto.Conditions);
        }

        public InfluenceRuleDTO ToDTO()
        {
            return new InfluenceRuleDTO() { Id = GUID, RuleName = RuleName, Target = Target.ToString(), Value = Value, Conditions = Conditions.ToDTO() };
        }
    }


}