using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace KnowledgeBase.WellFormedNames
{
	/// <summary>
	/// Well Formed Name composed by several symbols. 
	/// If S and s1,s2,...sn are symbols, then S(s1,s2,...,sn) is a Composed Name
	/// the first symbol "S" is called the major symbol and it is followed by a list of
	/// comma separated parameter symbols (s1,s2,..,sn), which are enclosed in parenthesis.
	/// 
	/// @author: João Dias
	/// @author: Pedro Gonçalves (C# version)
	/// </summary>
	[Serializable]
	public class ComposedName : Name
	{
		protected Symbol RootSymbol;
		protected Name[] Terms;

		public override bool IsUniversal
		{
			get { return false; }
		}

		public override bool IsVariable
		{
			get { return false; }
		}

		public override int NumberOfTerms
		{
			get { return Terms.Length+1; }
		}

		private bool m_isConstant;
		public override bool IsConstant
		{
			get { return m_isConstant; }
		}

		/// <summary>
		/// Creates a new ComposedName, receiving a major symbol, followed by several parameter symbols
		/// parameter symbols
		/// </summary>
		/// <param name="head">The head symbol</param>
		/// <param name="terms">A set of parameter symbols</param>
		public ComposedName(Symbol head, params Name[] terms)
		{
			if (terms.Length == 0)
				throw new ArgumentException("Need at least 1 term to create a composed symbol", "terms");

			Init(head,terms);
		}

		/// <summary>
		/// Creates a new ComposedName, receiving a set of terms to aglomerate.
		/// The first term must be a symbol and 
		/// </summary>
		public ComposedName(IEnumerable<Name> terms)
		{
			var set = ObjectPool<List<Name>>.GetObject();
			try
			{
				set.AddRange(terms);
				if (set.Count < 2)
					throw new ArgumentException("Need at least 2 term to create a composed symbol", "terms");

				Symbol head = set[0] as Symbol;
				if (head == null)
					throw new ArgumentException("The first term needs to be a Symbol object","terms");

				set.RemoveAt(0);
				Init(head, set.ToArray());
			}
			finally
			{
				set.Clear();
				ObjectPool<List<Name>>.Recycle(set);
			}
		}

		/// <summary>
		/// Clone Constructor
		/// </summary>
		/// <param name="composedName">The composedName to clone</param>
		protected ComposedName(ComposedName composedName)
		{
			Init(composedName.RootSymbol,composedName.Terms.Clone<Name>().ToArray());
		}

		/// <summary>
		/// Common initialization code between constructors
		/// </summary>
		private void Init(Symbol head, Name[] literals)
		{
			Terms = literals;
			RootSymbol = head;

			IsGrounded = RootSymbol.IsGrounded && Terms.All(n => n.IsGrounded);
			m_isConstant = RootSymbol.IsConstant && Terms.All(n => n.IsConstant);
		}

		public override Name GetFirstTerm()
		{
			return RootSymbol;
		}

		public override IEnumerable<Name> GetTerms()
		{
			return Terms.Prepend(RootSymbol);
		}

		public override IEnumerable<Symbol> GetLiterals()
		{
			return Terms.SelectMany(t => t.GetLiterals()).Prepend(RootSymbol);
		}

		public override IEnumerable<Symbol> GetVariableList()
		{
			return GetTerms().SelectMany(l => l.GetVariableList());
		}

		public override bool HasGhostVariable()
		{
			return GetTerms().Any(s => s.HasGhostVariable());
		}

		public override Name SwapPerspective(string original, string newName)
		{
			return new ComposedName(GetTerms().Select(t => t.SwapPerspective(original, newName)));
		}

		public override Name ReplaceUnboundVariables(long variableId)
		{
			if (IsGrounded)
				return (Name)Clone();

			return new ComposedName(GetTerms().Select(t => t.ReplaceUnboundVariables(variableId)));
		}

		public override Name MakeGround(IEnumerable<Substitution> bindings)
		{
			if (IsGrounded)
                return (Name)Clone();

            return new ComposedName(GetTerms().Select(t => t.MakeGround(bindings)));
		}

		public override object Clone()
		{
			return new ComposedName(this);
		}

		public override string ToString()
		{
			StringBuilder builder = ObjectPool<StringBuilder>.GetObject();
			builder.Append(RootSymbol);
			builder.Append('(');
			for (int i = 0; i < Terms.Length; i++)
			{
				builder.Append(Terms[i]);
				if (i + 1 < Terms.Length)
					builder.Append(", ");
			}

			builder.Append(')');

			string result = builder.ToString();
			builder.Length = 0;
			ObjectPool<StringBuilder>.Recycle(builder);
			return result;
		}

		public override bool Equals(object obj)
		{
			var other = obj as ComposedName;
			if (other == null)
				return false;
            
			if (other.Terms.Length != Terms.Length)
				return false;

		    if (!other.RootSymbol.Equals(RootSymbol))
		    {
		        return false;
		    }

			return !Terms.Where((t, i) => !other.Terms[i].Equals(t)).Any();
		}

        public override bool Match(Name name)
        {
            var other = name as ComposedName;
            if (other == null)
                return false;

            if (other.Terms.Length != Terms.Length)
                return false;

            if (!other.RootSymbol.Match(RootSymbol))
            {
                return false;
            }

            for (int i = 0; i < Terms.Length; i++)
                if (!Terms[i].Match(other.Terms[i]))
                    return false;

            return true;
        }

		public override Name Unfold(out SubstitutionSet set)
		{
			Name[] terms = new Name[Terms.Length];
			set = null;
			for (int i = 0; i < terms.Length; i++)
			{
				Symbol s = Terms[i] as Symbol;
				if (s == null)
				{
					s = Name.GenerateUniqueGhostVariable();
					SubstitutionSet aux;
					var sub = new Substitution(s,Terms[i].Unfold(out aux));

					if (set == null)
						set = aux ?? new SubstitutionSet();
					else if (aux != null)
						set.AddSubstitutions(aux);

					set.AddSubstitution(sub);
				}
				
				terms[i] = s;
			}

			return new ComposedName(RootSymbol,terms);
		}

		

		public override int GetHashCode()
		{
			return GetTerms().Select(t => t.GetHashCode()).Aggregate((h1, h2) => h1 ^ h2);
		}
	}
}
