using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Utilities;
using WellFormedNames;
using WellFormedNames.Collections;
using WellFormedNames.Exceptions;

namespace Tests.WellFormedNames
{
    [TestFixture]
    public class NameSearchTreeTests {

        [Test]
        public void Depth_EmptyNameSearchTree_0()
        {
            var tree = new NameSearchTree<string>();
            Assert.That(tree.Depth == 0);
        }

        [TestCase("x","1")]
        [TestCase("x(a)", "2")]
        [TestCase("x(a, b)", "3")]
        public void Add_EmptyNameSearchTree_True(string name, string value)
        {
            var tree = new NameSearchTree<string>();
			tree.Add(Name.BuildName(name), value);
        }

        [TestCase("x", "1")]
        [TestCase("x(a)", "2")]
        [TestCase("x(a, b)", "3")]
        public void Add_FilledNameSearchTree_False(string name, string value)
        {
            var tree = new NameSearchTree<string>();
	        Assert.Throws<DuplicatedKeyException>(() =>
	        {
				tree.Add(Name.BuildName(name), value);
				tree.Add(Name.BuildName(name), String.Empty);
			});
        }

        [TestCase("x", "1")]
        [TestCase("x(a)", "2")]
        [TestCase("x(a, b)", "3")]
        public void Remove_EmptyNameSearchTree_False(string name, string value)
        {
            var tree = new NameSearchTree<string>();
            var removeSuccess = tree.Remove(Name.BuildName(name));
            Assert.That(!removeSuccess);
        }

        [TestCase("x", "1")]
        [TestCase("x(a)", "2")]
        [TestCase("x(a, b)", "3")]
        public void Remove_NameSearchTreeThatContainsName_True(string name, string value)
        {
            var tree = new NameSearchTree<string>();
            tree.Add(Name.BuildName(name), value);
            var removeSuccess = tree.Remove(Name.BuildName(name));
            Assert.That(removeSuccess);
        }

        [TestCase("x", "1")]
        [TestCase("x(a)", "2")]
        [TestCase("x(a, b)", "3")]
        public void Contains_NameSearchTreeThatContainsName_True(string name, string value)
        {
            var tree = new NameSearchTree<string>();
            tree.Add(Name.BuildName(name), value);
            Assert.That(tree.ContainsKey(Name.BuildName(name)));
        }

