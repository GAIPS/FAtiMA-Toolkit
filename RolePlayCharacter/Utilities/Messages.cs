namespace RolePlayCharacter.Utilities
{
    public  class Messages
    {
        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public string ShowMessage()
        {
            return Message;
        }

        public Messages EMOTIONALAPPRAISALNOTIMPLEMENTED()
        {
            _message = "Emotional Appraisal Asset Not Implemented";
            return this;
        }

        public Messages EMOTIONDECISIONNOTIMPLEMENTED()
        {
            _message = "Emotional Decision Making Asset Not Implemented";
            return this;
        }

        public Messages FILEEXTENSIONNOTDEFINED()
        {
            _message = "File Extension of File To Load Not Defined";
            return this;
        }

        public Messages FILENAMENOTDEFINED()
        {
            _message = "Filename Not Defined";
            return this;
        }

        public Messages LOADTYPEINCORRECT()
        {
            _message = "Type of Loading Incorrect";
            return this;
        }
    }
}
