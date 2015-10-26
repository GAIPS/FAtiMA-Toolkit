using System;
using System.Collections.Generic;
using System.Linq;
using KnowledgeBase.WellFormedNames;
using NUnit.Framework;

namespace Tests.KnowledgeBase.WellFormedNames
{
    [TestFixture]
    public class ComposedNameTests
    {
        [TestCase("Like","A","B")]
        [TestCase("IsPerson", "A")]
        public void ComposedName_ValidTerms_NewComposedName(params string[] groundedSymbolStrings)
        {
            var groundedSymbols = groundedSymbolStrings.Select(s => new Symbol(s));
            var composedName = new ComposedName(groundedSymbols);
            Assert.That(composedName.NumberOfTerms == groundedSymbolStrings.Length);
        }

        [TestCase("IsPerson")]
        public void ComposedName_OneTerm_ArgumentException(string term1)
        {
            Assert.Throws<ArgumentException>(() => new ComposedName(new Symbol(term1)));
        }

        [Test]
        public void ComposedName_NoTerms_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new ComposedName(new List<Name>()));
        }

        [TestCase("IsPerson","A")]
        public void IsUniversal_AnyComposedName_False(string term1, string term2)
        {
            var composedName = new ComposedName(new Symbol(term1), new Symbol(term2));
            Assert.That(!composedName.IsUniversal);
        }

        [TestCase("IsPerson", "A")]
        public void IsVariable_AnyComposedName_False(string term1, string term2)
        {
            var composedName = new ComposedName(new Symbol(term1), new Symbol(term2));
            Assert.That(!composedName.IsVariable);
        }

        [TestCase("IsPerson", "A")]
        public void IsGrounded_GroundedComposedName_True(string term1, string term2)
        {
            var composedName = new ComposedName(new Symbol(term1), new Symbol(term2));
            Assert.That(composedName.IsGrounded);
        }

        [TestCase("IsPerson", "[x]")]
        [TestCase("[x]", "A")]
        [TestCase("[x]", "[y]")]
        public void IsGrounded_UnGroundedComposedName_False(string term1, string term2)
        {
            var composedName = new ComposedName(new Symbol(term1), new Symbol(term2));
            Assert.That(!composedName.IsGrounded);
        }

        [TestCase("IsPerson","[x]","B")]
        public void GetFirstTerm_ComposedName_FirstTerm(string firstTerm, string secondTerm, string thirdTerm)
        {
            var composedName = new ComposedName(new Symbol(firstTerm), new Symbol(secondTerm), new Symbol(thirdTerm));
            Assert.That(composedName.GetFirstTerm() == new Symbol(firstTerm));
        }

        [TestCase("IsPerson", "a")]
        [TestCase("IsPerson", "[x]", "B")]
        public void GetTerms_ComposedName_AllTerms(params string[] terms)
        {
            var symbolTerms = terms.Select(t => new Symbol(t));
            var composedName = new ComposedName(symbolTerms);
            Assert.That(composedName.GetTerms(), Is.EquivalentTo(symbolTerms));
        }


    }
}