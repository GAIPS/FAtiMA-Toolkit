using System;
using KnowledgeBase.WellFormedNames;
using NUnit.Framework;
using KnowledgeBase.WellFormedNames.Collections;
using System.Collections.Generic;
using System.Linq;

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
				yield return new TestCaseData(baseInput, (Name)"John(Name)", BuildResult());
				yield return new TestCaseData(baseInput, (Name)"John(Height)", null);
				yield return new TestCaseData(baseInput, (Name)"Paul([x])", null);
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

		[TestCaseSource(typeof(TestFactory), "TestCases")]
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
				if (expectedResults == null)
					return;
				Assert.Fail("Binder failed to find matches");
			}

			if (results.Count() != expectedResults.Count)
				Assert.Fail("Binder returned a different number of binds from the expected");
			if (!new HashSet<SubstitutionSet>(results).SetEquals(expectedResults))
				Assert.Fail("Binder didn't returned the expected substitutions.");
		}
    }
}