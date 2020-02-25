using System;
using System.Linq;
using CommeillFaut.DTOs;
using Conditions;
using KnowledgeBase;
using WellFormedNames;
using System.Collections.Generic;
using SerializationUtilities;

namespace CommeillFaut
{

    [Serializable]
    public class SocialExchange : ICustomSerialization
    {
             
        public Guid Id { get; set; }

        public string Description { get; set; }

        public Name Name { get; set; }

        public List<Name> Steps { get; set; }

        public ConditionSet StartingConditions { get; set; }

        public List<InfluenceRule> InfluenceRules { get; set; }


        /// The Social Exchange Name
        /// </summary>
        public Name Target { get; set; }


        public SocialExchange(SocialExchangeDTO s) 
        {
            if (s == null) return;

            Id = Guid.NewGuid();
            Name = s.Name;
            Description = s.Description;
            Steps = StringToSteps(s.Steps);
             Target = s.Target;
            StartingConditions =  new ConditionSet(s.StartingConditions);
            InfluenceRules = s.InfluenceRules.Select(x=>new InfluenceRule(x)).ToList();
        }
      
        public override string ToString()
        {
            return Name + " | " + Description + " | " + this.Id + " | Steps:" + Steps.Select(x=>x.ToString() + " ") + "\n";
        }

        public SocialExchangeDTO ToDTO => new SocialExchangeDTO()
        {
            Name = this.Name,
            Description = this.Description,
            Steps = this.StepsToString(),
            Target = this.Target,
            StartingConditions = this.StartingConditions.ToDTO(),
            InfluenceRules = this.InfluenceRules.Select(x=>x.ToDTO()).ToList(),
            Id = this.Id
            
        };

        public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            dataHolder.SetValue("Name", this.Name);
            dataHolder.SetValue("Description", this.Description);
            dataHolder.SetValue("Steps", this.Steps);
            dataHolder.SetValue("Target", this.Target);
            dataHolder.SetValue("StartingConditions", this.StartingConditions);
            dataHolder.SetValue("InfluenceRules", this.InfluenceRules);
          
          


        }

        public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            Name = dataHolder.GetValue<Name>("Name");
            Description = dataHolder.GetValue<string>("Description");
            Steps = dataHolder.GetValue<List<Name>>("Steps");
            Target = dataHolder.GetValue<Name>("Target");
            StartingConditions = dataHolder.GetValue<ConditionSet>("StartingConditions");
            InfluenceRules = dataHolder.GetValue<List<InfluenceRule>>("InfluenceRules");
          
        }

        public void AddStep(Name step)
        {
            Steps.Add(step);
        }
   
          public void RemoveStep(Name step)
        {
            Steps.Remove(step);
        }


        public string StepsToString()
        {
            var ret = "";

            foreach (var s in Steps)
            {
                ret += s + ", ";
            }

            ret = ret.Remove(ret.Length - 2, 2);

            return ret;
        }

        public List<Name> StringToSteps(string str)
        {
           var ret = str.Split(',');

            List<Name> steps = new List<Name>();
            foreach (var ste in ret)
            {
                steps.Add((Name)ste);
            }

            return steps;
        }


         public void AddStartingCondition(Condition cond)
        {
            StartingConditions = StartingConditions.Add(cond);
        }

        public void RemoveStartingCondition(Condition cond)
        {
            StartingConditions  = StartingConditions.Remove(cond);
        }

        
         public void AddInfluenceRule(InfluenceRule cond)
        {
           InfluenceRules.Add(cond);
        }

        public void RemoveInflluenceRule(InfluenceRule cond)
        {
           InfluenceRules.Remove(cond);
        }

         public Guid AddInfluenceRule(InfluenceRuleDTO dto)
        {
          
            var idx = InfluenceRules.FindIndex(x => x.Id == dto.Id);
            System.Guid actualID = new Guid();
            if (idx < 0)
            {
               
                InfluenceRules.Add(new InfluenceRule(dto));
                actualID = dto.Id;
            }
            else
            {

                InfluenceRules[idx].Rule = new ConditionSet(dto.Rule);
                InfluenceRules[idx].Value = dto.Value;;
                InfluenceRules[idx].Id = dto.Id;
                actualID = InfluenceRules[idx].Id;

            }

            return actualID;
        }


         public void RemoveInfluenceRule(Guid id)
        {
            var exchange = InfluenceRules.Find(se => se.Id == id);
            if (exchange != null)
                InfluenceRules.Remove(exchange);
        }


        public float VolitionValue(Name step, Name targ, Name mode, KB m_Kb)
        {
          
            
            if (m_Kb.Perspective == targ) return -1;

            var targetSub = new Substitution(Target, new ComplexValue(targ));

            var constraints = new SubstitutionSet();
            constraints.AddSubstitution(targetSub);
             float total = Single.NegativeInfinity;



           // List<SubstitutionSet> resultingConstraints = new List<SubstitutionSet>();
            
            if(step == this.Steps.FirstOrDefault()){
           var resultingConstraints = StartingConditions.FirstOrDefault()?.Unify(m_Kb, m_Kb.Perspective, new[] { constraints } ).ToList();

            if(StartingConditions.Count() > 1){
                int counter = 0;

                    resultingConstraints = StartingConditions.Unify(m_Kb, m_Kb.Perspective, new[] { constraints }).ToList();

            /*        foreach (var c in StartingConditions) // For instance SI([x]) >= 40
            {
                    counter++;
                    if(counter == 1) continue;
                        
                    resultingConstraints = c.Unify(m_Kb, m_Kb.Perspective, resultingConstraints ).ToList();  // Whats the sub here [x]/John
                }*/
            }

            
            
            if(!resultingConstraints.Any())
                return total;


                foreach (var res in resultingConstraints) 
                {
                    if (resultingConstraints.Any()) // Assuming all Starting COnditions match lets go into the Influence Rules
                    {
                   foreach(var constraint in resultingConstraints){  //  var condition = c.ToString();

                            var contraintVolitionValue = .0f;

                       // var certainty = res.FindMinimumCertainty();  // How do I ask SI(John) >= 40 and get its certainty

                        //total += certainty;

                            
                       List<InfluenceRule> influenceRuleList;

                             if(mode.IsUniversal)
                           influenceRuleList = this.InfluenceRules;
                             else
                                influenceRuleList = this.InfluenceRules.FindAll(x=>x.Mode == mode);
                        
                        foreach(var inf in influenceRuleList)
                                {
                                
                                var toSum = inf.EvaluateInfluenceRule(m_Kb, constraint);

                                contraintVolitionValue +=toSum;
                                
                                }

                        if(contraintVolitionValue > total)
                            {
                                total = contraintVolitionValue;
                            }
                        
                        }
                    
                    
                    
                    }

                }
            
                //What if the step is beyond the first one, we should not consider Starting Conditions, or any conditions at all, only the influence rules
                } else
                
                {           
                 
                 var volVal = .0f;

                      List<InfluenceRule> influenceRuleList;

                      if(mode.IsUniversal)
                         influenceRuleList = this.InfluenceRules;
                      else
                          influenceRuleList = this.InfluenceRules.FindAll(x=>x.Mode == mode);

                 
                foreach(var inf in influenceRuleList)
                     {
                                
                     var toSum = inf.EvaluateInfluenceRule(m_Kb, constraints);

                    volVal +=toSum;
                                
                 }
                               

                    if(volVal > total)
                        total = volVal;
                }
            
             

               return total;
        }

    }

}

