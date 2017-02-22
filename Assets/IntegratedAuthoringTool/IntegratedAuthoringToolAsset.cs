using System;
using System.Collections.Generic;
using System.Linq;
using GAIPS.Rage;
using SerializationUtilities;
using IntegratedAuthoringTool.DTOs;
using KnowledgeBase;
using RolePlayCharacter;
using WellFormedNames;
using Utilities;
using System.Text;
using System.Security.Cryptography;

namespace IntegratedAuthoringTool
{
    /// <summary>
    /// This asset is responsible for managing the scenario, including its characters and respective dialogues
    /// </summary>
    [Serializable]
    public class IntegratedAuthoringToolAsset : LoadableAsset<IntegratedAuthoringToolAsset>, ICustomSerialization
    {
        private DialogActionDictionary m_playerDialogues;
		private DialogActionDictionary m_agentDialogues;
        private IList<CharacterSourceDTO> m_characterSources;

        /// <summary>
        /// The name of the Scenario
        /// </summary>
        public string ScenarioName { get; set; }

		public string ScenarioDescription { get; set; }

        
	    protected override void OnAssetPathChanged(string oldpath)
	    {
            for(int i = 0; i < m_characterSources.Count; i++)
            {
                var absPath = ToAbsolutePath(oldpath, m_characterSources[i].Source);
                m_characterSources[i].Source = absPath;
            }
	    }

	    public IntegratedAuthoringToolAsset()
        {
            m_playerDialogues = new DialogActionDictionary();
            m_agentDialogues = new DialogActionDictionary();
	        m_characterSources = new List<CharacterSourceDTO>();
        }

		/// <summary>
        /// Retreives all the sources for the characters in the scenario.
        /// </summary>
        public IEnumerable<CharacterSourceDTO> GetAllCharacterSources()
        {
	        return m_characterSources.Select(p => new CharacterSourceDTO() {Id = p.Id, Source = ToAbsolutePath(p.Source)});
        }


        public static string GenerateUtteranceId(string utterance)
        {
            utterance = utterance.RemoveWhiteSpace();
            utterance = utterance.ToLowerInvariant();
            var bytes = Encoding.UTF8.GetBytes(utterance);
            var utteranceId = BitConverter.ToString(MD5.Create().ComputeHash(bytes));
            utteranceId = utteranceId.Replace("-", string.Empty);
            return IATConsts.TTS_PREFIX + utteranceId;
        }

        /// <summary>
        /// Adds a new role-play character asset to the scenario.
        /// </summary>
        /// <param name="character">The instance of the Role Play Character asset</param>
        public void AddNewCharacterSource(CharacterSourceDTO dto)
        {
	     	string errorsOnLoad;
			var asset = RolePlayCharacterAsset.LoadFromFile(dto.Source, out errorsOnLoad);
	        if (errorsOnLoad != null)
		        throw new Exception(errorsOnLoad);
            dto.Id = m_characterSources.Count;
            dto.Source = ToRelativePath(dto.Source);
			m_characterSources.Add(dto);
        }
        
        /// <summary>
        /// Removes a list of characters from the scenario
        /// </summary>
        /// <param name="character">A list of character names</param>
        public void RemoveCharacters(IList<int> charactersToRemove)
        {
            foreach (var characterId in charactersToRemove)
            {
	            m_characterSources.RemoveAt(characterId);
            }   
            for(int i = 0; i < m_characterSources.Count; i++)
            {
                m_characterSources[i].Id = i;
            }
        }

		#region Dialog System

		

		/// <summary>
		/// Adds a new dialogue action 
		/// </summary>
		/// <param name="dialogueStateActionDTO">The action to add.</param>
		public void AddPlayerDialogAction(DialogueStateActionDTO dialogueStateActionDTO)
		{
			m_playerDialogues.AddDialog(new DialogStateAction(dialogueStateActionDTO));
		}
		/// <summary>
		/// Updates an existing dialogue action for the player
		/// </summary>
		/// <param name="dialogueStateActionToEdit">The action to be updated.</param>
		/// <param name="newDialogueAction">The updated action.</param>
		public void EditPlayerDialogAction(DialogueStateActionDTO dialogueStateActionToEdit, DialogueStateActionDTO newDialogueAction)
		{
			this.AddPlayerDialogAction(newDialogueAction);
			this.RemoveDialogueActions(IATConsts.PLAYER, new[] { dialogueStateActionToEdit });
		}


