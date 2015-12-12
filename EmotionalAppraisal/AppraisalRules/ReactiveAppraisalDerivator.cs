using System;
using System.Linq;
using EmotionalAppraisal.Components;
using EmotionalAppraisal.Interfaces;
using EmotionalAppraisal.OCCModel;
using KnowledgeBase;
using KnowledgeBase.Conditions;
using KnowledgeBase.WellFormedNames;
using Utilities;

namespace EmotionalAppraisal.AppraisalRules
{
	/// <summary>
	/// Default reactive module implementation.
	/// It evaluates events through a evaluatorSet of rules, and determines the emotional reaction of that event.
	/// It then generates appropriate actions base on the agent's emotional state.
	/// </summary>
	/// @author João Dias
	/// @author Pedro Gonçalves
	[Serializable]
	public class ReactiveAppraisalDerivator : IAppraisalDerivator
	{
		private const short DEFAULT_APPRAISAL_WEIGHT = 1;
		public const long IGNORE_DURATION = 5000;

		private readonly ConditionalNST<Reaction> AppraisalRules;

		public ReactiveAppraisalDerivator()
		{
			this.AppraisalWeight = DEFAULT_APPRAISAL_WEIGHT;
			this.AppraisalRules = new ConditionalNST<Reaction>();
		}
		
		public Reaction Evaluate(string perspective, IEvent evt, KB kb)
		{
			Cause cause = new Cause(evt,perspective);
			Pair<Reaction,SubstitutionSet> r = AppraisalRules.UnifyAll(cause.CauseName, kb, cause.CauseParameters).FirstOrDefault();
			if (r == null)
				return null;

			return r.Item1.MakeGround(r.Item2);
		}

		/// <summary>
		/// Adds an emotional reaction to an event
		/// </summary>
		/// <param name="evt"></param>
		/// <param name="emotionalReaction">the Reaction to add</param>
		public void AddEmotionalReaction(Cause cause, ConditionEvaluatorSet conditionsEvaluator, Reaction emotionalReaction)
		{
			if (cause.CauseParameters != null)
			{
				conditionsEvaluator = conditionsEvaluator==null?new ConditionEvaluatorSet() : new ConditionEvaluatorSet(conditionsEvaluator);
				var conds =
					cause.CauseParameters.Select(s => Condition.BuildCondition(s.Variable, s.Value, ComparisonOperator.Equal));
				conditionsEvaluator.UnionWith(conds);
			}
			AppraisalRules.Add(cause.CauseName, conditionsEvaluator, emotionalReaction);
		}

		#region IAppraisalDerivator Implementation

		public short AppraisalWeight
		{
			get;
			set;
		}

		public void Appraisal(EmotionalAppraisalAsset emotionalModule, IEvent evt, IWritableAppraisalFrame frame)
		{
			Reaction selfEvaluation = Evaluate(emotionalModule.Perspective, evt,emotionalModule.Kb);
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
				Cause c = new Cause(frame.AppraisedEvent,emotionalModule.Perspective);
				AddEmotionalReaction(c, null, r);
			}
		}

		public IAppraisalFrame Reappraisal(EmotionalAppraisalAsset emotionalModule)
		{
			return null;
		}

		#endregion
	}
}
