using System;
using KnowledgeBase.WellFormedNames;
using NUnit.Framework;
using KnowledgeBase.WellFormedNames.Collections;
using System.Collections.Generic;
using System.Linq;
using Utilities;

using Tup = Utilities.Tuple;

namespace Tests.KnowledgeBase.WellFormedNames
{
    [TestFixture]
    public class NameSearchTreeTests {

        [Test]
        public void Depth_EmptyNameSearchTree_0()
        {
            var tree = new NameSearchTree<Symbol>();
            Assert.That(tree.Depth == 0);
        }

        [TestCase("x","1")]
        [TestCase("x(a)", "2")]
        [TestCase("x(a, b)", "3")]
        public void Add_EmptyNameSearchTree_True(string name, string value)
        {
            var tree = new NameSearchTree<string>();
			tree.Add(Name.Parse(name), value);
        }

        [TestCase("x", "1")]
        [TestCase("x(a)", "2")]
        [TestCase("x(a, b)", "3")]
		[ExpectedException(typeof(Exception))]
        public void Add_FilledNameSearchTree_False(string name, string value)
        {
            var tree = new NameSearchTree<string>();
            tree.Add(Name.Parse(name), value);
			tree.Add(Name.Parse(name), String.Empty);
        }

        [TestCase("x", "1")]
        [TestCase("x(a)", "2")]
        [TestCase("x(a, b)", "3")]
        public void Remove_EmptyNameSearchTree_False(string name, string value)
        {
            var tree = new NameSearchTree<string>();
            var removeSuccess = tree.Remove(Name.Parse(name));
            Assert.That(!removeSuccess);
        }

        [TestCase("x", "1")]
        [TestCase("x(a)", "2")]
        [TestCase("x(a, b)", "3")]
        public void Remove_NameSearchTreeThatContainsName_True(string name, string value)
        {
            var tree = new NameSearchTree<string>();
            tree.Add(Name.Parse(name), value);
            var removeSuccess = tree.Remove(Name.Parse(name));
            Assert.That(removeSuccess);
        }

        [TestCase("x", "1")]
        [TestCase("x(a)", "2")]
        [TestCase("x(a, b)", "3")]
        public void Contains_NameSearchTreeThatContainsName_True(string name, string value)
        {
            var tree = new NameSearchTree<string>();
            tree.Add(Name.Parse(name), value);
            Assert.That(tree.ContainsKey(Name.Parse(name)));
        }

        [TestCase("x", "1")]
        [TestCase("x(a)", "2")]
        [TestCase("x(a, b)", "3")]
        public void Contains_EmptySearchTree_False(string name, string value)
        {
            var tree = new NameSearchTree<string>();
            Assert.That(!tree.ContainsKey(Name.Parse(name)));
        }

        [TestCase("x", "1")]
        [TestCase("x(a)", "2")]
        [TestCase("x(a, b)", "3")]
        public void TryMatchValue_EmptySearchTree_False(string nameStr, string value)
        {
            var tree = new NameSearchTree<string>();
            var name = Name.Parse(nameStr);
            string res;
            Assert.That(!tree.TryMatchValue(name, out res));
            Assert.That(tree[name] == null);
        }

        [TestCase("x", "1")]
        [TestCase("x(a)", "2")]
        [TestCase("x(a, b)", "3")]
        public void TryMatchValue_SearchTreeThatContainsName_True(string nameStr, string value)
        {
            var tree = new NameSearchTree<string>();
            var name = Name.Parse(nameStr);
            tree.Add(name, value);
            string res;
            Assert.That(tree.TryMatchValue(name, out res));
            Assert.That(res == value);
            Assert.That(tree[name] == value);
        }

		private class TestFactory
		{
			private static string[] inputStrings = new string[]
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
				"A(D(E,B(O)),K)",
				"Event(Mary,Kiss,John)",
				"Event([z],Kiss,Justin)",
				"Event([x],Punch,[y])",
				"Event([x],Punch,Self)",
				"Jump(null,null)",
				"Jump([width],null)",
				"Jump(null,[height])",
				"Jump([width],[height])",
				"Jump(Short,null)",
				"Jump(Medium,null)",
				"Jump(Long,null)",
				"Jump(null,Short)",
				"Jump(null,Medium)",
				"Jump(null,LONG)",
				"Jump(Short,Short)",
				"Jump(MEDIUM,Short)",
				"Jump(LonG,Short)",
				"Jump(Short,Medium)",
				"Jump(MEDIUM,Medium)",
				"Jump(LonG,Medium)",
				"Jump(Short,LONG)",
				"Jump(MEDIUM,long)",
				"Jump(LonG,lONg)"
			};

			private static readonly NameSearchTree<int> baseInput = BuildInput();

