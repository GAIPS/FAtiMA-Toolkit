using System;
using ActionLibrary;
using KnowledgeBase.Conditions;
using KnowledgeBase.WellFormedNames;

namespace IntegratedAuthoringTool
{
    public class DialogStateAction : BaseActionDefinition
    {
        public static readonly Name DIALOG_ACTION_NAME = Name.BuildName("Speak");
        public static readonly Name SPEAKER_TYPE_PLAYER = Name.BuildName("P");
        public static readonly Name SPEAKER_TYPE_AGENT = Name.BuildName("A");

        public string Utterance { get; private set; }
        

        public DialogStateAction(Name speakerType, Name currentState, Name meaning, Name nextState, string utterance) : 
            base(Name.BuildName(DIALOG_ACTION_NAME, speakerType, currentState, nextState, nextState, meaning), Name.NIL_SYMBOL, new ConditionSet())
        {
            if (speakerType != SPEAKER_TYPE_PLAYER && speakerType != SPEAKER_TYPE_AGENT)
            {
                throw new Exception("Invalid Actor Type");
            }
            this.Utterance = utterance;
        }
     
    }
}
