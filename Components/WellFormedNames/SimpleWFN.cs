using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Utilities;

namespace WellFormedNames
{
    public enum LiteralType
    {
        Root,
        Param,
    }

    public class Literal
    {
        public string description;
        public readonly LiteralType type;
        public int depth;

        public Literal(string desc, LiteralType t, int depth)
        {
            this.description = desc;
            this.type = t;
            this.depth = depth;
        }
    }


    public class SimpleName
    {
        public List<Literal> literals;

        public SimpleName(IEnumerable<Literal> literalList)
        {
            this.literals = new List<Literal>();
            var delta = literalList.FirstOrDefault().depth;
            foreach (var l in literalList)
            {
                this.literals.Add(new Literal(l.description, l.type, l.depth - delta));
            }
        }

        public SimpleName(string description)
        {
            this.literals = new List<Literal>();
            description = RemoveWhiteSpace(description);
            var n = Name.BuildName(description); //Substituir

            if (n.IsComposed)
            {
                char[] literalLimiterChars = { ' ', ',', '(', ')' };
                string[] literals = description.Split(literalLimiterChars, StringSplitOptions.RemoveEmptyEntries);

                var lPos = 0;
                var depth = 0;
                for (int i = 0; i < description.Length; i++)
                {
                    //add literal
                    switch (description[i])
                    {
                        case '(':
                            this.literals.Add(new Literal(literals[lPos], LiteralType.Root, depth));
                            lPos++;
                            depth++;
                            break;
                        case ',':
                            if (description[i - 1] != ')')
                            {
                                this.literals.Add(new Literal(literals[lPos], LiteralType.Param, depth));
                                lPos++;
                            }
                            break;
                        case ')':
                            if (description[i - 1] != ')')
                            {
                                this.literals.Add(new Literal(literals[lPos], LiteralType.Param, depth));
                                lPos++;
                            }
                            depth--;
                            break;
                    }
                }
            }
            else //if n is non composed
            {
                this.literals.Add(new Literal(description, LiteralType.Param, 0));
            }
        }

        public override string ToString()
        {

            if (this.literals.Count == 1)
                return this.literals.First().description;

            var litArray = literals.ToArray();
            var builder = ObjectPool<StringBuilder>.GetObject();
            builder.Append(litArray[0].description + "(" + litArray[1].description);
            for (int i = 2; i < litArray.Length; i++)
            {
                ToStringHelper(builder, litArray[i - 1], litArray[i]);
            }
            builder.Append(')', litArray[litArray.Length - 1].depth);
            string result = builder.ToString();
            builder.Length = 0;
            ObjectPool<StringBuilder>.Recycle(builder);
            return result;
        }

        private void ToStringHelper(StringBuilder sb, Literal previous, Literal current)
        {
            if (previous.type == LiteralType.Root)
            {
                sb.Append("(" + current.description);
            }
            if (previous.type == LiteralType.Param && current.type == LiteralType.Param)
            {
                sb.Append(')', previous.depth - current.depth);
                sb.Append("," + current.description);
            }
            if (previous.type == LiteralType.Param && current.type == LiteralType.Root)
            {
                sb.Append(')', previous.depth - current.depth);
                sb.Append("," + current.description);
            }
        }

        private static string RemoveWhiteSpace(string input)
        {
            return new string(input.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray());
        }


    }



    /// <summary>
	/// This is an experiment to try and reproduce the same functionality of the WFN project but without using objects that have behavior.
	/// </summary>
	/// <remarks>
    public class SimpleWFN
    {
        public static int GetNumberOfTerms(SimpleName n)
        {
            return n.literals.Where(l => l.depth <= 1).Count();
        }

        /// <summary>
        /// Verifies if a specific variable is contained inside this Name.
        /// </summary>
        /// <param name="variable">The variable Name we want to verify</param>
        /// <exception cref="ArgumentException">Thrown if the given argument is not a variable definition.</exception>
        public static bool ContainsVariable(SimpleName name, string variable)
        {
            var v = Name.BuildName(variable);
            if (!v.IsVariable)
                throw new ArgumentException("Invalid variable", variable);
            return GetVariables(name).Any(l => l.description == variable);
        }

        public static bool ContainsUniversal(SimpleName name)
        {
            return name.literals.Any(l => l.description == Name.UNIVERSAL_STRING);
        }

        public static IEnumerable<Literal> GetVariables(SimpleName name)
        {
            return name.literals.Where(l => l.description[0] == '[');
        }

        public static bool HasSelf(SimpleName name)
        {
            return name.literals.Any(l => l.description == Name.SELF_STRING);
        }

        public static SimpleName ReplaceLiterals(SimpleName name, string oldLit, string newLit)
        {
            var clone = new SimpleName(name.literals);
            foreach (var l in clone.literals.Where(l => l.description == oldLit))
            {
                l.description = newLit;
            }
            return clone;
        }

        public static SimpleName AddVariableTag(SimpleName name, string tag)
        {
            var clone = new SimpleName(name.literals);
            foreach (var v in GetVariables(clone))
            {
                v.description = v.description.Replace("]", tag + "]");
            }
            return clone;
        }

