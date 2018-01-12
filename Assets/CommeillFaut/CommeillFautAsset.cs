using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using CommeillFaut.DTOs;
using Conditions;
using Conditions.DTOs;
using SerializationUtilities;
using WellFormedNames;
using GAIPS.Rage;
using KnowledgeBase;
using WellFormedNames.Collections;



namespace CommeillFaut
{
       [Serializable]
    public sealed class CommeillFautAsset  : LoadableAsset<CommeillFautAsset>, ICustomSerialization
    {

        public KB m_kB;
        public List<SocialExchange> m_SocialExchanges { get; set; }

        /// <summary>
        /// The Comme ill Faut constructor
        /// </summary>

        public KB LinkedEA
        {
            get { return m_kB; }
        }

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
            //       registry.RegistDynamicProperty(CIF_DYNAMIC_PROPERTY_NAME, CIFPropertyCalculator);
       
            registry.RegistDynamicProperty(VOLITION_PROPERTY_TEMPLATE, VolitionPropertyCalculator);
        }

       

        private static readonly Name VOLITION_PROPERTY_TEMPLATE = (Name)"Volition";

        public IEnumerable<DynamicPropertyResult> VolitionPropertyCalculator(IQueryContext context, Name socialMoveName, Name initiator, Name Target)
        {
            Dictionary<SubstitutionSet, Name> ret = new Dictionary<SubstitutionSet, Name>();
          //  var seSub = new Substitution(Name.BuildName("[x]"), new ComplexValue(Name.BuildName("default")));
            var stringVolition = "";
            var possibleSE = new List<SocialExchange>();
            bool SEConstraint = false;

            //   Console.WriteLine(" socialmovename" + socialMoveName);
            //    foreach (var c in context.Constraints)
            //   Console.WriteLine("Contraint: " + c.ToString());


            if (context.Perspective != Name.SELF_SYMBOL)
                yield break;

            if (initiator == Target)
                yield break;

            foreach (var s in context.AskPossibleProperties(socialMoveName))
                {
                if (m_SocialExchanges.Find(x => x.Name == s.Item1.Value) != null)
                {
                    SEConstraint = true;
                    possibleSE = m_SocialExchanges.Where(x => x.Name == s.Item1.Value).ToList();

                    foreach (var exchange in possibleSE)
                    {
                        var seName = exchange.Name.ToString();

                        if (Target.IsVariable)  // aka Target = [x]
                        {

                            foreach (var targ in context.AskPossibleProperties(Target))
                            {
                                var newValue = CalculateSocialMoveVolition((Name)seName, initiator, Target);

                                if (newValue != -1)
                                {

                                  
                                    var sub = new Substitution(Target, new ComplexValue(targ.Item1.Value, 1));
                                 

                                    stringVolition = CalculateStyle(newValue);

                                    foreach (var c in context.Constraints)
                                    {
                                        if( c.Conflicts(sub))
                                        continue;

                                            var newConstraints = new SubstitutionSet(c);
                                            newConstraints.AddSubstitution(sub);
                                            yield return new DynamicPropertyResult(new ComplexValue(Name.BuildName(stringVolition)), newConstraints);
                                        }
                                    

                                 

                                }
                            }
                        }

                        else
                        {
                          
                            var newValue = CalculateSocialMoveVolition((Name)seName, initiator, Target);

                            if (newValue != -1)
                            {

                              

                              

                                stringVolition = CalculateStyle(newValue);

                                foreach (var c in context.Constraints)
                                {
                                    
                                    var newConstraints = new SubstitutionSet(c);
                                    yield return new DynamicPropertyResult(new ComplexValue(Name.BuildName(stringVolition)), newConstraints);
                                }

                            }
                        }
                    }
                }
            }

                    if (socialMoveName.IsVariable && !SEConstraint) // socialMoveName = [se]
                    {
                        foreach (var se in m_SocialExchanges)
                        {

                    if (Target.IsVariable)  // target  = [x] or any other variable
                    {

                        foreach (var targ in context.AskPossibleProperties(Target))
                        {
                            var seName = se.Name.ToString();
                            
                            var newValue = CalculateSocialMoveVolition(se.Name, initiator,Target);

                            if (newValue != -1)
                            {

                                var seSub = new Substitution(socialMoveName, new ComplexValue(Name.BuildName(seName)));

                                var targetSub = new Substitution(Target, new ComplexValue(targ.Item1.Value, 1));


                                stringVolition = CalculateStyle(newValue);

                                foreach (var c in context.Constraints)
                                {
                                    var newConstraints = new SubstitutionSet(c);

                                    if (c.Conflicts(targetSub))
                                        continue;

                                    newConstraints.AddSubstitution(targetSub);

                                    if (c.Conflicts(seSub))
                                        continue;

                                    newConstraints.AddSubstitution(seSub);

                                    yield return new DynamicPropertyResult(new ComplexValue(Name.BuildName(stringVolition)), newConstraints);
                                }



                            }


                        }
                    }
                    else   // Target = Sarah or John or ...
                    {
                        var newValue = CalculateSocialMoveVolition(se.Name, initiator,
                        Target);

                        if (newValue != -1)
                        {


                            stringVolition = CalculateStyle(newValue);

                            foreach (var c in context.Constraints)
                            {
                                yield return new DynamicPropertyResult(new ComplexValue(Name.BuildName(stringVolition)), new SubstitutionSet(c));
                            }
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

        private void ValidateEALink()
        {
            if (m_kB == null)
                throw new InvalidOperationException($"Cannot execute operation as an instance of {nameof(CommeillFautAsset)} was not registed in this asset.");
        }





        public Guid AddExchange(SocialExchangeDTO newExchange)
        {
            var newSocialExchange = new SocialExchange(newExchange);

            
      
            if(m_SocialExchanges != null)
              {

              // m_SocialExchanges = new List<SocialExchange>();
            if(m_SocialExchanges.Find(x => x.Name == newExchange.Name) != null)
                    UpdateSocialExchange(newExchange);

                   else m_SocialExchanges.Add(newSocialExchange);

               
            }
            return new Guid();
        }


     
        public void UpdateSocialExchange(SocialExchangeDTO reactionToEdit, SocialExchangeDTO newReaction)
        {
            m_SocialExchanges.Remove(new SocialExchange(reactionToEdit));

            var newId = this.AddExchange(newReaction);

           
            
         
        }

        public double Floor(float value)
        {
            var toRet = Convert.ToDouble(value);
            // Console.WriteLine("Round method calculation for: " + x.ToString() + " the value : " + toRet);
            toRet = toRet / 10;
            toRet = Math.Round(toRet, 0);
            toRet = toRet * 10;
         //   Console.WriteLine("Round method calculation for: " + x.ToString() + " rounded value " + sub.Value.ToString() + " result : " + toRet);

            return toRet;
        }


        public string CalculateStyle(float value)
        {
        
            if(value > 0.6)
                return value <= 1 ? "Positive" : "VeryPositive";

            if (value < 0.4)
                return value >= 0 ? "Negative" : "VeryNegative";
           
            return "Neutral";
        }




        public void UpdateSocialExchange(SocialExchangeDTO newReaction)
        {
          

            m_SocialExchanges.Remove(m_SocialExchanges.Find(x => x.Name == newReaction.Name));

            m_SocialExchanges.Add(new SocialExchange(newReaction));
        }

        public void RemoveSocialExchanges(IList<Guid> toRemove)
        {
            foreach (var id in toRemove)
            {
                m_SocialExchanges.Remove(m_SocialExchanges.Find(x => new Guid() == id));
            }
        }


        public void RemoveSocialExchange(SocialExchange torem)
        {
            Console.WriteLine(" I'm removing this " + torem.Name);
                m_SocialExchanges.Remove(torem);
            Console.Read();
           
        }

        public SocialExchange GetSocialMove(Name socialExchangeName)
        {
          return  m_SocialExchanges.Find(x => x.Name == socialExchangeName);
        }


        public float CalculateSocialMoveVolition(Name seName, Name initiator, Name target)
        {
         return   m_SocialExchanges.Find(x => x.Name == seName).VolitionValue(initiator, target, this.m_kB);
        }


        #region Custom Serialization

        public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            
               dataHolder.SetValue("SocialExchanges", m_SocialExchanges.ToArray());

        }

        public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            
            m_SocialExchanges = new List<SocialExchange>(dataHolder.GetValue<SocialExchange[]>("SocialExchanges"));
        }


        #endregion

       

        /// <summary>
        /// Load a Social Importance Asset definition from a DTO object.
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

        /// <summary>
        /// Returns a DTO containing all the asset's configurations.
        /// </summary>
        public CommeillFautDTO GetDTO()
        {
            var at = m_SocialExchanges.Select(a => a.ToDTO()).ToArray();
          
            return new CommeillFautDTO() { _SocialExchangesDtos = at};
        }

   

        /// @cond DOXYGEN_SHOULD_SKIP_THIS

        protected override string OnAssetLoaded()
        {
            return null;
        }

        /// @endcond
    }

}

