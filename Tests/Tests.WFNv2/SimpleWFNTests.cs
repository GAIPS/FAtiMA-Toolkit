using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using WellFormedNames;

namespace Tests.WFNv2
{
    public enum Refactorization
    {
        Current,
        New
    }

    [TestFixture]
    public class SimpleWFNTests
    {
        private int reps = 10000;
        
        [TestCase("A(B,C(D,E([x],D)),[x],B(A,B(SELF)))", "[x]", "J(I)", "A(B,C(D,E(J(I),D)),J(I),B(A,B(SELF)))", Refactorization.New)]
        public void MakeGround_GroundedName(string n1, string var, string sub, string expectedResult, Refactorization r)
        {
            String result = string.Empty;

            if (r == Refactorization.New)
            {
                var name = new SimpleName(n1);
                SimpleName nameResult = null;
                Dictionary<string, SimpleName> subs = new Dictionary<string, SimpleName>();
                subs[var] = new SimpleName(sub);
                for (long i = 0; i < reps; i++)
                {
                    nameResult = SimpleWFN.MakeGround(name, subs);
                }
                result = nameResult.ToString();
            }
            else if (r == Refactorization.Current)
            {
                var name = Name.BuildName(n1);
                Name nameResult = null;
                SubstitutionSet subSet = new SubstitutionSet();
                subSet.AddSubstitution(new Substitution("[x]/J(I)"));
                for (long i = 0; i < reps; i++)
                {
                    nameResult = name.MakeGround(subSet);
                }
            }
            Assert.AreEqual(expectedResult, result);
        }


        [TestCase("A(B,C(D,E([x],D)),[x],B(A,B(SELF)))", true, Refactorization.Current)]
        [TestCase("A(B,C(D,E([x],D)),[x],B(A,B(SELF)))", true, Refactorization.New)]
        public void HasSelf(string nStr, bool expectedResult, Refactorization r)
        {
            bool result = false;
            if (r == Refactorization.Current)
            {
                var name = Name.BuildName(nStr);
                for (long i = 0; i < reps; i++)
                {
                    result = name.HasSelf();
                }
            }
            else if(r == Refactorization.New)
            {
                var name = new SimpleName(nStr);
                for (long i = 0; i < reps; i++)
                {
                    result = SimpleWFN.HasSelf(name);
                }
            }
            Assert.AreEqual(expectedResult,result);
        }

        [TestCase("SELF(B,C(D,E([x],D)),[x])","D","T","SELF(B,C(T,E([x],T)),[x])", Refactorization.Current)]
        [TestCase("SELF(B,C(D,E([x],D)),[x])", "D", "T", "SELF(B,C(T,E([x],T)),[x])", Refactorization.New)]
        public void ReplaceLiterals(string n1, string t1, string t2, string expectedResult, Refactorization r)
        {
            string resultStr = string.Empty;
            switch (r)
            {
                case Refactorization.Current:
                    Name result1 = null;
                    var name1 = Name.BuildName(n1);
                    for (long i = 0; i < reps; i++)
                    {
                        result1 = name1.SwapTerms((Name)t1, (Name)t2);
                    }
                    resultStr = result1.ToString();
                    break;
                case Refactorization.New:
                    SimpleName result2 = null;
                    var name2 = new SimpleName(n1);
                    for (long i = 0; i < reps; i++)
                    {
                        result2 = SimpleWFN.ReplaceLiterals(name2, t1, t2);
                    }
                    resultStr = result2.ToString();
                    break;
            }

            Assert.That(string.Equals(resultStr, expectedResult, StringComparison.InvariantCultureIgnoreCase));
        }


