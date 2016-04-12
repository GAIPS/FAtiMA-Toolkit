using System;
using System.Collections.Generic;
using System.Linq;
using GAIPS.Serialization;
using GAIPS.Serialization.SerializationGraph;
using KnowledgeBase.Conditions;
using KnowledgeBase.WellFormedNames;
using Utilities;

namespace EmotionalDecisionMaking
{
	public partial class ActionTendency : ICloneable
	{
		private const float DEFAULT_ACTIVATION_COOLDOWN = 1f;
        
		public List<Name> m_parameters;
		private DateTime m_lastActivationTimestamp;

        public Guid Id { get; set; }
        public Name ActionName { get; private set; }
		public Name Target { get; private set; }
		public float ActivationCooldown { get; set; }

		public ConditionSet ActivationConditions { get; private set; }
		public bool IsCoolingdown
		{
			get { return (DateTime.UtcNow - m_lastActivationTimestamp).TotalSeconds <= ActivationCooldown; }
		}

		public ActionTendency(Name actionName) : this(actionName,Enumerable.Empty<Condition>()) {}

		public ActionTendency(Name actionName, IEnumerable<Condition> activationConditions)
		{
            Id = Guid.NewGuid();
			var terms = actionName.GetTerms().ToArray();
			var name = terms[0];
			if (!name.IsPrimitive)
				throw new Exception("ActionName name needs to be node primitive value Name");

			ActivationCooldown = DEFAULT_ACTIVATION_COOLDOWN;
			ActivationConditions = new ConditionSet(activationConditions);
			ActionName = name;

			if (terms.Length > 1)
			{
				m_parameters = new List<Name>();
				m_parameters.AddRange(terms.Skip(1));
			}
		}

		public void AddParameter(Name value)
		{
			if(m_parameters==null)
				m_parameters=new List<Name>();

			m_parameters.Add(value);
		}

		private ActionTendency(ActionTendency other)
		{
		    Id = other.Id;
			ActionName = other.ActionName;
			ActivationCooldown = other.ActivationCooldown;
			ActivationConditions = new ConditionSet(other.ActivationConditions);

			if (other.m_parameters != null)
				m_parameters = new List<Name>(other.m_parameters);
		}

		public IAction GenerateAction(SubstitutionSet constraints)
		{
			List<Name> validParameters = null;
			if (m_parameters != null)
			{
				validParameters = new List<Name>();
				foreach (var p in m_parameters)
				{
					var value = p.MakeGround(constraints);
					if (!value.IsGrounded)
						return null;

					validParameters.Add(value);
				}
			}

			var targetName = Target.MakeGround(constraints);
			if (!targetName.IsGrounded)
				return null;

			m_lastActivationTimestamp = DateTime.UtcNow;
			return new Action(ActionName,targetName, validParameters);
		}

		public object Clone()
		{
			return new ActionTendency(this);
		}

		public Name ToName()
		{
			var actionName = (Name)ActionName;
			if (m_parameters != null)
				actionName = Name.BuildName(m_parameters.Prepend(actionName));
			return actionName;
		}

		internal void GetSerializationData(Graph serializationParent, IObjectGraphNode node,float defaultCooldown)
		{
			node["Action"] = serializationParent.BuildNode(ToName());

			if (Target != null && Target != Name.NIL_SYMBOL)
				node["Target"] = serializationParent.BuildNode(Target);

			node["Conditions"] = serializationParent.BuildNode(ActivationConditions);
			if (ActivationCooldown != defaultCooldown)
				node["Cooldown"] = serializationParent.BuildNode(ActivationCooldown);
		}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		internal ActionTendency(IObjectGraphNode node, float defaultActionCooldown)
		{
            Id = Guid.NewGuid();
			var terms = node["Action"].RebuildObject<Name>().GetTerms().ToArray();
			var name = terms[0];
			if (!name.IsPrimitive)
				throw new Exception("ActionName name needs to be node primitive value Name");

			ActionName = terms[0];
			if (terms.Length > 1)
			{
				m_parameters = new List<Name>();
				m_parameters.AddRange(terms.Skip(1));
			}

			Target = SerializationServices.GetFieldOrDefault(node, "Target", Name.NIL_SYMBOL);
			ActivationConditions = node["Conditions"].RebuildObject<ConditionSet>();
			ActivationCooldown = SerializationServices.GetFieldOrDefault(node, "Cooldown", defaultActionCooldown);
		}
	}
}