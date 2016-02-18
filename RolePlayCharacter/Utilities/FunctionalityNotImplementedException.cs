using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolePlayCharacter.Utilities
{
    public class FunctionalityNotImplementedException :Exception
    {
        private string _exceptionMessage;


        public string ExceptionMessage
        {
            get { return _exceptionMessage; }
            set { _exceptionMessage = value; }
        }

        public FunctionalityNotImplementedException()
        { }

        public FunctionalityNotImplementedException(string message) : base(message)
        {
            _exceptionMessage = message;
        }

        public FunctionalityNotImplementedException(string message, Exception exception) : base(message, exception)
        {

        }
    } 
}