        [TestCase("x", "1")]
        [TestCase("x(a)", "2")]
        [TestCase("x(a, b)", "3")]
        public void Contains_EmptySearchTree_False(string name, string value)
        {
            var tree = new NameSearchTree<string>();
            Assert.That(!tree.ContainsKey(Name.BuildName(name)));
        }
		/*
        [TestCase("x", "1")]
        [TestCase("x(a)", "2")]
        [TestCase("x(a, b)", "3")]
        public void TryMatchValue_EmptySearchTree_False(string nameStr, string value)
        {
            var tree = new NameSearchTree<string>();
            var name = Name.BuildName(nameStr);
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
            var name = Name.BuildName(nameStr);
            tree.Add(name, value);
            string res;
            Assert.That(tree.TryMatchValue(name, out res));
            Assert.That(res == value);
            Assert.That(tree[name] == value);
        }
		*/
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
					dict.Add(Name.BuildName(inputStrings[i]), i);
				}
				return dict;
			}

			private static SubstitutionSet BuildSet(params string[] str)
			{
				return new SubstitutionSet(str.Select(s => new Substitution(s)));
			}

			private static IEnumerable<Pair<int, SubstitutionSet>> BuildUnifyResult(
				params Pair<int, string[]>[] set)
			{
				return set.Select(p => Tuples.Create(p.Item1, BuildSet(p.Item2)));
			}

			public static IEnumerable<TestCaseData> TestMatchAllCases_Valid()
			{
				yield return new TestCaseData(baseInput, (Name)"Luke(*)", new []{0,1});
				yield return new TestCaseData(baseInput, (Name) "*(Strength)", new[] {1, 3});
				yield return new TestCaseData(baseInput, (Name) "*(*)", new[] {0, 1, 2, 3});

				yield return new TestCaseData(baseInput, (Name) "A(D(E,B(O)),K)", new[] {9});
				yield return new TestCaseData(baseInput, (Name) "A(*,K)", new[] {7, 6, 9});

				yield return new TestCaseData(baseInput, (Name) "A(D(e,*),*)", new[] {6,8,9});

				yield return new TestCaseData(baseInput, (Name)"EVENT(Mary,Kiss,*)", new []{10});

				yield return new TestCaseData(baseInput, (Name) "jump(null,[height])", new[] {16});
				yield return new TestCaseData(baseInput, (Name) "jump(*,[height])", new[] {16, 17});

				//yield return new TestCaseData(baseInput, (Name)"EVENT(Mary,Kiss,Justin)", null,
				//	BuildUnifyResult(
				//		Tup.Create(11, new[] { "[z]/Mary" })
				//	)
				//);

				//yield return new TestCaseData(baseInput, (Name)"EVENT(Batman,Punch,Jocker)", null,
				//	BuildUnifyResult(
				//		Tup.Create(12, new[] { "[x]/Batman", "[y]/Jocker" })
				//	)
				//);

				//yield return new TestCaseData(baseInput, (Name)"EVENT(Batman,Punch,Self)", null,
				//	BuildUnifyResult(
				//		Tup.Create(12, new[] { "[x]/Batman", "[y]/Self" }),
				//		Tup.Create(13, new[] { "[x]/Batman" })
				//	)
				//);

				//yield return new TestCaseData(baseInput, (Name)"jump(3,36)", null,
				//	BuildUnifyResult(
				//		Tup.Create(17, new[] { "[width]/3", "[height]/36" })
				//	)
				//);
			}

			public static IEnumerable<TestCaseData> TestMatchAllCases_Invalid()
			{
				yield return new TestCaseData(baseInput, (Name)"EVENT(Mary,Kiss,Justin)");
				yield return new TestCaseData(baseInput, (Name) "EVENT(Batman,Punch,Jocker)");
				yield return new TestCaseData(baseInput, (Name)"EVENT(Batman,Punch,Self)");
				yield return new TestCaseData(baseInput, (Name) "jump(3,36)");
			}

			public static IEnumerable<TestCaseData> TestUnifyCases_Valid()
			{
				yield return new TestCaseData(baseInput, (Name)"Luke([x])", null,
					BuildUnifyResult(
						Tuples.Create(0, new[] { "[x]/Name" }),
						Tuples.Create(1, new[] { "[x]/Strength" })
					)
				);
				yield return new TestCaseData(baseInput, (Name)"Luke([x])",
					BuildSet(
						"[x]/Name"
					),
					BuildUnifyResult(
						Tuples.Create(0, new[] { "[x]/Name" })
					)
				);

				yield return new TestCaseData(baseInput, (Name)"[x](Strength)", null,
					BuildUnifyResult(
						Tuples.Create(1,new []{"[x]/Luke"}),
						Tuples.Create(3,new []{"[x]/John"})
					)
				);

				yield return new TestCaseData(baseInput, (Name)"[x]([y])", null, 
					BuildUnifyResult(
						Tuples.Create(0,new []{"[x]/Luke", "[y]/Name"}),
						Tuples.Create(1,new []{"[x]/Luke", "[y]/Strength"}),
						Tuples.Create(2,new []{"[x]/John", "[y]/Name"}),
						Tuples.Create(3,new []{"[x]/John", "[y]/Strength"})
					)
				);

				yield return new TestCaseData(baseInput, (Name)"A(D(E,B(O)),K)", null,
					BuildUnifyResult( Tuples.Create(9,new string[0]))
				);

				yield return new TestCaseData(baseInput, (Name)"A([x],K)", null, 
					BuildUnifyResult(
						Tuples.Create(7, new []{"[x]/I"}),
						Tuples.Create(6, new []{"[x]/D(E,H)"}),
						Tuples.Create(9, new []{"[x]/D(E,B(O))"})
					)
				);

				yield return new TestCaseData(baseInput, (Name)"A(D(e,[X]),[y])", null, 
					BuildUnifyResult(
						Tuples.Create(6,new[] { "[x]/H","[y]/K" }),
						Tuples.Create(8,new[] { "[x]/H","[y]/j" }),
						Tuples.Create(9,new[] { "[x]/B(o)","[y]/K" })
					)
				);

				yield return new TestCaseData(baseInput, (Name)"EVENT(Mary,Kiss,Justin)", null, 
					BuildUnifyResult(
						Tuples.Create(11,new[] { "[z]/Mary"})
					)
				);

				yield return new TestCaseData(baseInput, (Name)"EVENT(Mary,Kiss,[y])", null,
					BuildUnifyResult(
						Tuples.Create(11, new[] { "[z]/Mary", "[y]/Justin" }),
						Tuples.Create(10, new[] { "[y]/John" })
					)
				);

				yield return new TestCaseData(baseInput, (Name)"EVENT(Batman,Punch,Jocker)", null,
					BuildUnifyResult(
						Tuples.Create(12, new[] { "[x]/Batman", "[y]/Jocker" })
					)
				);

				yield return new TestCaseData(baseInput, (Name)"EVENT(Batman,Punch,Self)", null,
					BuildUnifyResult(
						Tuples.Create(12, new[] { "[x]/Batman", "[y]/Self" }),
						Tuples.Create(13, new[] { "[x]/Batman"})
					)
				);

				//TODO not working for now. Need to improve SubstitutionSet
				//yield return new TestCaseData(baseInput, (Name)"EVENT(Self,Punch,Self)", new SubstitutionSet(new Inequality("[x]!=[y]")),
				//	BuildUnifyResult(
				//		Tup.Create(13, new[] { "[x]/Self" })
				//	)
				//);


				yield return new TestCaseData(baseInput, (Name)"jump(null,[meh])", null, 
					BuildUnifyResult(
						Tuples.Create(14,new[] { "[meh]/NULL"}),
						Tuples.Create(21,new[] { "[meh]/Short"}),
						Tuples.Create(22,new[] { "[meh]/medium"}),
						Tuples.Create(23,new[] { "[meh]/long"}),
						Tuples.Create(16,new[] { "[meh]/[height]"}),
						Tuples.Create(15,new[] { "[width]/null","[meh]/null"}),
						Tuples.Create(17,new[] { "[width]/null","[meh]/[height]"})
					)
				);

				yield return new TestCaseData(baseInput, (Name) "jump(null,[height])", null,
					BuildUnifyResult(
						Tuples.Create(14, new[] {"[height]/NULL"}),
						Tuples.Create(21, new[] {"[height]/Short"}),
						Tuples.Create(22, new[] {"[height]/medium"}),
						Tuples.Create(23, new[] {"[height]/long"}),
						Tuples.Create(16, new string[0]),
						Tuples.Create(15, new[] { "[width]/null", "[height]/null" }),
						Tuples.Create(17, new[] { "[width]/null"})
					)
				);

				yield return new TestCaseData(baseInput, (Name)"jump(3,36)", null,
					BuildUnifyResult(
						Tuples.Create(17, new[] { "[width]/3", "[height]/36" })
					)
				);

				yield return new TestCaseData(baseInput, (Name)"jump([x],[y])", null,
					BuildUnifyResult(
						Tuples.Create(14,new []{"[x]/null","[y]/null"}),
						Tuples.Create(15,new []{"[x]/[width]","[y]/null"}),
						Tuples.Create(16,new []{"[x]/null","[y]/[height]"}),
						Tuples.Create(17,new []{"[x]/[width]","[y]/[height]"}),
						Tuples.Create(18,new []{"[x]/short","[y]/null"}),
						Tuples.Create(19,new []{"[x]/medium","[y]/null"}),
						Tuples.Create(20,new []{"[x]/long","[y]/null"}),
						Tuples.Create(21,new []{"[x]/null","[y]/short"}),
						Tuples.Create(22,new []{"[x]/null","[y]/medium"}),
						Tuples.Create(23,new []{"[x]/null","[y]/long"}),
						Tuples.Create(24,new []{"[x]/short","[y]/short"}),
						Tuples.Create(25,new []{"[x]/medium","[y]/short"}),
						Tuples.Create(26,new []{"[x]/long","[y]/short"}),
						Tuples.Create(27,new []{"[x]/short","[y]/medium"}),
						Tuples.Create(28,new []{"[x]/medium","[y]/medium"}),
						Tuples.Create(29,new []{"[x]/long","[y]/medium"}),
						Tuples.Create(30,new []{"[x]/short","[y]/long"}),
						Tuples.Create(31,new []{"[x]/medium","[y]/long"}),
						Tuples.Create(32,new []{"[x]/long","[y]/long"})
					)
				);
				
				yield return new TestCaseData(baseInput, (Name)"jump([x],[y])", 
					BuildSet(
						"[y]/Short"
					),
					BuildUnifyResult(
						Tuples.Create(21, new[] { "[x]/null", "[y]/short" }),
						Tuples.Create(24, new[] { "[x]/short", "[y]/short" }),
						Tuples.Create(25, new[] { "[x]/medium", "[y]/short" }),
						Tuples.Create(26, new[] { "[x]/long", "[y]/short" }),
						Tuples.Create(16, new[] { "[y]/Short", "[x]/null", "[y]/[height]" }),
						Tuples.Create(17, new[] { "[y]/Short", "[x]/[width]", "[y]/[height]" })
					)
				);
			}

			public static IEnumerable<TestCaseData> TestUnifyCases_Invalid()
			{
				yield return new TestCaseData(baseInput, (Name)"John(Height)", null);
				yield return new TestCaseData(baseInput, (Name)"Paul([x])", null);
				yield return new TestCaseData(baseInput, (Name)"Paul([x])", null);
			}

			public static IEnumerable<TestCaseData> Test_NameSearchTree_Count_Cases()
			{
				yield return new TestCaseData(baseInput,inputStrings.Length);
			}
		}
		/*
		[TestCaseSource(typeof(TestFactory), "TestMatchAllCases_Valid")]
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

		[TestCaseSource(typeof(TestFactory), "TestMatchAllCases_Invalid")]
		public void NameDictionary_Invalid_MatcheAll(NameSearchTree<int> dict, Name expression)
		{
			Assert.False(dict.MatchAll(expression).Any(), string.Format("Has able to find matches for {0}",expression));
		}
		*/
		[TestCaseSource(typeof(TestFactory), nameof(TestFactory.TestUnifyCases_Valid))]
		public void NameDictionary_Valid_Unify(NameSearchTree<int> dict, Name expression, SubstitutionSet bindings,
			IEnumerable<Pair<int, SubstitutionSet>> expectedResults)
		{
			var result = dict.Unify(expression, bindings);
			var resultDict = result.ToDictionary(p => p.Item1, p => p.Item2);
			var expectDict = expectedResults.ToDictionary(p => p.Item1, p => p.Item2);

			Assert.AreEqual(expectDict.Count, resultDict.Count, "Number of results mismatch");

			foreach (var pair in resultDict)
			{
				SubstitutionSet t;
				Assert.That(expectDict.TryGetValue(pair.Key, out t), "Unable to find entry");
				Assert.That(t.Equals(pair.Value), "Binding list is not equal to the expected");
			}
		}

		[TestCaseSource(typeof(TestFactory), "TestUnifyCases_Invalid")]
		public void NameDictionary_Invalid_Unify(NameSearchTree<int> dict, Name expression, SubstitutionSet bindings)
		{
			var result = dict.Unify(expression, bindings);
			Assert.That(!result.Any(),"The unification returned valid results.");
		}

		[TestCaseSource(typeof(TestFactory), "Test_NameSearchTree_Count_Cases")]
	    public void Test_NameSearchTree_Count(NameSearchTree<int> nst, int expectedCount)
	    {
			Assert.AreEqual(nst.Count,expectedCount);
	    }

    }
}