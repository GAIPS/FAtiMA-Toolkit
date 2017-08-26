using System;
using System.Collections.Generic;
using Equin.ApplicationFramework;
using WellFormedNames;
using RolePlayCharacter;
using KnowledgeBase;

namespace RolePlayCharacterWF.ViewModels
{
    public class KnowledgeBaseVM
    {
	    private BaseRPCForm _mainForm;

	    private RolePlayCharacterAsset _rpcAsset => _mainForm.LoadedAsset;

	    public BindingListView<BeliefDTO> Beliefs {get;}

		public KnowledgeBaseVM(BaseRPCForm form)
		{
			_mainForm = form;
			Beliefs = new BindingListView<BeliefDTO>(new List<BeliefDTO>());
			UpdateBeliefList();
        }

	    public void UpdateBeliefList()
	    {
			Beliefs.DataSource.Clear();
		    foreach (var b in _rpcAsset.GetAllBeliefs())
				Beliefs.DataSource.Add(b);

			Beliefs.Refresh();
	    }

		public static readonly string[] KnowledgeVisibilities = { Name.SELF_STRING, Name.UNIVERSAL_STRING };

        public void AddBelief(BeliefDTO belief)
        {
            //This step is required to avoid storing beliefs with the SELF Keyword in their value or name
            belief.Name = ((Name)belief.Name).RemoveSelfPerspective(_rpcAsset.CharacterName).ToString();
            belief.Value = ((Name)belief.Value).RemoveSelfPerspective(_rpcAsset.CharacterName).ToString();

            if (_rpcAsset.m_kb.BeliefExists((Name)belief.Name))
            {
                throw new Exception("This belief already exists");
            }
            
            _rpcAsset.UpdateBelief(belief.Name, belief.Value, belief.Certainty, belief.Perspective);
            Beliefs.DataSource.Add(belief);
            Beliefs.Refresh();
			_mainForm.SetModified();
		}

        public void RemoveBeliefs(IEnumerable<BeliefDTO> beliefs)
        {
            foreach (var beliefDto in beliefs)
            {
                _rpcAsset.RemoveBelief(beliefDto.Name, beliefDto.Perspective);
                Beliefs.DataSource.Remove(beliefDto);
            }
            Beliefs.Refresh();
			_mainForm.SetModified();
		}
    }
}
