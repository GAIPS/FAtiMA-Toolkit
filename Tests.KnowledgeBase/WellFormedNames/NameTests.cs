using System;
using KnowledgeBase.Exceptions;
using KnowledgeBase.WellFormedNames;
using NUnit.Framework;

namespace Tests.KnowledgeBase.WellFormedNames
{
    [TestFixture]
    public class NameTests
    {
        [TestCase("[y]")]
        [TestCase("[x]")]
        [TestCase("IsPerson(x)")]
        [TestCase("Likes(x, y)")]
        [TestCase("Likes([x], y)")]
        [TestCase("Likes(x, Likes(x, x))")]
        [TestCase("Likes(x, Likes(y, Hates([y], x)))")]
        [TestCase("[x]([x], y, z)")]
		[TestCase("x")]
		[TestCase("9")]
		[TestCase("0")]
		[TestCase("0000")]
		[TestCase("0010")]
		[TestCase("10.56")]
		[TestCase("-10.56")]
		[TestCase("-10")]
		[TestCase("+10")]
		[TestCase("10.7654e10")]
		[TestCase("1.7654e-5")]
		[TestCase("true")]
		[TestCase("false")]
		[TestCase("_17654")]
		[TestCase("[x]")]
		[TestCase("-")]
		[TestCase("[_x]")]
		[TestCase("[x-93]")]
		[TestCase("Likes([x], 10.7654e10)")]
		[TestCase(Name.UNIVERSAL_STRING)]
		[TestCase(Name.AGENT_STRING)]
		[TestCase(Name.SELF_STRING)]
		[TestCase(Name.NIL_STRING)]
        public void Parse_CorrectNameString_NewName(string nameString)
        {
            Name.BuildName(nameString);
        }

		[Test]
		public void Parse_CorrectNameString_Null()
		{
			var name = Name.BuildName((string)null);
			Assert.That(string.Equals(name.ToString(), "-", StringComparison.InvariantCultureIgnoreCase));
		}


        [TestCase("[y]")]
        [TestCase("[x]")]
        [TestCase("IsPerson(x)")]
        [TestCase("Likes(x, y)")]
        [TestCase("Likes([x], y)")]
        [TestCase("Likes(x, Likes(x, x))")]
        [TestCase("Likes(x, Likes(y, Hates([y], x)))")]
        [TestCase("[x]([x], y, z)")]
        public void ExplicitCastName_CorrectNameString_NewName(string nameString)
        {
            var name = (Name)nameString;
            Assert.That(name.ToString() == nameString);
        }


        [TestCase("IsPerson(x")]
		[TestCase("@")]
		[TestCase("/")]
		[TestCase("(x)")]
		[TestCase("x)")]
		[TestCase("(x")]
		[TestCase("x, y")]
		[TestCase("1.76.54e-5")]
		[TestCase("1.76.54")]
		[TestCase("_1.76")]
		[TestCase("[x")]
		[TestCase("x]")]
		[TestCase("[]")]
		[TestCase("Likes(-56.34, 1.76.54e-5)")]
        public void Parse_InvalidNameString_NewName(string nameString)
        {
            Assert.Throws<ParsingException>(() => Name.BuildName(nameString));
        }

        [Test]
        public void Parse_EmptyNameString_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Name.BuildName(""));
        }


        [TestCase("IsPerson([x])", "[x]")]
        [TestCase("Likes(x, Likes([x], y))", "[x]")]
        public void ContainsVariable_NameWithMatchingVariable_True(string nameString, string variable)
        {
            var name = Name.BuildName(nameString);
            Assert.That(name.ContainsVariable(Name.BuildName(variable)));
        }

        [TestCase("IsPerson(x)", "[x]")]
        public void ContainsVariable_NameWithNoVariable_False(string nameString, string variable)
        {
            var name = Name.BuildName(nameString);
            Assert.That(!name.ContainsVariable(Name.BuildName(variable)));
        }

        [TestCase("IsPerson([y])", "[x]")]
        public void ContainsVariable_NameWithDifferentVariable_False(string nameString, string variable)
        {
            var name = Name.BuildName(nameString);
            Assert.That(!name.ContainsVariable(Name.BuildName(variable)));
        }

        [TestCase("IsPerson([y])", "y")]
        public void ContainsVariable_SymbolIsNotVariable_ArgumentExcpetion(string nameString, string variable)
        {
            var name = Name.BuildName(nameString);
			Assert.Throws<ArgumentException>(() => name.ContainsVariable(Name.BuildName(variable)));
        }

        [TestCase("IsPerson(x)", "x", "IsPerson(SELF)")]
        [TestCase("Likes(x, [y])", "x", "Likes(SELF, [y])")]
        public void ApplyPerspective_NameWithAgentName_ClonedNameWithSelf(string nameString, string namePerspective, string resultName)
        {
            var name = Name.BuildName(nameString);
            var clonedName = name.ApplyPerspective(namePerspective);
            Assert.That(clonedName.ToString() == resultName);
            Assert.That(!ReferenceEquals(name,clonedName));
        }


        [TestCase("is-person(SELF)", "x", "is-person(x)")]
        [TestCase("likes(SELF, [x])", "x", "likes(x, [x])")]
        public void RemovePerspective_NameWithSELF_ClonedNameWithAgentName(string nameString, string namePerspective, string resultName)
        {
            var name = Name.BuildName(nameString);
            var clonedName = name.RemovePerspective(namePerspective);
            Assert.That(clonedName.ToString() == resultName);
            Assert.That(!ReferenceEquals(name, clonedName));
        }
		
		[TestCase("John", "John")]
		[TestCase("John", "JOHN")]
		[TestCase("John", "john")]
		[TestCase("John", "JoHn")]
		[TestCase("Albert(Smart)", "Albert(Smart)")]
		[TestCase("Albert(SMART)", "ALBERT(Smart)")]
		[TestCase("[_x]", "[_X]")]
		[TestCase("SELF", "self")]
		[TestCase("*", "*")]
		[TestCase("0", "000")]
		[TestCase("34", "00034")]
		[TestCase("34", "34.000")]
		public void Equals_NameWithEquivalentName(string nameString1, string nameString2)
		{
			var name1 = Name.BuildName(nameString1);
			var name2 = Name.BuildName(nameString2);
			Assert.That(name1.Equals(name2));
		}
		
        [Test]
        public void GenerateUniqueGhostVariable_AnyState_NewSymbol()
        {
            var ghost1 = Name.GenerateUniqueGhostVariable();
            var ghost2 = Name.GenerateUniqueGhostVariable();

            Assert.That(ghost1.HasGhostVariable());
            Assert.That(ghost2.HasGhostVariable());

            Assert.That(ghost1 != ghost2);
        }

		[TestCase("A")]
		[TestCase("[x]")]
		[TestCase("A(B,C)")]
		[TestCase("A(B,C(D))")]
		[TestCase("A(B(C,D,E(F,G)),H(I(J(K)),L,M))")]
	    public void Fold_Unfold_Test(string name)
		{
			var n = (Name) name;
			Name output;
			SubstitutionSet set;
			output = n.Unfold(out set);
			if (set != null)
			{
				output = output.MakeGround(set);
			}
			Assert.AreEqual(n,output);
		}
    }
}