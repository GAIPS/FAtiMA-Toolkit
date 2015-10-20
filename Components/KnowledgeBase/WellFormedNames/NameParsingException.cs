using Exception = KnowledgeBase.WellFormedNames.Exceptions.Exception;

namespace KnowledgeBase.WellFormedNames
{
	public class NameParsingException : Exception
	{
		public NameParsingException(string formatMsg, params object[] args) : base(string.Format(formatMsg,args)) { }
	}
}