        [TestCase("John", "John", Refactorization.Current)]
        [TestCase("John", "John", Refactorization.New)]
        [TestCase("J(A,*,*,B)", "J(*,B,D,B)", Refactorization.Current)]
        [TestCase("J(A,*,*,B)", "J(*,B,D,B)", Refactorization.New)]
        [TestCase("J(A(B,C),*(B,D),*,B)", "J(*,B(B,D),D,B)", Refactorization.Current)]
        [TestCase("J(A(B,C),*(B,D),*,B)", "J(*,B(B,D),D,B)", Refactorization.New)]
        public void MatchNames_True(string n1, string n2, Refactorization r)
        {
            bool result = false;

            switch (r)
            {
                case Refactorization.Current:
                    var name1 = Name.BuildName(n1);
                    var name2 = Name.BuildName(n2);
                    for (long i = 0; i < reps; i++)
                    {
                        result = name1.Match(name2);
                    }
                    break;
                case Refactorization.New:
                    var name3 = new SimpleName(n1);
                    var name4 = new SimpleName(n2);
                    for (long i = 0; i < reps; i++)
                    {
                        result = SimpleWFN.Match(name3, name4);
                    }
                    break;
            }
            Assert.IsTrue(result);
        }
        
        [Test]
        public void Add_Variable_Tag_SimpleWFN()
        {
            var name = new SimpleName("SELF(B,[y](D,E([x],D)),[x])");
            var expectedResult = new SimpleName("SELF(B,[y_tag](D,E([x_tag],D)),[x_tag])");
            SimpleName result = null;

            for (long i = 0; i < reps; i++)
            {
                result = SimpleWFN.AddVariableTag(name, "_tag");
            }

            Assert.That(string.Equals(result.ToString(), expectedResult.ToString(), StringComparison.InvariantCultureIgnoreCase));
        }

        [Test]
        public void Add_Variable_Tag_NormalWFN()
        {
            var name = Name.BuildName("SELF(B,[y](D,E([x],D)),[x])");
            var expectedResult = Name.BuildName("SELF(B,[y_tag](D,E([x_tag],D)),[x_tag])");
            Name result = null;

            for (long i = 0; i < reps; i++)
            {
                result = name.ReplaceUnboundVariables("_tag");
            }

            Assert.That(string.Equals(result.ToString(), expectedResult.ToString(), StringComparison.InvariantCultureIgnoreCase));
        }

        [Test]
        public void ReplaceLiterals_SimpleWFN()
        {
            var name = new SimpleName("SELF(B,[x](D,B([x],B)),[b])");
            var expectedResult = "SELF(D,[x](D,D([x],D)),[b])";
            SimpleName test = null;

            for (long i = 0; i < reps; i++)
            {
                test = SimpleWFN.ReplaceLiterals(name, "B", "D");
            }
            Assert.That(string.Equals(expectedResult, test.ToString(), StringComparison.InvariantCultureIgnoreCase));
        }

        [Test]
        public void ReplaceLiterals_NormalWFN()
        {
            var name = Name.BuildName("SELF(B,[x](D,B([x],B)),[b])");
            var expectedResult = "SELF(D, [x](D, D([x], D)), [b])";
            Name test = null;

            for (long i = 0; i < reps; i++)
            {
                test = name.SwapTerms((Name)"B", (Name)"D");
            }

            Assert.That(string.Equals(expectedResult, test.ToString(), StringComparison.InvariantCultureIgnoreCase));
        }

        [TestCase("  SELF(B(1), [y] (D, E([x],D)),[x])","SELF(B(1),[y](D,E([x],D)),[x])",Refactorization.Current)]
        [TestCase("  SELF(B(1) , [y] (D, E([x],D)),[x])","SELF(B(1),[y](D,E([x],D)),[x])",Refactorization.New)]
        public void ToString(string n1, string expectedResult, Refactorization r)
        {
            string result = string.Empty;
            if(r == Refactorization.Current)
            {
                var name = Name.BuildName(n1);
                for (long i = 0; i < reps; i++)
                {
                    result = name.ToString();
                }
                result = RemoveWhiteSpace(result);
            }
            else if(r == Refactorization.New)
            {
                var name = new SimpleName(n1);
                for (long i = 0; i < reps; i++)
                {
                    result = name.ToString();
                }
            }
            Assert.That(string.Equals(expectedResult, result, StringComparison.InvariantCultureIgnoreCase));
        }

        private static string RemoveWhiteSpace(string input)
        {
            return new string(input.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray());
        }
    }

    //i had to copy this from unifiertests cause nUnit was not working in that project for some reason
    [TestFixture]
    public class UnifierTests
    {

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
        [TestCase("S([x])", "S(t(k))", new[] { "[x]/t(k)"}, Refactorization.Current)]
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
}
