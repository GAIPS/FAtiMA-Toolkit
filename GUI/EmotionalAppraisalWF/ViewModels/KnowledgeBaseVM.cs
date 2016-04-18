using System;
using System.Collections.Generic;
using System.Linq;
using EmotionalAppraisal;
using EmotionalAppraisal.DTOs;
using EmotionalAppraisalWF.Properties;
using Equin.ApplicationFramework;
using KnowledgeBase.WellFormedNames;

namespace EmotionalAppraisalWF.ViewModels
{
    public class KnowledgeBaseVM
    {
        private EmotionalAppraisalAsset _emotionalAppraisalAsset;

        public BindingListView<BeliefDTO> Beliefs {get;}

		public string Perspective { get; set; }

		public KnowledgeBaseVM(EmotionalAppraisalAsset ea)
        {
            _emotionalAppraisalAsset = ea;
			Perspective = _emotionalAppraisalAsset.Perspective;
			this.Beliefs = new BindingListView<BeliefDTO>(new List<BeliefDTO>());
			UpdateBeliefList();
        }

		public void UpdatePerspective()
		{
			var n = (Name) Perspective;
			if((Name)_emotionalAppraisalAsset.Perspective == n)
				return;

			_emotionalAppraisalAsset.SetPerspective(n);
			UpdateBeliefList();
		}

	    public void UpdateBeliefList()
	    {
			Beliefs.DataSource.Clear();
		    var beliefList = _emotionalAppraisalAsset.Kb.GetAllBeliefs().Select(b => new BeliefDTO
		    {
			    Name = b.Name.ToString(),
			    Perspective = b.Perspective.ToString(),
			    Value = b.Value.ToString()
		    });

		    foreach (var b in beliefList)
				Beliefs.DataSource.Add(b);

			Beliefs.Refresh();
	    }

		public string[] GetKnowledgeVisibilities()
        {
            return _emotionalAppraisalAsset.KnowledgeVisibilities;
        }

        public void AddBelief(BeliefDTO belief)
        {
            if (_emotionalAppraisalAsset.BeliefExists(belief.Name))
            {
                throw new Exception(Resources.BeliefAlreadyExistsExceptionMessage);
            }
            _emotionalAppraisalAsset.AddOrUpdateBelief(belief);
            this.Beliefs.DataSource.Add(belief);
            this.Beliefs.Refresh();
        }

        public void RemoveBeliefs(IEnumerable<BeliefDTO> beliefs)
        {
            foreach (var beliefDto in beliefs)
            {
                _emotionalAppraisalAsset.RemoveBelief(beliefDto.Name, beliefDto.Perspective);
                Beliefs.DataSource.Remove(beliefDto);
            }
            Beliefs.Refresh();
        }
    }
}
