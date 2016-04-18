using System;
using System.Collections.Generic;
using System.Linq;
using GAIPS.Serialization;
using KnowledgeBase.Conditions;
using KnowledgeBase.WellFormedNames;

namespace ActionLibrary
{
	[Serializable]
	public abstract class BaseActionDefinition : IActionDefinition, ICustomSerialization
	{
		public Guid Id { get; private set; }
		private Name m_actionTemplate;
		public Name ActionName {
			get { return m_actionTemplate.GetFirstTerm(); }
		}
		public Name Target { get; private set; }

		public ConditionSet ActivationConditions { get; set; }

		private void AssertAndInitialize(Name actionTemplate, Name target, ConditionSet activationConditions)
		{
			var terms = actionTemplate.GetTerms().ToArray();
			var name = terms[0];
			if (!name.IsPrimitive)
				throw new ArgumentException("Invalid Action Template format", nameof(actionTemplate));
			for (int i = 1; i < terms.Length; i++)
			{
				if (terms[i].IsComposed)
					throw new ArgumentException("Invalid Action Template format", nameof(actionTemplate));
			}

			if (target.IsComposed)
				throw new ArgumentException("Action Definition Target must be a symbol definition", nameof(target));

			Id = Guid.NewGuid();
			m_actionTemplate = actionTemplate;
			Target = target;
			ActivationConditions = activationConditions;
		}

		protected BaseActionDefinition(Name actionTemplate, Name target, IEnumerable<Condition> activationConditions)
		{
			AssertAndInitialize(actionTemplate,target, new ConditionSet(activationConditions));
		}

		protected BaseActionDefinition(BaseActionDefinition other)
		{
			Id = other.Id;
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
			
			var a = new Action(actionName.GetTerms(), targetName);
			OnActionGenerated(a);
			return a;
		}

		protected virtual void OnActionGenerated(IAction action){}

		public abstract object Clone();

		public Name GetActionTemplate()
		{
			return m_actionTemplate;
		}

		///// <summary>
		///// Deserialization constructor
		///// </summary>
		//protected BaseActionDefinition(IObjectGraphNode node, object contextData)
		//{
		//	var actionTemplate = node["Action"].RebuildObject<Name>();
		//	var terms = actionTemplate.GetTerms().ToArray();
		//	var name = terms[0];
		//	if (!name.IsPrimitive)
		//		throw new Exception("Invalid Action Template format");
		//	for (int i = 1; i < terms.Length; i++)
		//	{
		//		if (terms[i].IsComposed)
		//			throw new Exception("Invalid Action Template format");
		//	}

		//	var target = SerializationServices.GetFieldOrDefault(node, "Target", Name.NIL_SYMBOL);
		//	if (target.IsComposed)
		//		throw new ArgumentException("Action Definition Target must be a symbol definition", nameof(target));

		//	Id = Guid.NewGuid();
		//	m_actionTemplate = actionTemplate;
		//	Target = target;
		//	ActivationConditions = node["Conditions"].RebuildObject<ConditionSet>();
		//}

		public virtual void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			dataHolder.SetValue("Action",GetActionTemplate());
			if (Target != null && Target != Name.NIL_SYMBOL)
				dataHolder.SetValue("Target",Target);
			dataHolder.SetValue("Conditions",ActivationConditions);
		}

		public virtual void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			var actionTemplate = dataHolder.GetValue<Name>("Action");
			var target = dataHolder.ContainsField("Target") ? dataHolder.GetValue<Name>("Target") : Name.NIL_SYMBOL;
			var conditions = dataHolder.GetValue<ConditionSet>("Conditions");
			AssertAndInitialize(actionTemplate, target, conditions);
		}
	}
}