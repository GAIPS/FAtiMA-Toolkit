using System;
using System.Linq;
using EmotionalAppraisal;
using EmotionalAppraisalWF.Properties;
using Equin.ApplicationFramework;
using KnowledgeBase;

namespace EmotionalAppraisalWF.ViewModels
{
    public class KnowledgeBaseVM
    {
        public class BeliefDTO
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public string Visibility { get; set; }
        }

        private static int _beliefCounter;

        private string BASE_BELIEF_NAME = "bel({0})";
        private string BASE_BELIEF_VALUE = "val";
        private string BASE_BELIEF_VISIBILITY = KnowledgeVisibility.Universal.ToString();

        private EmotionalAppraisalAsset _emotionalAppraisalAsset;

        public BindingListView<BeliefDTO> Beliefs { get;}

        public KnowledgeBaseVM(EmotionalAppraisalAsset ea)
        {
            _emotionalAppraisalAsset = ea;
            var beliefList = ea.Kb.GetAllBeliefs().Select(b => new BeliefDTO
            {
                Name = b.Name.ToString(),
                Value = b.Value.ToString(),
                Visibility = b.Visibility.ToString()
            }).ToList();

            this.Beliefs = new BindingListView<BeliefDTO>(beliefList);
        }

        public string[] GetKnowledgeVisibilities()
        {
            return Enum.GetNames(typeof(KnowledgeVisibility));
        }

      
        public void AddBelief(BeliefDTO belief)
        {
            var newBelief = new BeliefDTO
            {
                Name = string.Format(BASE_BELIEF_NAME, _beliefCounter),
                Value = BASE_BELIEF_VALUE,
                Visibility = BASE_BELIEF_VISIBILITY,
            };
            _beliefCounter++;
            _emotionalAppraisalAsset.AddOrUpdateBelief(newBelief.Name, newBelief.Value, newBelief.Visibility);
            Beliefs.DataSource.Add(newBelief);
            Beliefs.Refresh();
            
            /*
            if (_emotionalAppraisalAsset.BeliefExists(belief.Name))
            {
                throw new Exception(Resources.BeliefAlreadyExistsExceptionMessage);
            }
            _emotionalAppraisalAsset.AddOrUpdateBelief(belief.Name, belief.Value, belief.Visibility);
            this.Beliefs.DataSource.Add(belief);
            this.Beliefs.Refresh();*/
        }

        public void EditBelief(BeliefDTO belief)
        {
            _emotionalAppraisalAsset.AddOrUpdateBelief(belief.Name, belief.Value, belief.Visibility);
        }

    }
}
