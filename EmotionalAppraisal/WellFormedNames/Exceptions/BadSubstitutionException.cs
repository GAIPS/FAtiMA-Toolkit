namespace WellFormedNames
{
	public class BadSubstitutionException : Exception
	{
		public BadSubstitutionException(string msg) : base(msg) { }

		public BadSubstitutionException(string msg, System.Exception innerException) : base(msg,innerException) { }
	}
}
