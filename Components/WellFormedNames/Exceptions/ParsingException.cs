using System;

namespace WellFormedNames.Exceptions
{
	public class ParsingException : Exception
	{
		public ParsingException(string msg) : base(msg) { }
	}
}
