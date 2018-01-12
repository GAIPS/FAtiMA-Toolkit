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
        public Dictionary<string, string[]> ConditionList;
        private List<Name> _actorsList;

        //Volatile Statements
        private NameSearchTree<NameSearchTree<float>> m_cachedCIF = new NameSearchTree<NameSearchTree<float>>();

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
            ConditionList = new Dictionary<string, string[]>();
            m_cachedCIF = new NameSearchTree<NameSearchTree<float>>();
            _actorsList = new List<Name>();
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
            InvalidateCachedCIF();

        
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
                if (m_SocialExchanges.Find(x => x.ActionName == s.Item1.Value) != null)
                {
                    SEConstraint = true;
                    possibleSE = m_SocialExchanges.Where(x => x.ActionName == s.Item1.Value).ToList();

                    foreach (var exchange in possibleSE)
                    {
                        var seName = exchange.ActionName.ToString();

                        if (Target.IsVariable)  // aka Target = [x]
                        {

                            foreach (var targ in context.AskPossibleProperties(Target))
                            {
                                var newValue = CalculateVolitions(seName, targ.Item1.Value.ToString(),
                              context.Perspective.ToString());

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
                            var newValue = CalculateVolitions(seName, Target.ToString(),
                             context.Perspective.ToString());

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
                            var seName = se.ActionName.ToString();
                            var newValue = CalculateVolitions(seName, targ.Item1.Value.ToString(),
                          context.Perspective.ToString());

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
                        var seName = se.ActionName.ToString();
                        var newValue = CalculateVolitions(seName, Target.ToString(),
                         context.Perspective.ToString());

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


        /// <summary>
        /// Calculate the Volition value of a given target, in a particular perspective.
        /// If no perspective is given, the current agent's perspective is used as default.
        /// </summary>
        /// <remarks>
        /// All values calculated by this method are automatically cached, in order to optimize future searches.
        /// If the values are needed to be recalculated, call InvalidateCachedSI() to clear the cached values.
        /// </remarks>
        /// <param name="target">The name of target which we want to calculate the SI</param>
        /// <param name="perspective">From which perspective do we want to calculate de SI.</param>
        /// <returns>The value of Social Importance attributed to given target by the perspective of a particular agent.</returns>
        public string GetSocialVolition(string target, string perspective = "self")
        {
            ValidateEALink();

            var t = Name.BuildName(target);
            if (!t.IsPrimitive)
                throw new ArgumentException("must be a primitive name", nameof(target));

            var p = m_kB.AssertPerspective(Name.BuildName(perspective));

            return internal_GetSocialVolition(t, p);
        }

        private string internal_GetSocialVolition(Name target, Name perspective)
        {
            Console.WriteLine("internal Get social Volition");
            NameSearchTree<float> targetDict;
            string ret_value = "";
            if (!m_cachedCIF.TryGetValue(perspective, out targetDict))
            {
                targetDict = new NameSearchTree<float>();
                m_cachedCIF[perspective] = targetDict;
            }

          
            float value;
            if (!targetDict.TryGetValue(target, out value))
            {
             
                var action = CalculateSocialMove(target.ToString(), perspective.ToString());
                ret_value = action.ActionName.ToString();
               
                targetDict[target] = value;
            }
         
        //    Console.WriteLine("retvalue: " + ret_value + " target " + target + " perpective " + perspective);
            return ret_value;
        }


        /// <summary>
        /// Clears all cached Social Importance values, allowing new values to be recalculated uppon request.
        /// </summary>
        public void InvalidateCachedCIF()
        {
            m_cachedCIF.Clear();
        }


        public Guid AddExchange(SocialExchangeDTO newExchange)
        {
            var newSocialExchange = new SocialExchange(newExchange);

            
      
            if(m_SocialExchanges != null)
              {

              // m_SocialExchanges = new List<SocialExchange>();
            if(m_SocialExchanges.Find(x => x.ActionName.ToString() == newExchange.Action) != null)
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
          

            m_SocialExchanges.Remove(m_SocialExchanges.Find(x => x.ActionName.ToString() == newReaction.Action));

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
            Console.WriteLine(" I'm removing this " + torem.ActionName);
                m_SocialExchanges.Remove(torem);
            Console.Read();
           
        }

        public SocialExchange GetSocialMove(Name socialExchangeName)
        {
          return  m_SocialExchanges.Find(x => x.ActionName == socialExchangeName);
        }

        public SocialExchange GetHighestVolition(Dictionary<string, float> _volitions)
        {

            var first = _volitions.First();
            string key = first.Key;
            float compareValue = _volitions[key];
            

            foreach (var aux in _volitions.Keys)
            {
                if (compareValue < _volitions[aux])
                {
                    compareValue = _volitions[aux];
                    key = aux;
                }
            }
           
           
            return m_SocialExchanges.Find(x =>x.ActionName.ToString() == key);

        }

      

        public Dictionary<string, float> CalculateSocialMovesVolitions(string target, string perspective)
        {

            
            Dictionary<string,float> volitions = new Dictionary<string, float>();

            foreach (var socialMove in m_SocialExchanges)
            {
                float volitionResult = socialMove.CalculateVolition(perspective, target, this.m_kB);
                volitions.Add(socialMove.ActionName.ToString(), volitionResult);

            }
            return volitions;
        }


        public SocialExchange CalculateSocialMove(string target, string perpective)
        {

            return GetHighestVolition(CalculateSocialMovesVolitions(target, perpective));
        }


        public float CalculateVolitions(string socialMove, string target, string perpective)
        {

            return m_SocialExchanges.Find(x => x.ActionName.ToString() == socialMove)
                .CalculateVolition(perpective, target, this.m_kB);
        }

        public List<Name> getActorList()
        {
            return _actorsList;
        }

        #region Custom Serialization

        public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            
               dataHolder.SetValue("SocialExchanges", m_SocialExchanges.ToArray());
            ConditionList = new Dictionary<string, string[]>();

        }

        public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            
            m_SocialExchanges = new List<SocialExchange>(dataHolder.GetValue<SocialExchange[]>("SocialExchanges"));
            m_cachedCIF = new NameSearchTree<NameSearchTree<float>>();
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
            _actorsList = new List<Name>();

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

