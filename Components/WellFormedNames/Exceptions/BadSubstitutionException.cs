using System;

namespace WellFormedNames.Exceptions
{
	public class BadSubstitutionException : Exception
	{
		public BadSubstitutionException(string msg) : base(msg) { }

		public BadSubstitutionException(string msg, Exception innerException) : base(msg,innerException) { }
	}
}
