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
        private HashSet<InfluenceRule> m_influencerules;
        private List<SocialExchange> SocialExchanges;
      


        /// <summary>
        /// The Comme ill Faut constructor
        /// </summary>
 

        public CommeillFautAsset()
        {
            m_influencerules = new HashSet<InfluenceRule>();
            SocialExchanges = new List<SocialExchange>();
          

        }


       

        public Guid AddSocialExchange(SocialExchangeDTO newExchange)
        {
            var newSocialExchange = new SocialExchange(newExchange);
            this.SocialExchanges.Add(newSocialExchange);
            return newSocialExchange.Id;
        }

        /// <summary>
        /// Updates a reaction definition.
        /// </summary>
        public void UpdateSocialExchange(SocialExchangeDTO reactionToEdit, SocialExchangeDTO newReaction)
        {
            newReaction.Conditions = reactionToEdit.Conditions;
            var newId = this.AddSocialExchange(newReaction);
            SocialExchanges.Remove(new SocialExchange(reactionToEdit));
        }


        public void RemoveSocialExchanges(IList<Guid> toRemove)
        {
            foreach (var id in toRemove)
            {
               SocialExchanges.Remove(SocialExchanges.Find(x => x.Id == id));
            }
        }

     



        public SocialExchange GetSocialMove(Name target)
        {
            Dictionary<SocialExchange,int> return_list = new Dictionary<SocialExchange, int>();
           
                foreach (var _socialMove in SocialExchanges)
                {
                    return_list.Add(_socialMove, _socialMove.CalculateVolition(Name.BuildName("uhm"), target));
              
            }
            return return_list.Max().Key;
        }



        public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
         //   dataHolder.SetValue("Name", m_attributionRules.ToArray());
        }

        public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            
        }



        /// @cond DOXYGEN_SHOULD_SKIP_THIS

        protected override string OnAssetLoaded()
        {
            return null;
        }

        /// @endcond
    }

}

