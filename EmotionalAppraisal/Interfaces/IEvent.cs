using System;
using System.Collections.Generic;
using KnowledgeBase.WellFormedNames;

namespace EmotionalAppraisal.Interfaces
{
	public interface IEvent
	{
		/// <summary>
		/// The action specified by the event
		/// </summary>
		string Action
		{
			get;
		}

		/// <summary>
		/// How performed the action
		/// </summary>
		string Subject
		{
			get;
		}

		/// <summary>
		/// The target of the event
		/// </summary>
		string Target
		{
			get;
		}

		/// <summary>
		/// The time stamp in which the event occured
		/// </summary>
		DateTime Timestamp
		{
			get;
		}

		/// <summary>
		/// The paramenters or arguments of the event
		/// </summary>
		IEnumerable<IEventParameter> Parameters
		{
			get;
		}
	}
}
