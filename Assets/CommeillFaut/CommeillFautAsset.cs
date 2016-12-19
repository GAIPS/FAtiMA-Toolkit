using System;
using System.Collections.Generic;
using System.Linq;
using CommeillFaut.DTOs;
using SerializationUtilities;
using WellFormedNames;
using EmotionalAppraisal;
using GAIPS.Rage;


namespace CommeillFaut
{
       [Serializable]
    public sealed class CommeillFautAsset  : LoadableAsset<CommeillFautAsset>, ICustomSerialization
    {

     //   private EmotionalAppraisalAsset m_ea = null;
        private List<InfluenceRule> m_influencerules;
        public List<SocialExchange> m_SocialExchanges { get; set; }
      


        /// <summary>
        /// The Comme ill Faut constructor
        /// </summary>
 

        public CommeillFautAsset()
        {
            m_influencerules = new List<InfluenceRule>();
            m_SocialExchanges = new List<SocialExchange>();
          

        }


       

        public Guid AddExchange(SocialExchangeDTO newExchange)
        {
            var newSocialExchange = new SocialExchange(newExchange);

            
      
            if(m_SocialExchanges != null)
                m_SocialExchanges.Add(newSocialExchange);
            else
            {

              // m_SocialExchanges = new List<SocialExchange>();
                m_SocialExchanges.Add(newSocialExchange);

                foreach (var newrule in newSocialExchange.InfluenceRules)
                {
                    this.m_influencerules.Add(newrule);
                }
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

            var newId = this.AddExchange(newReaction);
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


        public SocialExchange GetSocialMove(Name target)
        {
            Dictionary<SocialExchange,int> return_list = new Dictionary<SocialExchange, int>();
           
                foreach (var _socialMove in m_SocialExchanges)
                {
                    return_list.Add(_socialMove, _socialMove.CalculateVolition(Name.BuildName("uhm"), target));
              
            }
            return return_list.Max().Key;
        }



        public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
               dataHolder.SetValue("SocialExchanges", m_SocialExchanges.ToArray());
      //      dataHolder.SetValue("InfluenceRules", m_influencerules.ToArray());
            
        }

        public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            m_SocialExchanges = new List<SocialExchange>(dataHolder.GetValue<SocialExchange[]>("SocialExchanges"));
        //    m_influencerules = new List<InfluenceRule>(dataHolder.GetValue<List<InfluenceRule>>("InfluenceRules"));
        }



        /// @cond DOXYGEN_SHOULD_SKIP_THIS

        protected override string OnAssetLoaded()
        {
            return null;
        }

        /// @endcond
    }

}

