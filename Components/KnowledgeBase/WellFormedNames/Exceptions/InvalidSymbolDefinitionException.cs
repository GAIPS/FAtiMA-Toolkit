namespace KnowledgeBase.WellFormedNames.Exceptions
{
	public class InvalidSymbolDefinitionException : Exception
	{
		public InvalidSymbolDefinitionException(string name) : base(name+" is not a well formated name definition.")
		{
		}
	}
}
