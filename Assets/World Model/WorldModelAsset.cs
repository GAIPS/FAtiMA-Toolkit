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

namespace WorldModel
{
    [Serializable]
    public sealed partial class WorldModelAsset : LoadableAsset<WorldModelAsset>
    {

        private Dictionary<ActionRule, List<Effect>> m_EffectsByActions;


        public WorldModelAsset()
        {
            m_EffectsByActions = new Dictionary<ActionRule, List<Effect>>();
        }

        protected override string OnAssetLoaded()
        {
            return null;
        }


        public IEnumerable<IAction> Simulate(List<IBaseEvent> events)
        {

            var result = new List<IAction>();


            foreach (var action in m_EffectsByActions.Keys)
            {
                

            }


            return result;
        }

    }
}
