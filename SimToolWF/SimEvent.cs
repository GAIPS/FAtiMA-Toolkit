using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutobiographicMemory;
using AutobiographicMemory.Interfaces;
using KnowledgeBase.WellFormedNames;

namespace SimToolWF
{
    class SimEvent : IEvent
    {
        private string _action;

        private string _subject;

        private string _target;

        private IEnumerable<IEventParameter> _parameters;

        private DateTime _timestamp;

        public string Action
        {
            get { return _action; }

            set { _action = value; }
        }

        public string Subject
        {
            get { return _subject; }

            set { _subject = value; }
        }

        public string Target
        {
            get { return _target; }

            set { _target = value; }
        }

        public IEnumerable<IEventParameter> Parameters
        {
            get { return _parameters; }

            set { this._parameters = value; }
        }

        public DateTime Timestamp
        {
            get { return _timestamp; }

            set { _timestamp = value; }
        }

        public SimEvent (string subject, string action, string target, params IEventParameter[] parameters)
        {
            _action = action;
            _subject = subject;
            _target = target;
            _parameters = parameters;
            _timestamp = DateTime.UtcNow;
        }

        public class SimEventParameter : IEventParameter
        {
            private string _parameterName;

            private Name _value;

            public string ParameterName
            {
                get { return _parameterName; }

                set { _parameterName = value; }
            }

            public Name Value
            {
                get { return _value; }

                set { _value = value; } }

            public object Clone()
            {
                return new SimEventParameter() { ParameterName = _parameterName, Value = _value };
            }
        }
    }
}
