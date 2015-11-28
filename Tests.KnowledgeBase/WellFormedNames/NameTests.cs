using System;
using KnowledgeBase.WellFormedNames;
using KnowledgeBase.WellFormedNames.Exceptions;
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
        public void Parse_CorrectNameString_NewName(string nameString)
        {
            var name = Name.Parse(nameString);
            Assert.That(name.ToString() == nameString);
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
        public void Parse_InvalidNameString_NewName(string nameString)
        {
            Assert.Throws<NameParsingException>(() => Name.Parse(nameString));
        }


        [Test]
        public void Parse_NullNameString_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Name.Parse(null));
        }

        [Test]
        public void Parse_EmptyNameString_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Name.Parse(""));
        }


        [TestCase("IsPerson([x])", "[x]")]
        [TestCase("Likes(x, Likes([x], y))", "[x]")]
        public void ContainsVariable_NameWithMatchingVariable_True(string nameString, string variable)
        {
            var name = Name.Parse(nameString);
            Assert.That(name.ContainsVariable(new Symbol(variable)));
        }

        [TestCase("IsPerson(x)", "[x]")]
        public void ContainsVariable_NameWithNoVariable_False(string nameString, string variable)
        {
            var name = Name.Parse(nameString);
            Assert.That(!name.ContainsVariable(new Symbol(variable)));
        }

        [TestCase("IsPerson([y])", "[x]")]
        public void ContainsVariable_NameWithDifferentVariable_False(string nameString, string variable)
        {
            var name = Name.Parse(nameString);
            Assert.That(!name.ContainsVariable(new Symbol(variable)));
        }

        [TestCase("IsPerson([y])", "y")]
        public void ContainsVariable_SymbolIsNotVariable_ArgumentExcpetion(string nameString, string variable)
        {
            var name = Name.Parse(nameString);
            Assert.Throws<ArgumentException>(() => name.ContainsVariable(new Symbol(variable)));
        }

        [TestCase("IsPerson(x)", "x", "IsPerson(SELF)")]
        [TestCase("Likes(x, [y])", "x", "Likes(SELF, [y])")]
        public void ApplyPerspective_NameWithAgentName_ClonedNameWithSelf(string nameString, string namePerspective, string resultName)
        {
            var name = Name.Parse(nameString);
            var clonedName = name.ApplyPerspective(namePerspective);
            Assert.That(clonedName.ToString() == resultName);
            Assert.That(!ReferenceEquals(name,clonedName));
        }


        [TestCase("is-person(SELF)", "x", "is-person(x)")]
        [TestCase("likes(SELF, [x])", "x", "likes(x, [x])")]
        public void RemovePerspective_NameWithSELF_ClonedNameWithAgentName(string nameString, string namePerspective, string resultName)
        {
            var name = Name.Parse(nameString);
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
		public void Equals_NameWithEquivalentName(string nameString1, string nameString2)
		{
			var name1 = Name.Parse(nameString1);
			var name2 = Name.Parse(nameString2);
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