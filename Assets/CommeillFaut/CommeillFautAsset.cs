using System;
using System.Collections.Generic;
using System.Linq;
using CommeillFaut.DTOs;
using Conditions;
using SerializationUtilities;
using WellFormedNames;
using GAIPS.Rage;
using KnowledgeBase;

namespace CommeillFaut
{
    [Serializable]
    public sealed class CommeillFautAsset  : Asset<CommeillFautAsset>, ICustomSerialization
    {
        public KB m_kB;
        private List<SocialExchange> m_SocialExchanges { get; set; }

      

        /// <summary>
        /// The Comme ill Faut constructor
        /// </summary>
        public CommeillFautAsset()
        {
            m_kB = null;
            m_SocialExchanges = new List<SocialExchange>();
        }

        /// <summary>
        /// Binds a KB to this Comme ill Faut Asset instance. Without a KB instance binded to this asset, 
        /// comme ill faut evaluations will not work properly.
        /// InvalidateCachedCIF() is automatically called by this method.
        /// </summary>
        /// <param name="kb">The Knowledge Base to be binded to this asset.</param>
        public void RegisterKnowledgeBase(KB kB)
        {
            if (m_kB != null)
            {
                //Unregist bindings
                UnbindToRegistry(m_kB);
                m_kB = null;
            }

            m_kB = kB;
            BindToRegistry(kB);
        }

        #region Dynamic Properties

        public void BindToRegistry(IDynamicPropertiesRegistry registry)
        {
            registry.RegistDynamicProperty(VOLITION_PROPERTY_TEMPLATE, "", VolitionPropertyCalculator);
        }

        private static readonly Name VOLITION_PROPERTY_TEMPLATE = (Name)"Volition";

        public IEnumerable<DynamicPropertyResult> VolitionPropertyCalculator(IQueryContext context, Name socialMoveName,  Name Step, Name Target, Name Mode)
        {
            Dictionary<SubstitutionSet, Name> ret = new Dictionary<SubstitutionSet, Name>();


            if (context.Perspective != Name.SELF_SYMBOL)
                yield break;

            if (this.m_kB.Perspective == Target)
                yield break;

            List<Name> possibleSEs = new List<Name>();

            if (socialMoveName.IsVariable)
            {
                foreach (var s in context.AskPossibleProperties(socialMoveName))
                    possibleSEs.Add(s.Item1.Value);

                foreach (var se in this.m_SocialExchanges)
                    possibleSEs.Add(se.Name);
            }
            else if(socialMoveName.IsUniversal)
                foreach (var se in this.m_SocialExchanges)
                    possibleSEs.Add(se.Name);
            
            else possibleSEs.Add(socialMoveName);

            List<Name> possibleTargets = new List<Name>();

            if (Target.IsVariable)
            {
                foreach (var s in context.AskPossibleProperties(Target))
                    possibleTargets.Add(s.Item1.Value);



            }
            else possibleTargets.Add(Target);

     

     

            List<Name> possibleModes = new List<Name>();

            if(Mode.IsVariable){
                 foreach (var s in context.AskPossibleProperties(Mode))
                    possibleModes.Add(s.Item1.Value);
                 }

            else possibleModes.Add(Mode);

            foreach (var seName in possibleSEs)
            {
                if (!m_SocialExchanges.Exists(x => x.Name == seName))
                    continue;


                    foreach (var targ in possibleTargets)
                    {



                        var actualStep = FilterStep(seName, targ);

                        if(actualStep.ToString() == "-") continue;

                        foreach(var seMode in possibleModes){

                        var volValue = CalculateSocialMoveVolition(seName, actualStep, targ, seMode);


                        if (volValue != Single.NegativeInfinity)
                        {

                            var subSet = new SubstitutionSet();

                            if (socialMoveName.IsVariable)
                            {
                                var sub = new Substitution(socialMoveName, new ComplexValue(seName, 1));
                                subSet.AddSubstitution(sub);
                            }

                            
                            if (Step.IsVariable)
                            {
                                var sub = new Substitution(Step, new ComplexValue(actualStep, 1));
                                subSet.AddSubstitution(sub);
                            }


                            if (Target.IsVariable)
                            {
                                var sub = new Substitution(Target, new ComplexValue(targ, 1));
                                subSet.AddSubstitution(sub);
                            }

                              if (Mode.IsVariable)
                            {
                                var sub = new Substitution(Mode, new ComplexValue(seMode, 1));
                                subSet.AddSubstitution(sub);
                            }

                            if (context.Constraints.Count() > 0)

                                foreach (var c in context.Constraints)
                                {
                                    if (c.Conflicts(subSet))
                                        continue;

                                    var newcontext = new SubstitutionSet();

                                    newcontext.AddSubstitutions(c);

                                    newcontext.AddSubstitutions(subSet);

                                    yield return new DynamicPropertyResult(new ComplexValue(Name.BuildName(volValue)), newcontext);
                                }

                            else yield return new DynamicPropertyResult(new ComplexValue(Name.BuildName(volValue)), subSet);

                        }
                    }

                    }
            }
        }

      public void UnbindToRegistry(IDynamicPropertiesRegistry registry)
        {
            registry.UnregistDynamicProperty((Name)"Volition");
        }

        #endregion

