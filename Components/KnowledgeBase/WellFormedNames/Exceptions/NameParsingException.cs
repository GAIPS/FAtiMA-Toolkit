namespace KnowledgeBase.WellFormedNames.Exceptions
{
	public class NameParsingException : Exception
	{
		public NameParsingException(string formatMsg, params object[] args) : base(string.Format(formatMsg,args)) { }
	}
}
