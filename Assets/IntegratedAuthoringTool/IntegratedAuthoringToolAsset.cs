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
using ActionLibrary;
using WorldModel;

namespace IntegratedAuthoringTool
{
    /// <summary>
    /// This asset is responsible for managing the scenario, including its characters and respective dialogues
    /// </summary>
    [Serializable]
    public class IntegratedAuthoringToolAsset : LoadableAsset<IntegratedAuthoringToolAsset>, ICustomSerialization
    {
        private DialogActionDictionary m_dialogues;
        private IList<CharacterSourceDTO> m_characterSources;

        public  EventTriggers eventTriggers;

        public  WorldModelSourceDTO m_worldModelSource { get; set; }

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

	        if (m_worldModelSource != null)
	            m_worldModelSource.Source = ToRelativePath(AssetFilePath, ToAbsolutePath(oldpath, m_worldModelSource.Source));

	    }

	    public IntegratedAuthoringToolAsset()
        {
            m_dialogues = new DialogActionDictionary();
	        m_characterSources = new List<CharacterSourceDTO>();
            m_worldModelSource =  new WorldModelSourceDTO();

            eventTriggers = new EventTriggers();
        }

		/// <summary>
        /// Retreives all the sources for the characters in the scenario.
        /// </summary>
        public IEnumerable<CharacterSourceDTO> GetAllCharacterSources()
        {
	        return m_characterSources.Select(p => new CharacterSourceDTO() {Id = p.Id, Source = ToAbsolutePath(p.Source), RelativePath = p.Source});

        }

