using System;
using System.Collections.Generic;
using System.Linq;
using AutobiographicMemory;
using AutobiographicMemory.Interfaces;
using EmotionalAppraisal.Components;
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

		private readonly ConditionalNST<Reaction> Rules;

		public ReactiveAppraisalDerivator()
		{
			this.AppraisalWeight = DEFAULT_APPRAISAL_WEIGHT;
			this.Rules = new ConditionalNST<Reaction>();
		}
		
		public Reaction Evaluate(string perspective, IEvent evt, KB kb)
		{
			var name = evt.ToIdentifierName().ApplyPerspective(perspective);
			Pair<Reaction,SubstitutionSet> r = Rules.UnifyAll(name, kb, new SubstitutionSet(evt.GenerateBindings())).FirstOrDefault();
			if (r == null)
				return null;

			return r.Item1.MakeGround(r.Item2);
		}

		/// <summary>
		/// Adds an emotional reaction to an event
		/// </summary>
		/// <param name="evt"></param>
		/// <param name="emotionalReaction">the Reaction to add</param>
		public void AddEmotionalReaction(IEvent cause, string perspective, ConditionEvaluatorSet conditionsEvaluator, Reaction emotionalReaction)
		{
			if (cause.Parameters!=null && cause.Parameters.Any())
			{
				conditionsEvaluator = conditionsEvaluator==null?new ConditionEvaluatorSet() : new ConditionEvaluatorSet(conditionsEvaluator);
				var conds = cause.GenerateParameterBindings().Select(s => Condition.BuildCondition(s.Variable, s.Value, ComparisonOperator.Equal));
				conditionsEvaluator.UnionWith(conds);
			}
			Rules.Add(cause.ToIdentifierName().ApplyPerspective(perspective), conditionsEvaluator, emotionalReaction);
		}

	    public IEnumerable<Name> GetAppraisalRuleNames()
	    {

	        return Rules.GetNames();
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

				//if (selfEvaluation.DesirabilityForOther != 0)
				//{
				//	string other;
				//	if (selfEvaluation.Other != null)
				//		other = selfEvaluation.Other.ToString();
				//	else if (evt.Target != null)
				//		other = evt.Target;
				//	else
				//		other = evt.Subject;

				//	frame.SetAppraisalVariable(OCCAppraisalVariables.DESIRABILITY_FOR_OTHER + other, selfEvaluation.DesirabilityForOther);
				//}

				if (selfEvaluation.Praiseworthiness != 0)
					frame.SetAppraisalVariable(OCCAppraisalVariables.PRAISEWORTHINESS, selfEvaluation.Praiseworthiness);

				//if (selfEvaluation.Like != 0)
				//	frame.SetAppraisalVariable(OCCAppraisalVariables.LIKE, selfEvaluation.Like);
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
				AddEmotionalReaction(frame.AppraisedEvent,emotionalModule.Perspective, null, r);
			}
		}

		public IAppraisalFrame Reappraisal(EmotionalAppraisalAsset emotionalModule)
		{
			return null;
		}

		#endregion
	}
}
