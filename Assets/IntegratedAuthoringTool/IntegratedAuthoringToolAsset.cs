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
using Utilities.Json;

namespace IntegratedAuthoringTool
{
    /// <summary>
    /// This asset is responsible for managing the scenario, including its characters and respective dialogues
    /// </summary>
    [Serializable]
    public class IntegratedAuthoringToolAsset : ICustomSerialization
    {
        private DialogActionDictionary m_dialogues;

        public AssetStorage Assets { get; set; }
        
        public EventTriggers eventTriggers;

        public  WorldModelAsset WorldModel { get; private set; }
        public IEnumerable<RolePlayCharacterAsset> Characters { get; private set; }

        /// <summary>
        /// The name of the Scenario
        /// </summary>
        public string ScenarioName { get; set; }

		public string ScenarioDescription { get; set; }

	    public IntegratedAuthoringToolAsset()
        {
            m_dialogues = new DialogActionDictionary();
            Characters = new List<RolePlayCharacterAsset>();
            WorldModel =  new WorldModelAsset();
            eventTriggers = new EventTriggers();
        }

        public static IntegratedAuthoringToolAsset FromJson(string json, AssetStorage storage)
        {
            var serializer = new JSONSerializer();
            var aux = (JsonObject)JsonParser.Parse(json);
            var iat = serializer.DeserializeFromJson<IntegratedAuthoringToolAsset>(aux);
            foreach(var c in iat.Characters)
            {
                c.LoadAssociatedAssets(storage);
                iat.BindToRegistry(c.DynamicPropertiesRegistry);
            }
            iat.Assets = storage;
            return iat;
        }

        public string ToJson()
        {
            var serializer = new JSONSerializer();
            return serializer.SerializeToJson(this).ToString(true);
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


        public void AddNewCharacter(Name characterName)
        {
            if (this.Characters.Any(c => c.CharacterName == characterName))
                throw new Exception("A character with the given name already exists");

            var rpc = new RolePlayCharacterAsset();
            rpc.CharacterName = characterName;
            this.Characters = this.Characters.Concat(new[]{rpc});
        }

        /// <summary>
        /// Removes a list of characters from the scenario
        /// </summary>
        /// <param name="character">A list of character names</param>
        public void RemoveCharacters(IEnumerable<string> characterNames)
        {
            var newList = new List<RolePlayCharacterAsset>();
            foreach (var c in Characters)
            {
                if (!characterNames.Contains(c.CharacterName.ToString())) 
                {
                    newList.Add(c);
                }
            }
            Characters = newList;
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
			registry.RegistDynamicProperty(VALID_DIALOGUE_PROPERTY_TEMPLATE, "", ValidDialogPropertyCalculator);
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
			dataHolder.SetValue("Description", ScenarioDescription);

            // Save Dialogues
            if (m_dialogues.Count > 0)
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

            dataHolder.SetValue("Characters", Characters.ToArray());
            dataHolder.SetValue("WorldModel", WorldModel);
        }

        public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
           
            ScenarioName = dataHolder.GetValue<string>("ScenarioName");

            ScenarioDescription = dataHolder.GetValue<string>("Description");

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
            Characters = dataHolder.GetValue<RolePlayCharacterAsset[]>("Characters");
            WorldModel = dataHolder.GetValue<WorldModelAsset>("WorldModel");
        }

        #endregion
    }
}
