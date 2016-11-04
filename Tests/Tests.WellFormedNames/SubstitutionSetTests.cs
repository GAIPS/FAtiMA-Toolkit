using System;
using System.Linq;
using NUnit.Framework;
using WellFormedNames;

namespace Tests.WellFormedNames
{
	[TestFixture]
	public class SubstitutionSetTests
	{
		[TestCase(1, new[] { "[x]/john" })]
		[TestCase(2, new[] { "[x]/john", "[y]/mary" })]
		//[TestCase(2, new[] { "[x]/john", "[y]/john" })]
		//[TestCase(2, new[] { "[x]/john", "[y]/[x]" })]
		//[TestCase(1, new[] { "[x]/[y]", "[y]/[x]" })]
		//[TestCase(3, new[] { "[x]/john", "[y]/john", "[x]/[y]" })]
		//[TestCase(3, new[] { "[x]/john", "[y]/[x]", "[z]/[y]" })]
		//[TestCase(3, new[] { "[x]/john", "[x]/[y]", "[y]/[z]" })]
		[TestCase(3, new[] {"[y]/[x]", "[z]/[y]", "[x]/[z]", "[x]/john"})]
		public void SubstitutionSet_CountTest(int amount, string[] substitutions)
		{
			var s = new SubstitutionSet(substitutions.Select(str => new Substitution(str)));
			Assert.AreEqual(s.Count(), amount);
		}

		[TestCase("[x]", new[] { "[x]/john" })]
		[TestCase("[y]", new[] { "[x]/john", "[y]/mary" })]
		[TestCase("[z]", new[] { "[y]/[x]", "[z]/[y]", "[x]/[z]", "[x]/john" })]
		public void SubstitutionSet_Contains_Pass(string variable, string[] substitutions)
		{
			var s = new SubstitutionSet(substitutions.Select(str => new Substitution(str)));
			Assert.True(s.Contains((Name) variable));
		}

		[TestCase("[y]", new[] { "[x]/john" })]
		[TestCase("[z]", new[] { "[x]/john", "[y]/mary" })]
		[TestCase("[w]", new[] { "[y]/[x]", "[z]/[y]", "[x]/[z]", "[x]/john" })]
		public void SubstitutionSet_Contains_Fail(string variable, string[] substitutions)
		{
			var s = new SubstitutionSet(substitutions.Select(str => new Substitution(str)));
			Assert.False(s.Contains((Name)variable));
		}

		[TestCase("test", new[] { "[x]/john" })]
		public void SubstitutionSet_Contains_Assert_Name_as_Variable(string variable, string[] substitutions)
		{
			var s = new SubstitutionSet(substitutions.Select(str => new Substitution(str)));
			Assert.Throws<ArgumentException>(()=>s.Contains((Name) variable));
		}

		[TestCase("[x]/john", "[y]/eve")]
		public void AddSubstitution_V1_Pass(params string[] substitutions)
		{
			var s = new SubstitutionSet();
			foreach (var s2 in substitutions.Select(s1 => new Substitution(s1)))
				s.AddSubstitution(s2);
		}

		[TestCase("[x]/john", "[y]/eve")]
		[TestCase("[x]/john", "[y]/john", "[x]/[y]")]
		[TestCase("[z]/[y]", "[x]/john", "[x]/[y]")]
		public void AddSubstitutions_V2_Pass(params string[] substitutions)
		{
			var s = new SubstitutionSet();
			s.AddSubstitutions(substitutions.Select(s1 => new Substitution(s1)));
		}

		[TestCase("[x]/john", "[x]/eve")]
		public void AddSubstitutions_V2_Fail(params string[] substitutions)
		{
			var s = new SubstitutionSet();
			s.AddSubstitutions(substitutions.Select(s1 => new Substitution(s1)));
		}

		[TestCase("[x]/john", "[y]/eve")]
		public void AddSubstitutions_V3_Pass(params string[] substitutions)
		{
			var s = new SubstitutionSet(substitutions.Select(s1 => new Substitution(s1)));
			var s2 = new SubstitutionSet();
			s2.AddSubstitutions(s);
		}

		[TestCase("[x]/john", "[x]/eve")]
		public void AddSubstitutions_V3_Fail(string s1, string s2)
		{
			var set1 = new SubstitutionSet(new Substitution(s1));
			var set2 = new SubstitutionSet(new Substitution(s2));
			set2.AddSubstitutions(set1);
		}

		[TestCase(new[] {"[x]/john", "[y]/eve"}, new[] { "[z]/Eric", "[y]/[x]" })]
		public void AddSubstitutions_V4_Fail(string[] s1, string[] s2)
		{
			var set1 = new SubstitutionSet(s1.Select(str => new Substitution(str)));
			set1.AddSubstitutions(s2.Select(str => new Substitution(str)));
		}
	}
}