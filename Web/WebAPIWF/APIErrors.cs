namespace WebAPIWF
{
    public class APIErrors
    {
        public static string ERROR_UNKNOWN_GET_REQUEST = "ERROR: Unknown GET request was sent!";
        public static string ERROR_UNKNOWN_POST_REQUEST = "ERROR: Unknown PUT request was sent!";
        public static string ERROR_EMPTY_EVENT_LIST = "ERROR: The 'perceive' method requires a list of events as input!";
        public static string ERROR_EXCEPTION_PERCEIVE = "ERROR: When perceiving event '{0}' the following exception occured: {1}!";
        public static string ERROR_EXCEPTION_TRANSLATE = "ERROR: When translating action '{0}' the following exception occured: {1}!";
        public static string ERROR_UNKOWN_SPEAK_ACTION = "ERROR: Could not find a dialogue for the following speak action: '{0}'!";
    }
}
