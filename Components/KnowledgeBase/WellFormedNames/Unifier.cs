using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace KnowledgeBase.WellFormedNames
{
	/// <summary>
	/// Static Class that implements the Unifying algorithm
	/// 
	/// @author: João Dias
	/// @author: Pedro Gonçalves (C# version)
	/// </summary>
	public static class Unifier
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
		public static bool Unify(Name name1, Name name2, out IEnumerable<Substitution> bindings)
		{
			bindings = null;
			if (name1 == null || name2 == null)
				return false;

			if (name1.IsGrounded && name2.IsGrounded)
			{
				return name1.Equals(name2);
			}

			bindings = FindSubst(name1, name2,false);
			return bindings != null;
		}

		public static IEnumerable<Substitution> Unify(Name name1, Name name2)
		{
			IEnumerable<Substitution> bindings;
			if (Unify(name1, name2, out bindings))
				return bindings;
			return null;
		}

		/// <summary>
		/// Unifying Method similar to Unify but with an important difference. If one of the unifying names
		/// is smaller or bigger than the other, this method considers that the names can still be partially unifiable. 
		/// 
		/// The regular Unify method will always return false in such situations. 
		/// </summary>
		/// <see cref="FAtiMA.Core.WellFormedNames.Substitution"/>
		/// <see cref="FAtiMA.Core.WellFormedNames.Name"/>
		/// <param name="name1">The first Name</param>
		/// <param name="name2">The second Name</param>
		/// <param name="bindings">The out paramenter for the founded substitutions</param>
		/// <returns>True if the names are unifiable, in this case the bindings list will contain the found Substitutions, otherwise it will be empty</returns>
		public static bool PartialUnify(Name name1, Name name2, out IEnumerable<Substitution> bindings)
		{
			bindings = null;
			if (name1 == null || name2 == null)
				return false;

			if (name1.IsGrounded && name2.IsGrounded)
				return (name1.ToString().StartsWith(name2.ToString()))||(name2.ToString().StartsWith(name1.ToString()));

			bindings = FindSubst(name1, name2,true);
			return bindings != null;
		}

		/// <summary>
		/// Inverse of the Unify.
		/// Receives two WellFormedNames and tries to find a list of Inequalities that will make 
		/// both names syntatically different. 
		/// </summary>
		/// <see cref="FAtiMA.Core.WellFormedNames.Inequality"/>
		/// <see cref="FAtiMA.Core.WellFormedNames.Name"/>
		/// <param name="name1">The first Name</param>
		/// <param name="name2">The second Name</param>
		/// <param name="bindings">The out paramenter for the founded inequalities</param>
		/// <returns>True if the names are disunifiable, in this case the bindings list will contain the found Inequalities, otherwise it will be empty</returns>
		public static bool Disunify(Name name1, Name name2, out IEnumerable<Substitution> bindings)
		{
			bool result = Unify(name1, name2, out bindings);
			if (result)
				bindings = (IEnumerable<Substitution>)(bindings.Select(s => new Inequality(s)));
			return result;
		}

	   
        #region Private Methods

        private static IEnumerable<Substitution> FindSubst(Name n1, Name n2, bool allowPartial)
		{
			SubstitutionSet bindings = new SubstitutionSet();
			if (!FindSubst(n1, n2,allowPartial, bindings))
				return null;
			return bindings;
		}

        private static IEnumerable<Pair<Name, Name>> GetTerms(Name n1, Name n2, bool allowPartial)
        {
            if (!(allowPartial || n1.SimilarStructure(n2)))
                return null;

            return n1.GetTerms().Zip(n2.GetTerms(), (t1, t2) => Tuple.Create(t1, t2));
        }


        private static bool FindSubst(Name n1, Name n2, bool allowPartialTerms, SubstitutionSet bindings)
		{
			n1 = n1.MakeGround(bindings);
			n2 = n2.MakeGround(bindings);
			var t = GetTerms(n1, n2, allowPartialTerms);
			if (t == null)
				return false;

			foreach (var p in t)
			{
				Substitution candidate = null;
				bool isVar1 = p.value1.IsVariable;
				bool isVar2 = p.value2.IsVariable;

				// Case 1: x = t, where t is not a variable and x is a variable, and create substitution x/t
				if (isVar1 != isVar2)
				{
					Symbol variable = (Symbol)(isVar1 ? p.value1 : p.value2);
					Name value = isVar1 ? p.value2 : p.value1;
					if (value.ContainsVariable(variable))		//Occurs check to prevent cyclical evaluations
						return false;

					candidate = new Substitution(variable, value);
				}
				else if (isVar1) //isVar1 == isVar2 == true
				{
					//Case 2: x = x, where x is a variable, ignore it. otherwise add the substitution
					if (!(p.value1 == p.value2))
						candidate = new Substitution((Symbol)p.value1, p.value2); //TODO in this case, it should return two possible substitutions
				}
				else //isVar1 == isVar2 == false
				{
					// Case 3: t1 = t2, where t1,t2 are not variables.
					// If they don't contain variables, compare them to see if they are equal. If they are not equal the unification fails.
					if (p.value1.IsGrounded && p.value2.IsGrounded)
					{
						if (p.value1 == p.value2)
							continue;
						return false;
					}

					//If one or both contain variables, unify the terms
					if (!FindSubst(p.value1, p.value2,allowPartialTerms, bindings))
						return false;
				}

				if (candidate != null)
				{
					// Step 4: check to see if the newly created substitution conflicts with any of the already created substitution.
					// If it does, the unification fails.
					if (bindings.Conflicts(candidate))
						return false;
					bindings.AddSubstitution(candidate);
				}
			}
			return true;
		}

		#endregion
	}
}
