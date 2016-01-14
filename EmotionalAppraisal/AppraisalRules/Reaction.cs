using System;
using System.Collections.Generic;
using KnowledgeBase.Conditions;
using KnowledgeBase.WellFormedNames;

namespace EmotionalAppraisal.AppraisalRules
{
	/// <summary>
	///     Represents an Emotional Reaction based in Construal Frames that specify values
	///     for some of OCC's appraisal variables: Desirability, DesirabilityForOther, Like and
	///     Praiseworthiness.
	/// </summary>
	/// @author João Dias
	/// @author Pedro Gonçalves
	[Serializable]
	public class Reaction : IGroundable<Reaction>
	{
		/// <summary>
		///     Creates a new empty Emotional Reaction
		/// </summary>
		public Reaction()
		{
			Desirability = Praiseworthiness = 0;//DesirabilityForOther = Like = 0;
			//ReferencedEventName = null;
			Other = null;
		}

		/// <summary>
		///     Creates a new empty Emotional Reaction
		/// </summary>
		/// <param name="evt">the event that this reaction references</param>
		//public Reaction(IEvent evt)
		//{
		//	ReferencedEventName = evt.ToName();
		//	Desirability = DesirabilityForOther = Praiseworthiness = Like = 0;
		//	Other = null;
		//}
		
		/// <summary>
		///     Creates a new Emotional Reaction
		/// </summary>
		/// <param name="desirability">the desirability of the event</param>
		/// <param name="desirabilityForOther">the desirability of the event for other agents</param>
		/// <param name="praiseworthiness">the paiseworthiness of the event</param>
		/// <param name="other">which character does the desirabilityForOther variable reference</param>
		public Reaction(float desirability, float desirabilityForOther, float praiseworthiness, Name other)
		{
			Desirability = desirability;
			//DesirabilityForOther = desirabilityForOther;
			Praiseworthiness = praiseworthiness;
			Other = other;
			//Like = 0;
		}

		/// <summary>
		///     Clone Constructor
		/// </summary>
		/// <param name="other">the reaction to clone</param>
		public Reaction(Reaction other)
		{
			Desirability = other.Desirability;
			//DesirabilityForOther = other.DesirabilityForOther;
			Praiseworthiness = other.Praiseworthiness;
			//Like = other.Like;
			//ReferencedEventName = (Name)other.ReferencedEventName.Clone();
			if (Other != null)
				Other = (Name) other.Other.Clone();
		}

		/// <summary>
		///     Desirability of the event
		/// </summary>
		public float Desirability { get; set; }

		/// <summary>
		///     Desirability For Other of the event
		/// </summary>
		//public float DesirabilityForOther { get; set; }

		/// <summary>
		///     Like value of the event
		/// </summary>
		//public float Like { get; set; }

		/// <summary>
		///     Praiseworthiness of the event
		/// </summary>
		public float Praiseworthiness { get; set; }

		/// <summary>
		///     Event referenced by the emotional reaction
		/// </summary>
		//public Name ReferencedEventName { get; private set; }

		/// <summary>
		///     The name of the character that the appraisal variable DesirabilityForOther refers
		/// </summary>
		public Name Other { get; private set; }

		public Reaction ReplaceUnboundVariables(string id)
		{
			if (IsGrounded)
				return this;

			var o = Other;
			Other = null;
			var r = new Reaction(this);
			Other = o;
			r.Other = o.ReplaceUnboundVariables(id);
			return r;
		}

		public Reaction RemoveBoundedVariables(string id)
		{
			if (IsGrounded)
				return this;

			var o = Other;
			Other = null;
			var r = new Reaction(this);
			Other = o;
			r.Other = o.RemoveBoundedVariables(id);
			return r;
		}

		public Reaction MakeGround(SubstitutionSet bindings)
		{
			if (IsGrounded)
				return this;

			var o = Other;
			Other = null;
			var r = new Reaction(this);
			Other = o;
			r.Other = o.MakeGround(bindings);
			return r;
		}

		public bool IsGrounded
		{
			get
			{
				if (Other == null)
					return true;
				return Other.IsGrounded;
			}
		}
	}
}