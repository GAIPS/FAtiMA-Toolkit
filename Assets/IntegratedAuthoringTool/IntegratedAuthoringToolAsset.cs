using System;
using System.Collections.Generic;
using System.Linq;
using AssetPackage;
using GAIPS.Rage;
using SerializationUtilities;
using IntegratedAuthoringTool.DTOs;
using KnowledgeBase;
using RolePlayCharacter;
using Utilities;
using WellFormedNames;
using WellFormedNames.Collections;
using IQueryable = WellFormedNames.IQueryable;

namespace IntegratedAuthoringTool
{
    /// <summary>
    /// This asset is responsible for managing the scenario, including its characters and respective dialogues
    /// </summary>
    [Serializable]
    public class IntegratedAuthoringToolAsset : LoadableAsset<IntegratedAuthoringToolAsset>, ICustomSerialization
    {
	    private class CharacterHolder
	    {
		    public string Source;
		    public RolePlayCharacterAsset RPCAsset;
        }

        public static readonly string INITIAL_DIALOGUE_STATE = "Start";
        public static readonly string TERMINAL_DIALOGUE_STATE = "End";
        //public static readonly string ANY_DIALOGUE_STATE = "*";
        public static readonly string PLAYER = "Player";
        public static readonly string AGENT = "Agent";

        private DialogActionDictionary m_playerDialogues;
		private DialogActionDictionary m_agentDialogues;

        private Dictionary<string, CharacterHolder> m_characterSources;
        private Dictionary<string, string> m_dialogueStates;

        /// <summary>
        /// The name of the Scenario
        /// </summary>
        public string ScenarioName { get; set; }

        /// <summary>
        /// This method is used to automatically load any associated assets.
        /// </summary>
        protected override string OnAssetLoaded()
        {
	        string currentKey = string.Empty;
		    string currentAbsolutePath = null;
            try
		    {
				if(m_characterSources==null)
					m_characterSources = new Dictionary<string, CharacterHolder>();
				else
				{
					foreach (var pair in m_characterSources)
					{
						currentKey = pair.Key;
						currentAbsolutePath = ToAbsolutePath(pair.Value.Source);
						if (pair.Value.RPCAsset == null)
						{
							string errorsOnLoad;
							pair.Value.RPCAsset = RolePlayCharacterAsset.LoadFromFile(currentAbsolutePath, out errorsOnLoad);
							if (errorsOnLoad != null)
								return errorsOnLoad;

							RegistDynamicProperties(pair.Value.RPCAsset);
						}

						//if (!string.Equals(pair.Key, pair.Value.RPCAsset.CharacterName))
						//	return $"Name mismatch. IAT name \"{pair.Key}\" != RPC File Name \"{pair.Value.RPCAsset.CharacterName}\" for file \"{currentAbsolutePath}\"";
					}
				}
			}
		    catch (Exception e)
		    {
#if DEBUG
				getInterface<ILog>()?.Log(Severity.Error, e.ToString());
#endif
				return $"An error occured when trying to load the RPC \"{currentKey}\" at \"{currentAbsolutePath}\". Please check if the path is correct.";
			}
		    return null;
		}

	    protected override void OnAssetPathChanged(string oldpath)
	    {
		    foreach (var holder in m_characterSources.Values)
		    {
			    var absPath = ToAbsolutePath(oldpath, holder.Source);
			    holder.Source = ToRelativePath(AssetFilePath, absPath);
		    }
	    }

	    public IntegratedAuthoringToolAsset()
        {
            m_playerDialogues = new DialogActionDictionary();
            m_agentDialogues = new DialogActionDictionary();
	        m_characterSources = new Dictionary<string, CharacterHolder>();
            m_dialogueStates = new Dictionary<string, string>();
        }

		/// <summary>
        /// Retreives all the sources for the characters in the scenario.
        /// </summary>
        public IEnumerable<CharacterSourceDTO> GetAllCharacterSources()
        {
	        return m_characterSources.Select(p => new CharacterSourceDTO() {Name = p.Key, Source = ToAbsolutePath(p.Value.Source)});
        }

        /// <summary>
        /// Retreives the instance of the RPC asset associated to a specific character.
        /// </summary>
        /// <param name="characterName">The name of the character</param>
	    public RolePlayCharacterAsset GetCharacterAsset(string characterName)
	    {
		    CharacterHolder holder;
		    if (!m_characterSources.TryGetValue(characterName, out holder))
				throw new Exception($"Character \"{characterName}\" not found");

		    return holder.RPCAsset;
	    }

