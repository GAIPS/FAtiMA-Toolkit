using System;
using Utilities;

namespace KnowledgeBase
{
	public static class Util
	{
		public static bool CompareObjects(object a, object b)
		{
			if (Equals(a, b))
				return true;

			if (a == null || b == null)
				return false;

			var na = a.GetType().IsNumeric();
			var nb = b.GetType().IsNumeric();
			if (na && nb)
			{
				var d = Convert.ToDecimal(a) - Convert.ToDecimal(b);
				return d == 0;
			}

			if (na || nb)
				return false;

			var ca = a as IComparable;
			if (ca != null && a.GetType() == b.GetType())
				return ca.CompareTo(b) == 0;

			// Anything else should be considered different.
			return false;
		}
	}
}