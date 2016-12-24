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
        private int unifierReps = 10000;
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
        [TestCase("S([x],k([x],[z]),j([y],k(t(d,l),y)))", "S(t(d,l),k([x],y),j(P,k([x],[z])))", new[] { "[x]/t(d,l)", "[z]/y", "[y]/P" }, Refactorization.Current)]
        [TestCase("S([x],k([x],[z]),j([y],k(t(d,l),y)))", "S(t(d,l),k([x],y),j(P,k([x],[z])))", new[] { "[x]/t(d,l)", "[z]/y", "[y]/P" }, Refactorization.New)]
        [TestCase("S([x])", "S(t(k))", new[] { "[x]/t(k)" }, Refactorization.Current)]
        public void Unify_UnifiableNames_True(string n1, string n2, string[] result, Refactorization r)
        {
            var expectedBindings = result.Select(s => new Substitution(s));
            IEnumerable<Substitution> bindings = null;
            var isUnifiable = false;

            if(r == Refactorization.Current)
            {
                var name1 = Name.BuildName(n1);
                var name2 = Name.BuildName(n2);
                for (int i = 0; i < unifierReps; i++)
                {
                    isUnifiable = Unifier.Unify(name1, name2, out bindings);
                }
            }
            else if(r == Refactorization.New)
            {
                var name1 = new SimpleName(n1);
                var name2 = new SimpleName(n2);

                for (int i = 0; i < unifierReps; i++)
                {
                    isUnifiable = SimpleUnifier.Unify(name1, name2, out bindings);
                }
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


        [TestCase("John", "J", Refactorization.Current)]
        [TestCase("John", "J", Refactorization.New)]
        [TestCase("Strong(John)", "John", Refactorization.Current)]
        [TestCase("Strong(John)", "John", Refactorization.New)]
        [TestCase("[x]([x])", "IsPerson(John)", Refactorization.Current)]
        [TestCase("[x]([x])", "IsPerson(John)", Refactorization.New)]
        [TestCase("[x](John,Paul)", "Friend(John,[x])", Refactorization.Current)]
        [TestCase("[x](John,Paul)", "Friend(John,[x])", Refactorization.New)]
        [TestCase("Like([x],[y])", "Like(John,Strong([y]))", Refactorization.Current)]
        [TestCase("Like([x],[y])", "Like(John,Strong([y]))", Refactorization.New)]
        public void Unify_NonUnifiableNames_False(string n1, string n2, Refactorization r)
        {
            IEnumerable<Substitution> bindings = new List<Substitution>();
            var isUnifiable = true;

            if (r == Refactorization.Current)
            {
                var name1 = Name.BuildName(n1);
                var name2 = Name.BuildName(n2);
                for (int i = 0; i < unifierReps; i++)
                {
                    isUnifiable = Unifier.Unify(name1, name2, out bindings);
                }
            }else if(r == Refactorization.New)
            {
                var name1 = new SimpleName(n1);
                var name2 = new SimpleName(n2);
                for (int i = 0; i < unifierReps; i++)
                {
                    isUnifiable = SimpleUnifier.Unify(name1, name2, out bindings);
                }
            }
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
}