        /// <summary>
		/// Adds a new dialogue action
		/// </summary>
		/// <param name="dialogueStateActionDTO">The dto that specifies the dialogue action</param>
		public void AddAgentDialogAction(DialogueStateActionDTO dialogueStateActionDTO)
        {
            m_agentDialogues.AddDialog(new DialogStateAction(dialogueStateActionDTO));
        }
        /// <summary>
        /// Updates an existing dialogue action for the agents
        /// </summary>
        /// <param name="dialogueStateActionToEdit">The action to be updated.</param>
        /// <param name="newDialogueAction">The updated action.</param>
        public void EditAgentDialogAction(DialogueStateActionDTO dialogueStateActionToEdit, DialogueStateActionDTO newDialogueAction)
		{
            this.AddAgentDialogAction(newDialogueAction);
			this.RemoveDialogueActions(IATConsts.AGENT, new[] { dialogueStateActionToEdit });
		}

	    public DialogueStateActionDTO GetDialogActionById(string speaker, Guid id)
	    {
		    return SelectDialogActionList(speaker).GetDialogById(id).ToDTO();
	    }

        public Name BuildSpeakActionName(string speaker, Guid id)
        {
            var dialogue = new DialogStateAction(GetDialogActionById(speaker, id));
          
            var speakAction = string.Format(IATConsts.DIALOG_ACTION_KEY + "({0},{1},{2},{3})",
                dialogue.CurrentState, dialogue.NextState, 
                DialogStateAction.PackageList((Name)IATConsts.MEANINGS_PACKAGING_NAME, dialogue.Meanings),
                DialogStateAction.PackageList((Name)IATConsts.STYLES_PACKAGING_NAME, dialogue.Styles));
            return (Name)speakAction; 
        }

        public DialogueStateActionDTO GetDialogueAction(string speaker, Name currentState, Name nextState, Name meaning, Name style)
        {
            var dialogList = SelectDialogActionList(speaker);
            var action = dialogList.Where(d => d.CurrentState == currentState &&
                                               d.NextState == nextState &&
                                               DialogStateAction.PackageList((Name)IATConsts.MEANINGS_PACKAGING_NAME, d.Meanings) == meaning &&
                                               DialogStateAction.PackageList((Name)IATConsts.STYLES_PACKAGING_NAME, d.Styles) == style).FirstOrDefault();
            return action.ToDTO();
        }


        /// <summary>
        /// Retrives a list containing all the dialogue actions for the player or the agents filtered by a specific state.
        /// </summary>
        /// <param name="speaker">Either "Player" or "Agent".</param>
        /// <param name="state">Works as a filter for the state. </param>
        public IEnumerable<DialogueStateActionDTO> GetDialogueActionsByState(string speaker, string currentState)
		{
			var dialogList = SelectDialogActionList(speaker);
            return dialogList.Select(d => d.ToDTO()).Where(d => d.CurrentState == currentState);
		}

        /// <summary>
        /// Retrives a list containing all the dialogue actions for the player or the agents filtered by a specific state.
        /// </summary>
        /// <param name="speaker">Either "Player" or "Agent".</param>
        /// <param name="state">Works as a filter for the state. </param>
        public IEnumerable<DialogueStateActionDTO> GetAllDialogueActionsByState(string currentState)
        {
            var dialogList = this.GetAllDialogueActions();
            return dialogList.Select(d => d.ToDTO()).Where(d => d.CurrentState == currentState);
        }

        public IEnumerable<DialogueStateActionDTO> GetDialogueActionsBySpeaker(string speaker)
        {
            return SelectDialogActionList(speaker).Select(d => d.ToDTO());
        }

        public IEnumerable<DialogStateAction> GetAllDialogueActions()
        {
            return this.m_agentDialogues.Concat<DialogStateAction>(this.m_playerDialogues);
        }
        

        /// <summary>
        /// Removes a list of dialogue actions for either the player or the agent.
        /// </summary>
        /// <param name="speaker">Either "Player" or "Agent".</param>
        /// <param name="actionsToRemove">The list of dialogues that are to be removed.</param>
        public int RemoveDialogueActions(string speaker, IEnumerable<DialogueStateActionDTO> actionsToRemove)
		{
			return RemoveDialogueActions(speaker, actionsToRemove.Select(d => d.Id));
		}

