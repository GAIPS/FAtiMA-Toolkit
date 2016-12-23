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
        internal List<Literal> literals;
        internal bool isGrounded;

        public SimpleName(IEnumerable<Literal> literalList)
        {
            this.literals = new List<Literal>();
            this.isGrounded = true;

            // used to reset the depth of the first literal to 0
            var delta = literalList.FirstOrDefault().depth; 

            foreach (var l in literalList)
            {
                this.literals.Add(new Literal(l.description, l.type, l.depth - delta));
                if (SimpleWFN.IsVariable(l))
                {
                    this.isGrounded = false;
                }
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
            return GetVariables(name).Any(l => l.description.EqualsIgnoreCase(variable));
        }

        public static bool ContainsUniversal(SimpleName name)
        {
            return name.literals.Any(l => l.description == Name.UNIVERSAL_STRING);
        }

        public static IEnumerable<Literal> GetVariables(SimpleName name)
        {
            return name.literals.Where(l => IsVariable(l));
        }

        public static bool IsVariable(Literal literal)
        {
            return literal.description[0] == '[';
        }

        public static bool IsGrounded(SimpleName name)
        {
            return name.isGrounded;
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

        public static SimpleName BuildNameFromContainedLiteral(SimpleName name, Literal literal)
        {
           if (literal.type != LiteralType.Root)
                return new SimpleName(new Literal[] { literal });

            var i = name.literals.IndexOf(literal);
            var list = name.literals.Skip(i+1).TakeWhile(l => l.depth > literal.depth);
            return new SimpleName(list.Prepend(literal));
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

               
        public static bool Match(SimpleName n1, SimpleName n2)
        {
            var idx1 = n1.literals.GetEnumerator();
            var idx2 = n2.literals.GetEnumerator();
            while(idx1.MoveNext() && idx2.MoveNext())
            {
                var l1 = idx1.Current; var l2 = idx2.Current;

                //auxiliary variables
                bool typesMatch = l1.type == l2.type;
                bool l1IsUniv = l1.description == Name.UNIVERSAL_STRING;
                bool l2IsUniv = l2.description == Name.UNIVERSAL_STRING;
                bool existsUniversal = l1IsUniv || l2IsUniv;

                //The easy matching scenario is when both types match
                if (typesMatch)
                {
                    if (existsUniversal || l1.description.EqualsIgnoreCase(l2.description))
                    {
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
                        var jump = FindJumpUntilDepthN(n2.literals, n2.literals.IndexOf(l2), l1.depth);
                        for(int i = 0; i < jump - 1; i++)
                        {
                            idx2.MoveNext();
                        }
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
                        // the index on n1 must jump until it reaches the depth of l2 again
                        var jump = FindJumpUntilDepthN(n1.literals, n1.literals.IndexOf(l1), l2.depth);
                        for (int i = 0; i < jump - 1; i++)
                        {
                            idx1.MoveNext();
                        }
                        continue;
                    }
                    //a root universal never matches any parameter except a universal
                    if (l2.type == LiteralType.Root && !l1IsUniv)
                        return false;
                }
            } 

            if (idx1.MoveNext() || idx2.MoveNext())
            {
                return false; // only partial match
            }
            else
            {
                return true; 
            }
        }

        private static int FindJumpUntilDepthN(List<Literal> literals, int currentPos, int depthN)
        {
            for (int i = currentPos + 1; i < literals.Count; i++)
            {
                if (literals.ElementAt(i).depth <= depthN)
                {
                    return i - currentPos;
                }
            }
            //If the codes reaches here the jump will be to the end of the array
            return literals.Count - currentPos;
        }


        public static bool MatchDescription(Literal n1, Literal n2)
        {
            if (n1.description == Name.UNIVERSAL_STRING ||
                n2.description == Name.UNIVERSAL_STRING)
                return true;

            return n1.description == n2.description;
        }


        public static SimpleName MakeGround(SimpleName n, Dictionary<string, SimpleName> bindings)
        {
            var literals = new List<Literal>();

            foreach (var l in n.literals)
            {
                if (bindings.ContainsKey(l.description))
                {
                    var nameValue = bindings[l.description];
                    var subName = new SimpleName(nameValue.literals);
                    foreach(var subL in subName.literals)
                    {
                        subL.depth += l.depth;
                    }
                    literals.AddRange(subName.literals);
                }
                else
                {
                    literals.Add(l);
                }
            }
            return new SimpleName(literals);
        }
    
        
    }

    public static class SimpleUnifier
    {
        /// <summary>
        /// Unifying Method, receives two WellFormedNames and tries 
        /// to find a list of Substitutions that will make 
        /// both names syntatically equal. The algorithm performs Occur Check,
        /// as such the unification of [X] and Luke([X]) will allways fail.
        /// 
        /// The method goes on each symbol (for both names) at a time, and tries to find 
        /// a substitution between them. Take into account that the Unification between
        /// [X](John,Paul) and Friend(John,[X]) fails because the algorithm considers [X]
        /// to be the same variable
        /// </summary>
        /// <see cref="FAtiMA.Core.WellFormedNames.Substitution"/>
        /// <see cref="FAtiMA.Core.WellFormedNames.Name"/>
        /// <param name="name1">The first Name</param>
        /// <param name="name2">The second Name</param>
        /// <param name="bindings">The out paramenter for the founded substitutions</param>
        /// <returns>True if the names are unifiable, in this case the bindings list will contain the found Substitutions, otherwise it will be empty</returns>
        public static bool Unify(SimpleName name1, SimpleName name2, out IEnumerable<Substitution> bindings)
        {
            bindings = null;
            if (name1 == null || name2 == null)
                return false;

            if (SimpleWFN.IsGrounded(name1) && SimpleWFN.IsGrounded(name2))
            {
                if (SimpleWFN.Match(name1, name2))
                {
                    bindings = Enumerable.Empty<Substitution>();
                    return true;
                }
                else   {  return false; }
            }

            bindings = FindSubst(name1, name2);
            return bindings != null;
        }

        private static IEnumerable<Substitution> FindSubst(SimpleName n1, SimpleName n2)
        {
            SubstitutionSet bindings = new SubstitutionSet();
            
            var arrayLit1 = n1.literals.ToArray();
            var arrayLit2 = n2.literals.ToArray();
            var idx1 = 0;
            var idx2 = 0;

            do
            {
                var l1 = arrayLit1[idx1];
                var l2 = arrayLit2[idx2];
                //neither literal is a variable
                if (!SimpleWFN.IsVariable(l1) && !SimpleWFN.IsVariable(l2))
                {
                    if (l1.description.EqualsIgnoreCase(l2.description))
                    {
                        idx1++; idx2++;
                        continue;
                    }
                    else { return null; }
                }
                //both literals are a variable
                if (SimpleWFN.IsVariable(l1) && SimpleWFN.IsVariable(l2))
                {
                    if (!l1.description.EqualsIgnoreCase(l2.description) &&
                         !bindings.AddSubstitution(
                             new Substitution(l1.description, l2.description)))
                        return null;

                    idx1++; idx2++;
                    continue;
                }
                //only l1 is a variable 
                if (SimpleWFN.IsVariable(l1) && !SimpleWFN.IsVariable(l2))
                {
                    var res = FindSubsAux(l1, l2, n2, bindings);
                    if (res == -1) return null;
                    else { idx1++; idx2 += res; continue; }
                }
                //only l2 is a variable 
                if (!SimpleWFN.IsVariable(l1) && SimpleWFN.IsVariable(l2))
                {
                    var res = FindSubsAux(l2, l1, n1, bindings);
                    if (res == -1) return null;
                    else { idx1 += res; idx2++; continue; }
                }
                throw new Exception("Unexpected Situation");
            } while (idx1 < arrayLit1.Length && idx2 < arrayLit2.Length);

            if (idx1 == arrayLit1.Length && idx2 == arrayLit2.Length)
            {
                return bindings; // full match
            }
            else
            {
                return null; // only partial match
            };
        }


        //return the idx jump on the valName or -1 if it can't add the substitution
        private static int FindSubsAux(Literal var, Literal val, SimpleName valName, SubstitutionSet bindings)
        {
            if (val.type == LiteralType.Root)
            {
                var n = SimpleWFN.BuildNameFromContainedLiteral(valName, val);
                if (!bindings.AddSubstitution(new Substitution(var.description, n.ToString())))
                    return -1;
                else return n.literals.Count;
            }
            else //l2 is a parameter
            {
                if (!bindings.AddSubstitution(new Substitution(var.description, val.description)))
                    return -1;
                else return 1;
            }
        }
        /*
        private static Dictionary<string, SimpleName> SubstitutionSetConverter(SubstitutionSet bindings)
        {
            var result = new Dictionary<string, SimpleName>();

            foreach (var b in bindings)
            {
                result.Add(b.Variable.ToString(), new SimpleName(b.Variable.ToString()));
            }
            return result;
        }*/

    }
        

}
