using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CommeillFaut.DTOs;
using Conditions;
using Conditions.DTOs;
using SerializationUtilities;
using WellFormedNames;
using EmotionalAppraisal;
using GAIPS.Rage;
using KnowledgeBase;
using WellFormedNames.Collections;


namespace CommeillFaut
{
       [Serializable]
    public sealed class CommeillFautAsset  : LoadableAsset<CommeillFautAsset>, ICustomSerialization
    {

        private KB m_kB;
        public List<SocialExchange> m_SocialExchanges { get; set; }
        public Dictionary<string, string[]> ConditionList;
        public TriggerRules _TriggerRules;

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
            _TriggerRules = new TriggerRules();
        }

        /// <summary>
        /// Binds a KB to this Social Importance Asset instance. Without a KB instance binded to this asset, 
        /// social importance evaluations will not work properly.
        /// InvalidateCachedSI() is automatically called by this method.
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

        public void BindToRegistry(IDynamicPropertiesRegistry registry)
        {
            registry.RegistDynamicProperty(CIF_DYNAMIC_PROPERTY_NAME, CIFPropertyCalculator);
        }

        public void UnbindToRegistry(IDynamicPropertiesRegistry registry)
        {
            registry.UnregistDynamicProperty((Name)"cif([target])");
        }

        private void ValidateEALink()
        {
            if (m_kB == null)
                throw new InvalidOperationException($"Cannot execute operation as an instance of {nameof(CommeillFautAsset)} was not registed in this asset.");
        }


        /// <summary>
        /// Calculate the Social Importance value of a given target, in a particular perspective.
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
                ret_value = CalculateSocialMove(target.ToString(), perspective.ToString()).ActionName.ToString();
                targetDict[target] = value;
            }
            return ret_value;
        }


        /// <summary>
        /// Clears all cached Social Importance values, allowing new values to be recalculated uppon request.
        /// </summary>
        public void InvalidateCachedCIF()
        {
            m_cachedCIF.Clear();
        }




        public void CheckTriggerRules()
        {
            _TriggerRules.Verify(this.m_kB);
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


        public Guid AddTriggerRule(InfluenceRuleDTO rule, string cond)
        {

            return _TriggerRules.AddTriggerRule(rule, cond);
        }
        /// <summary>
        /// Updates a reaction definition.
        /// </summary>
        public void UpdateSocialExchange(SocialExchangeDTO reactionToEdit, SocialExchangeDTO newReaction)
        {
            m_SocialExchanges.Remove(new SocialExchange(reactionToEdit));

            var newId = this.AddExchange(newReaction);

           
            
         
        }

        public void UpdateSocialExchange(SocialExchangeDTO newReaction)
        {

          

            m_SocialExchanges.Remove(m_SocialExchanges.Find(x => x.ActionName.ToString() == newReaction.Action));

            m_SocialExchanges.Add(new SocialExchange(newReaction));
        }


        public void UpdateTriggerRule(InfluenceRuleDTO rule, string cond)
        {
           _TriggerRules.UpdateTriggerRule(rule, cond);
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
          
                m_SocialExchanges.Remove(torem);
           
        }

        public void RemoveTriggerRule(InfluenceRuleDTO rule)
        {

            _TriggerRules.RemoveTriggerRule(rule);
        }

        public void RemoveTriggerRuleByName(string ruleName)
        {
            
            _TriggerRules.RemoveTriggerRuleByName(ruleName);
        }

        public SocialExchange GetSocialMove(Name socialExchangeName)
        {
          return  m_SocialExchanges.Find(x => x.ActionName == socialExchangeName);
        }

        public SocialExchange GetHighestVolition(Dictionary<string, int> _volitions)
        {

            int index = 0;
            var first = _volitions.First();
            string key = first.Key;
            int compareValue = _volitions[key];
            

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

        public Dictionary<string, int> CalculateSocialMovesVolitions(string target, string perspective)
        {

            
            Dictionary<string,int> volitions = new Dictionary<string, int>();

            foreach (var socialMove in m_SocialExchanges)
            {
                int volitionResult = socialMove.CalculateVolition(perspective, target, m_kB);
                volitions.Add(socialMove.ActionName.ToString(), volitionResult);
                Console.WriteLine(" Name " + socialMove.ActionName + " volResult: " + volitionResult);

            }
            return volitions;
        }


        public SocialExchange CalculateSocialMove(string target, string perpective)
        {

            return GetHighestVolition(CalculateSocialMovesVolitions(target, perpective));
        }

        #region Dynamic Properties

        private static readonly Name CIF_DYNAMIC_PROPERTY_NAME = Name.BuildName("CIF");
        private IEnumerable<DynamicPropertyResult> CIFPropertyCalculator(IQueryContext context, Name target)
        {
            foreach (var t in context.AskPossibleProperties(target))
            {
                var si = internal_GetSocialVolition(t.Item1, context.Perspective);
                foreach (var s in t.Item2)
                    yield return new DynamicPropertyResult(Name.BuildName(si), s);
            }
        }
        #endregion


        #region Serialization

        public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
               dataHolder.SetValue("SocialExchanges", m_SocialExchanges.ToArray());
            ConditionList = new Dictionary<string, string[]>();

            foreach (var social in m_SocialExchanges)
            {
                foreach (var rule in social.InfluenceRules)
                {
                    if(rule.RuleConditions?.ConditionSet?.Length > 0)
                        ConditionList.Add(rule.RuleName, rule.RuleConditions.ConditionSet);
                }
            }

            dataHolder.SetValue("RuleList", ConditionList);
            dataHolder.SetValue("_triggerRules", _TriggerRules);

        }

        public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            m_SocialExchanges = new List<SocialExchange>(dataHolder.GetValue<SocialExchange[]>("SocialExchanges"));
           ConditionList = dataHolder.GetValue<Dictionary<string,string[]>>("RuleList");

            foreach (var social in m_SocialExchanges)
            {
                foreach (var rule in social.InfluenceRules)
                {
                    if (ConditionList.ContainsKey(rule.RuleName))
                        rule.RuleConditions.ConditionSet = ConditionList[rule.RuleName];
                }
                
            }
            _TriggerRules = dataHolder.GetValue<TriggerRules>("_triggerRules");
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
          
            if (dto._SocialExchangesDtos != null)
            {
                foreach (var c in dto._SocialExchangesDtos)
                {
                 m_SocialExchanges.Add(new SocialExchange(c));
                }
            }

            _TriggerRules = dto._triggerRules;


        }

        /// <summary>
        /// Returns a DTO containing all the asset's configurations.
        /// </summary>
        public CommeillFautDTO GetDTO()
        {
            var at = m_SocialExchanges.Select(a => a.ToDTO()).ToArray();
          
            return new CommeillFautDTO() { _SocialExchangesDtos = at, _triggerRules = _TriggerRules};
        }




        /// @cond DOXYGEN_SHOULD_SKIP_THIS

        protected override string OnAssetLoaded()
        {
            return null;
        }

        /// @endcond
    }

}

