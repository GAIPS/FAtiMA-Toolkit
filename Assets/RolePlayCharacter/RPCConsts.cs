using WellFormedNames;
using AutobiographicMemory;

namespace RolePlayCharacter
{
    public class RPCConsts
    {
        public static readonly Name DEFAULT_CHARACTER_NAME = (Name)"Nameless";

        public static readonly Name STRONGEST_EMOTION_PROPERTY_NAME = (Name)"StrongestEmotion";

        public static readonly Name STRONGEST_EMOTION_FOR_EVENT_PROPERTY_NAME = (Name)"StrongestEmotionForEvent";

        public static readonly Name STRONGEST_WELL_BEING_EMOTION_PROPERTY_NAME = (Name)"StrongestWellBeingEmotion";

        public static readonly Name STRONGEST_ATTRIBUTION_PROPERTY_NAME = (Name)"StrongestAttributionEmotion";

        public static readonly Name STRONGEST_COMPOUND_PROPERTY_NAME = (Name)"StrongestCompoundEmotion";

        public static readonly Name MOOD_PROPERTY_NAME = (Name)"Mood";

        public static readonly Name GOAL_PROPERTY_NAME = (Name)"GoalLikelihood";

        public static readonly Name TICK_PROPERTY_NAME = (Name)"Tick";

        public static readonly Name ACTION_START_EVENT_PROTOTYPE = Name.BuildName(string.Format("{0}({1},*,*,*)",
                                                                            AMConsts.EVENT, AMConsts.ACTION_START));
        public static readonly Name ACTION_END_EVENT_PROTOTYPE = Name.BuildName(string.Format("{0}({1},*,*,*)",
                                                                            AMConsts.EVENT, AMConsts.ACTION_END));
        public static readonly Name COMMITED_ACTION_KEY = (Name)"Busy";

    }
}
