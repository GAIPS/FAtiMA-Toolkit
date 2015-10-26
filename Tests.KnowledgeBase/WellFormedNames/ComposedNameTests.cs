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

        [TestCase("IsPerson", "A")]
        public void ComposedName_FirstTermNotASymbol_ArgumentException(params string[] symbolStrings)
        {
            var symbols = symbolStrings.Select(s => new Symbol(s));
            var composedName = new ComposedName(symbols);
            var symbolsWithName = new List<Name>();
            symbolsWithName.Add(composedName);
            symbolsWithName.AddRange(symbols);

            Assert.Throws<ArgumentException>(() => new ComposedName(symbolsWithName));
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


        [TestCase("IsPerson", "a")]
        [TestCase("IsPerson", "a", "B")]
        public void GetLiterals_ComposedName_AllLiterals(params string[] terms)
        {
            var symbolTerms = new List<Name>(terms.Select(t => new Symbol(t))) ;
            var symbolTermsRepeated = symbolTerms.Concat(symbolTerms);
            var composedName = new ComposedName(symbolTerms);

            var symbolTermsWithAddedName = new List<Name>(symbolTerms.ToArray());
            symbolTermsWithAddedName.Add(composedName);

            var composedNameWithComposedName = new ComposedName(symbolTermsWithAddedName);

            var literals = composedNameWithComposedName.GetLiterals();

            Assert.That(literals, Is.EquivalentTo(symbolTermsRepeated));
        }

        [TestCase("[y]", "[x]")]
        [TestCase("IsPerson", "[a]", "[b]")]
        public void GetVariableList_ComposedNameWithTwoVariables_TwoVariables(params string[] terms)
        {
            var symbolTerms = terms.Select(t => new Symbol(t));
            var composedName = new ComposedName(symbolTerms);

            var variables = composedName.GetVariableList();
            
            foreach (var variable in variables)
            {
                Assert.That(symbolTerms.Contains(variable));
                Assert.That(variable.IsVariable);
            }
        }

        [TestCase("IsPerson", "x")]
        [TestCase("IsPerson", "[a]", "[b]")]
        public void HasGhostVariable_ComposedNameWithNoGhostVariables_False(params string[] terms)
        {
            var symbolTerms = terms.Select(t => new Symbol(t));
            var composedName = new ComposedName(symbolTerms);
            Assert.That(!composedName.HasGhostVariable());
        }

        [TestCase("IsPerson", "[a]", "[_b]")]
        public void HasGhostVariable_ComposedNameWithGhostVariables_True(params string[] terms)
        {
            var symbolTerms = terms.Select(t => new Symbol(t));
            var composedName = new ComposedName(symbolTerms);
            Assert.That(composedName.HasGhostVariable());
        }


        [TestCase("IsPerson", "A")]
        public void Clone_ValidComposedName_ClonedComposedName(string term1, string term2)
        {
            var composedName = new ComposedName(new Symbol(term1), new Symbol(term2));
            var clone = (ComposedName)composedName.Clone();
            Assert.That(clone == composedName);
            Assert.That(!ReferenceEquals(clone, composedName));
        }

        [TestCase("John", "Mary", "IsPerson", "John")]
        public void SwapPerspective_ComposedName_ClonedComposedNameWithSwappedPerspective(string original, string perspective, params string[] symbolsStrings)
        {
            var composedName = new ComposedName(symbolsStrings.Select(s => new Symbol(s)));
            var clone = (ComposedName)composedName.Clone();

            var nameWithSwappedPerspective = composedName.SwapPerspective(original, perspective);
            
            Assert.That(!nameWithSwappedPerspective.GetLiterals().Contains(new Symbol(original)));
            Assert.That(nameWithSwappedPerspective.GetLiterals().Contains(new Symbol(perspective)));
            Assert.That(!ReferenceEquals(composedName, nameWithSwappedPerspective));
            Assert.That(composedName == clone);
        }

        [TestCase(1, "x1", "IsPerson", "[x]")]
        [TestCase(3, "y3", "IsPerson", "[y3]")]
        public void ReplaceUnboundVariables_ComposedNameWithVariables_ClonedNameWithVariablesReplaced(int variableID, string expectedVariable, params string[] symbolsStrings)
        {
            var composedName = new ComposedName(symbolsStrings.Select(s => new Symbol(s)));
            var nameWithReplacedVariables = composedName.ReplaceUnboundVariables(variableID);
            var clone = (ComposedName)composedName.Clone();

            Assert.That(!nameWithReplacedVariables.GetVariableList().Contains(new Symbol(expectedVariable)));
            Assert.That(!ReferenceEquals(composedName, nameWithReplacedVariables));
            Assert.That(composedName == clone);
        }

        [TestCase(1, "IsPerson", "x")]
        public void ReplaceUnboundVariables_ComposedNameWithNoVariables_Clone(int variableID, params string[] symbolsStrings)
        {
            var composedName = new ComposedName(symbolsStrings.Select(s => new Symbol(s)));
            var clone = composedName.ReplaceUnboundVariables(variableID);

            Assert.That(!ReferenceEquals(composedName, clone));
            Assert.That(composedName == clone);
        }

        [TestCase("[x]","John","IsPerson","[x]","x")]
        public void MakeGround_NameWithVariables_GroundedClone(string variableName, string variableValue, params string[] symbolsStrings)
        {
            var substitution = new Substitution(variableName, variableValue);
            var composedName = new ComposedName(symbolsStrings.Select(s => new Symbol(s)));
            var groundedClone = composedName.MakeGround(new List<Substitution> {substitution});
  
            Assert.That(!composedName.IsGrounded);
            Assert.That(groundedClone.IsGrounded);
            Assert.That(groundedClone.GetLiterals().Contains(new Symbol(variableValue)));
            Assert.That(!groundedClone.GetLiterals().Contains(new Symbol(variableName)));
            Assert.That(!ReferenceEquals(composedName, groundedClone));
            Assert.That(composedName != groundedClone);
        }

        [TestCase("[x]", "John", "IsPerson","x")]
        public void MakeGround_NameWithNoVariables_Clone(string variableName, string variableValue, params string[] symbolsStrings)
        {
            var substitution = new Substitution(variableName, variableValue);
            var composedName = new ComposedName(symbolsStrings.Select(s => new Symbol(s)));
            var groundedClone = composedName.MakeGround(new List<Substitution> { substitution });

            Assert.That(!ReferenceEquals(composedName, groundedClone));
            Assert.That(composedName == groundedClone);
        }


        [TestCase("IsPerson(John)", "IsPerson", "John")]
        [TestCase("Likes(John, [x])", "Likes", "John","[x]")]
        public void ToString_ComposedName_StringRepresentation(string stringRepresentation, params string[] symbolsStrings)
        {
            var composedName = new ComposedName(symbolsStrings.Select(s => new Symbol(s)));
            Assert.That(stringRepresentation == composedName.ToString());
        }


        [TestCase("IsPerson", "John","IsPerson","John")]
        public void Equals_TwoEqualComposedNames_True(string name1term1, string name1term2, string name2term1, string name2term2)
        {
            var composedName1 = new ComposedName(new Symbol(name1term1), new Symbol(name1term2));
            var composedName2 = new ComposedName(new Symbol(name2term1), new Symbol(name2term2));
            Assert.That(composedName1 == composedName2);
        }

        [TestCase("IsPerson", "John", "IsPerson", "Mary")]
        [TestCase("IsAnimal", "John", "IsPerson", "John")]
        public void Equals_DifferentComposedNames_Fase(string name1term1, string name1term2, string name2term1, string name2term2)
        {
            var composedName1 = new ComposedName(new Symbol(name1term1), new Symbol(name1term2));
            var composedName2 = new ComposedName(new Symbol(name2term1), new Symbol(name2term2));
            Assert.That(composedName1 != composedName2);
        }

        [TestCase("IsPerson", "John", "IsPerson","John","Mary")]
        public void Equals_DifferentLengths_False(string name1term1, string name1term2, string name2term1, string name2term2, string name2term3)
        {
            var composedName1 = new ComposedName(new Symbol(name1term1), new Symbol(name1term2));
            var composedName2 = new ComposedName(new Symbol(name2term1), new Symbol(name2term2), new Symbol(name2term3));
            Assert.That(composedName1 != composedName2);
        }

        [TestCase("IsPerson","John","IsPerson")]
        public void Equals_DifferentTypes_False(string name1term1, string name1term2, string name2term1)
        {
            var composedName1 = new ComposedName(new Symbol(name1term1), new Symbol(name1term2));
            var composedName2 = new Symbol(name2term1);
            Assert.That(composedName1 != composedName2);
        }

        [TestCase("IsPerson", "John", "IsPerson", "John")]
        [TestCase("IsPerson", "John", "IsPerson", Symbol.UNIVERSAL_STRING)]
        [TestCase(Symbol.UNIVERSAL_STRING, "John", "IsPerson", Symbol.UNIVERSAL_STRING)]
        [TestCase(Symbol.UNIVERSAL_STRING, Symbol.UNIVERSAL_STRING, Symbol.UNIVERSAL_STRING, Symbol.UNIVERSAL_STRING)]
        public void Match_MatchingNames_True(string name1term1, string name1term2, string name2term1, string name2term2)
        {
            var composedName1 = new ComposedName(new Symbol(name1term1), new Symbol(name1term2));
            var composedName2 = new ComposedName(new Symbol(name2term1), new Symbol(name2term2));
            Assert.That(composedName1.Match(composedName2));
            Assert.That(composedName2.Match(composedName1));
        }

        [TestCase("IsAnimal", "John", "IsPerson", Symbol.UNIVERSAL_STRING)]
        [TestCase("IsPerson", "John", "IsPerson", "Mary")]
        public void Match_NamesDoNotMatch_False(string name1term1, string name1term2, string name2term1, string name2term2)
        {
            var composedName1 = new ComposedName(new Symbol(name1term1), new Symbol(name1term2));
            var composedName2 = new ComposedName(new Symbol(name2term1), new Symbol(name2term2));
            Assert.That(!composedName1.Match(composedName2));
            Assert.That(!composedName2.Match(composedName1));
        }

        [TestCase("IsPerson", "John", "IsPerson", Symbol.UNIVERSAL_STRING, "x")]
        public void Match_DifferentLenghts_False(string name1term1, string name1term2, string name2term1, string name2term2, string name2term3)
        {
            var composedName1 = new ComposedName(new Symbol(name1term1), new Symbol(name1term2));
            var composedName2 = new ComposedName(new Symbol(name2term1), new Symbol(name2term2), new Symbol(name2term3));
            Assert.That(!composedName1.Match(composedName2));
            Assert.That(!composedName2.Match(composedName1));
        }


        [TestCase("IsPerson", "John")]
        public void Match_DifferentTypes_False(string name1term1, string name1term2)
        {
            var composedName1 = new ComposedName(new Symbol(name1term1), new Symbol(name1term2));
            var composedName2 = new Symbol(name1term1);
            Assert.That(!composedName1.Match(composedName2));
            Assert.That(!composedName2.Match(composedName1));
        }

        

    }
}