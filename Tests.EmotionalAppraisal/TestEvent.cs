using System;
using System.Collections.Generic;
using EmotionalAppraisal.Interfaces;

namespace Tests.EmotionalAppraisal
{
	public class TestEvent : IEvent
	{
		public string Action { get; set; }

		public IEnumerable<IEventParameter> Parameters { get; set; }

		public string Subject { get; set; }

		public string Target { get; set; }

		public DateTime Timestamp { get; private set; }

		public TestEvent(string subject, string action, string target)
		{
			Subject = subject;
			Action = action;
			Target = target;
			Timestamp = DateTime.UtcNow;
		}
	}

	public class EventParameter : IEventParameter
	{

		public string ParameterName { get; set; }

		public object Value { get; set; }

		public object Clone()
		{
			return new EventParameter() { ParameterName = this.ParameterName, Value = this.Value };
		}
	}
}
