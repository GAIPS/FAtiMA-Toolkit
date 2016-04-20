using System;
using ActionLibrary;
using IntegratedAuthoringTool.DTOs;
using KnowledgeBase.Conditions;
using KnowledgeBase.WellFormedNames;

namespace IntegratedAuthoringTool
{
    [Serializable]
    public class DialogStateAction : BaseActionDefinition
    {
        public static readonly Name DIALOG_ACTION_NAME = Name.BuildName("Speak");

        public static readonly string SPEAKER_TYPE_PLAYER ="Player";
        public static readonly string SPEAKER_TYPE_AGENT = "Agent";

        public string SpeakerType { get; private set; }
        public string CurrentState { get; private set; }
        public string Meaning { get; private set; }
        public string Style { get; private set; }
        public string NextState { get; private set; }
        public string Utterance { get; private set; }

        private DialogStateAction(Name speakerType, Name currentState, Name meaning, Name style, Name nextState) : 
            base(Name.BuildName(DIALOG_ACTION_NAME, speakerType, currentState, meaning, style, nextState), Name.NIL_SYMBOL, new ConditionSet()){}

        public DialogStateAction(DialogueStateActionDTO dto) :
            this(Name.BuildName(dto.SpeakerType), Name.BuildName(dto.CurrentState), Name.BuildName(dto.Meaning), Name.BuildName(dto.Style), Name.BuildName(dto.NextState))
        {
            if (dto.SpeakerType != SPEAKER_TYPE_PLAYER && dto.SpeakerType != SPEAKER_TYPE_AGENT)
            {
                throw new Exception("Invalid Speaker Type");
            }

            this.SpeakerType = dto.SpeakerType;
            this.CurrentState = dto.CurrentState;
            this.Meaning = dto.Meaning;
            this.Style = dto.Style;
            this.NextState = dto.NextState;
            this.Utterance = dto.Utterance;
        }


        public DialogueStateActionDTO ToDTO()
        {
            return new DialogueStateActionDTO
            {
                SpeakerType = this.SpeakerType,
                CurrentState = this.CurrentState,
                Meaning = this.Meaning,
                NextState = this.NextState,
                Style = this.Style,
                Utterance = this.Utterance
            };
        }
    }
}