		public int RemoveDialogueActions(string speaker, IEnumerable<Guid> actionsIdToRemove)
		{
			var dialogList = SelectDialogActionList(speaker);
			return actionsIdToRemove.Count(d => dialogList.RemoveDialog(d));
		}


        
        /// <summary>
        /// Retrieves the list of all dialogue actions for a speaker.
        /// </summary>
        /// <param name="speaker">Either "Player" or "Agent"</param>
        private DialogActionDictionary SelectDialogActionList(string speaker)
        {
            if (speaker != IATConsts.AGENT && speaker != IATConsts.PLAYER)
            {
                throw new Exception("Invalid Speaker");
            }

            if (speaker == IATConsts.AGENT)
                return m_agentDialogues;
            else
                return m_playerDialogues;
        }

		#endregion

		#region Dynamic Properties

	    public void BindToRegistry(IDynamicPropertiesRegistry registry)
	    {
			registry.RegistDynamicProperty(VALID_DIALOGUE_PROPERTY_TEMPLATE, ValidDialogPropertyCalculator);
		}

	    private static readonly Name VALID_DIALOGUE_PROPERTY_TEMPLATE = (Name)"ValidDialogue";
		private IEnumerable<DynamicPropertyResult> ValidDialogPropertyCalculator(IQueryContext context, Name currentState, Name nextState, Name meaning, Name style)
		{
			if (!context.Perspective.Match(Name.SELF_SYMBOL))
				return Enumerable.Empty<DynamicPropertyResult>();

			var key = DialogStateAction.BuildSpeakAction(currentState, nextState, new Name[] { meaning },new Name[] { style });
     		return context.Constraints.SelectMany(c => m_agentDialogues.GetAllDialogsForKey(key,c)).Select(p => new DynamicPropertyResult(Name.BuildName(true), p.Item2));
		}

		#endregion

		#region Serialization

		public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            dataHolder.SetValue("ScenarioName", ScenarioName);
			dataHolder.SetValue("Description",ScenarioDescription);

            // Save Character Sources
            if (m_characterSources.Count > 0)
            {
                dataHolder.SetValue("CharacterSources", m_characterSources.Select(d => ToRelativePath(d.Source)).ToArray());
            }

            // Save Player Dialogues
            if (m_playerDialogues.Count>0)
            {
	            dataHolder.SetValue("PlayerDialogues", m_playerDialogues.Select(d => d.ToDTO()).ToArray());
            }

            // Save Agent Dialogues
            if (m_agentDialogues.Count>0)
            {
                var agentDialogues = m_agentDialogues.Select(d => d.ToDTO()).ToArray();

                //Generate the automatic TTS code except if there is already an UtterancId that does not 
                //start with the generation prefix
                foreach (var d in agentDialogues)
                {
                    if(d.UtteranceId == null || d.UtteranceId.StartsWith(IATConsts.TTS_PREFIX))
                    {
                        d.UtteranceId = GenerateUtteranceId(d.Utterance);
                    }
                }
                dataHolder.SetValue("AgentDialogues", agentDialogues);
            }
        }

        public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
           
            ScenarioName = dataHolder.GetValue<string>("ScenarioName");

            ScenarioDescription = dataHolder.GetValue<string>("Description");

            //Load Character Sources
            m_characterSources = new List<CharacterSourceDTO>();
            var charArray = dataHolder.GetValue<string[]>("CharacterSources");
            if(charArray != null)
            { 
                for(int i=0; i < charArray.Length; i++)
                {
                    m_characterSources.Add(new CharacterSourceDTO { Id = i, Source = charArray[i] });
                }
            }

            //Load Player Dialogues
            m_playerDialogues = new DialogActionDictionary();
			var playerDialogueArray = dataHolder.GetValue<DialogueStateActionDTO[]>("PlayerDialogues");
            if (playerDialogueArray != null)
            {
	            foreach (var d in playerDialogueArray.Select(dto => new DialogStateAction(dto)))
	            {
					m_playerDialogues.AddDialog(d);
	            }
            }

            //Load Agent Dialogues
            m_agentDialogues = new DialogActionDictionary();
			var agentDialogueArray = dataHolder.GetValue<DialogueStateActionDTO[]>("AgentDialogues");
            if (agentDialogueArray != null)
            {
	            foreach (var d in agentDialogueArray.Select(dto => new DialogStateAction(dto)))
	            {
                    m_agentDialogues.AddDialog(d);
	            }
            }
		}

        protected override string OnAssetLoaded() { return null; }

        #endregion
    }
}
