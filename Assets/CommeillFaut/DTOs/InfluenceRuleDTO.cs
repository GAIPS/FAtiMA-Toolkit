using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conditions.DTOs;
using WellFormedNames;

namespace CommeillFaut.DTOs
{
    [Serializable]
    public class InfluenceRuleDTO 
    {
        public Guid Id { get; set; }

        public ConditionSetDTO Rule{ get; set; }
            
        public int Value{ get; set; }

         public Name Mode{ get; set; }

        }
}
