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

        private EmotionalAppraisalAsset m_ea = null;
       
        public List<SocialExchange> m_SocialExchanges { get; set; }
        public Dictionary<string, string[]> ConditionList;

        //Volatile Statements
        private NameSearchTree<NameSearchTree<float>> m_cachedCIF = new NameSearchTree<NameSearchTree<float>>();

        /// <summary>
        /// The Comme ill Faut constructor
        /// </summary>

        public EmotionalAppraisalAsset LinkedEA
        {
            get { return m_ea; }
        }

        public CommeillFautAsset()
        {
            m_ea = null;
            m_SocialExchanges = new List<SocialExchange>();
            ConditionList = new Dictionary<string, string[]>();

        }

        public void BindEmotionalAppraisalAsset(EmotionalAppraisalAsset eaa)
        {
            if (m_ea != null)
            {
                //Unregist bindings
                RemoveKBBindings();
                m_ea = null;
            }

            m_ea = eaa;
            PerformKBBindings();
            InvalidateCachedCIF();
        }

        private void PerformKBBindings()
        {
            m_ea.DynamicPropertiesRegistry.RegistDynamicProperty(CIF_DYNAMIC_PROPERTY_NAME, CIFPropertyCalculator, "The value of Social Importance attributed to [target]");
        }

        private void RemoveKBBindings()
        {
            m_ea.UnregistDynamicProperty(CIF_DYNAMIC_PROPERTY_NAME);
        }

        private void ValidateEALink()
        {
            if (m_ea == null)
                throw new InvalidOperationException($"Cannot execute operation as an instance of {nameof(EmotionalAppraisalAsset)} was not registed in this asset.");
        }

        public void InvalidateCachedCIF()
        {
            m_cachedCIF.Clear();
        }


        private float internal_GetSocialImportance(Name target, Name perspective)
        {
            NameSearchTree<float> targetDict;
            if (!m_cachedCIF.TryGetValue(perspective, out targetDict))
            {
                targetDict = new NameSearchTree<float>();
                m_cachedCIF[perspective] = targetDict;
            }

            float value;
            if (!targetDict.TryGetValue(target, out value))
            {
                value = 0;
               // value = CalculateSocialMove(target, perspective);
                targetDict[target] = value;
            }
            return value;
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


        public SocialExchange GetSocialMove(Name socialExchangeName)
        {
          return  m_SocialExchanges.Find(x => x.ActionName == socialExchangeName);
        }



        public SocialExchange CalculateSocialMove(string target, string perspective)
        {

            
            List<int> volitions = new List<int>();

            foreach (var socialMove in m_SocialExchanges)
            {
                int volitionResult = socialMove.CalculateVolition(perspective, target, m_ea);
                volitions.Add(volitionResult);
                Console.WriteLine(" Name " + socialMove.ActionName + " volResult: " + volitionResult);

            }

            int index = 0;
            int compareValue = volitions[0];

            for (int j = 1; j < volitions.Count; j++)
            {
                if (compareValue < volitions[j])
                {
                    compareValue = volitions[j];
                    index = j;
                }
                    
                    
            }
            Console.WriteLine("Highest volition: " + m_SocialExchanges[index] + " " + compareValue);
            return m_SocialExchanges[index];
        }


        #region Dynamic Properties

        private static readonly Name CIF_DYNAMIC_PROPERTY_NAME = Name.BuildName("CIF");
        private IEnumerable<DynamicPropertyResult> CIFPropertyCalculator(IQueryContext context, Name target)
        {
            foreach (var t in context.AskPossibleProperties(target))
            {
                var si = internal_GetSocialImportance(t.Item1, context.Perspective);
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
                    if(rule.RuleConditions?.ConditionSet.Length > 0)
                        ConditionList.Add(rule.RuleName, rule.RuleConditions.ConditionSet);
                }
            }

            dataHolder.SetValue("RuleList", ConditionList);

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

