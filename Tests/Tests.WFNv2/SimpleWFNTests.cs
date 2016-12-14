using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using WellFormedNames;

namespace Tests.WFNv2
{
    [TestFixture]
    public class SimpleWFNTests
    {
        private int reps = 10000;


        [Test]
        public void Test_MakeGround_SimpleWFN()
        {
            var nStr = "A(B,C(D,E([x],D)),[x],B(A,B(SELF)))";
            var name = new SimpleName(nStr);
            var expectedResult = "A(B,C(D,E(J(I),D)),J(I),B(A,B(SELF)))";

            SimpleName result = null;

            Dictionary<string, SimpleName> subs = new Dictionary<string, SimpleName>();

            subs["[x]"] = new SimpleName("J(I)");

            for (long i = 0; i < reps; i++)
            {
                result = SimpleWFN.MakeGround(name, subs);
            }

            Assert.AreEqual(expectedResult, result.ToString());
        }


        [Test]
        public void Test_MakeGround_NormalWFN()
        {
            var nStr = "A(B,C(D,E([x],D)),[x],B(A,B(SELF)))";
            var name = Name.BuildName(nStr);
            var expectedResult = "A(B, C(D, E(J(I), D)), J(I), B(A, B(SELF)))"; ;

            Name result = null;

            SubstitutionSet subSet = new SubstitutionSet();
            subSet.AddSubstitution(new Substitution("[x]/J(I)"));

            for (long i = 0; i < reps; i++)
            {
                result = name.MakeGround(subSet);
            }
            Assert.AreEqual(result.ToString(), expectedResult);
        }



        [TestCase("A(B,C(D,E([x],D)),[x],B(A,B(SELF)))")]
        public void Test_HasSelf_SimpleWFN(string nStr)
        {
            var name = new SimpleName(nStr);
            var expectedResult = true;
            bool result = false;

            for (long i = 0; i < reps; i++)
            {
                result = SimpleWFN.HasSelf(name);
            }

            Assert.AreEqual(result, expectedResult);
        }

        [TestCase("A(B,C(D,E([x],D)),[x],B(A,B(SELF)))")]
        public void Test_HasSelf_NormalWFN(string nStr)
        {
            var name = Name.BuildName(nStr);
            var expectedResult = true;
            bool result = false;

            for (long i = 0; i < reps; i++)
            {
                result = name.HasSelf();
            }

            Assert.AreEqual(result, expectedResult);
        }

        [Test]
        public void Test_Replace_Literals_SimpleWFN()
        {
            var name = new SimpleName("SELF(B,C(D,E([x],D)),[x])");
            var expectedResult = new SimpleName("SELF(B,C(T,E([x],T)),[x])");
            SimpleName result = null;

            for (long i = 0; i < reps; i++)
            {
                result = SimpleWFN.ReplaceLiterals(name, "D", "T");
            }

            Assert.That(string.Equals(result.ToString(), expectedResult.ToString(), StringComparison.InvariantCultureIgnoreCase));
        }


        [Test]
        public void Test_Replace_Literals_NormalWFN()
        {
            var name = Name.BuildName("SELF(B,C(D,E([x],D)),[x])");
            var expectedResult = Name.BuildName("SELF(B,C(T,E([x],T)),[x])");
            Name result = null;

            for (long i = 0; i < reps; i++)
            {
                result = name.SwapTerms((Name)"D", (Name)"T");
            }

            Assert.That(string.Equals(result.ToString(), expectedResult.ToString(), StringComparison.InvariantCultureIgnoreCase));
        }


        [Test]
        public void Test_Match_Names_SimpleWFN()
        {
            var name1 = new SimpleName("A");
            var name2 = new SimpleName("A");
            var expectedResult = true;
            bool result = false;

            for (long i = 0; i < reps; i++)
            {
                result = SimpleWFN.Match(name1, name2);
            }

            Assert.AreEqual(result, expectedResult);
        }

        [Test]
        public void Test_Match_Names_NormalWFN()
        {
            var name1 = Name.BuildName("A");
            var name2 = Name.BuildName("A");
            var expectedResult = true;
            bool result = false;

            for (long i = 0; i < reps; i++)
            {
                result = name1.Match(name2);
            }

            Assert.AreEqual(result, expectedResult);
        }

