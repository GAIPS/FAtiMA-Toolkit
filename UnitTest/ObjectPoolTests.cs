using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Utilities;

namespace UnitTest
{
	[TestClass]
	public class ObjectPoolTests
	{
		[TestMethod]
		public void ObjectPoolTest()
		{
			const int iterations = 100;
			const int numElements = 500;

			Stack<object> objs = new Stack<object>();

			for (int i = 0; i < iterations; i++)
			{
				for (int j = 0; j < numElements; j++)
				{
					object o = ObjectPool<object>.GetObject();
					Assert.IsNotNull(o);
					objs.Push(o);
				}

				while (objs.Count > 0)
				{
					ObjectPool<object>.Recycle(objs.Pop());
				}
			}
		}
	}
}
