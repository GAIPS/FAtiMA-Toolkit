namespace WebAPIWF
{
    public class APIMethod
    {
        public string Name { get; private set; }
        public string Type { get; private set; }

        //GET METHODS (NO BODY)
        public static APIMethod DECIDE = new APIMethod() { Name = "decide", Type = "GET" };
        public static APIMethod EMOTIONALSTATE = new APIMethod() { Name = "emotional-state", Type = "GET" };
        public static APIMethod RESET = new APIMethod() { Name = "reset", Type = "GET" };

        //POST METHODS (BODY REQUIRED)
        public static APIMethod PERCEIVE = new APIMethod() { Name = "perceive", Type = "POST" };
        public static APIMethod UPDATE = new APIMethod() { Name = "update", Type = "POST" };
        public static APIMethod TRANSLATE = new APIMethod() { Name = "translate", Type = "POST" };

        public static APIMethod[] Methods = { DECIDE, EMOTIONALSTATE, TRANSLATE, PERCEIVE, UPDATE, RESET };

        public override string ToString()
        {
            return this.Name;
        }
    }
}
