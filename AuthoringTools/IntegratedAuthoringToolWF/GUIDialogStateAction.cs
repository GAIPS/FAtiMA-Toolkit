using IntegratedAuthoringTool.DTOs;
using System;

namespace IntegratedAuthoringToolWF
{
    [Serializable]
    internal class GUIDialogStateAction
    {
        public Guid Id { get; }
        public string CurrentState { get; }
        public string NextState { get; }
        public string Meaning { get; }
        public string Style { get; }
        public string Utterance { get; }

        public GUIDialogStateAction(DialogueStateActionDTO dto)
        {
            Id = dto.Id;
            CurrentState = dto.CurrentState;
            NextState = dto.NextState;
            Meaning = dto.Meaning;
            Style = dto.Style;
            Utterance = dto.Utterance;
        }
    }
}