			private static NameSearchTree<int> BuildInput()
			{
				NameSearchTree<int> dict = new NameSearchTree<int>();
				for (int i = 0; i < inputStrings.Length; i++)
				{
					dict.Add(Name.Parse(inputStrings[i]), i);
				}
				return dict;
			}

			private static SubstitutionSet BuildSet(params string[] str)
			{
				return new SubstitutionSet(str.Select(s => new Substitution(s)));
			}

			private static IEnumerable<SubstitutionSet> BuildResult(params string[][] str)
			{
				return new HashSet<SubstitutionSet>(str.Select(set => new SubstitutionSet(set.Select(s => new Substitution(s)))));
			}

			private static IEnumerable<Pair<int, SubstitutionSet>> BuildUnifyResult(
				params Pair<int, string[]>[] set)
			{
				return set.Select(p => Tup.Create(p.Item1, BuildSet(p.Item2)));
			}

			public static IEnumerable<TestCaseData> TestBindingCases()
			{
				yield return new TestCaseData(baseInput, (Name)"Luke([x])",null, BuildResult(new[] { new[] { "[x]/Name" }, new[] { "[x]/Strength" } }));
				yield return new TestCaseData(baseInput, (Name)"[x](Strength)", null, BuildResult(new[] { new[] { "[x]/John" }, new[] { "[x]/Luke" } }));
				yield return new TestCaseData(baseInput, (Name)"[x]([y])", null, BuildResult(new[]
				{
					new[] { "[x]/John", "[y]/Name" }, 
					new[] { "[x]/John", "[y]/Strength" },
					new[] { "[x]/Luke", "[y]/Name" }, 
					new[] { "[x]/Luke", "[y]/Strength" },
				}));
				yield return new TestCaseData(baseInput, (Name)"John(Name)", null, BuildResult());
				yield return new TestCaseData(baseInput, (Name)"John(Height)", null, null);
				yield return new TestCaseData(baseInput, (Name)"Paul([x])", null, null);
				yield return new TestCaseData(baseInput, (Name)"A([x],K)", null, BuildResult(new[]
				{
					new[] { "[x]/I" },
					new[] { "[x]/D(E,H)" },
					new[] { "[x]/D(E,B(O))" }
				}));
				yield return new TestCaseData(baseInput, (Name)"A(D(e,[X]),[y])", null, BuildResult(new[]
				{
					new[] { "[x]/H","[y]/K" },
					new[] { "[x]/H","[y]/J" },
					new[] { "[x]/B(o)","[y]/K" },
				}));

				yield return new TestCaseData(baseInput, (Name)"EVENT(Mary,Kiss,Justin)", null, BuildResult(new[]
				{
					new[] { "[z]/Mary"}
				}));
				yield return new TestCaseData(baseInput, (Name)"EVENT(Mary,Kiss,[y])", null, BuildResult(new[]
				{
					new[] { "[z]/Mary","[y]/Justin"},
					new[] { "[y]/John"},
				}));
				yield return new TestCaseData(baseInput, (Name)"jump(null,[meh])", null, BuildResult(new[]
				{
					new[] { "[meh]/NULL"},
					new[] { "[meh]/Short"},
					new[] { "[meh]/medium"},
					new[] { "[meh]/long"},
					new[] { "[meh]/[height]"},
					new[] { "[width]/null","[meh]/null"},
					new[] { "[width]/null","[meh]/[height]"},
				}));
				yield return new TestCaseData(baseInput, (Name)"jump(3,67)", null, BuildResult(new[]
				{
					new[] { "[width]/3", "[height]/67"}
				}));

				yield return new TestCaseData(baseInput, (Name)"jump([x],[y])", null, BuildResult(new[]
				{
					new[] { "[x]/null", "[y]/null"},
					new[] { "[x]/[width]", "[y]/null"},
					new[] { "[x]/null", "[y]/[height]"},
					new[] { "[x]/[width]", "[y]/[height]"},
					new[] { "[x]/Short", "[y]/null"},
					new[] { "[x]/Medium", "[y]/null"},
					new[] { "[x]/Long", "[y]/null"},
					new[] { "[x]/null", "[y]/short"},
					new[] { "[x]/null", "[y]/medium"},
					new[] { "[x]/null", "[y]/long"},
					new[] { "[x]/Short", "[y]/short"},
					new[] { "[x]/Medium", "[y]/short"},
					new[] { "[x]/long", "[y]/short"},
					new[] { "[x]/Short", "[y]/medium"},
					new[] { "[x]/Medium", "[y]/medium"},
					new[] { "[x]/long", "[y]/medium"},
					new[] { "[x]/Short", "[y]/long"},
					new[] { "[x]/Medium", "[y]/long"},
					new[] { "[x]/long", "[y]/long"},
				}));

				yield return new TestCaseData(baseInput, (Name)"jump([x],[y])", BuildSet("[y]/SHORT"), BuildResult(new[]
				{
					new[] { "[x]/null", "[y]/short"},
					new[] { "[x]/Short", "[y]/short"},
					new[] { "[x]/Medium", "[y]/short"},
					new[] { "[x]/long", "[y]/short"}
				}));
			}

