using System;

namespace EmotionalAppraisal.Interfaces
{
	public interface IEventParameter : ICloneable
	{
		string ParameterName
		{
			get;
		}

		object Value
		{
			get;
		}
	}
}
