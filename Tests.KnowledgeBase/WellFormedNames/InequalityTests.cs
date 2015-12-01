using KnowledgeBase.WellFormedNames;
using NUnit.Framework;

namespace Tests.KnowledgeBase.WellFormedNames
{
	//[TestFixture]
	//public class InequalityTests
	//{
	//	[TestCase("[x]", "John")]
	//	[TestCase("[x]", "IsPerson(John)")]
	//	[TestCase("[x]", "IsPerson(John, [y])")]
	//	public void Inequality_ValidVariableAndValidValue_NewInequality(string var, string val)
	//	{
	//		var sub = new Inequality(new Symbol(var), Name.Parse(val));
	//		Assert.That(sub.Variable.Name == var);
	//		Assert.That(sub.Value.ToString() == val);
	//	}

	//	[TestCase("[x]!=John", "[x]", "John")]
	//	public void Inequality_ValidInequalityString_NewInequality(string i, string var , string val)
	//	{
	//		var sub = new Inequality(i);
	//		Assert.That(sub.Variable.Name == var);
	//		Assert.That(sub.Value.ToString() == val);
	//	}

	//	[TestCase("[x]!John")]
	//	[TestCase("[x]/John")]
	//	[TestCase("[x]=!John")]
	//	[TestCase("x!=John")]
	//	public void Inequality_InvalidInequalityString_BadSubstitutionException(string i)
	//	{
	//		Assert.Throws<BadSubstitutionException>(() => new Inequality(i));
	//	}

	//	[TestCase("[x]", "John")]
	//	public void Clone_ValidInequality_Cloned(string var, string val)
	//	{
	//		var sub = new Inequality(new Symbol(var), Name.Parse(val));
	//		var clone = sub.Clone();
	//		Assert.That(!ReferenceEquals(sub, clone));
	//		Assert.That(sub.Equals(clone));
	//	}

	//	[TestCase("[x]", "John", "[x]!=John")]
	//	public void ToString_ValidInequality_StringRepresentation(string var, string val, string strInequality)
	//	{
	//		var sub = new Inequality(new Symbol(var), Name.Parse(val));
	//		Assert.That(sub.ToString() == strInequality);
	//	}
	//}
}