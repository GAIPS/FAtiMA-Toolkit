using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer
{
    public class APIErrors
    {
        public static string ERROR_EMPTY_EVENT_LIST = "ERROR: The 'perceive' method requires a list of events as input!";
        public static string ERROR_EXCEPTION = "ERROR: The following exception occured: {0}!";
        public static string ERROR_EXCEPTION_UPDATE = "ERROR: The method's body needs to be a single number!";
        public static string ERROR_EMPTY_ACTION_REQUEST_LIST = "ERROR: The 'Actions' resource requires a list of actions as input!";
        public static string ERROR_EXCEPTION_PERCEIVE = "ERROR: When perceiving event '{0}' the following exception occured: {1}!";
        public static string ERROR_UNKOWN_SPEAK_ACTION = "ERROR: Could not find a dialogue for the following speak action: '{0}'!";
        public static string ERROR_UNKNOWN_SCENARIO = "ERROR: Unknown Scenario!";
        public static string ERROR_INVALID_HTTP_METHOD = "ERROR: Unsupported HTTP Method for this resource!";
        public static string ERROR_ACCESS_DENIED = "ERROR: Acess Denied!";
        public static string ERROR_INVALID_URL_SIZE = "ERROR: URL size is incorrect!";
    }
}
