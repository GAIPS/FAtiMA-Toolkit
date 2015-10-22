using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GAIPS.Serialization.Attributes
{
	[AttributeUsage(AttributeTargets.Field,AllowMultiple = false, Inherited=false)]
	public class SerializeFieldAttribute : Attribute
	{
	}
}
