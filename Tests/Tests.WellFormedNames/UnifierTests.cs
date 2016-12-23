using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using WellFormedNames;
using System;
using WellFormedNames.Exceptions;

namespace Tests.WellFormedNames
{
    [TestFixture]
    public class UnifierTests {
        
        //This is still a bit slower in the "Simple" refactorization
        private int unifierReps = 100000;
        [TestCase("John", "John", new string[0], Refactorization.Current)]
        [TestCase("John", "John", new string[0], Refactorization.New)]
        [TestCase("Strong(John,A(B,C(D)))", "Strong(John,A(B,C(*)))", new string[0], Refactorization.Current)]
        [TestCase("Strong(John,A(B,C(D)))", "Strong(John,A(B,C(*)))", new string[0], Refactorization.New)]
        [TestCase("John", "[x]", new[] { "[x]/John" }, Refactorization.Current)]
        [TestCase("John", "[x]", new[] { "[x]/John" }, Refactorization.New)]
        [TestCase("Strong([x])", "Strong(John)", new[] { "[x]/John" }, Refactorization.Current)]
        [TestCase("Strong([x])", "Strong(John)", new[] { "[x]/John" }, Refactorization.New)]
        [TestCase("Likes([x],[y])", "Likes(John, [z])", new[] { "[x]/John", "[y]/[z]" }, Refactorization.Current)]
        [TestCase("Likes([x],[y])", "Likes(John, [z])", new[] { "[x]/John", "[y]/[z]" }, Refactorization.New)]
        [TestCase("Likes([x],John)", "Likes(John, [x])", new[] { "[x]/John" }, Refactorization.Current)]
        [TestCase("Likes([x],John)", "Likes(John, [x])", new[] { "[x]/John" }, Refactorization.New)]
        [TestCase("Likes([x],[y])", "Likes(John, Mary)", new[] { "[x]/John", "[y]/Mary" }, Refactorization.Current)]
        [TestCase("Likes([x],[y])", "Likes(John, Mary)", new[] { "[x]/John", "[y]/Mary" }, Refactorization.New)]
        [TestCase("S([x],k([x],[z]),j([y],k(t(k,l),y)))", "S(t(k,l),k([x],y),j(P,k([x],[z])))", new[] { "[x]/t(k,l)", "[z]/y", "[y]/P" }, Refactorization.Current)]
        [TestCase("S([x],k([x],[z]),j([y],k(t(k,l),y)))", "S(t(k,l),k([x],y),j(P,k([x],[z])))", new[] { "[x]/t(k,l)", "[z]/y", "[y]/P" }, Refactorization.New)]
        [TestCase("S([x])", "S(t(k))", new[] { "[x]/t(k)" }, Refactorization.Current)]
        public void Unify_UnifiableNames_True(string n1, string n2, string[] result, Refactorization r)
        {
            var expectedBindings = result.Select(s => new Substitution(s));
            IEnumerable<Substitution> bindings = null;
            var isUnifiable = false;

            switch (r)
            {
                case Refactorization.Current:
                    var name1 = Name.BuildName(n1);
                    var name2 = Name.BuildName(n2);
                    for (int i = 0; i < unifierReps; i++)
                    {
                        isUnifiable = Unifier.Unify(name1, name2, out bindings);
                    }
                    break;
                case Refactorization.New:
                    var name3 = new SimpleName(n1);
                    var name4 = new SimpleName(n2);

                    for (int i = 0; i < unifierReps; i++)
                    {
                        isUnifiable = SimpleUnifier.Unify(name3, name4, out bindings);
                    }
                    break;
            }
            Assert.That(isUnifiable);
            if (result.Any())
            {
                Assert.That(bindings, Is.EquivalentTo(expectedBindings));
            }
            else
            {
                Assert.That(bindings.Count() == 0);
            }
        }


        [TestCase("John", "J")]
        [TestCase("Strong(John)", "John")]
        [TestCase("[x]([x])", "IsPerson(John)")]
        [TestCase("[x](John,Paul)", "Friend(John,[x])")]
        [TestCase("Like([x],[y])", "Like(John,Strong([y]))")]
        public void Unify_NonUnifiableNames_False(string n1, string n2)
        {
            var name1 = Name.BuildName(n1);
            var name2 = Name.BuildName(n2);
            IEnumerable<Substitution> bindings = new List<Substitution>();
            var isUnifiable = Unifier.Unify(name1, name2, out bindings);
            Assert.That(!isUnifiable);
            Assert.That(bindings == null);
        }


        [TestCase("x", "x(a)", new string[0])]
        [TestCase("x", "[y](a)", new[] { "[y]/x" })]
        [TestCase("x(a)", "x", new string[0])]
        [TestCase("[y](a)", "x", new[] { "[y]/x" })]
        [TestCase("x(a, b)", "x(a, b, c)", new string[0])]
        public void PartialUnify_PartiallyUnifiableNames_True(string n1, string n2, string[] result)
        {
            var name1 = Name.BuildName(n1);
            var name2 = Name.BuildName(n2);
            var expectedBindings = result.Select(s => new Substitution(s));

            IEnumerable<Substitution> bindings = new List<Substitution>();
            var isPartiallyUnifiable = Unifier.PartialUnify(name1, name2, out bindings);

            Assert.That(isPartiallyUnifiable);
            if (result.Any())
            {
                Assert.That(bindings, Is.EquivalentTo(expectedBindings));
            }
            else
            {
                Assert.That(bindings == null);
            }
        }


    }
    [TestFixture]
    public class NameTests
    {
        [TestCase("[y]")]
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
        [TestCase("-")]
        [TestCase("[_x]")]
        [TestCase("[x-93]")]
        [TestCase("Likes([x], 10.7654e10)")]
        [TestCase(Name.UNIVERSAL_STRING)]
        [TestCase(Name.SELF_STRING)]
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
    }
}