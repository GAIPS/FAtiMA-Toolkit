using System;
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
        public void Parse_CorrectNameString_NewName(string nameString)
        {
            var name = Name.Parse(nameString);
            Assert.That(name.ToString() == nameString);
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


    }
}