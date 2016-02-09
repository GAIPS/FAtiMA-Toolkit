using System;
using System.Collections.Generic;
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

        private EmotionalAppraisalAsset _emotionalAppraisalAsset;

        public BindingListView<BeliefDTO> Beliefs {get;}
        
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
            if (_emotionalAppraisalAsset.BeliefExists(belief.Name))
            {
                throw new Exception(Resources.BeliefAlreadyExistsExceptionMessage);
            }
            _emotionalAppraisalAsset.AddOrUpdateBelief(belief.Name, belief.Value, belief.Visibility);
            this.Beliefs.DataSource.Add(belief);
            this.Beliefs.Refresh();
        }

        
        /*public void EditBelief(BeliefDTO belief)
        {
            _emotionalAppraisalAsset.AddOrUpdateBelief(belief.Name, belief.Value, belief.Visibility);
            var previousBelief = this.Beliefs.FirstOrDefault(b => b.Name == belief.Name);
            if (previousBelief != null)
            {
                this.Beliefs.DataSource[this.Beliefs.DataSource.IndexOf(previousBelief)] = belief;
            }
            else
            {
                this.Beliefs.DataSource.Add(belief);
            }
            this.Beliefs.Refresh();
        }*/

        public void RemoveBeliefs(IEnumerable<BeliefDTO> beliefs)
        {
            foreach (var beliefDto in beliefs)
            {
                _emotionalAppraisalAsset.RemoveBelief(beliefDto.Name);
                Beliefs.DataSource.Remove(beliefDto);
            }
            Beliefs.Refresh();
        }


        
    }
}
