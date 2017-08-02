using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using Utilities;


namespace Tests.Utilities
{
	[TestFixture]
	public class ObjectPoolTests
	{
		const int Elements = 100;

		private struct Performance
		{
			public string MethodName { get; }
			public TimeSpan ExecutionTime { get; }
			public long MemoryAllocated { get; }

			public Performance(string name, TimeSpan time, long mem)
			{
				MethodName = name;
				ExecutionTime = time;
				MemoryAllocated = mem;
			}
		}
		private struct PerformanceDiff
		{
			public readonly Performance p1;
			public readonly Performance p2;

			public double TimePercentage
			{
				get
				{
					return p1.ExecutionTime.Ticks / (double)p2.ExecutionTime.Ticks - 1d;
				}
			}

			public double MemoryPercentage
			{
				get { return p1.MemoryAllocated/(double) p2.MemoryAllocated - 1d; }
			}

			public PerformanceDiff(Performance p1, Performance p2)
			{
				this.p1 = p1;
				this.p2 = p2;
			}

			public void Print()
			{
				Console.WriteLine($"Execution Time\n{p1.MethodName}: {p1.ExecutionTime}, {p2.MethodName}: {p2.ExecutionTime} (gain: {TimePercentage:P})");
				Console.WriteLine($"Allocated Memory\n{p1.MethodName}: {p1.MemoryAllocated}, {p2.MethodName}: {p2.MemoryAllocated} (gain: {MemoryPercentage:P})");
			}
		}

		[Test]
		public void ObjectPoolBenchmarkTest()
		{
			var iterrations = new[] {10,25, 100, 5000, 10000, 50000, 100000, 1000000, 5000000};

			var results =
				iterrations.Select(
						i =>
							new
							{
								i,
								diff = new PerformanceDiff(BatchTests(i, BaseCase), BatchTests(i, Pooled_TestCase,Pooled_TestCase_Reset))
							})
					.ToArray();

			foreach (var r in results)
			{
				Console.WriteLine($"{r.i} Iterations:");
				r.diff.Print();
				Console.WriteLine("-----------------------------------------------------------------------");
			}

			var weigthedExec = results.Select(r => new {w = r.i, v = r.diff.TimePercentage*r.i}).Aggregate((v1,v2)=>new {w = v1.w + v2.w, v = v1.v+v2.v});
			var weigthedMem = results.Select(r => new { w = r.i, v = r.diff.MemoryPercentage * r.i }).Aggregate((v1, v2) => new { w = v1.w + v2.w, v = v1.v + v2.v });

			Console.WriteLine();
			Console.WriteLine($"Avg. Execution Time Gain: {results.Select(r => r.diff.TimePercentage).Average():P}");
			Console.WriteLine($"Avg. Weigthed Execution Time Gain: {weigthedExec.v/weigthedExec.w:P}");
			Console.WriteLine($"Avg. Memory Allocation Gain: {results.Select(r => r.diff.MemoryPercentage).Average():P}");
			Console.WriteLine($"Avg. Weigthed Memory Allocation Gain: {weigthedMem.v / weigthedMem.w:P}");
		}

		private Performance BatchTests(int numOfIterations, Action testMethod,Action postTestRun=null)
		{
			var w1 = new Stopwatch();
			var before = GC.GetTotalMemory(true);
			w1.Start();
			for (int i = 0; i < numOfIterations; i++)
				testMethod();
			w1.Stop();
			var after = GC.GetTotalMemory(false);

			postTestRun?.Invoke();

			var diff = after - before;
			return new Performance(testMethod.Method.Name, w1.Elapsed, diff);
		}

		private void BaseCase()
		{
			var l = new List<int>();
			for (int j = 0; j < Elements; j++)
			{
				l.Add(j);
			}
		}

		private void Pooled_TestCase()
		{
			var l = ObjectPool<List<int>>.GetObject();
			for (int j = 0; j < Elements; j++)
			{
				l.Add(j);
			}
			l.Clear();
			ObjectPool<List<int>>.Recycle(l);
		}

		private void Pooled_TestCase_Reset()
		{
			ObjectPool<List<int>>.DropPool();
		}
	}
}
