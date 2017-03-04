using System;
using NUnit.Framework;
using WellFormedNames;
using WellFormedNames.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Tests.WellFormedNames
{
    public enum Refactorization
    {
        Current,
        New
    }


    [TestFixture]
    public class SimpleWFNTests
    {
        private int reps = 1;

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
            else if (r == Refactorization.New)
            {
                var name = new SimpleName(nStr);
                for (long i = 0; i < reps; i++)
                {
                    result = SimpleWFN.HasSelf(name);
                }
            }
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("SELF(B,C(D,E([x],D)),[x])", "D", "T", "SELF(B,C(T,E([x],T)),[x])", Refactorization.Current)]
        [TestCase("SELF(B,C(D,E([x],D)),[x])", "D", "T", "SELF(B,C(T,E([x],T)),[x])", Refactorization.New)]
        public void ReplaceLiterals(string n1, string t1, string t2, string expectedResult, Refactorization r)
        {
            string resultStr = string.Empty;

            if(r == Refactorization.Current)
            {
                Name result = null;
                var name = Name.BuildName(n1);
                for (long i = 0; i < reps; i++)
                {
                    result = name.SwapTerms((Name)t1, (Name)t2);
                }
                resultStr = RemoveWhiteSpace(result.ToString());
            }else if(r == Refactorization.New)
            {
                SimpleName result = null;
                var name = new SimpleName(n1);
                for (long i = 0; i < reps; i++)
                {
                    result = SimpleWFN.ReplaceLiterals(name, t1, t2);
                }
                resultStr = result.ToString();
            }

            Assert.That(string.Equals(resultStr, expectedResult, StringComparison.InvariantCultureIgnoreCase));
        }


        
        [TestCase("JoHn", "*", Refactorization.Current)]
        [TestCase("JoHn", "*", Refactorization.New)]
        [TestCase("*", "JoHn", Refactorization.Current)]
        [TestCase("*", "JoHn", Refactorization.New)]
        [TestCase("john", "joHn", Refactorization.Current)]
        [TestCase("john", "JoHn", Refactorization.New)]
        [TestCase("J(*)", "j(A(b))", Refactorization.Current)]
        [TestCase("J(*)", "j(A(b))", Refactorization.New)]
        [TestCase("J(A(B))", "j(*(b))", Refactorization.Current)]
        [TestCase("J(A(B))", "j(*(b))", Refactorization.New)]
        [TestCase("J(A,*,*,B)", "J(*,B,D,B)", Refactorization.Current)]
        [TestCase("J(A,*,*,B)", "J(*,B,D,B)", Refactorization.New)]
        [TestCase("J(A(B,C),*(B,D),*,B)", "J(*,B(B,D),D,B)", Refactorization.Current)]
        [TestCase("J(A(B,C),*(B,D),*,B)", "J(*,B(B,D),D,B)", Refactorization.New)]
        [TestCase("J(*,*(B,D),*,B(d,f))", "J(A(B(C,D)),B(b,D),D,*)", Refactorization.Current)]
        [TestCase("J(*,*(B,D),*,B(d,f))", "J(A(B(C,D)),B(b,D),D,*)", Refactorization.New)]
        public void MatchNames_True(string n1, string n2, Refactorization r)
        {
            bool result = false;

            if(r == Refactorization.Current)
            {
                var name = Name.BuildName(n1);
                var name2 = Name.BuildName(n2);
                for (long i = 0; i < reps; i++)
                {
                    result = name.Match(name2);
                }
            }
            else if (r == Refactorization.New)
            {
                var name = new SimpleName(n1);
                var name2 = new SimpleName(n2);
                for (long i = 0; i < reps; i++)
                {
                    result = SimpleWFN.Match(name, name2);
                }
            }
            Assert.IsTrue(result);
        }

        [TestCase("JoHn", "Jo", Refactorization.Current)]
        [TestCase("JoHn", "Jo", Refactorization.New)]
        [TestCase("Jo", "JoHn", Refactorization.Current)]
        [TestCase("Jo", "JoHn", Refactorization.New)]
        [TestCase("J(*,b)", "j(A(b))", Refactorization.Current)]
        [TestCase("J(*,b)", "j(A(b))", Refactorization.New)]
        [TestCase("J(A(d))", "j(*(b))", Refactorization.Current)]
        [TestCase("J(A(d))", "j(*(b))", Refactorization.New)]
        [TestCase("J(A,*,*,B,D)", "J(*,B,D,B)", Refactorization.Current)]
        [TestCase("J(A,*,*,B,D)", "J(*,B,D,B)", Refactorization.New)]
        [TestCase("J(A(B,C),*(B,D(D)),*,B)", "J(*,B(B,D),D,B)", Refactorization.Current)]
        [TestCase("J(A(B,C),*(B,D(D)),*,B)", "J(*,B(B,D),D,B)", Refactorization.New)]
        [TestCase("A(A(A,A))", "A(A(A),A)", Refactorization.Current)] 
        [TestCase("A(A(A,A))", "A(A(A),A)", Refactorization.New)]
        [TestCase("A(A(A,A),A)", "A(A(A),A)", Refactorization.Current)]
        [TestCase("A(A(A,A),A)", "A(A(A),A)", Refactorization.New)]
        public void MatchNames_False(string n1, string n2, Refactorization r)
        {
            bool result = true;

            if (r == Refactorization.Current)
            {
                var name = Name.BuildName(n1);
                var name2 = Name.BuildName(n2);
                for (long i = 0; i < reps; i++)
                {
                    result = name.Match(name2);
                }
            }
            else if (r == Refactorization.New)
            {
                var name = new SimpleName(n1);
                var name2 = new SimpleName(n2);
                for (long i = 0; i < reps; i++)
                {
                    result = SimpleWFN.Match(name, name2);
                }
            }
            Assert.IsFalse(result);
        }

        [TestCase("SELF(B,[y](D,E([x],D)),[x])","_tag","SELF(B,[y_tag](D,E([x_tag],D)),[x_tag])",Refactorization.New)]
        [TestCase("SELF(B,[y](D,E([x],D)),[x])", "_tag", "SELF(B,[y_tag](D,E([x_tag],D)),[x_tag])", Refactorization.Current)]
        public void Add_Variable_Tag_SimpleWFN(string n1, string tag, string expectedResult, Refactorization r)
        {
            string result = string.Empty;
            if(r == Refactorization.New)
            {
                SimpleName name = new SimpleName(n1);
                SimpleName resName = null;
                for (long i = 0; i < reps; i++)
                {
                    resName = SimpleWFN.AddVariableTag(name, tag);
                }
                result = resName.ToString();
            }
            else if(r == Refactorization.Current)
            {
                Name name = Name.BuildName(n1);
                Name resName = null;
                for (long i = 0; i < reps; i++)
                {
                    resName = name.ReplaceUnboundVariables("_tag");
                }
                result = RemoveWhiteSpace(resName.ToString());
            }

            Assert.AreEqual(expectedResult, result);
        }

      
        [TestCase("  SELF(B(1), [y] (D, E([x],D)),[x])", "SELF(B(1),[y](D,E([x],D)),[x])", Refactorization.Current)]
        [TestCase("  SELF(B(1) , [y] (D, E([x],D)),[x])", "SELF(B(1),[y](D,E([x],D)),[x])", Refactorization.New)]
        public void ToString(string n1, string expectedResult, Refactorization r)
        {
            string result = string.Empty;
            if (r == Refactorization.Current)
            {
                var name = Name.BuildName(n1);
                for (long i = 0; i < reps; i++)
                {
                    result = name.ToString();
                }
                result = RemoveWhiteSpace(result);
            }
            else if (r == Refactorization.New)
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
    }
}