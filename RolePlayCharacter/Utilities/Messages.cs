using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
