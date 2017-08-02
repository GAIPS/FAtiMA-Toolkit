using System;
using System.Runtime.InteropServices;

namespace SerializationUtilities.Attributes
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum, Inherited = false)]
	[ComVisible(true)]
	public sealed class SerializableAttribute : Attribute
	{
		 
	}
}