using System;
using System.Runtime.InteropServices;

namespace SerializationUtilities.Attributes
{
	[AttributeUsage(AttributeTargets.Field)]
	[ComVisible(true)]
	public sealed class NonSerializedAttribute : Attribute
	{
	}
}