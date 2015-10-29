using KnowledgeBase.WellFormedNames;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using KnowledgeBase.WellFormedNames.Collections;

namespace Tests.KnowledgeBase.WellFormedNames
{
	[TestFixture]
	public class NameDictionaryTests
	{
		private class TestFactory
		{
			private static HashSet<SubstitutionSet> BuildResult(params string[][] str)
			{
				return new HashSet<SubstitutionSet>(str.Select(set => new SubstitutionSet(set.Select(s => new Substitution(s)))));
			}

			public static IEnumerable<TestCaseData> TestCases()
			{
				var baseInput = new string[]
				{
					"Luke(Name)",
					"Luke(Strength)",
					"John(Name)",
					"John(Strength)",
					"A(B,C)",
					"A(C,D)",
					"A(D(E,H),K)",
					"A(I,K)",
					"A(D(E,H),J)",
					"A(D(E,B(O)),K)"
				}.Select(s => (Name)s).ToArray();
				yield return new TestCaseData(baseInput, (Name)"Luke([x])", BuildResult(new[] { new[] { "[x]/Name" }, new[] { "[x]/Strength" } }));
				yield return new TestCaseData(baseInput, (Name)"[x](Strength)", BuildResult(new[] { new[] { "[x]/John" }, new[] { "[x]/Luke" } }));
				yield return new TestCaseData(baseInput, (Name)"[x]([y])", BuildResult(new[]
				{
					new[] { "[x]/John", "[y]/Name" }, 
					new[] { "[x]/John", "[y]/Strength" },
					new[] { "[x]/Luke", "[y]/Name" }, 
					new[] { "[x]/Luke", "[y]/Strength" },
				}));
				yield return new TestCaseData(baseInput, (Name)"John(Name)",BuildResult());
				yield return new TestCaseData(baseInput, (Name)"John(Height)",null);
				yield return new TestCaseData(baseInput, (Name)"Paul([x])",null);
				yield return new TestCaseData(baseInput, (Name)"A([x],K)", BuildResult(new[]
				{
					new[] { "[x]/I" },
					new[] { "[x]/D(E,H)" },
					new[] { "[x]/D(E,B(O))" }
				}));
				yield return new TestCaseData(baseInput, (Name)"A(D(e,[X]),[y])", BuildResult(new[]
				{
					new[] { "[x]/H","[y]/K" },
					new[] { "[x]/H","[y]/J" },
					new[] { "[x]/B(o)","[y]/K" },
				}));
			}
		}

		[TestCaseSource(typeof(TestFactory),"TestCases")]
		public void NameDictionary_Valid_Binds(Name[] names, Name binder, HashSet<SubstitutionSet> expectedResults)
		{
			NameSearchTree<int> dict = new NameSearchTree<int>();
			for (int i = 0; i < names.Length; i++)
			{
				dict.Add(names[i], i);
			}

			var results = dict.GetPosibleBindings(binder);
			if (results == null)
			{
				if(expectedResults==null)
					return;
				Assert.Fail("Binder failed to find matches");
			}
				
			if(results.Count() != expectedResults.Count)
				Assert.Fail("Binder returned a different number of binds from the expected");
			if (!new HashSet<SubstitutionSet>(results).SetEquals(expectedResults))
				Assert.Fail("Binder didn't returned the expected substitutions.");
		}
	}
}