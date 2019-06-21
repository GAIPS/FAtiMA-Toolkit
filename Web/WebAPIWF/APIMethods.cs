namespace WebAPIWF
{
    public class APIMethod
    {
        public string Name { get; private set; }
        public string Type { get; private set; }
        public string Params { get; private set; }


        //GET METHODS (NO BODY)
        public static APIMethod DECIDE = new APIMethod() { Name = "decide", Params = "?c='character'", Type = "GET" };
        public static APIMethod CHARACTERS = new APIMethod() { Name = "characters", Type = "GET" };


        //POST METHODS (BODY REQUIRED)
        public static APIMethod RESET = new APIMethod() { Name = "reset", Type = "POST" };
        public static APIMethod PERCEIVE = new APIMethod() { Name = "perceive", Type = "POST" };
        public static APIMethod UPDATE = new APIMethod() { Name = "update", Type = "POST" };
        public static APIMethod EXECUTE = new APIMethod() { Name = "execute", Type = "POST" };

        public static APIMethod[] Methods = { DECIDE, CHARACTERS, PERCEIVE, UPDATE, EXECUTE, RESET };

        public override string ToString()
        {
            return this.Name;
        }
    }
}
