using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Utilities;

namespace WellFormedNames
{
	/// <summary>
	/// Well Formed Name composed by several symbols. 
	/// If S and s1,s2,...sn are symbols, then S(s1,s2,...,sn) is a Composed Name
	/// the first symbol "S" is called the major symbol and it is followed by a list of
	/// comma separated parameter symbols (s1,s2,..,sn), which are enclosed in parenthesis.
	/// 
	/// <see cref="FAtiMA.Core.WellFormedName.Symbol"/>
	/// <see cref="FAtiMA.Core.WellFormedName.Name"/>
	/// 
	/// @author: João Dias
	/// @author: Pedro Gonçalves (C# version)
	/// </summary>
	public class ComposedName : Name
	{
		protected Symbol m_rootSymbol;
		protected Name[] _terms;

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
			get { return _terms.Length+1; }
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

			init(head,terms);
		}

		/// <summary>
		/// Creates a new ComposedName, receiving a set of terms to aglomerate.
		/// The first term must be a symbol and 
		/// </summary>
		/// <param name="name">Major symbol</param>
		/// <param name="literals">A set of parameter symbols</param>
		public ComposedName(IEnumerable<Name> terms)
		{
			List<Name> set = ObjectPool<List<Name>>.GetObject();
			try
			{
				set.AddRange(terms);
				if (set.Count < 2)
					throw new ArgumentException("Need at least 2 term to create a composed symbol", "terms");

				Symbol head = set[0] as Symbol;
				if (head == null)
					throw new ArgumentException("The first term needs to be a Symbol object","terms");

				set.RemoveAt(0);
				init(head, set.ToArray());
			}
			finally
			{
				set.Clear();
				ObjectPool<List<Name>>.Recycle(set);
			}
		}

		/// <summary>
		/// Creates a new ComposedName, receiving a set of terms to aglomerate.
		/// The first term must be a symbol and 
		/// </summary>
		/// <param name="name">Major symbol</param>
		/// <param name="literals">A set of parameter symbols</param>
		public ComposedName(Symbol head, IEnumerable<Name> terms) : this(head,terms.ToArray()){
			
		}

		/// <summary>
		/// Clone Constructor
		/// </summary>
		/// <param name="composedName">The composedName to clone</param>
		public ComposedName(ComposedName composedName)
		{
			this.IsGrounded = composedName.IsGrounded;
			this.m_rootSymbol = composedName.m_rootSymbol;
			this._terms = composedName._terms.Clone<Name>().ToArray();
		}

		/// <summary>
		/// Common initialization code between constructors
		/// </summary>
		private void init(Symbol head, Name[] literals)
		{
			this._terms = literals;
			this.m_rootSymbol = head;

			this.IsGrounded =  this.m_rootSymbol.IsGrounded && this._terms.All(l => l.IsGrounded);
		}

		public override Name GetFirstTerm()
		{
			return m_rootSymbol;
		}

		public override IEnumerable<Name> GetTerms()
		{
			return this._terms.Prepend(m_rootSymbol);
		}

		public override IEnumerable<Symbol> GetLiterals()
		{
			return this._terms.SelectMany(t => t.GetLiterals()).Prepend(m_rootSymbol);
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

		public override Name ReplaceUnboundVariables(int variableID)
		{
			if (IsGrounded)
				return this;

			return new ComposedName(GetTerms().Select(t => t.ReplaceUnboundVariables(variableID)));
		}

		public override Name MakeGround(IEnumerable<Substitution> bindings)
		{
			if (IsGrounded)
				return this;

			return new ComposedName(GetTerms().Select(t => t.MakeGround(bindings)));
		}

		public override object Clone()
		{
			return new ComposedName(this);
		}

		public override string ToString()
		{
			StringBuilder builder = ObjectPool<StringBuilder>.GetObject();
			builder.Append(this.m_rootSymbol);
			builder.Append('(');
			for (int i = 0; i < this._terms.Length; i++)
			{
				builder.Append(this._terms[i]);
				if (i + 1 < this._terms.Length)
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
			if (obj == null)
				return false;

			ComposedName other = obj as ComposedName;
			if (other == null)
				return false;

			if (other._terms.Length != this._terms.Length)
				return false;

			for (int i = 0; i < this._terms.Length; i++)
				if (!other._terms[i].Equals(this._terms[i]))
					return false;

			return true;
		}

		public override int GetHashCode()
		{
			return GetTerms().Select(t => t.GetHashCode()).Aggregate((h1, h2) => h1 ^ h2);
		}

		public override bool Match(Name name)
		{
			if (name.IsUniversal)
				return true;

			ComposedName other = name as ComposedName;
			if (other == null)
				return false;

			if (other._terms.Length != this._terms.Length)
				return false;

			for (int i = 0; i < this._terms.Length; i++)
				if (!this._terms[i].Match(other._terms[i]))
					return false;

			return true;
		}

		public override bool SimilarStructure(Name other)
		{
			if (other == null)
				return false;

			ComposedName name = other as ComposedName;
			if (name == null)
				return false;

			return name._terms.Length == this._terms.Length;
		}
	}
}
