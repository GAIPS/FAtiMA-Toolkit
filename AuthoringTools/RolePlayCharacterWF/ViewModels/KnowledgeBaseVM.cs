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
	    private RolePlayCharacterAsset _rpcAsset;

	    public BindingListView<BeliefDTO> Beliefs {get;}

		public KnowledgeBaseVM(RolePlayCharacterAsset asset)
		{
            _rpcAsset = asset;
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
            _rpcAsset.UpdateBelief(belief.Name, belief.Value, belief.Certainty, belief.Perspective);
            Beliefs.DataSource.Add(belief);
            Beliefs.Refresh();
		}

        public void RemoveBeliefs(IEnumerable<BeliefDTO> beliefs)
        {
            foreach (var beliefDto in beliefs)
            {
                _rpcAsset.RemoveBelief(beliefDto.Name, beliefDto.Perspective);
                Beliefs.DataSource.Remove(beliefDto);
            }
            Beliefs.Refresh();
		}
    }
}
