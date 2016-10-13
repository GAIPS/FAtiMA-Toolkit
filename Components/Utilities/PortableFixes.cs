using System;
using System.Reflection;

#if !PORTABLE

namespace Utilities
{
	public static class PortableFixes
	{
		public static MethodInfo GetMethodInfo(this Delegate d)
		{
			return d.Method;
		}
	}
}

#endif