namespace KnowledgeBase.WellFormedNames.Exceptions
{
	/// <summary>
	/// Base class for FAtiMA exceptions
	/// 
	/// @author Pedro Gonçalves
	/// </summary>
	public class Exception : System.Exception
	{
		public Exception() : base(string.Empty) { }

		public Exception(string message) : base(message) {}

		public Exception(string message, System.Exception innerException) : base(message,innerException) { }
	}
}