        public WorldModelSourceDTO GetWorldModelSource()
        {
            if (m_worldModelSource?.Source != null)
            {
                if (m_worldModelSource.RelativePath == null)
                {
                    m_worldModelSource.RelativePath = m_worldModelSource.Source;

                    m_worldModelSource.Source = ToAbsolutePath(m_worldModelSource.Source);
                }

                return m_worldModelSource;
            }
            return null;


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
		public Guid AddDialogAction(DialogueStateActionDTO dialogueStateActionDTO)
		{
            var newDA = new DialogStateAction(dialogueStateActionDTO);
            m_dialogues.AddDialog(newDA);
            return newDA.Id;
		}

		/// <summary>
		/// Updates an existing dialogue action for the player
		/// </summary>
		/// <param name="dialogueStateActionToEdit">The action to be updated.</param>
		/// <param name="newDialogueAction">The updated action.</param>
		public Guid EditDialogAction(DialogueStateActionDTO dialogueStateActionToEdit, DialogueStateActionDTO newDialogueAction)
		{
			var id = this.AddDialogAction(newDialogueAction);
			this.RemoveDialogueActions(new[] { dialogueStateActionToEdit });
            return id;
		}

	    public DialogueStateActionDTO GetDialogActionById(Guid id)
	    {
		    return m_dialogues.GetDialogById(id).ToDTO();
	    }

        public Name BuildSpeakActionName(Guid id)
        {
            var dialogue = new DialogStateAction(GetDialogActionById(id));
            return dialogue.BuildSpeakAction();
        }

        public List<DialogueStateActionDTO> GetDialogueActions(Name currentState, Name nextState, Name meaning, Name style)
        {
            var actions = (IEnumerable<DialogStateAction>)m_dialogues.ToList();

            if (currentState.ToString() != Name.UNIVERSAL_STRING)
            {
                actions = actions.Where(d => d.CurrentState == currentState);
            }

            if (nextState.ToString() != Name.UNIVERSAL_STRING)
            {
                actions = actions.Where(d => d.NextState == nextState);
            }

            if (meaning.ToString() != Name.UNIVERSAL_STRING)
            {
                actions = actions.Where(d => d.Meaning == meaning);
            }

            if (style.ToString() != Name.UNIVERSAL_STRING)
            {
                actions = actions.Where(d => d.Style == style);
            }

            var retList = new List<DialogueStateActionDTO>();

            foreach (var action in actions)
            {
                retList.Add(action.ToDTO());
            }

            return retList;
        }

        public List<DialogueStateActionDTO> GetDialogueActionsByState(string currentState)
        {
            return this.GetDialogueActions(Name.BuildName(currentState), Name.UNIVERSAL_SYMBOL, Name.UNIVERSAL_SYMBOL, Name.UNIVERSAL_SYMBOL);
        }

        public DialogueStateActionDTO GetDialogAction(IAction action, out string error)
        {
            error = null; 

            if (action.Parameters.Count != 4)
            {
                error = "ERROR - Speak action does not have four parameters'" + action + "'\n";
            }

            var diag = this.GetDialogueActions(action.Parameters[0],
                                               action.Parameters[1],
                                               action.Parameters[2],
                                               action.Parameters[3]).Shuffle().FirstOrDefault();
            if (diag == null)
            {
                error = "ERROR - No dialogue defined for action '" + action + "'\n";
            }

            return diag;
        }


        public IEnumerable<DialogueStateActionDTO> GetAllDialogueActions()
        {
            return this.m_dialogues.Select(d => d.ToDTO());
        }

        /// <summary>
        /// Removes a list of dialogue actions for either the player or the agent.
        /// </summary>
        /// <param name="speaker">Either "Player" or "Agent".</param>
        /// <param name="actionsToRemove">The list of dialogues that are to be removed.</param>
        public int RemoveDialogueActions(IEnumerable<DialogueStateActionDTO> actionsToRemove)
		{
			return RemoveDialogueActions(actionsToRemove.Select(d => d.Id));
		}

		public int RemoveDialogueActions(IEnumerable<Guid> actionsIdToRemove)
		{
			return actionsIdToRemove.Count(d => m_dialogues.RemoveDialog(d));
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
		        yield break;

			var key = Name.BuildName(string.Format(IATConsts.DIALOG_ACTION_KEY + "({0},{1},{2},{3})",
                currentState, nextState, meaning, style));

		    foreach (var c in context.Constraints)
		    {
		        var dialogues = m_dialogues.GetAllDialogsForKey(key, c);

		        foreach (var d in dialogues)
		        {
		            yield return new DynamicPropertyResult(new ComplexValue(Name.BuildName(true)), d.Item2);
		        }
		    }
	
		}

		#endregion

		#region Serialization

		public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            dataHolder.SetValue("ScenarioName", ScenarioName);
			dataHolder.SetValue("Description",ScenarioDescription);
            if(m_worldModelSource != null)
            dataHolder.SetValue("WorldModelSource", ToRelativePath(m_worldModelSource.Source));
            // Save Character Sources
            if (m_characterSources.Count > 0)
            {
                dataHolder.SetValue("CharacterSources", m_characterSources.Select(d => ToRelativePath(d.Source)).ToArray());
            }

            // Save Dialogues
            if (m_dialogues.Count>0)
            {
                var dialogues = m_dialogues.Select(d => d.ToDTO()).ToArray();

                //Generate the automatic TTS code except if there is already an UtterancId that does not 
                //start with the generation prefix
                foreach (var d in dialogues)
                {
                    if(d.UtteranceId == null || d.UtteranceId.StartsWith(IATConsts.TTS_PREFIX))
                    {
                        d.UtteranceId = GenerateUtteranceId(d.Utterance);
                    }
                }
                dataHolder.SetValue("Dialogues", dialogues);
            }
        }

        public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
           
            ScenarioName = dataHolder.GetValue<string>("ScenarioName");

            ScenarioDescription = dataHolder.GetValue<string>("Description");
            var relativePath = dataHolder.GetValue<string>("WorldModelSource");
            if(relativePath != null)
            m_worldModelSource = new WorldModelSourceDTO() {Source = ToAbsolutePath(relativePath), RelativePath = null};

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


            //Load Agent Dialogues
            m_dialogues = new DialogActionDictionary();
			var agentDialogueArray = dataHolder.GetValue<DialogueStateActionDTO[]>("Dialogues");
            if (agentDialogueArray != null)
            {
	            foreach (var d in agentDialogueArray.Select(dto => new DialogStateAction(dto)))
	            {
                    m_dialogues.AddDialog(d);
	            }
            }
		}

        protected override string OnAssetLoaded() { return null; }

        #endregion
    }
}
