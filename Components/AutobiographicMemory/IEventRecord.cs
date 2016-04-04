using System;
using System.Collections.Generic;
using KnowledgeBase.WellFormedNames;

namespace AutobiographicMemory
{
	public interface IEventRecord
	{
		uint Id { get; }

        /// <summary>
        /// The WFN representation of this event record
        /// </summary>
        Name EventName { get; }

        /// <summary>
        /// The type of the event
        /// </summary>
        string Type
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
		/// The action of the event
		/// </summary>
		Name Action
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
		/// The property of the event
		/// </summary>
		Name Property
        {
            get;
        }


        /// <summary>
		/// The new value of the property changed
		/// </summary>
		string NewValue
        {
            get;
        }

        
	    /// <summary>
		/// The time stamp in which the event occured
		/// </summary>
		ulong Timestamp
		{
			get;
		}

        
        //EventDTO ToDto();
        IEnumerable<string> LinkedEmotions { get; }
		void LinkEmotion(string emotionType);
	}
}