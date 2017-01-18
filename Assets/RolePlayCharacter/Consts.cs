using WellFormedNames;

namespace RolePlayCharacter
{
    public class Consts
    {
        public static readonly Name DEFAULT_CHARACTER_NAME = (Name)"Nameless";
        public static readonly Name STRONGEST_EMOTION_PROPERTY_NAME = (Name)"StrongestEmotion";
        public static readonly Name MOOD_PROPERTY_NAME = (Name)"Mood";
        public static readonly Name EVENT_MATCHING_AGENT_ADDED = (Name)"Event(Agent-Added,Self,*,Self)";
        public static readonly Name EVENT_MATCHING_AGENT_REMOVED = (Name)"Event(Agent-Added,Self,*,Self)";
        public static readonly Name EVT_ROOT_NAME = Name.BuildName("Event");
        public static readonly Name ACTION_START_NAME = Name.BuildName("Action-Start");
        public static readonly Name ACTION_FINISHED_NAME = Name.BuildName("Action-Finished");
    }
}
