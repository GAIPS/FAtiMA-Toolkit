using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using Utilities;

namespace UnitTest
{
	[TestFixture]
	public class ObjectPoolTests
	{
		[Test]
		public void ObjectPoolTest()
		{
			const int numElements = 500;

			Stack<object> objs = new Stack<object>();

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

			Assert.AreEqual(ObjectPool<object>.Count(), numElements);
		}

		[Test]
		public void ObjectPoolBenchmarkTest()
		{
			const int iterations = 500000;
			const int Elements = 100;

			var w1 = new Stopwatch();
			w1.Start();
			for (int i = 0; i < iterations; i++)
			{
				var l = new List<int>();
				for (int j = 0; j < Elements; j++)
				{
					l.Add(i);
				}
			}
			w1.Stop();
			var normalTime = w1.Elapsed;

			w1.Restart();
			for (int i = 0; i < iterations; i++)
			{
				var l = ObjectPool<List<int>>.GetObject();
				for (int j = 0; j < Elements; j++)
				{
					l.Add(i);
				}
				l.Clear();
				ObjectPool<List<int>>.Recycle(l);
			}
			w1.Stop();
			var poolTime = w1.Elapsed;

			Console.WriteLine("Normal: {0}, Pooled: {1}",normalTime,poolTime);
		}
	}
}
