using KnowledgeBase;
using System.Collections.Generic;
using WellFormedNames;

namespace MCTS
{
    public class MCTSAsset
    {
        private static readonly Name MCTS_DYNAMIC_PROPERTY_NAME = Name.BuildName("MCTS");

        private KB m_kb;


        public MCTSAsset()
        {
            m_kb = null;
        }

        /// <summary>
        /// Binds a KB to this asset.
        /// </summary>
        /// <param name="kb">The Knowledge Base to be binded to this asset.</param>
        public void RegisterKnowledgeBase(KB kB)
        {
            if (m_kb != null)
            {
                //Unregist bindings
                UnbindToRegistry(m_kb);
                m_kb = null;
            }

            m_kb = kB;
            BindToRegistry(kB);
        }

        private void BindToRegistry(IDynamicPropertiesRegistry registry)
        {
            registry.RegistDynamicProperty(MCTS_DYNAMIC_PROPERTY_NAME, MCTSSearch);
        }

        public void UnbindToRegistry(IDynamicPropertiesRegistry registry)
        {
            registry.UnregistDynamicProperty(MCTS_DYNAMIC_PROPERTY_NAME);
        }

        private IEnumerable<DynamicPropertyResult> MCTSSearch(IQueryContext context, Name depth)
        {
            //This is just an example of how to return the value Jump for every possible subset
            foreach (var subSet in context.Constraints)
            {
                yield return new DynamicPropertyResult(new ComplexValue((Name)"Jump"), subSet);
            }
        }
    }
}
