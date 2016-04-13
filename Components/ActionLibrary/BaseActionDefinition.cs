using System;
using System.Collections.Generic;
using System.Linq;
using GAIPS.Serialization;
using GAIPS.Serialization.SerializationGraph;
using KnowledgeBase.Conditions;
using KnowledgeBase.WellFormedNames;

namespace ActionLibrary
{
	public abstract class BaseActionDefinition : IActionDefinition
	{
		private Name m_actionTemplate;
		public Name ActionName {
			get { return m_actionTemplate.GetFirstTerm(); }
		}
		public Name Target { get; }

		public ConditionSet ActivationConditions { get; }

		protected BaseActionDefinition(Name actionTemplate, Name target, IEnumerable<Condition> activationConditions)
		{
			var terms = actionTemplate.GetTerms().ToArray();
			var name = terms[0];
			if (!name.IsPrimitive)
				throw new ArgumentException("Invalid Action Template format", nameof(actionTemplate));
			for (int i = 1; i < terms.Length;i++)
			{
				if(terms[i].IsComposed)
					throw new ArgumentException("Invalid Action Template format", nameof(actionTemplate));
			}

			if(target.IsComposed)
				throw new ArgumentException("Action Definition Target must be a symbol definition", nameof(target));

			m_actionTemplate = actionTemplate;
			Target = target;
			ActivationConditions = new ConditionSet(activationConditions);
		}

		protected BaseActionDefinition(BaseActionDefinition other)
		{
			m_actionTemplate = other.m_actionTemplate;
			Target = other.Target;
			ActivationConditions = new ConditionSet(other.ActivationConditions);
		}

		public IAction GenerateAction(SubstitutionSet constraints)
		{
			var actionName = m_actionTemplate.MakeGround(constraints);
			if (!actionName.IsGrounded)
				return null;

			var targetName = Target.MakeGround(constraints);
			if (!targetName.IsGrounded)
				return null;
			
			var a = new Action(ActionName.GetTerms(), targetName);
			OnActionGenerated(a);
			return a;
		}

		protected virtual void OnActionGenerated(IAction action){}

		public abstract object Clone();

		public Name GetActionTemplate()
		{
			return m_actionTemplate;
		}

		public virtual void GetSerializationData(Graph serializationParent, IObjectGraphNode node, object contextData)
		{
			node["Action"] = serializationParent.BuildNode(GetActionTemplate());
			if (Target != null && Target != Name.NIL_SYMBOL)
				node["Target"] = serializationParent.BuildNode(Target);

			node["Conditions"] = serializationParent.BuildNode(ActivationConditions);
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		protected BaseActionDefinition(IObjectGraphNode node, object contextData)
		{
			var actionTemplate = node["Action"].RebuildObject<Name>();
			var terms = actionTemplate.GetTerms().ToArray();
			var name = terms[0];
			if (!name.IsPrimitive)
				throw new Exception("Invalid Action Template format");
			for (int i = 1; i < terms.Length; i++)
			{
				if (terms[i].IsComposed)
					throw new Exception("Invalid Action Template format");
			}

			var target = SerializationServices.GetFieldOrDefault(node, "Target", Name.NIL_SYMBOL);
			if (target.IsComposed)
				throw new ArgumentException("Action Definition Target must be a symbol definition", nameof(target));

			m_actionTemplate = actionTemplate;
			Target = target;
			ActivationConditions = node["Conditions"].RebuildObject<ConditionSet>();
		}
	}
}