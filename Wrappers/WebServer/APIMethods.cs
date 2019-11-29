using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer
{
    public class APIMethods
    {
        public string Name { get; private set; }
        public string Type { get; private set; }
        public string Params { get; private set; }
        public string Description { get; private set; }



        //GET METHODS (NO BODY)
        public static APIMethods DECIDE = new APIMethods() { Name = "decide", Params = "?c='char'", Type = "GET", Description = "Returns the actions decided by the character to execute." };
        public static APIMethods CHARACTERS = new APIMethods() { Name = "characters", Type = "GET", Description = "Returns all loaded characters and their emotional state." };
        public static APIMethods ASK = new APIMethods() { Name = "ask", Params = "?c='character'&bh='belHead'&bb='belBody'", Type = "GET", Description = "Returns the value of a belief for a given character" };
        public static APIMethods BELIEFS = new APIMethods() { Name = "beliefs", Params = "?c='character", Type = "GET", Description = "Returns the current beliefs for a given character" };
        public static APIMethods AM = new APIMethods() { Name = "am", Params = "?c='character", Type = "GET", Description = "Returns all the events stored in the agent's Autobiographical Memory" };

        //POST METHODS (BODY REQUIRED)
        public static APIMethods PERCEIVE = new APIMethods() { Name = "perceive", Type = "POST", Description = "Makes the characters perceive all the event strings passed in the body." };
        public static APIMethods UPDATE = new APIMethods() { Name = "update", Type = "POST", Description = "Updates the characters' internal state for 'x' amount of ticks." };
        public static APIMethods EXECUTE = new APIMethods() { Name = "execute", Type = "POST", Description = "Executes a given action and trigger all the effects defined in the World Model." };
        public static APIMethods CREATE = new APIMethods() { Name = "create", Type = "POST", Description = "Creates or resets a new instance of RPCs in the scenario." };

        public static APIMethods[] Methods = { DECIDE, CHARACTERS, ASK, BELIEFS, AM, PERCEIVE, UPDATE, EXECUTE, CREATE };

        public override string ToString()
        {
            return this.Name;
        }
    }
}
