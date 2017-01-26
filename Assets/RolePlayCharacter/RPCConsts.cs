using WellFormedNames;
using AutobiographicMemory;

namespace RolePlayCharacter
{
    public class RPCConsts
    {
        public static readonly Name DEFAULT_CHARACTER_NAME = (Name)"Nameless";

        public static readonly Name STRONGEST_EMOTION_PROPERTY_NAME = (Name)"StrongestEmotion";

        public static readonly Name MOOD_PROPERTY_NAME = (Name)"Mood";

        public static readonly Name ACTION_START_EVENT_PROTOTYPE = Name.BuildName(string.Format("{0}({1},*,*,*)",
                                                                            AMConsts.EVENT, AMConsts.ACTION_START));
        public static readonly Name ACTION_END_EVENT_PROTOTYPE = Name.BuildName(string.Format("{0}({1},*,*,*)",
                                                                            AMConsts.EVENT, AMConsts.ACTION_END));
    }
}
