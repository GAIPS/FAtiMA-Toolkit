using System;

namespace WellFormedNames.Exceptions
{
	public class DuplicatedKeyException : ArgumentException
	{
		public DuplicatedKeyException()
		{
		}

		public DuplicatedKeyException(string message) : base(message)
		{
		}
	}
}