	    public RolePlayCharacterAsset InstantiateCharacterAsset(string characterName)
	    {
			CharacterHolder holder;
			if (!m_characterSources.TryGetValue(characterName, out holder))
				throw new Exception($"Character \"{characterName}\" not found");

			var currentAbsolutePath = ToAbsolutePath(holder.Source);
			var asset = RolePlayCharacterAsset.LoadFromFile(currentAbsolutePath);
			RegistDynamicProperties(asset);
		    return asset;
		}

        /// <summary>
        /// Retreives all instance of the RPC assets.
        /// </summary>
        public IEnumerable<RolePlayCharacterAsset> GetAllCharacters()
	    {
		    return m_characterSources.Values.Select(h => h.RPCAsset);
	    }

        /// <summary>
        /// Adds a new role-play character asset to the scenario.
        /// </summary>
        /// <param name="character">The instance of the Role Play Character asset</param>
        public void AddCharacter(RolePlayCharacterAsset character)
        {
	        if(m_characterSources.ContainsKey(character.CharacterName))
				throw new Exception("A character with the same name already exists.");

			m_characterSources.Add(character.CharacterName,new CharacterHolder() {Source = ToRelativePath(character.AssetFilePath),RPCAsset = character});
        }
        
        /// <summary>
        /// Removes a list of characters from the scenario
        /// </summary>
        /// <param name="character">A list of character names</param>
        public void RemoveCharacters(IList<string> charactersToRemove)
        {
            foreach (var characterName in charactersToRemove)
            {
	            m_characterSources.Remove(characterName);
            }   
        }

		#region Dialog System

		/// <summary>
		/// Adds a new dialogue action
		/// </summary>
		/// <param name="dialogueStateActionDTO">The dto that specifies the dialogue action</param>
		public void AddAgentDialogAction(DialogueStateActionDTO dialogueStateActionDTO)
		{
			m_agentDialogues.AddDialog(new DialogStateAction(dialogueStateActionDTO));
		}

		/// <summary>
		/// Adds a new dialogue action 
		/// </summary>
		/// <param name="dialogueStateActionDTO">The action to add.</param>
		public void AddPlayerDialogAction(DialogueStateActionDTO dialogueStateActionDTO)
		{
			m_playerDialogues.AddDialog(new DialogStateAction(dialogueStateActionDTO));
		}

		/// <summary>
		/// Retrieves the current dialogue state for a specific character
		/// </summary>
		/// <param name="character">The name of the character</param>
		public string GetCurrentDialogueState(string character)
		{
			string state;
			if (m_dialogueStates.TryGetValue(character, out state))
				return state;

			m_dialogueStates[character] = INITIAL_DIALOGUE_STATE;
			return INITIAL_DIALOGUE_STATE;
		}

		/// <summary>
		/// Updates the current dialogue state for a specific character
		/// </summary>
		/// <param name="character">The name of the character</param>
		/// <param name="state">The name of the character</param>
		public void SetDialogueState(string character, string state)
		{
			m_dialogueStates[character] = state;
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

		private void RegistDynamicProperties(RolePlayCharacterAsset character)
	    {
			character.DynamicPropertiesRegistry.RegistDynamicProperty(VALID_DIALOGUE_PROPERTY_TEMPLATE,ValidDialogPropertyCalculator, "No description");
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
            if (m_characterSources.Count > 0)
            {
				dataHolder.SetValue("Characters", m_characterSources.Select(p => new CharacterSourceDTO() { Name = p.Key, Source = ToRelativePath(p.Value.Source) }).ToArray());
            }
            if (m_playerDialogues.Count>0)
            {
	            dataHolder.SetValue("PlayerDialogues", m_playerDialogues.Select(d => d.ToDTO()).ToArray());
            }
            if (m_agentDialogues.Count>0)
            {
                dataHolder.SetValue("AgentDialogues", m_agentDialogues.Select(d => d.ToDTO()).ToArray());
            }
        }

        public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
			ScenarioName = dataHolder.GetValue<string>("ScenarioName");
			
			var charArray = dataHolder.GetValue<CharacterSourceDTO[]>("Characters");
            if (charArray != null)
				m_characterSources = charArray.ToDictionary(c => c.Name, c => new CharacterHolder() {Source = c.Source });

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

			m_dialogueStates = new Dictionary<string, string>();
		}

        #endregion
    }
}