        [Test]
        public void Test_Add_Variable_Tag_SimpleWFN()
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
        public void Test_Add_Variable_Tag_NormalWFN()
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
        public void Test_ReplaceLiterals_SimpleWFN()
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
        public void Test_ReplaceLiterals_NormalWFN()
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

        [Test]
        public void Test_To_String_SimpleWFN()
        {
            var expectedResult = "SELF(B,[y](D,E([x],D)),[x])";
            var name = new SimpleName(expectedResult);
            string test = "";

            for (long i = 0; i < reps; i++)
            {
                test = name.ToString();
            }
            Assert.That(string.Equals(expectedResult, test, StringComparison.InvariantCultureIgnoreCase));
        }

        [Test]
        public void Test_To_String_NameWFN()
        {
            var expectedResult = "SELF(B, [y](D, E([x], D)), [x])";
            var name = Name.BuildName("SELF(B,[y](D,E([x],D)),[x])");
            string test = "";

            for (long i = 0; i < reps; i++)
            {
                test = name.ToString();
            }
            Assert.That(string.Equals(expectedResult, test, StringComparison.InvariantCultureIgnoreCase));
        }
    }

    //i had to copy this from unifiertests cause nUnit was not working in that project for some reason
    [TestFixture]
    public class UnifierTests
    {

        //This is still a bit slower in the "Simple" refactorization
        private int unifierReps = 100000;
        [TestCase("John", "John", new string[0], "Normal")]
        [TestCase("John", "John", new string[0], "Simple")]
        [TestCase("Strong(John,A(B,C(D)))", "Strong(John,A(B,C(*)))", new string[0], "Normal")]
        [TestCase("Strong(John,A(B,C(D)))", "Strong(John,A(B,C(*)))", new string[0], "Simple")]
        [TestCase("John", "[x]", new[] { "[x]/John" }, "Normal")]
        [TestCase("John", "[x]", new[] { "[x]/John" }, "Simple")]
        [TestCase("Strong([x])", "Strong(John)", new[] { "[x]/John" }, "Normal")]
        [TestCase("Strong([x])", "Strong(John)", new[] { "[x]/John" }, "Simple")]
        [TestCase("Likes([x],[y])", "Likes(John, [z])", new[] { "[x]/John", "[y]/[z]" }, "Normal")]
        [TestCase("Likes([x],[y])", "Likes(John, [z])", new[] { "[x]/John", "[y]/[z]" }, "Simple")]
        [TestCase("Likes([x],John)", "Likes(John, [x])", new[] { "[x]/John" }, "Normal")]
        [TestCase("Likes([x],John)", "Likes(John, [x])", new[] { "[x]/John" }, "Simple")]
        [TestCase("Likes([x],[y])", "Likes(John, Mary)", new[] { "[x]/John", "[y]/Mary" }, "Normal")]
        [TestCase("Likes([x],[y])", "Likes(John, Mary)", new[] { "[x]/John", "[y]/Mary" }, "Simple")]
        [TestCase("S([x],k([x],[z]),j([y],k(t(k,l),y)))", "S(t(k,l),k([x],y),j(P,k([x],[z])))", new[] { "[x]/t(k,l)", "[z]/y", "[y]/P" }, "Normal")]
        [TestCase("S([x],k([x],[z]),j([y],k(t(k,l),y)))", "S(t(k,l),k([x],y),j(P,k([x],[z])))", new[] { "[x]/t(k,l)", "[z]/y", "[y]/P" }, "Simple")]
        [TestCase("S([x])", "S(t(k))", new[] { "[x]/t(k)"}, "Normal")]
        public void Unify_UnifiableNames_True(string n1, string n2, string[] result, string refactorization)
        {
            var expectedBindings = result.Select(s => new Substitution(s));
            IEnumerable<Substitution> bindings = null;
            var isUnifiable = false;

            if(refactorization == "Simple")
            {
                var name1 = new SimpleName(n1);
                var name2 = new SimpleName(n2);
                for (int i = 0; i < unifierReps; i++)
                {
                    isUnifiable = SimpleUnifier.Unify(name1, name2, out bindings);
                }
            }
            else
            {
                var name1 = Name.BuildName(n1);
                var name2 = Name.BuildName(n2);
                for (int i = 0; i < unifierReps; i++)
                {
                    isUnifiable = Unifier.Unify(name1, name2, out bindings);
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
