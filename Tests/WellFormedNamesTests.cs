using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using KnowledgeBase.WellFormedNames;
using KnowledgeBase.WellFormedNames.Collections;
using KnowledgeBase.WellFormedNames.Exceptions;
using Exception = System.Exception;

namespace UnitTest.WellFormedNames
{
	[TestClass]
	public class WellFormedNamesTests
	{
		#region Parse Tests

		struct ParseResults
		{
			public string text;
			public bool isValid;
			public Name matchName;

			public ParseResults(string text, bool isValid, Name matchName = null)
			{
				this.text = text;
				this.isValid = isValid;
				this.matchName = matchName;
			}
		}

		private static readonly ParseResults[] PARSE_TESTS = new ParseResults[] {
			new ParseResults("John",true,new Symbol("John")),
			new ParseResults("  \tDog\t",true,new Symbol("Dog")),
			new ParseResults("Like(Denise,Davis)",true,new ComposedName(new Symbol("Like"),new Symbol("Denise"),new Symbol("Davis"))),
			new ParseResults("EVENT(*,[_x],John)",true,new ComposedName(new Symbol("EVENT"),Symbol.UNIVERSAL_SYMBOL,new Symbol("[_x]"),new Symbol("John"))),
			new ParseResults("EVENT(Talk,SELF,Davis,Volume(High),Tone(Mad))",true,new ComposedName(new Symbol("EVENT"),
					new Symbol("Talk"),Symbol.SELF_SYMBOL,new Symbol("Davis"),
					new ComposedName(new Symbol("Volume"),new Symbol("High")),
					new ComposedName(new Symbol("Tone"),new Symbol("Mad"))
				))
		};
		
		[TestMethod]
		public void TestWellformedNameParser() {
			foreach (var test in PARSE_TESTS)
			{
				Name result = null;
				try
				{
					result = Name.Parse(test.text);
				}
				catch (System.Exception e)
				{
					if (test.isValid)
						Assert.Fail(string.Format(@"Failed to parse ""{0}"". {1}",test.text,e));
				}
				if (!test.isValid)
					Assert.Fail(string.Format(@"The welformed name parser has parsed ""{0}"" sucessfully. It should have failed."));

				Assert.AreEqual(test.matchName,result);
			}
		}

		#endregion

		#region Symbol Tests

		private struct SymbolDef
		{
			public string name;
			public bool isValid;
			public bool isGround;
			public bool isVariable;
			public bool isGhost;

			public SymbolDef(string name, bool isValid, bool grounded, bool isVariable, bool isGhost)
			{
				this.name = name;
				this.isValid = isValid;
				this.isGround = grounded;
				this.isVariable = isVariable;
				this.isGhost = isGhost;
			}

			public SymbolDef(string name)
			{
				this.name = name;
				this.isValid = false;
				this.isGround = false;
				this.isVariable = false;
				this.isGhost = false;
			}
		}

		private static SymbolDef[] symbolTest = new SymbolDef[]{
			new SymbolDef("*",true,true,false,false),
			new SymbolDef("123",true,true,false,false),
			new SymbolDef("test",true,true,false,false),
			new SymbolDef("_ssas-asas",true,true,false,false),
			new SymbolDef(Symbol.SELF_STRING,true,true,false,false),
			new SymbolDef("Joao",true,true,false,false),
			new SymbolDef("[x]",true,false,true,false),
			new SymbolDef("[Agent]",true,false,true,false),
			new SymbolDef("[K123]",true,false,true,false),
			new SymbolDef("[_dsd]",true,false,true,true),
			new SymbolDef(""),
			new SymbolDef("?"),
			new SymbolDef("[1ssd]"),
			new SymbolDef("[ssssd"),
			new SymbolDef("asaa]"),
			new SymbolDef("asasa[K123]asas"),
		};

		[TestMethod]
		public void TestSymbolCreation()
		{
			foreach (SymbolDef d in symbolTest)
			{
				try
				{
					var s = new Symbol(d.name);
					Assert.AreEqual(s.IsVariable, d.isVariable);
					Assert.AreEqual(s.IsGrounded, d.isGround);
					Assert.AreEqual(s.HasGhostVariable(), d.isGhost);
				}
				catch (InvalidSymbolDefinitionException e)
				{
					if (d.isValid)
						throw e;
					else
						continue;
				}
				if (!d.isValid)
					Assert.Fail();
			}
		}

		[TestMethod]
		public void TestCloneSymbol()
		{
			SymbolDef t = symbolTest.Where(d => d.isValid).FirstOrDefault();

			var s = new Symbol(t.name);
			var clone = s.Clone();

			Assert.IsNotNull(clone);
			Assert.AreEqual(s, clone);
			Assert.AreNotSame(s, clone);
		}

		#endregion

		#region Unification Test

		private struct UnificationTest
		{
			public string A;
			public string B;
			public bool Valid;
			public string[] ExpectedSubstitutions;

