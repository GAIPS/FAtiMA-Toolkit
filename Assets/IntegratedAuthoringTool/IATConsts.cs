using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegratedAuthoringTool
{
    public class IATConsts
    {
        public static readonly string INITIAL_DIALOGUE_STATE = "Start";
        public static readonly string TERMINAL_DIALOGUE_STATE = "End";
        public static readonly string DIALOGUE_STATE_PROPERTY = "DialogueState({0})";
        public static readonly string PLAYER = "Player";
        public static readonly string AGENT = "Agent";
        public static readonly string DIALOG_ACTION_KEY = "Speak";
        public static readonly string TTS_PREFIX = "TTS-";
    }
}
