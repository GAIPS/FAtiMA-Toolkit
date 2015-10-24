using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GAIPS.Serialization.SerializationGraph
{
	public sealed class TypeEntry
	{
		public byte TypeId
		{
			get;
			private set;
		}

		public Type ClassType
		{
			get;
			private set;
		}

		internal TypeEntry(Type type, byte typeId)
		{
			this.ClassType = type;
			this.TypeId = typeId;
		}
	}
}