			public UnificationTest(string A, string B, bool valid = false, params string[] expected)
			{
				this.A = A;
				this.B = B;
				this.Valid = valid;
				this.ExpectedSubstitutions = expected;
			}
		}
		
		[TestMethod]
		public void UnifierTests()
		{
			UnificationTest[] tests = {
										   new UnificationTest("John","John",true),
										   new UnificationTest("Strong(John)","Strong(John)",true),
										   new UnificationTest("[X]","John",true,"[X]/John"),
										   new UnificationTest("Strong([X])","John",false),
										   new UnificationTest("Strong([X])","Strong(John)",true,"[x]/John"),
										   new UnificationTest("Strong(John)","Strong([X])",true,"[x]/John"),
										   new UnificationTest("Strong([X])","[y](John)",true,"[x]/John","[y]/Strong"),
										   new UnificationTest("Like([X],[y])","Like(John,[z])",true,"[x]/John","[y]/[z]"),
										   new UnificationTest("Like([X],[y])","Like(John,[y])",true,"[x]/John"),
										   new UnificationTest("Like([X],Strong(John))","Like(John,Strong(John))",true,"[x]/John"),
										   new UnificationTest("Like([X],Strong(John))","Like(John,Strong(Lily))",false),
										   new UnificationTest("Like([X],Strong(John))","Like(John,Strong([y]))",true,"[x]/John","[y]/John"),
										   new UnificationTest("Like([X],[y])","Like(John,Strong([y]))",false),
										   new UnificationTest("Like([X],John)","Like(John,[x])",true,"[x]/John"),
										   new UnificationTest("Like([X],Lily)","Like(John,[x])",false),
										   new UnificationTest("S([x],k([x],[z]),j([y],k(t(k,l),y)))","S(t(k,l),k([x],y),j(P,k([x],[z])))",true,
										    "[x]/t(k,l)","[z]/y","[y]/P"),
										    new UnificationTest("S([x],k([x],[z]),j([y],k(t([y],P),y)))","S([x],k(t([y],P),[z]),j([y],k(t(X,[z]),y)))",true,
										   	 "[x]/t([y],P)","[y]/X","[z]/P"),
									   };
			
			foreach (var t in tests)
			{
				var A = Name.Parse(t.A);
				var B = Name.Parse(t.B);

				try
				{
					IEnumerable<Substitution> result;
					if (Unifier.Unify(A, B, out result))
					{
						if (!t.Valid)
							Assert.Fail("Should have failed");

						if (result == null)
							Assert.Fail("Unifier should return a substitution set in case of sucess.");

						var AG = A.MakeGround(result);
						var BG = B.MakeGround(result);
						if(!AG.Equals(BG))
							Assert.Fail("The resulting generated substitutions fail to ground both expressions.");

						var tmp = t.ExpectedSubstitutions.Select(s => new Substitution(s)).ToArray();

						if (result.Except(tmp).Any())
							Assert.Fail("The unifier returned more substitutions that the minimum required.");
					}
					else
					{
						if (t.Valid)
							Assert.Fail("Failed to unify.");

						if (result != null)
							Assert.Fail("Unifier should return a null substitution set in case of failure");
					}
				}
				catch (System.Exception e)
				{
					throw new AssertFailedException(string.Format("Failed at {0} <-> {1}",A,B), e);
				}
			}
		}

		#endregion

		#region NameSearchTree Tests

		private static string[] _searchTreeEntries = {
														 "S(l(v,y),u)",
														 "S(*,u)",
														 "S(k(x,y,z),u)",
														 "K",
														 "S(l(v),u)",
														 "S(l(*,y),u)",
														 "S(l(*,y),y)",
														 "S(l(*,y),p)",
														 "S(l(k,y),u)",
														 "K(l(v),u)",
														 "K(l(k(x,*,z),*),u)",
														 "K(l(k(x,y,z),*),u)",
														 "K(l(k(x,*,z),u),u)",
														 "K(*(*(x,*,z),u),u)",
														 "*",
														 "S(*,l(*,o))"};

		private struct MatchResults
		{
			public readonly string Entry;
			public readonly bool IsValid;
			public readonly int[] ExpectedResults;

			public MatchResults(string entry, bool isValid, params int[] expectedResults)
			{
				this.Entry = entry;
				this.IsValid = isValid;
				this.ExpectedResults = expectedResults;
			}
		}

		private static MatchResults[] _matchEntries = {
														  new MatchResults("S(l(v,y),u)",true,0),
														  new MatchResults("S(*,u)",true,1),
														  new MatchResults("S(A,u)",true,1),
														  new MatchResults("K",true,3),
														  new MatchResults("S(l(*,y),u)",true,5),
														  new MatchResults("S(l(p,*),*)",true,5,6,7),
														  new MatchResults("S(l(p,*),l(p,o))",true,15),
														  new MatchResults("S(l(p,*),k)",true,14),
														  new MatchResults("K(l(v),u)",true,9),
														  new MatchResults("K(l(k(x,y,z),*),u)",true,11),
														  new MatchResults("K(l(t(x,g,z),u),u)",true,13),
														  new MatchResults("*",true,14),
													  };

