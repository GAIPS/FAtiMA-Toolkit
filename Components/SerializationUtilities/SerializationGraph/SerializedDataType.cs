﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GAIPS.Serialization.SerializationGraph
{
	public enum SerializedDataType : byte
	{
		Boolean,
		Number,
		String,
		Enum,
		DataSequence,
		Object
	}
}