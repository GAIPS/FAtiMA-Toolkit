using System;
using System.Collections.Generic;
using System.Linq;
using AssetPackage;
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
    public class IntegratedAuthoringToolAsset : LoadableAsset<IntegratedAuthoringToolAsset>, ICustomSerialization, IDynamicPropertiesRegister
    {
		public static readonly string INITIAL_DIALOGUE_STATE = "Start";
        public static readonly string TERMINAL_DIALOGUE_STATE = "End";
        public static readonly string PLAYER = "Player";
        public static readonly string AGENT = "Agent";

        private static readonly string TTSGenerationPrefix = "TTS-";

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
            return TTSGenerationPrefix + utteranceId;
        }

        /// <summary>
        /// Adds a new role-play character asset to the scenario.
        /// </summary>
        /// <param name="character">The instance of the Role Play Character asset</param>
        public void AddNewCharacterSource(CharacterSourceDTO dto)
        {
	        var absPath = ToAbsolutePath(dto.Source);
			string errorsOnLoad;
			var asset = RolePlayCharacterAsset.LoadFromFile(absPath, out errorsOnLoad);
	        if (errorsOnLoad != null)
		        throw new Exception(errorsOnLoad);
            dto.Id = m_characterSources.Count;
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
			this.RemoveDialogueActions(PLAYER, new[] { dialogueStateActionToEdit });
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
			this.RemoveDialogueActions(AGENT, new[] { dialogueStateActionToEdit });
		}

	    public DialogueStateActionDTO GetDialogActionById(string speaker, Guid id)
	    {
		    return SelectDialogActionList(speaker).GetDialogById(id).ToDTO();
	    }

		/// <summary>
		/// Retrives a list containing all the dialogue actions for the player or the agents filtered by a specific state.
		/// </summary>
		/// <param name="speaker">Either "Player" or "Agent".</param>
		/// <param name="state">Works as a filter for the state. The value "*" will consider all states.</param>
		public IEnumerable<DialogueStateActionDTO> GetDialogueActions(string speaker, Name currentState)
		{
			return GetDialogueActions(speaker, currentState, Name.UNIVERSAL_SYMBOL, Name.UNIVERSAL_SYMBOL, Name.UNIVERSAL_SYMBOL);
		}

		public IEnumerable<DialogueStateActionDTO> GetDialogueActions(string speaker, Name currentState, Name nextState)
		{
			return GetDialogueActions(speaker, currentState, nextState, Name.UNIVERSAL_SYMBOL, Name.UNIVERSAL_SYMBOL);
		}

		public IEnumerable<DialogueStateActionDTO> GetDialogueActions(string speaker, Name currentState, Name nextState, Name meanings)
		{
			return GetDialogueActions(speaker, currentState, nextState, meanings, Name.UNIVERSAL_SYMBOL);
		}

		public IEnumerable<DialogueStateActionDTO> GetDialogueActions(string speaker, Name currentState, Name nextState, Name meanings, Name styles)
		{
			var dialogList = SelectDialogActionList(speaker);
			var S = DialogStateAction.BuildSpeakAction(currentState, nextState, meanings, styles);
			return dialogList.GetAllDialogsForKey(S).Select(d => d.Item1.ToDTO());
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
            if (speaker != AGENT && speaker != PLAYER)
            {
                throw new Exception("Invalid Speaker");
            }

            if (speaker == AGENT)
                return m_agentDialogues;
            else
                return m_playerDialogues;
        }

		#endregion

		#region Dynamic Properties

	    public void BindToRegistry(IDynamicPropertiesRegistry registry)
	    {
			registry.RegistDynamicProperty(VALID_DIALOGUE_PROPERTY_TEMPLATE, ValidDialogPropertyCalculator,"Returns all valid dialogues that unify with [currentState], [nextState], [meaning] and [style]");
		}

	    public void UnbindToRegistry(IDynamicPropertiesRegistry registry)
	    {
		    throw new NotImplementedException();
	    }

	    private static readonly Name VALID_DIALOGUE_PROPERTY_TEMPLATE = (Name)"ValidDialogue";
		private IEnumerable<DynamicPropertyResult> ValidDialogPropertyCalculator(IQueryContext context, Name currentState, Name nextState, Name meaning, Name style)
		{
			if (!context.Perspective.Match(Name.SELF_SYMBOL))
				return Enumerable.Empty<DynamicPropertyResult>();

			var key = DialogStateAction.BuildSpeakAction(currentState, nextState, meaning, style);
			return context.Constraints.SelectMany(c => m_agentDialogues.GetAllDialogsForKey(key,c)).Select(p => new DynamicPropertyResult(Name.BuildName(true), p.Item2));
		}

		#endregion

		#region Serialization

		public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            dataHolder.SetValue("ScenarioName", ScenarioName);
			dataHolder.SetValue("Description",ScenarioDescription);
            if (m_characterSources.Count > 0)
            {
                dataHolder.SetValue("Characters", m_characterSources.Select(d => d.Source).ToArray());
            }
            if (m_playerDialogues.Count>0)
            {
	            dataHolder.SetValue("PlayerDialogues", m_playerDialogues.Select(d => d.ToDTO()).ToArray());
            }
            if (m_agentDialogues.Count>0)
            {
                var agentDialogues = m_agentDialogues.Select(d => d.ToDTO()).ToArray();
                foreach(var d in agentDialogues)
                {
                    if(d.UtteranceId == null || d.UtteranceId.StartsWith(TTSGenerationPrefix))
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
			var charArray = dataHolder.GetValue<string[]>("Characters");
            if(charArray == null)
            {
                m_characterSources = new List<CharacterSourceDTO>();
            }
            else
            {
                for(int i=0; i < charArray.Length; i++)
                {
                    m_characterSources.Add(new CharacterSourceDTO { Id = i, Source = charArray[i] });
                }
            }

            m_playerDialogues = new DialogActionDictionary();
			var playerDialogueArray = dataHolder.GetValue<DialogueStateActionDTO[]>("PlayerDialogues");
            if (playerDialogueArray != null)
            {
	            foreach (var d in playerDialogueArray.Select(dto => new DialogStateAction(dto)))
	            {
					m_playerDialogues.AddDialog(d);
	            }
            }
			m_agentDialogues= new DialogActionDictionary();
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
