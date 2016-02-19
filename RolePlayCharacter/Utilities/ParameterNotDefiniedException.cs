using System;


namespace RolePlayCharacter.Utilities
{
    public class ParameterNotDefiniedException : Exception
    {
        private string _exceptionMessage;

        public string ExceptionMessage
        {
            get { return _exceptionMessage; }
            set { _exceptionMessage = value; }
        }

        public ParameterNotDefiniedException()
        { }

        public ParameterNotDefiniedException(string message) : base(message)
        {
            _exceptionMessage = message;
        }

        public ParameterNotDefiniedException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