			public static IEnumerable<TestCaseData> TestMatchAllCases()
			{
				yield return new TestCaseData(baseInput,(Name)"Luke(*)",new []{0,1});
				yield return new TestCaseData(baseInput, (Name)"*(Name)", new[] { 0, 2 });
				yield return new TestCaseData(baseInput, (Name)"*(*)", new[] { 0, 1, 2, 3});
				yield return new TestCaseData(baseInput, (Name)"A(*,K)", new[] { 6,7,9 });
				yield return new TestCaseData(baseInput, (Name)"A(D(E,*),*)", new[] { 6, 8, 9 });
				yield return new TestCaseData(baseInput, (Name)"A(D(E,H),*)", new[] { 6, 8});
				yield return new TestCaseData(baseInput, (Name)"A(D(E,H),W)", new int[0]);
				//yield return new TestCaseData(baseInput, (Name)"Event(Mary,Kiss)", new[] { 10 , 11});
				//yield return new TestCaseData(baseInput, (Name)"Event(Mary,Punch)", new int[0]);
				//yield return new TestCaseData(baseInput, (Name)"Event(Mary,Punch,John)", new []{12});
				//yield return new TestCaseData(baseInput, (Name)"Event(Mary,Punch,[y])", new[] { 12 });
			}

			public static IEnumerable<TestCaseData> TestUnifyCases()
			{

				yield return new TestCaseData(baseInput, (Name)"Luke([x])", new SubstitutionSet(), BuildUnifyResult(Tup.Create(0, new[] { "[x]/Name" }),
					Tup.Create(1, new[] { "[x]/Strength" })));
			}

			//public static IEnumerable<TestCaseData> TestMatchCases()
			//{
			//	yield return new TestCaseData(baseInput, (Name)"Event(Mary,Kiss)", 10);
			//	yield return new TestCaseData(baseInput, (Name)"Event(Mary,Punch, John)", 12);
			//	yield return new TestCaseData(baseInput, (Name)"Event(Mary,Punch)", 0);
			//	yield return new TestCaseData(baseInput, (Name)"Event([x],Punch,Mary)", new[] { 12 });
			//}
		}

		[TestCaseSource(typeof(TestFactory), "TestBindingCases")]
		public void NameDictionary_Valid_Binds(NameSearchTree<int> dict, Name binder,SubstitutionSet constraints, HashSet<SubstitutionSet> expectedResults)
		{
			var results = dict.GetPosibleBindings(binder,constraints);
			if (results == null)
			{
				if (expectedResults == null)
					return;
				Assert.Fail("Binder failed to find matches");
			}

			if (results.Count() != expectedResults.Count)
				Assert.Fail("Binder returned a different number of binds from the expected");
			if (!new HashSet<SubstitutionSet>(results).SetEquals(expectedResults))
				Assert.Fail("Binder didn't returned the expected substitutions.");
		}

		[TestCaseSource(typeof(TestFactory), "TestMatchAllCases")]
		public void NameDictionary_Valid_MatcheAll(NameSearchTree<int> dict, Name expression, IEnumerable<int> expectedResults)
	    {
			var result = dict.MatchAll(expression).ToArray();
			if (result.Length != result.Distinct().Count())
			{
				Assert.Fail("Match All Produced more results that the ones expected");
			}

			if(!new HashSet<int>(expectedResults).SetEquals(result))
				Assert.Fail("Matcher didn't returned the expected results.");
	    }

		//[TestCaseSource(typeof(TestFactory), "TestUnifyCases")]
		//public void NameDictionary_Valid_Unify(NameSearchTree<int> dict, Name expression, SubstitutionSet bindings,
		//	IEnumerable<Pair<int, SubstitutionSet>> expectedResults)
		//{
		//	var result = dict.Unify(expression, bindings);
		//	var resultDict = result.ToDictionary(p => p.Item1, p => p.Item2);
		//	var expectDict = expectedResults.ToDictionary(p => p.Item1, p => p.Item2);

		//	Assert.AreEqual(expectDict.Count, resultDict.Count,"Number of results mismatch");

		//	foreach (var pair in resultDict)
		//	{
		//		SubstitutionSet t;
		//		Assert.That(expectDict.TryGetValue(pair.Key,out t),"Unable to find entry");
		//		Assert.AreEqual(t, pair.Value, "Binding list is not equal to the expected");
		//	}
		//}
    }
}