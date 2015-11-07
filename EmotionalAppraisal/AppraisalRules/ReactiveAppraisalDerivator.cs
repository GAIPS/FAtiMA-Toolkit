using System;
using EmotionalAppraisal.Components;
using EmotionalAppraisal.Interfaces;
using EmotionalAppraisal.OCCModel;
using KnowledgeBase.WellFormedNames;
using KnowledgeBase.WellFormedNames.Collections;

namespace EmotionalAppraisal.AppraisalRules
{
	/// <summary>
	/// Default reactive module implementation.
	/// It evaluates events through a set of rules, and determines the emotional reaction of that event.
	/// It then generates appropriate actions base on the agent's emotional state.
	/// </summary>
	/// @author João Dias
	/// @author Pedro Gonçalves
	[Serializable]
	public class ReactiveAppraisalDerivator : IAppraisalDerivator
	{
		private const short DEFAULT_APPRAISAL_WEIGHT = 1;
		public const long IGNORE_DURATION = 5000;

		private readonly NameSearchTree<Reaction> AppraisalRules;

		public ReactiveAppraisalDerivator()
		{
			this.AppraisalWeight = DEFAULT_APPRAISAL_WEIGHT;
			this.AppraisalRules = new NameSearchTree<Reaction>();
		}
		
		public Reaction Evaluate(string perspective, IEvent evt)
		{
			Name eventName = evt.ToName();
			if(!string.IsNullOrEmpty(perspective))
				eventName = eventName.ApplyPerspective(perspective);
			Reaction emotionalReaction = AppraisalRules[eventName];
			if(emotionalReaction != null)
			{
				emotionalReaction = emotionalReaction.MakeGround(evt.GenerateBindings());
			}
			return emotionalReaction;
		}

		/// <summary>
		/// Adds an emotional reaction to an event
		/// </summary>
		/// <param name="evt"></param>
		/// <param name="emotionalReaction">the Reaction to add</param>
		public void AddEmotionalReaction(IEvent evt, Reaction emotionalReaction)
		{
			AppraisalRules.Add(evt.ToName(), emotionalReaction);
		}

		#region IAppraisalDerivator Implementation

		public short AppraisalWeight
		{
			get;
			set;
		}

		public void Appraisal(EmotionalAppraisalAsset emotionalModule, IEvent evt, IWritableAppraisalFrame frame)
		{
			Reaction selfEvaluation = Evaluate(emotionalModule.Perspective, evt);
			if (selfEvaluation != null)
			{
				if (selfEvaluation.Desirability != 0)
					frame.SetAppraisalVariable(OCCAppraisalVariables.DESIRABILITY, selfEvaluation.Desirability);

				if (selfEvaluation.DesirabilityForOther != 0)
				{
					string other;
					if (selfEvaluation.Other != null)
						other = selfEvaluation.Other.ToString();
					else if (evt.Target != null)
						other = evt.Target;
					else
						other = evt.Subject;

					frame.SetAppraisalVariable(OCCAppraisalVariables.DESIRABILITY_FOR_OTHER + other, selfEvaluation.DesirabilityForOther);
				}

				if (selfEvaluation.Praiseworthiness != 0)
					frame.SetAppraisalVariable(OCCAppraisalVariables.PRAISEWORTHINESS, selfEvaluation.Praiseworthiness);

				if (selfEvaluation.Like != 0)
					frame.SetAppraisalVariable(OCCAppraisalVariables.LIKE, selfEvaluation.Like);
			}
		}

		public void InverseAppraisal(EmotionalAppraisalAsset emotionalModule, IAppraisalFrame frame)
		{
			float desirability = frame.GetAppraisalVariable(OCCAppraisalVariables.DESIRABILITY);
			float praiseworthiness = frame.GetAppraisalVariable(OCCAppraisalVariables.PRAISEWORTHINESS);

			if (desirability != 0 || praiseworthiness != 0)
			{
				Reaction r = new Reaction();
				r.Desirability = desirability;
				r.Praiseworthiness = praiseworthiness;
				AddEmotionalReaction(frame.AppraisedEvent, r);
			}
		}

		public IAppraisalFrame Reappraisal(EmotionalAppraisalAsset emotionalModule)
		{
			return null;
		}

		#endregion
	}
}
