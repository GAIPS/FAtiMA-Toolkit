namespace KnowledgeBase.WellFormedNames.Exceptions
{
	public class InvalidSymbolDefinitionException : NameParsingException
	{
		public InvalidSymbolDefinitionException(string name) : base(name+" is not a well formated name definition.")
		{
		}
	}
}
