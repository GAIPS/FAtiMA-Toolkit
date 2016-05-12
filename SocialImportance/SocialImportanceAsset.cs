using System;
using System.Collections.Generic;
using System.Linq;
using ActionLibrary;
using EmotionalAppraisal;
using GAIPS.Rage;
using GAIPS.Serialization;
using KnowledgeBase;
using KnowledgeBase.WellFormedNames;
using KnowledgeBase.WellFormedNames.Collections;
using SocialImportance.DTOs;
using Utilities;

namespace SocialImportance
{
	[Serializable]
	public sealed class SocialImportanceAsset: LoadableAsset<SocialImportanceAsset>, ICustomSerialization
	{
		private EmotionalAppraisalAsset m_ea;
		private AttributionRule[] m_attributionRules;
		private NameSearchTree<uint> m_claimTree;
		private ActionSelector<Conferral> m_conferalActions;

		//Volatile Statements
		private NameSearchTree<NameSearchTree<float>> m_cachedSI = new NameSearchTree<NameSearchTree<float>>();

		public EmotionalAppraisalAsset LinkedEA {
			get { return m_ea; }
		}

		public SocialImportanceAsset()
		{
			m_ea = null;
			m_attributionRules = new AttributionRule[0];
			m_claimTree = new NameSearchTree<uint>();
			m_conferalActions = new ActionSelector<Conferral>(ValidateConferral);

			m_cachedSI = new NameSearchTree<NameSearchTree<float>>();
		}

		public void RegisterEmotionalAppraisalAsset(EmotionalAppraisalAsset eaa)
		{
			if (m_ea != null)
			{
				//Unregist bindings
				RemoveKBBindings();
				m_ea = null;
			}

			m_ea = eaa;
			PerformKBBindings();
		}

		private void PerformKBBindings()
		{
			m_ea.Kb.RegistDynamicProperty(SI_DYNAMIC_PROPERTY_TEMPLATE,SIPropertyCalculator);
		}

		private void RemoveKBBindings()
		{
			m_ea.Kb.UnregistDynamicProperty(SI_DYNAMIC_PROPERTY_TEMPLATE);
		}

		private void ValidateEALink()
		{
			if(m_ea==null)
				throw new InvalidOperationException($"Cannot execute operation as an instance of {nameof(EmotionalAppraisalAsset)} was not registed in this asset.");
		}

		public float GetSocialImportance(Name target, Name perspective)
		{
			ValidateEALink();
			if(!target.IsPrimitive)
				throw new ArgumentException("must be a primitive name",nameof(target));

			var p = m_ea.Kb.AssertPerspective(perspective);

			NameSearchTree<float> targetDict;
			if (!m_cachedSI.TryGetValue(p, out targetDict))
			{
				targetDict = new NameSearchTree<float>();
				m_cachedSI[p] = targetDict;
			}

			float value;
			if (!targetDict.TryGetValue(target, out value))
			{
				value = CalculateSI(target, p);
				targetDict[target] = value;
			}
			return value;
		}

		public void InvalidateCachedSI()
		{
			m_cachedSI.Clear();
		}

		public IAction DecideConferral(Name perspective)
		{
			ValidateEALink();
			var a = m_conferalActions.SelectAction(m_ea.Kb, perspective).OrderByDescending(p=>p.Item2.ConferralSI);
			return FilterActions(perspective, a.Select(p=>p.Item1)).FirstOrDefault();
		}

		public IEnumerable<IAction> FilterActions(Name perspective, IEnumerable<IAction> actionsToFilter)
		{
			foreach (var a in actionsToFilter)
			{
				uint minSI;
				if(m_claimTree.TryGetValue(a.ToNameRepresentation(),out minSI))
					if (GetSocialImportance(a.Target, perspective) < minSI)
						continue;

				yield return a;
			}
		}

		private bool ValidateConferral(Conferral c,Name perspective, SubstitutionSet set)
		{
			var t = c.Target.MakeGround(set);
			if (!t.IsGrounded)
				return false;

			var si = GetSocialImportance(t, perspective);
			return si >= c.ConferralSI;
		}

		private uint CalculateSI(Name target, Name perspective)
		{
			long value = 0;
			foreach (var a in m_attributionRules)
			{
				var sub = new Substitution(a.Target, target);
				if (a.Conditions.Evaluate(m_ea.Kb, perspective, new[] { new SubstitutionSet(sub) }))
					value += a.Value;
			}
			return value<1?1:(uint)value;
		}

		protected override string OnAssetLoaded()
		{
			return null;
		}

		#region DTO operations

		public void LoadFromDTO(SocialImportanceDTO dto)
		{
			m_attributionRules = dto.AttributionRules.Select(adto => new AttributionRule(adto)).ToArray();

			m_claimTree.Clear();
			if (dto.Claims != null)
			{
				foreach (var c in dto.Claims)
				{
					var n = Name.BuildName(c.ActionTemplate);
					m_claimTree.Add(n, c.ClaimSI);
				}
			}

			m_conferalActions.Clear();
			if (dto.Conferral != null)
			{
				foreach (var con in dto.Conferral.Select(c => new Conferral(c)))
				{
					m_conferalActions.AddActionDefinition(con);
				}
			}
		}

		public SocialImportanceDTO GetDTO()
		{
			var at = m_attributionRules.Select(a => a.ToDTO()).ToArray();
			var claims = m_claimTree.Select(p => new ClaimDTO() { ActionTemplate = p.Key.ToString(), ClaimSI = p.Value }).ToArray();
			var conferrals = m_conferalActions.GetAllActionDefinitions().Select(c => c.ToDTO()).ToArray();
			return new SocialImportanceDTO() { AttributionRules = at, Claims = claims, Conferral = conferrals };
		}

		#endregion

		#region Dynamic Properties

		private static readonly Name SI_DYNAMIC_PROPERTY_TEMPLATE = Name.BuildName("SI([target])");

		private IEnumerable<Pair<PrimitiveValue, SubstitutionSet>> SIPropertyCalculator(KB kb, Name perspective, IDictionary<string, Name> args, IEnumerable<SubstitutionSet> constraints)
		{
			Name target;
			if(!args.TryGetValue("target",out target))
				yield break;

			foreach (var t in kb.AskPossibleProperties(target,perspective,constraints))
			{
				var si = GetSocialImportance(Name.BuildName(t.Item1), perspective);
				foreach (var s in t.Item2)
					yield return new Pair<PrimitiveValue, SubstitutionSet>(si,s);
			}
		}

		#endregion

		#region Serialization

		public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			dataHolder.SetValue("AttributionRules",m_attributionRules);
			dataHolder.SetValue("Claims",m_claimTree.Select(p=>new ClaimDTO() {ActionTemplate = p.Key.ToString(),ClaimSI = p.Value}).ToArray());
			dataHolder.SetValue("Conferrals",m_conferalActions.GetAllActionDefinitions().ToArray());
		}

		public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			m_attributionRules = dataHolder.GetValue<AttributionRule[]>("AttributionRules");

			var claims = dataHolder.GetValue<ClaimDTO[]>("Claims");
			m_claimTree.Clear();
			foreach (var c in claims)
				m_claimTree.Add(Name.BuildName(c.ActionTemplate), c.ClaimSI);

			var conferrals = dataHolder.GetValue<Conferral[]>("Conferrals");
			m_conferalActions.Clear();
			foreach (var c in conferrals)
				m_conferalActions.AddActionDefinition(c);
		}

		#endregion
	}
}