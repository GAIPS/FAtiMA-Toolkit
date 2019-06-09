namespace FAtiMAHTTPServer
{
    public class APIMethod
    {
        public string Name { get; private set; }

        //GET METHODS (NO BODY)
        public static APIMethod DECIDE = new APIMethod() { Name = "decide" };
        public static APIMethod EMOTIONALSTATE = new APIMethod() { Name = "emotional-state" };
        public static APIMethod RESET = new APIMethod() { Name = "reset" };

        //POST METHODS (BODY REQUIRED)
        public static APIMethod PERCEIVE = new APIMethod() { Name = "perceive" };
        public static APIMethod UPDATE = new APIMethod() { Name = "update" };
        public static APIMethod TRANSLATE = new APIMethod() { Name = "translate" };

        public static APIMethod[] methods =  { DECIDE,EMOTIONALSTATE, TRANSLATE, PERCEIVE, UPDATE, RESET};

        public override string ToString()
        {
            return this.Name;
        }
    }
}
