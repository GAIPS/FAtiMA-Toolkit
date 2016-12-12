using NUnit.Framework;
using System;
using System.Collections.Generic;
using WellFormedNames;

namespace Tests.WFNv2
{
    [TestFixture]
    public class SimpleWFNTests
    {
        private int reps = 100000;


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
                result = SimpleWFN.MakeGround(name,subs);
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
            var name1 = new SimpleName("AA(BB,C(C1,X([D])),DD(EE,FF),*,II,*(L))");
            var name2 = new SimpleName("AA(BB,C(C1,X([D])),DD(*,FF),GG(HHH),II,K(L))");
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
            var name1 = Name.BuildName("AA(BB,C(C1,X([D])),DD(EE,FF),*,II,*(L))");
            var name2 = Name.BuildName("AA(BB,C(C1,X([D])),DD(*,FF),GG(HHH),II,K(L))");
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
}
