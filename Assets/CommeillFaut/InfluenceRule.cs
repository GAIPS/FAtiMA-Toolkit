using System;
using Conditions;
using WellFormedNames;


namespace CommeillFaut
{
    [Serializable]
    public class InfluenceRule
    {
        public string RuleName { get; private set; }
     
        public int Value { get; private set; }
       public bool cond { get; private set; }
      public ConditionSet Conditions;

        public InfluenceRule(string name, int value, bool condition)
        {
            RuleName = name;
           
            Value = value;
            cond = condition;

        }

        public int Result(Name init, Name targ)
        {

            foreach (var cond in Conditions)
            {
                 return Value;
            }
          
            return 0;

        }


    }
}