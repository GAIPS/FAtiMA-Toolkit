using System;

namespace KnowledgeBase.Exceptions
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