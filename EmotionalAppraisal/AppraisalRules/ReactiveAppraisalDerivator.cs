using System;
using System.Collections.Generic;
using System.Linq;
using AutobiographicMemory;
using AutobiographicMemory.Interfaces;
using EmotionalAppraisal.Components;
using EmotionalAppraisal.OCCModel;
using GAIPS.Serialization;
using KnowledgeBase;
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
	public class ReactiveAppraisalDerivator : IAppraisalDerivator, ICustomSerialization
	{
		private const short DEFAULT_APPRAISAL_WEIGHT = 1;
		public const long IGNORE_DURATION = 5000;

		private readonly ConditionalNST<AppraisalRule> Rules;

		public ReactiveAppraisalDerivator()
		{
			this.AppraisalWeight = DEFAULT_APPRAISAL_WEIGHT;
			this.Rules = new ConditionalNST<AppraisalRule>();
		}
		
		public AppraisalRule Evaluate(string perspective, IEvent evt, KB kb)
		{
			var name = evt.ToIdentifierName().ApplyPerspective(perspective);
			Pair<AppraisalRule,SubstitutionSet> r = Rules.UnifyAll(name, kb, new SubstitutionSet(evt.GenerateBindings())).FirstOrDefault();
			if (r == null)
				return null;

			return r.Item1;//.MakeGround(r.Item2);
		}

		/// <summary>
		/// Adds an emotional reaction to an event
		/// </summary>
		/// <param name="evt"></param>
		/// <param name="emotionalAppraisalRule">the AppraisalRule to add</param>
		public void AddEmotionalReaction(string perspective, AppraisalRule emotionalAppraisalRule)
		{
			Rules.Add(emotionalAppraisalRule.EventName.ApplyPerspective(perspective), emotionalAppraisalRule.Conditions, emotionalAppraisalRule);
		}

	    public IEnumerable<AppraisalRule> GetAppraisalRules()
	    {
		    return Rules.Values;
	    }
        
		#region IAppraisalDerivator Implementation

		public short AppraisalWeight
		{
			get;
			set;
		}

		public void Appraisal(EmotionalAppraisalAsset emotionalModule, IEvent evt, IWritableAppraisalFrame frame)
		{
			AppraisalRule selfEvaluation = Evaluate(emotionalModule.Perspective, evt,emotionalModule.Kb);
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
				AppraisalRule r = new AppraisalRule(frame.AppraisedEvent);
				r.Desirability = desirability;
				r.Praiseworthiness = praiseworthiness;
				//r.EventName = frame.AppraisedEvent.ToIdentifierName().RemovePerspective(emotionalModule.Perspective);
				AddEmotionalReaction(emotionalModule.Perspective, r);
			}
		}

		public IAppraisalFrame Reappraisal(EmotionalAppraisalAsset emotionalModule)
		{
			return null;
		}

		#endregion

		#region Custom Serializer

		public void GetObjectData(ISerializationData dataHolder)
		{
			dataHolder.SetValue("AppraisalWeight",AppraisalWeight);
			dataHolder.SetValue("Rules",Rules.Values.ToArray());
		}

		public void SetObjectData(ISerializationData dataHolder)
		{
			AppraisalWeight = dataHolder.GetValue<short>("AppraisalWeight");
			var rules = dataHolder.GetValue<AppraisalRule[]>("Rules");
			Rules.Clear();
			foreach (var r in rules)
				Rules.Add(r.EventName, r.Conditions, r);
		}

		#endregion
	}
}
