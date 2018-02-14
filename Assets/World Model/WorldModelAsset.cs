using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using ActionLibrary;
using GAIPS.Rage;
using WellFormedNames;
using KnowledgeBase;
using Conditions.DTOs;
using WorldModel.DTOs;
using AutobiographicMemory;
using Utilities;
using WellFormedNames.Collections;
using RolePlayCharacter;

namespace WorldModel
{
    [Serializable]
    public sealed partial class WorldModelAsset : LoadableAsset<WorldModelAsset>
    {

        private Dictionary<Name, List<Effect>> m_EffectsByEventNames;

        private NameSearchTree<Name> _EventNames;

        public WorldModelAsset()
        {
            m_EffectsByEventNames = new Dictionary<Name, List<Effect>>();
            _EventNames = new NameSearchTree<Name>();
        }

        protected override string OnAssetLoaded()
        {
            return null;
        }


        public IEnumerable<EffectDTO> Simulate(List<Name> events)
        {

            var result = new List<EffectDTO>();


            foreach (var e in events)
            {
                
                foreach (var possibleEvent in this._EventNames.Unify(e))
                {


                        var substitutions = new[] { possibleEvent.Item2 }; //this adds the subs found in the eventName

                        var effects = m_EffectsByEventNames[possibleEvent.Item1];

                    var responsibleAgent = (Name) e.ToString().Split(',')[1];

                        foreach (var ef in effects)
                        {
                            var truePropertyName = (Name) "default";
                            var trueNewValueName = (Name) "default";
                            var trueResponsibleAgentName = (Name) "World";


                            if (!ef.PropertyName.IsGrounded)
                                foreach (var sub in substitutions)
                                {
                                    truePropertyName = ef.PropertyName.MakeGround(sub);

                                }

                            else truePropertyName = ef.PropertyName;

                            if (!ef.NewValue.IsGrounded)
                                foreach (var sub in substitutions)
                                {
                                    trueNewValueName = ef.NewValue.MakeGround(sub);
                                }
                            else trueNewValueName = ef.NewValue;


                            if (!ef.ResponsibleAgent.IsGrounded)
                                foreach (var sub in substitutions)
                                {
                                    trueResponsibleAgentName = ef.ResponsibleAgent.MakeGround(sub);
                                }
                            else trueResponsibleAgentName = responsibleAgent;

                            var trueEffect = new EffectDTO()
                            {
                                PropertyName = truePropertyName,
                                NewValue = trueNewValueName,
                                ResponsibleAgent = trueResponsibleAgentName
                            };

                              
                            result.Add(trueEffect);

                        }
                       
                    }
                    
                }

            return result;
        }


        public void AddEventEffect(Name ev, EffectDTO eff)
        {

            if(m_EffectsByEventNames.ContainsKey(ev))
                m_EffectsByEventNames[ev].Add(new Effect(eff));

            else
            {
                m_EffectsByEventNames.Add(ev, new List<Effect>());

                _EventNames.Add(new KeyValuePair<Name, Name>(ev, ev));
                if(eff.PropertyName != null)
                    m_EffectsByEventNames[ev].Add(new Effect(eff));
            }
           
        }

        public void AddEventEffects(Name ev, IEnumerable<EffectDTO> effs)
        {

            foreach (var ef in effs)
            {
                AddEventEffect(ev, ef);
            }
        }

        public Dictionary<Name, List<Effect>> GetAllEventEffects()
        {
            return m_EffectsByEventNames;
        }

        public IEnumerable<Name> GetAllEvents()
        {
            return m_EffectsByEventNames.Keys;
        }
    }
}
