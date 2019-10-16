using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Utilities;
using WellFormedNames.Exceptions;

namespace WellFormedNames
{
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
            this.isGrounded = true;
            description = description.RemoveWhiteSpace();
            var n = Name.BuildName(description); //Substituir

            if (n.IsComposed)
            {
                char[] literalLimiterChars = { ' ', ',', '(', ')' };
                string[] stringLiterals = description.Split(literalLimiterChars, StringSplitOptions.RemoveEmptyEntries);

                var lPos = 0;
                var depth = 0;
                for (int i = 0; i < description.Length; i++)
                {
                    //add literal
                    switch (description[i])
                    {
                        case '(':
                            this.literals.Add(new Literal(stringLiterals[lPos], LiteralType.Root, depth));
                            lPos++;
                            depth++;
                            break;
                        case ',':
                            if (description[i - 1] != ')')
                            {
                                this.literals.Add(new Literal(stringLiterals[lPos], LiteralType.Param, depth));
                                lPos++;
                            }
                            break;
                        case ')':
                            if (description[i - 1] != ')')
                            {
                                this.literals.Add(new Literal(stringLiterals[lPos], LiteralType.Param, depth));
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

            //check if grounded
            foreach (var l in this.literals)
            {
                if (SimpleWFN.IsVariable(l))
                {
                    this.isGrounded = false;
                }
            }
        }

        public override string ToString()
        {

            if (this.literals.Count == 1)
                return this.literals.First().description;

            var litArray = literals.ToArray();
            var builder = new StringBuilder();
            builder.Append(litArray[0].description + "(" + litArray[1].description);
            for (int i = 2; i < litArray.Length; i++)
            {
                ToStringHelper(builder, litArray[i - 1], litArray[i]);
            }
            builder.Append(')', litArray[litArray.Length - 1].depth);
            
            return builder.ToString(); 
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


        public static int GetNumberOfTerms(SimpleName name)
        {
            return name.literals.Count(l => l.depth <= 1);
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
            
        private static int FindJumpUntilDepthN(IList<Literal> list, int currentPos, int depthN)
        {
            for (int i = currentPos + 1; i < list.Count; i++)
            {
                if (list[i].depth <= depthN)
                {
                    return i - currentPos;
                }
            }
            //If the codes reaches here the jump will be to the end of the array
            return list.Count - currentPos;
        }

     
        public static bool Match(SimpleName n1, SimpleName n2)
        {
            if (SimpleWFN.GetNumberOfTerms(n1) != SimpleWFN.GetNumberOfTerms(n2))
                return false;

            var idx1 = 0;
            var idx2 = 0;
            do
            {
                var l1 = n1.literals[idx1];
                var l2 = n2.literals[idx2];

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
                        idx2 += FindJumpUntilDepthN(n2.literals, idx2, l1.depth);
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
                        idx1 += FindJumpUntilDepthN(n1.literals, idx1, l2.depth);
                        idx2++;
                        continue;
                    }
                    //a root universal never matches any parameter except a universal
                    if (l2.type == LiteralType.Root && !l1IsUniv)
                        return false;
                }
            } while (idx1 < n1.literals.Count && idx2 < n2.literals.Count);

            if (idx1 == n1.literals.Count && idx2 == n2.literals.Count)
            {
                return true; // full match
            }
            else
            {
                return false; // only partial match
            }
        }
        public static bool MatchDescription(Literal n1, Literal n2)
        {
            if (n1.description == Name.UNIVERSAL_STRING ||
                n2.description == Name.UNIVERSAL_STRING)
                return true;

            return n1.description.EqualsIgnoreCase(n2.description);
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
            // SubstitutionSet bindings = new SubstitutionSet();
            Dictionary<string, Substitution> bindings = new Dictionary<string, Substitution>();
            var idx1 = 0;
            var idx2 = 0;

            do
            {
                var l1 = n1.literals[idx1];
                var l2 = n2.literals[idx2];
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
                    if (!l1.description.EqualsIgnoreCase(l2.description))
                    {
                        if (bindings.ContainsKey(l1.description))
                            return null;
                        bindings[l1.description] = new Substitution(l1.description, l2.description);
                    }    
                    idx1++; idx2++;
                    continue;
                }
                //only l1 is a variable 
                if (SimpleWFN.IsVariable(l1) && !SimpleWFN.IsVariable(l2))
                {
                    var res = FindSubsAux(l1, l2, n2, bindings);
                    if (res == -1)
                        return null;
                    else { idx1++; idx2 += res; continue; }
                }
                //only l2 is a variable 
                if (!SimpleWFN.IsVariable(l1) && SimpleWFN.IsVariable(l2))
                {
                    var res = FindSubsAux(l2, l1, n1, bindings);
                    if (res == -1)
                        return null;
                    else { idx1 += res; idx2++; continue; }
                }
                throw new Exception("Unexpected Situation");
            } while (idx1 < n1.literals.Count && idx2 < n2.literals.Count);

            if (idx1 == n1.literals.Count && idx2 == n2.literals.Count)
            {
                return bindings.Values; // full match
            }
            else
            {
                return null; // only partial match
            };
        }


        //return the idx jump on the valName or -1 if it can't add the substitution
        private static int FindSubsAux(Literal var, Literal val, SimpleName valName, IDictionary<string, Substitution> bindings)
        {
            //first, analyse if the value is a composed name or a single parameter 
            string valDescription;
            int valLiteralCount;
            if (val.type == LiteralType.Root)
            {
                SimpleName auxName = null;
                auxName = SimpleWFN.BuildNameFromContainedLiteral(valName, val);
                valDescription = auxName.ToString();
                valLiteralCount = auxName.literals.Count;
            }
            else
            {
                valDescription = val.description;
                valLiteralCount = 1;
            }
            
            //check if a binding for the same variable already exists
            Substitution existingSub = null;
            bindings.TryGetValue(var.description, out existingSub);
            if (existingSub != null)
            {
                if (existingSub.SubValue.ToString().RemoveWhiteSpace().EqualsIgnoreCase(valDescription))
                    return valLiteralCount;
                else return -1;
            }
            //if there was no existing binding to the variable
            try
            {
                bindings[var.description] = new Substitution(var.description, valDescription);
                return valLiteralCount;
            }
            catch (BadSubstitutionException)
            {
                return -1;
            }
        }
    }
        

}