        public static SimpleName RemoveVariableTag(SimpleName name, string tag)
        {
            var clone = new SimpleName(name.literals);
            foreach (var v in GetVariables(clone))
            {
                v.description = v.description.Replace(tag + "]", "]");
            }
            return clone;
        }


        //not really needed for now but might be useful for building sub names
        public static SimpleName BuildNameFromNLiteral(SimpleName name, int n)
        {
            if (name.literals[n].type != LiteralType.Root)
                return new SimpleName(name.literals[n].description);

            var list = name.literals.Skip(n + 1).TakeWhile(l => l.depth > name.literals[n].depth);
            return new SimpleName(list.Prepend(name.literals[n]));
        }

        //not really needed for now but might be useful for building sub names
        public static List<SimpleName> GetAllComposedNames(SimpleName name)
        {
            List<SimpleName> res = new List<SimpleName>();

            for (int i = 1; i < name.literals.Count; i++)
            {
                if (name.literals.ElementAt(i).type == LiteralType.Root)
                {
                    res.Add(BuildNameFromNLiteral(name, i));
                }
            }
            return res;
        }


        public static int FindJumpUntilDepthN(Literal[] array, int currentPos, int depthN)
        {
           for(int i = currentPos + 1; i < array.Length; i++)
           {
                if(array[i].depth <= depthN)
                {
                    return i - currentPos;
                }
           }
           //If the codes reaches here the jump will be to the end of the array
           return array.Length - currentPos; 
        }

        //A bit more complicated but 3x faster than the old Match method
        public static bool Match(SimpleName n1, SimpleName n2)
        {
            var arrayLit1 = n1.literals.ToArray();
            var arrayLit2 = n2.literals.ToArray();
            var idx1 = 0;
            var idx2 = 0;
            do
            {
                var l1 = arrayLit1[idx1];
                var l2 = arrayLit2[idx2];

                //auxiliary variables
                bool typesMatch = l1.type == l2.type;
                bool l1IsUniv = l1.description == Name.UNIVERSAL_STRING;
                bool l2IsUniv = l2.description == Name.UNIVERSAL_STRING;
                bool existsUniversal = l1IsUniv || l2IsUniv;

                //The easy matching scenario is when both types match
                if (typesMatch)
                {
                    if (existsUniversal || l1.description == l2.description)
                    {
                        idx1++; idx2++;
                        continue;
                    }
                    return false; //no universals and different descritions
                }

                //eg. matching S with S(A)
                if (!typesMatch && !existsUniversal)
                {
                    return false;
                }

                //l1 is a universal and l2 has a different type
                if (!typesMatch && l1IsUniv)
                {
                    //l1 is a universal parameter and n2 is a root
                    if (l1.type == LiteralType.Param)
                    {
                        // the index on n2 must jump until it reaches the depth of l1 again
                        idx2 += FindJumpUntilDepthN(arrayLit2, idx2, l1.depth);
                        idx1++;
                        continue;
                    }
                    //a root universal never matches any parameter except a universal
                    if (l1.type == LiteralType.Root && !l2IsUniv)
                        return false;
                }

                //the last case is when l2 is the universal
                if (!typesMatch && l2IsUniv)
                {
                    if (l2.type == LiteralType.Param)
                    {
                        // the index on n2 must jump until it reaches the depth of n1 again
                        idx1 += FindJumpUntilDepthN(arrayLit1, idx1, l2.depth);
                        idx2++;
                        continue;
                    }
                    //a root universal never matches any parameter except a universal
                    if (l2.type == LiteralType.Root && !l1IsUniv)
                        return false;
                }
            } while (idx1 < arrayLit1.Length && idx2 < arrayLit2.Length);
     
            if (idx1 == arrayLit1.Length && idx2 == arrayLit2.Length)
            {
                return true; // full match
            }
            else{
                return false; // only partial match
            }
        }
       
        public static bool MatchDescription(Literal n1, Literal n2)
        {
            if (n1.description == Name.UNIVERSAL_STRING ||
                n2.description == Name.UNIVERSAL_STRING)
                return true;

            return n1.description == n2.description;
        }

     
     
        //needs input validation
        public static SimpleName MakeGround(SimpleName n, Dictionary<string, string> bindings)
        {
            var nameClone = new SimpleName(n.literals);
            List<int> positionsNeedDepthFix = new List<int>();

            foreach (var var in GetVariables(nameClone))
            {
                if (bindings.ContainsKey(var.description))
                {
                    var nameValue = bindings[var.description];
                    if (nameValue.Contains('(')) //replace this with a method
                    {
                        int i = n.literals.FindIndex(l => l == var);
                        positionsNeedDepthFix.Add(i);
                    }
                    var.description = nameValue;
                }

            }

            //still needs position fix
            return nameClone;
        }



        #region Unecessary Methods
        //public abstract Name GetFirstTerm();
        //public abstract IEnumerable<Name> GetTerms();
        //public abstract Name GetNTerm(int index);
        #endregion
    }

}
