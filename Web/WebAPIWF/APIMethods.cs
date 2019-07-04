namespace WebAPIWF
{
    public class APIMethod
    {
        public string Name { get; private set; }
        public string Type { get; private set; }
        public string Params { get; private set; }
        public string Description { get; private set; }



        //GET METHODS (NO BODY)
        public static APIMethod DECIDE = new APIMethod() { Name = "decide", Params = "?c='char'", Type = "GET", Description = "Returns the actions decided by the character to execute." };
        public static APIMethod CHARACTERS = new APIMethod() { Name = "characters", Type = "GET", Description = "Returns all loaded characters and their emotional state." };
        public static APIMethod ASK = new APIMethod() { Name = "ask", Params = "?c='character'&bh='belHead'&bb='belBody'",Type = "GET", Description = "Returns the value of a belief for a given character" };

        //POST METHODS (BODY REQUIRED)
        public static APIMethod RESET = new APIMethod() { Name = "reset", Type = "POST" , Description = "Reloads the scenario and all the characters."};
        public static APIMethod PERCEIVE = new APIMethod() { Name = "perceive", Type = "POST", Description = "Makes the characters perceive all the event strings passed in the body." };
        public static APIMethod UPDATE = new APIMethod() { Name = "update", Type = "POST",  Description = "Updates the characters' internal state for 'x' amount of ticks."};
        public static APIMethod EXECUTE = new APIMethod() { Name = "execute", Type = "POST",  Description = "Executes a given action and trigger all the effects defined in the World Model." };

        public static APIMethod[] Methods = { DECIDE, CHARACTERS, ASK, PERCEIVE, UPDATE, EXECUTE, RESET };

        public override string ToString()
        {
            return this.Name;
        }
    }
}