        public Guid AddOrUpdateExchange(SocialExchangeDTO dto)
        {
          
            var idx = m_SocialExchanges.FindIndex(x => x.Id == dto.Id);
            System.Guid actualId;
            if (idx < 0)
            {
                var se = new SocialExchange(dto);
                m_SocialExchanges.Add(se);
                actualId = se.Id;
            }
            else
            {
                m_SocialExchanges[idx].StartingConditions = new ConditionSet(dto.StartingConditions);
           
                m_SocialExchanges[idx].Description = dto.Description;
                m_SocialExchanges[idx].Target = dto.Target;
                m_SocialExchanges[idx].Name = dto.Name;
                 m_SocialExchanges[idx].Steps = m_SocialExchanges[idx].StringToSteps(dto.Steps);
                 m_SocialExchanges[idx].InfluenceRules = dto.InfluenceRules.Select(x=>new InfluenceRule(x)).ToList();
                actualId = m_SocialExchanges[idx].Id;

            }

            return actualId;
        }

        public void RemoveSocialExchange(Guid id)
        {
            var exchange = m_SocialExchanges.Find(se => se.Id == id);
            if (exchange != null)
                m_SocialExchanges.Remove(exchange);
        }


        public void UpdateSocialExchange(SocialExchangeDTO newReaction)
        {
            m_SocialExchanges.Remove(m_SocialExchanges.Find(x => x.Name == newReaction.Name));

            m_SocialExchanges.Add(new SocialExchange(newReaction));
        }

    
        public SocialExchangeDTO GetSocialExchange(Guid id)
        {
            return m_SocialExchanges.Find(x => x.Id == id).ToDTO;
        }


        public float CalculateSocialMoveVolition(Name seName, Name seStep, Name target, Name seMode)
        {
         return   m_SocialExchanges.Find(x => x.Name == seName).VolitionValue(seStep, target, seMode, this.m_kB);
        }


        
        /// <summary>
        /// Retrives the definitions of all the stored action rules.
        /// </summary>
        /// <returns>A set of DTOs containing the data of all action rules.</returns>
        public IEnumerable<SocialExchangeDTO> GetAllSocialExchanges()
        {
	        return m_SocialExchanges.Select(at => at.ToDTO);
        }


        #region Custom Serialization

        public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            
               dataHolder.SetValue("SocialExchanges", m_SocialExchanges.ToArray());

        }

        public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            
            m_SocialExchanges = new List<SocialExchange>(dataHolder.GetValue<SocialExchange[]>("SocialExchanges"));

            foreach (var se in m_SocialExchanges)
            {
                se.Id =  Guid.NewGuid();
            }
        }


        #endregion

       

        /// <summary>
        /// Load a Comme ill Faut Asset definition from a DTO object.
        /// </summary>
        /// <remarks>
        /// Use this to procedurally configure the asset.
        /// </remarks>
        /// <param name="dto">
        /// The DTO containing the data to load
        /// </param>
        public void LoadFromDTO(CommeillFautDTO dto)
        {
            m_SocialExchanges.Clear();


            if (dto._SocialExchangesDtos != null)
            {
                foreach (var c in dto._SocialExchangesDtos)
                {
                 m_SocialExchanges.Add(new SocialExchange(c));
                }
            }

        }


        private Name FilterStep(Name SE , Name target) // Missing the target
        {


            Name properStep = (Name)"-";

            // 1) Determine the Social Exchange
            var socialExchange = this.m_SocialExchanges.Find(x=> x.Name == SE);
            if(socialExchange == null)
                return properStep;

            // 2) Determine the steps of the Social Exchange
            var _Steps = new List<Name>();

             if(socialExchange.Steps.Count > 0)
               _Steps = socialExchange.Steps;
             else return properStep;


             if(_Steps.Count == 1)
                return _Steps.FirstOrDefault();
               
           // Now we need to see if the Agent has just performed the social exchange

            
            var MethodCall = "LastEventId(Action-End," + target.ToString() + ", Speak(*, *, SE(" + SE.ToString() + "," + "[step]" + "), *), SELF) >= 0";
       

            var lastStep = "";
            var condSet = new ConditionSet();
            condSet = new ConditionSet();
            var  cond = Condition.Parse(MethodCall);
            condSet = condSet.Add(cond);    
           var result = condSet.Unify(this.m_kB, Name.SELF_SYMBOL, null);

          
            if(result != null)
                if(result.FirstOrDefault() !=null)
                    if(result.FirstOrDefault().Where(x=>x.Variable == (Name)"[step]") != null)
                         lastStep = result.FirstOrDefault().Where(x=>x.Variable == (Name)"[step]").FirstOrDefault().SubValue.Value.ToString();
                    else return  _Steps.FirstOrDefault();
                else return  _Steps.FirstOrDefault();
            else return  _Steps.FirstOrDefault();
           


         var ind = _Steps.IndexOf((Name)lastStep);

            if(ind + 1 == _Steps.Count)
                return _Steps.FirstOrDefault();
            else return _Steps[ind + 1];
            



        }

        /// <summary>
        /// Returns a DTO containing all the asset's configurations.
        /// </summary>
        public CommeillFautDTO GetDTO()
        {
            var at = m_SocialExchanges.Select(a => a.ToDTO).ToArray();
          
            return new CommeillFautDTO() { _SocialExchangesDtos = at};
        }

   

     
    }

}

