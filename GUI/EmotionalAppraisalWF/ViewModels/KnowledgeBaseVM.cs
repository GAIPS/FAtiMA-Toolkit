using System;
using System.Collections.Generic;
using EmotionalAppraisal;
using EmotionalAppraisal.DTOs;
using EmotionalAppraisalWF.Properties;
using Equin.ApplicationFramework;
using WellFormedNames;

namespace EmotionalAppraisalWF.ViewModels
{
    public class KnowledgeBaseVM
    {
	    private BaseEAForm _mainForm;

	    private EmotionalAppraisalAsset emotionalAppraisalAsset => _mainForm.CurrentAsset;

	    public BindingListView<BeliefDTO> Beliefs {get;}

		public string Perspective { get; set; }

		public KnowledgeBaseVM(BaseEAForm form)
		{
			_mainForm = form;
			Perspective = emotionalAppraisalAsset.Perspective.ToString();
			Beliefs = new BindingListView<BeliefDTO>(new List<BeliefDTO>());
			UpdateBeliefList();
        }

		public void UpdatePerspective()
		{
			var n = (Name) Perspective;
			if((Name)emotionalAppraisalAsset.Perspective == n)
				return;

			emotionalAppraisalAsset.SetPerspective(Perspective);
			UpdateBeliefList();
			_mainForm.SetModified();
		}

	    public void UpdateBeliefList()
	    {
			Beliefs.DataSource.Clear();
		    foreach (var b in emotionalAppraisalAsset.GetAllBeliefs())
				Beliefs.DataSource.Add(b);

			Beliefs.Refresh();
	    }

		public static readonly string[] KnowledgeVisibilities = { Name.SELF_STRING, Name.UNIVERSAL_STRING };

        public void AddBelief(BeliefDTO belief)
        {
            if (emotionalAppraisalAsset.BeliefExists(belief.Name))
            {
                throw new Exception(Resources.BeliefAlreadyExistsExceptionMessage);
            }
            emotionalAppraisalAsset.AddOrUpdateBelief(belief);
            Beliefs.DataSource.Add(belief);
            Beliefs.Refresh();
			_mainForm.SetModified();
		}

        public void RemoveBeliefs(IEnumerable<BeliefDTO> beliefs)
        {
            foreach (var beliefDto in beliefs)
            {
                emotionalAppraisalAsset.RemoveBelief(beliefDto.Name, beliefDto.Perspective);
                Beliefs.DataSource.Remove(beliefDto);
            }
            Beliefs.Refresh();
			_mainForm.SetModified();
		}
    }
}
