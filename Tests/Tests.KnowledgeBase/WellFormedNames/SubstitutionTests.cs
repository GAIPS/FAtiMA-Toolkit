using KnowledgeBase.Exceptions;
using KnowledgeBase.WellFormedNames;
using NUnit.Framework;

namespace Tests.KnowledgeBase.WellFormedNames
{
	[TestFixture]
	public class SubstitutionTests
	{
		[TestCase("[x]", "John")]
		[TestCase("[x]", "IsPerson(John)")]
		[TestCase("[x]", "IsPerson(John, [y])")]
		public void Substitution_ValidVariableAndValidValue_NewSubstitution(string var, string val)
		{
			var sub = new Substitution(var, val);
			Assert.That(sub.Variable.ToString() == var);
			Assert.That(sub.Value.ToString() == val);

			var sub2 = new Substitution(Name.BuildName(var), Name.BuildName(val));
			Assert.That(sub2.Equals(sub));
		}

		[TestCase("x", "John")]
		[TestCase("[x]", "IsPerson([x])")]
		public void Substitution_InvalidVariableOrCyclicalVariableInValue_BadSubstitutionException(string var, string val)
		{
			Assert.Throws<BadSubstitutionException>(() => new Substitution(var, val));
		}

		[TestCase("[x]/John", "[x]", "John")]
		[TestCase("[x]/IsPerson(John)", "[x]", "IsPerson(John)")]
		public void Substitution_ValidSubstitutionString_NewSubstitution(string s, string var, string val)
		{
			var sub = new Substitution(s);
			Assert.That(sub.Variable.ToString() == var);
			Assert.That(sub.Value.ToString() == val);
		}

		[TestCase("/[x]/John")]
		[TestCase("[x]|IsPerson(John)")]
		[TestCase("x/IsPerson(John)")]
		public void Substitution_InvalidSubstitutionString_BadSubstitutionException(string s)
		{
			Assert.Throws<BadSubstitutionException>(() => new Substitution(s));
		}

		[TestCase("[x]/John")]
		public void ToString_AnySubstitution_StringRepresentation(string s)
		{
			var sub = new Substitution(s);
			Assert.That(sub.ToString() == s);
		}

		[TestCase("[x]/John")]
		public void Clone_AnySubstitution_ClonedSubstitution(string s)
		{
			var sub = new Substitution(s);
			var clone = sub.Clone();
			Assert.That(!ReferenceEquals(sub, clone));
			Assert.That(sub.Equals(clone));
		}
	}
}
