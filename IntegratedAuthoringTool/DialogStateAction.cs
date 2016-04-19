using System;
using System.Collections.Generic;
using System.Linq;
using ActionLibrary;
using KnowledgeBase.Conditions;
using KnowledgeBase.WellFormedNames;

namespace IntegratedAuthoringTool
{
    public class DialogStateAction : BaseActionDefinition
    {
        public static readonly Name DIALOG_ACTION_NAME = Name.BuildName("Speak");
        public static readonly Name ACTOR_TYPE_PLAYER = Name.BuildName("Player");
        public static readonly Name ACTOR_TYPE_AGENT = Name.BuildName("Agent");

        public string Utterance { get; private set; }
        

        public DialogStateAction(Name actorType, Name currentState, Name meaning, Name nextState, string utterance) : 
            base(Name.BuildName(DIALOG_ACTION_NAME, actorType, currentState, nextState, nextState, meaning), Name.NIL_SYMBOL, Enumerable.Empty<Condition>())
        {
            if (actorType != ACTOR_TYPE_AGENT && actorType != ACTOR_TYPE_PLAYER)
            {
                throw new Exception("Invalid Actor Type");
            }
            this.Utterance = utterance;
        }
        
        public override object Clone()
        {
            throw new System.NotImplementedException();
        }
    }
}
