using System;
using KnowledgeBase.WellFormedNames;

namespace AutobiographicMemory.Interfaces
{
	public interface IEventParameter : ICloneable
	{
		string ParameterName
		{
			get;
		}

		Name Value
		{
			get;
		}
	}
}
