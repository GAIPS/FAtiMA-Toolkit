using System;

namespace GAIPS.Serialization.Attributes
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public class DefaultSerializationSystemAttribute : Attribute
	{
		public readonly Type AssociatedType;
		public readonly bool UseInChildren;

		public DefaultSerializationSystemAttribute(Type associatedType, bool useInChildren = false)
		{
			this.AssociatedType = associatedType;
			this.UseInChildren = useInChildren;
		}
	}
}
