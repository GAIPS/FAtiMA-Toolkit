using System;

namespace KnowledgeBase.Exceptions
{
	public class ParsingException : Exception
	{
		public ParsingException(string msg) : base(msg) { }
		public ParsingException(string formatMsg, params object[] args) : base(string.Format(formatMsg,args)) { }
	}
}
