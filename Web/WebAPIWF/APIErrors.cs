namespace WebAPIWF
{
    public class APIErrors
    {
        public static string ERROR_UNKNOWN_GET_REQUEST = "ERROR: Unknown GET request was sent!";
        public static string ERROR_UNKNOWN_POST_REQUEST = "ERROR: Unknown POST request was sent!";
        public static string ERROR_EMPTY_EVENT_LIST = "ERROR: The 'perceive' method requires a list of events as input!";
        public static string ERROR_EMPTY_ACTION_REQUEST_LIST = "ERROR: The 'execute' method requires a list of actions as input!";
        public static string ERROR_EXCEPTION_PERCEIVE = "ERROR: When perceiving event '{0}' the following exception occured: {1}!";
        public static string ERROR_EXCEPTION_UPDATE = "ERROR: The method's body needs to be a single number!";
        public static string ERROR_EXCEPTION_TRANSLATE = "ERROR: When translating action '{0}' the following exception occured: {1}!";
        public static string ERROR_UNKOWN_SPEAK_ACTION = "ERROR: Could not find a dialogue for the following speak action: '{0}'!";
        public static string ERROR_EXCEPTION_ASK = "ERROR: Incorrect URL format!";
    }
}
