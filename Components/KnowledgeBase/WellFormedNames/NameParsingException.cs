using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WellFormedNames
{
	public class NameParsingException : Exception
	{
		public NameParsingException(string formatMsg, params object[] args) : base(string.Format(formatMsg,args)) { }
	}
}
