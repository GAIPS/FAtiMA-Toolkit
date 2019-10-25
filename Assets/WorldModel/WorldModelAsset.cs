using GAIPS.Rage;
using SerializationUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;
using WellFormedNames;
using WellFormedNames.Collections;
using WorldModel.DTOs;

namespace WorldModel
{
    [Serializable]
    public sealed partial class WorldModelAsset : Asset<WorldModelAsset>, ICustomSerialization
    {
        private Dictionary<Name, List<Effect>> actions;
        private Dictionary<Name, int> priorityRules;

        private NameSearchTree<Name> actionNames;

        public WorldModelAsset()
        {
            actions = new Dictionary<Name, List<Effect>>();
            priorityRules = new Dictionary<Name, int>();
            actionNames = new NameSearchTree<Name>();
        }

        public IEnumerable<EffectDTO> Simulate(Name[] events)
        {
            var result = new List<EffectDTO>();

            foreach (var e in events)
            {
                var matchingActionRules = this.actionNames.Unify(e);
                Pair<Name, SubstitutionSet> actionRule = null;

                var maxPriority = int.MinValue;
                foreach (var match in matchingActionRules)
                {
                    if (priorityRules[match.Item1] > maxPriority)
                    {
                        maxPriority = priorityRules[match.Item1];
                        actionRule = match;
                    }
                }

                if(actionRule == null)
                    return result;

                var substitutions = new[] { actionRule.Item2 }; //this adds the subs found in the eventName

                var effects = actions[actionRule.Item1];

                var responsibleAgent = e.GetNTerm(2);

                foreach (var ef in effects)
                {
                    var truePropertyName = (Name)"default";
                    var trueNewValueName = (Name)"default";
                    var trueResponsibleAgentName = (Name)"World";
                    var trueObserverAgentName = (Name)"*";

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
                        ObserverAgent = trueObserverAgentName,
                    };
                    result.Add(trueEffect);
                }
            }
            return result;
        }

        public void AddActionEffect(Name action, EffectDTO eff)
        {
            if (actions.ContainsKey(action))
            {
                actions[action].Add(new Effect(eff));
            }
            else
            {
                throw new Exception("Action name not found");
            }
        }

        public void EditEventEffect(Name a, EffectDTO eff, int index)
        {
            if (actions.ContainsKey(a))
            {
                actions[a].RemoveAt(index);
                actions[a].Add(new Effect(eff));
            }
        }

        public void AddActionEffectsDTOs(Name a, IEnumerable<EffectDTO> effs)
        {
            foreach (var ef in effs)
            {
                AddActionEffect(a, ef);
            }
        }

        public void AddActionEffects(Name a, List<Effect> effs)
        {
            foreach (var ef in effs)
            {
                AddActionEffect(a, ef.ToDTO());
            }
        }

        public void addActionTemplate(Name a, int p)
        {
            if (!actions.ContainsKey(a))
            {
                actions.Add(a, new List<Effect>());
                actionNames.Add(new KeyValuePair<Name, Name>(a, a));
                priorityRules.Add(a, p);
            }
        }

        public void UpdateActionTemplate(Name old, Name action, int p)
        {
            var pastEffects = actions[old];

            actions.Remove(old);
            actionNames.Remove(old);

            actions.Add(action, pastEffects);

            actionNames.Add(new KeyValuePair<Name, Name>(action, action));

            priorityRules[action] = p;
        }

        public Dictionary<Name, List<Effect>> GetAllEventEffects()
        {
            return actions;
        }

        public IEnumerable<Pair<Name, int>> GetAllActions()
        {
            return actions.Keys.Select(x => new Pair<Name, int>(x, priorityRules[x]));
        }

        public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            dataHolder.SetValue("Effects", actions);
            dataHolder.SetValue("Priorities", priorityRules);
        }

        public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            actions = new Dictionary<Name, List<Effect>>();
            priorityRules = new Dictionary<Name, int>();

            var actionsJson = dataHolder.GetValue<Dictionary<Name, List<Effect>>>("Effects");
           
            foreach (var k in actionsJson.Keys)
            {
                if( actionsJson[k] != null)
                    actions.Add(k, actionsJson[k]);
            }
            
            
            priorityRules = dataHolder.GetValue<Dictionary<Name, int>>("Priorities");

            actionNames = new NameSearchTree<Name>();

            foreach (var k in actions.Keys)
            {
                actionNames.Add(new KeyValuePair<Name, Name>(k, k));
            }
        }

        public void RemoveEffect(Name action, Effect eff)
        {
            actions?[action].Remove(eff);
        }

        public void RemoveAction(Name ActionName)
        {
            actions?.Remove(ActionName);
            if (actionNames.ContainsKey(ActionName))
                actionNames.Remove(ActionName);
            if (priorityRules.ContainsKey(ActionName))
                priorityRules.Remove(ActionName);
        }
    }
}