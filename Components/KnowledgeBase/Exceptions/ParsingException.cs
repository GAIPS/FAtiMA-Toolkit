using System;

namespace KnowledgeBase.Exceptions
{
	public class ParsingException : Exception
	{
		public ParsingException(string msg) : base(msg) { }
	}
}
