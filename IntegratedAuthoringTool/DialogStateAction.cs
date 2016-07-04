using System;
using ActionLibrary;
using IntegratedAuthoringTool.DTOs;
using KnowledgeBase.Conditions;
using KnowledgeBase.WellFormedNames;

namespace IntegratedAuthoringTool
{
    public class DialogStateAction// : BaseActionDefinition
    {
        public static readonly Name DIALOG_ACTION_NAME = Name.BuildName("Speak");
	    public Guid Id { get; private set; }
	    public string CurrentState { get; private set; }
        public string NextState { get; private set; }
        public string Meaning { get; private set; }
        public string Style { get; private set; }
        public string Utterance { get; private set; }

        //private DialogStateAction(Name currentState, Name meaning, Name style, Name nextState) : 
        //    base(Name.BuildName(DIALOG_ACTION_NAME, currentState, meaning, style, nextState), Name.NIL_SYMBOL, new ConditionSet()){}

        public DialogStateAction(DialogueStateActionDTO dto)
			//: this(Name.BuildName(dto.CurrentState), Name.BuildName(dto.Meaning), Name.BuildName(dto.Style), Name.BuildName(dto.NextState))
        {
	        this.Id = dto.Id == Guid.Empty?Guid.NewGuid() : dto.Id;
            this.CurrentState = Name.BuildName(dto.CurrentState).ToString();
            this.Meaning = Name.BuildName(dto.Meaning).ToString();
            this.Style = Name.BuildName(dto.Style).ToString();
            this.NextState = Name.BuildName(dto.NextState).ToString();
            this.Utterance = dto.Utterance;
        }

		public DialogueStateActionDTO ToDTO()
        {
            return new DialogueStateActionDTO
            {
                Id = this.Id,
                CurrentState = this.CurrentState,
                Meaning = this.Meaning,
                NextState = this.NextState,
                Style = this.Style,
                Utterance = this.Utterance
            };
        }

	    //protected override float CalculateActionUtility(IAction a)
	    //{
		   // return 1;
	    //}
    }
}