		[TestMethod]
		public void NameSearchTree_InsertTests()
		{
			int NumOfIterations = 30;
			var baseSet = _searchTreeEntries.Select((n, i) => new { v = i, name = Name.Parse(n) });
			while (NumOfIterations > 0)
			{
				var set = baseSet.OrderBy(e => Guid.NewGuid());
				NameSearchTree<int> tree = new NameSearchTree<int>();
				foreach (var e in set)
				{
					tree.Add(e.name, e.v);
				}

				foreach (var e in set)
				{
					try
					{
						tree.Add(e.name, e.v);
						Assert.Fail("duplicated key added with success.");
					}
					catch (Exception)//TODO this must reflect the correct exception thrown
					{
						
					}
				}

				Trace.WriteLine(string.Format("Input: {0}", Utilities.LinqExtentions.AggregateToString(set.Select(a => a.name), ", ")));
				Trace.WriteLine("Depth: " + tree.Depth);
				Trace.WriteLine(tree.ToString());

				NumOfIterations--;
			}
		}

		[TestMethod]
		public void NameSearchTree_RemoveTests()
		{
			int NumOfIterations = 30;
			var baseSet = _searchTreeEntries.Select((n, i) => new { v = i, name = Name.Parse(n) });
			var set = baseSet.OrderBy(e => Guid.NewGuid());
			NameSearchTree<int> baseTree = new NameSearchTree<int>();
			foreach (var e in set)
			{
				baseTree.Add(e.name, e.v);
			}

			while (NumOfIterations > 0)
			{
				NameSearchTree<int> tree = new NameSearchTree<int>(baseTree);
				set = baseSet.OrderBy(e => Guid.NewGuid());

				foreach (var e in set)
				{
					if (!tree.Remove(e.name))
					{
						Assert.Fail("Fail to remove relation {0} -> {1}", e.name, e.v);
					}
				}

				Trace.WriteLine("Depth: " + tree.Depth);
				Trace.WriteLine(tree.ToString());

				NumOfIterations--;
			}
		}

		[TestMethod]
		public void NameSearchTree_ContainsTest()
		{
			int NumOfIterations = 30;
			var baseSet = _searchTreeEntries.Select((n, i) => new { v = i, name = Name.Parse(n) });
			NameSearchTree<int> tree = new NameSearchTree<int>();
			foreach (var e in baseSet)
			{
				tree.Add(e.name, e.v);
			}

			while (NumOfIterations > 0)
			{
				var set = baseSet.OrderBy(e => Guid.NewGuid());

				foreach (var e in set)
				{
					if (!tree.ContainsKey(e.name))
					{
						Assert.Fail("Failed to find a relation with {0}", e.name);
					}
				}

				NumOfIterations--;
			}
		}

		[TestMethod]
		public void NameSearchTree_MatchTest()
		{
			var baseSet = _searchTreeEntries.Select((n, i) => new { v = i, name = Name.Parse(n) });
			NameSearchTree<int> tree = new NameSearchTree<int>();
			foreach (var e in baseSet)
			{
				tree.Add(e.name, e.v);
			}
			var set = _matchEntries.OrderBy(e => Guid.NewGuid());

			foreach (var e in set)
			{
				Name m = Name.Parse(e.Entry);
				int v;
				if (tree.TryMatchValue(m, out v))
				{
					if (!e.IsValid)
						Assert.Fail("Matched with {0}. Should have failed", m);

					if (!e.ExpectedResults.Contains(v))
					{
						Assert.Fail("{0} Matched value {1}. It's not contained within the expected result set.", m,v);
					}
				}
				else
				{
					if(e.IsValid)
						Assert.Fail("Failed to find a match with {0}", m);
				}
			}
		}

		[TestMethod]
		public void NameSearchTree_GetAllValues()
		{
			var baseSet = _searchTreeEntries.Select((n, i) => new { v = i, name = Name.Parse(n) });
			NameSearchTree<int> tree = new NameSearchTree<int>();
			foreach (var e in baseSet)
			{
				tree.Add(e.name, e.v);
			}

			var values = tree.Values;
			if (baseSet.Select(s => s.v).Except(values).Any())
				Assert.Fail("Could not retrieve all expected values within the tree");
		}

		[TestMethod]
		public void NameSearchTree_GetAllValuePairs()
		{
			var baseSet = _searchTreeEntries.Select((n, i) => new KeyValuePair<Name,int>(Name.Parse(n),i)).OrderBy(p => Guid.NewGuid());
			NameSearchTree<int> tree = new NameSearchTree<int>();
			foreach (var e in baseSet)
			{
				tree.Add(e.Key, e.Value);
			}

			var values = tree.ToList();
			if (baseSet.Except(values).Any())
				Assert.Fail("Could not retrieve all expected values within the tree");
		}

		#endregion
	}
}
