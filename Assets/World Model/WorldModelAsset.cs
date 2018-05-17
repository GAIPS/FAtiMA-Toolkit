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
using SerializationUtilities;

namespace WorldModel
{
    [Serializable]
    public sealed partial class WorldModelAsset : LoadableAsset<WorldModelAsset>, ICustomSerialization
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
                            var trueObserverAgentName = (Name) "*";


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

                            if (!ef.ObserverAgent.IsGrounded)
                                foreach (var sub in substitutions)
                                {
                                    trueObserverAgentName = ef.ObserverAgent.MakeGround(sub);
                                }
                            else trueResponsibleAgentName = responsibleAgent;

                            var trueEffect = new EffectDTO()
                            {
                                PropertyName = truePropertyName,
                                NewValue = trueNewValueName,
                                ResponsibleAgent = trueResponsibleAgentName,
                                ObserverAgent = trueObserverAgentName
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

                _EventNames.Add(new KeyValuePair<Name, Name>(ev, ev));
                if(eff.PropertyName != null)
                    m_EffectsByEventNames.Add(ev, new List<Effect>(){new Effect(eff)});
                else m_EffectsByEventNames.Add(ev, new List<Effect>());

            }
           
        }


        public void EditEventEffect(Name ev, EffectDTO eff, int index)
        {

            if (m_EffectsByEventNames.ContainsKey(ev))
            {
                m_EffectsByEventNames[ev].RemoveAt(index);
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

        public void addEventTemplate(Name ev)
        {
            if (!m_EffectsByEventNames.ContainsKey(ev))
            {
                m_EffectsByEventNames.Add(ev, new List<Effect>());
                _EventNames.Add(new KeyValuePair<Name, Name>(ev, ev));
            }

        }

        public void UpdateEventTemplate(Name old, Name ev)
        {
            var pastEffects = m_EffectsByEventNames[old];

            m_EffectsByEventNames.Remove(old);


           List<Effect> newEffects = new List<Effect>();

            foreach (var effs in pastEffects)
            {

                var eff = effs;
                eff.ResponsibleAgent = ev.GetNTerm(2);
                newEffects.Add(eff);

            }
            m_EffectsByEventNames.Add(ev, newEffects);

            if(_EventNames.ContainsKey(old))
                _EventNames.Remove(old);
            _EventNames.Add(new KeyValuePair<Name, Name>(ev, ev));
        }

       

        public Dictionary<Name, List<Effect>> GetAllEventEffects()
        {
            return m_EffectsByEventNames;
        }

        public IEnumerable<Name> GetAllEvents()
        {
            return m_EffectsByEventNames.Keys;
        }

        public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            dataHolder.SetValue("EffectsByEventNames", m_EffectsByEventNames);
        }

        public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            m_EffectsByEventNames = new Dictionary<Name, List<Effect>>();

            m_EffectsByEventNames = dataHolder.GetValue<Dictionary<Name, List<Effect>>>("EffectsByEventNames");

            _EventNames = new NameSearchTree<Name>();

            foreach (var k in m_EffectsByEventNames.Keys)
            {
                _EventNames.Add(new KeyValuePair<Name, Name>(k,k));
            }

        }


        public void RemoveEffect(Name EventName, Effect eff)
        {
            m_EffectsByEventNames?[EventName].Remove(eff);
        
        }

        public void RemoveEvent(Name EventName)
        {
            m_EffectsByEventNames?.Remove(EventName);
            if(_EventNames.ContainsKey(EventName))
            _EventNames.Remove(EventName);
        }
    }
}
