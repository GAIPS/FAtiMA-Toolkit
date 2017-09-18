using System;
using IntegratedAuthoringTool.DTOs;
using WellFormedNames;

namespace IntegratedAuthoringTool
{

    /// <summary>
    /// Represents a dialogue action
    /// </summary>
    public class DialogStateAction
    {
        
        public Guid Id { get; private set; }
        public Name CurrentState { get; private set; }
        public Name NextState { get; private set; }
        public Name Meaning { get; private set; }
        public Name Style { get; private set; }
        public string Utterance { get; private set; }
        public string UtteranceId { get; set;}

        /// <summary>
        /// Creates a new instance of a dialogue action from the corresponding DTO
        /// </summary>
        public DialogStateAction(DialogueStateActionDTO dto)
        {
	        this.Id = dto.Id == Guid.Empty?Guid.NewGuid() : dto.Id;
	        this.CurrentState = Name.BuildName(dto.CurrentState);
            this.Meaning = Name.BuildName(dto.Meaning);
	        this.Style = Name.BuildName(dto.Style);
	        this.NextState = Name.BuildName(dto.NextState);
            this.Utterance = dto.Utterance;
            this.UtteranceId = dto.UtteranceId;
        }

        public Name BuildSpeakAction()
        {
            var speakAction = string.Format(IATConsts.DIALOG_ACTION_KEY + "({0},{1},{2},{3})",
                CurrentState, NextState,Meaning,Style);
            return (Name)speakAction;
        }

        /// <summary>
        /// Creates a DTO from the dialogue action
        /// </summary>
        public DialogueStateActionDTO ToDTO()
        {
            return new DialogueStateActionDTO
            {
                Id = this.Id,
                CurrentState = this.CurrentState.ToString(),
                NextState = this.NextState.ToString(),
                Meaning = this.Meaning.ToString(),
                Style = this.Style.ToString(),
                Utterance = this.Utterance,
                UtteranceId = this.UtteranceId
            };
        }
    }
}
