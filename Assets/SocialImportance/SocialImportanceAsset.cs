using System;
using System.Collections.Generic;
using System.Linq;
using ActionLibrary;
using EmotionalAppraisal;
using GAIPS.Rage;
using KnowledgeBase;
using SerializationUtilities;
using SocialImportance.DTOs;
using WellFormedNames;
using WellFormedNames.Collections;
using WellFormedNames.Exceptions;
using IQueryable = WellFormedNames.IQueryable;

namespace SocialImportance
{
	/// <summary>
	/// Main class of the Social Importance Asset.
	/// </summary>
	/// <remarks>
	/// New dynamic properties available by this asset uppon binding it with a Emotional Appraisal Asset:
	///		- SI([target])
	///			- Gives the Social Importance value attributed to the given target
	/// </remarks>
	[Serializable]
	public sealed class SocialImportanceAsset: LoadableAsset<SocialImportanceAsset>, ICustomSerialization
	{
		private KB m_kB;
		private HashSet<AttributionRule> m_attributionRules;
		private NameSearchTree<uint> m_claimTree;
		private ActionSelector<Conferral> m_conferalActions;

		//Volatile Statements
		private NameSearchTree<NameSearchTree<float>> m_cachedSI = new NameSearchTree<NameSearchTree<float>>();

		/// <summary>
		/// The Knowledge Base that is binded to this Social Importance Asset instance
		/// </summary>
		public KB LinkedKB {
			get { return m_kB; }
		}

		public SocialImportanceAsset()
		{
			m_kB = null;
			m_attributionRules = new HashSet<AttributionRule>();
			m_claimTree = new NameSearchTree<uint>();
			m_conferalActions = new ActionSelector<Conferral>(ValidateConferral);

			m_cachedSI = new NameSearchTree<NameSearchTree<float>>();
		}

		/// <summary>
		/// Binds a KB to this Social Importance Asset instance. Without a KB instance binded to this asset, 
        /// social importance evaluations will not work properly.
        /// InvalidateCachedSI() is automatically called by this method.
        /// </summary>
		/// <param name="kb">The Knowledge Base to be binded to this asset.</param>
		public void RegisterKnowledgeBase(KB kB)
		{
			if (m_kB != null)
			{
				//Unregist bindings
				UnbindToRegistry(m_kB);
				m_kB = null;
			}

			m_kB = kB;
			BindToRegistry(kB);
			InvalidateCachedSI();
		}

		private void BindToRegistry(IDynamicPropertiesRegistry registry)
		{
			registry.RegistDynamicProperty(SI_DYNAMIC_PROPERTY_NAME, SIPropertyCalculator);
		}

		public void UnbindToRegistry(IDynamicPropertiesRegistry registry)
		{
			registry.UnregistDynamicProperty((Name)"SI([target])");
		}

		private void ValidateKBLink()
		{
			if(m_kB==null)
				throw new InvalidOperationException($"Cannot execute operation as an instance of {nameof(EmotionalAppraisalAsset)} was not registed in this asset.");
		}

		/// <summary>
		/// Calculate the Social Importance value of a given target, in a particular perspective.
		/// If no perspective is given, the current agent's perspective is used as default.
		/// </summary>
		/// <remarks>
		/// All values calculated by this method are automatically cached, in order to optimize future searches.
		/// If the values are needed to be recalculated, call InvalidateCachedSI() to clear the cached values.
		/// </remarks>
		/// <param name="target">The name of target which we want to calculate the SI</param>
		/// <param name="perspective">From which perspective do we want to calculate de SI.</param>
		/// <returns>The value of Social Importance attributed to given target by the perspective of a particular agent.</returns>
		public float GetSocialImportance(string target, string perspective = "self")
		{
			ValidateKBLink();

			var t = Name.BuildName(target);
			if (!t.IsPrimitive)
				throw new ArgumentException("must be a primitive name", nameof(target));

			var p = m_kB.AssertPerspective(Name.BuildName(perspective));

			return internal_GetSocialImportance(t,p);
		}

		private float internal_GetSocialImportance(Name target, Name perspective)
		{
			NameSearchTree<float> targetDict;
			if (!m_cachedSI.TryGetValue(perspective, out targetDict))
			{
				targetDict = new NameSearchTree<float>();
				m_cachedSI[perspective] = targetDict;
			}

			float value;
			if (!targetDict.TryGetValue(target, out value))
			{
				value = CalculateSI(target, perspective);
				targetDict[target] = value;
			}
			return value;
		}

		/// <summary>
		/// Clears all cached Social Importance values, allowing new values to be recalculated uppon request.
		/// </summary>
		public void InvalidateCachedSI()
		{
			m_cachedSI.Clear();
		}

		/// <summary>
		/// Request a conferral action from the Social Importance Asset.
		/// </summary>
		/// <remarks>
		/// The action will be generated based on the defined Conferrals, and will always be the
		/// maximum valued conferral that can still respect the asset's defined Claims.
		/// </remarks>
		/// <param name="perspective">
		/// From which perspective do we want to generate the action.
		/// If the perspective is diferent from "self", it will be like the asset predicting which action will be
		/// executed by the entity defined in the perspective.
		/// </param>
		/// <returns>The action we want to execute or predict.</returns>
		public IAction DecideConferral(string perspective)
		{
			ValidateKBLink();

			var prp = Name.BuildName(perspective);
			var a = m_conferalActions.SelectAction(m_kB, prp).OrderByDescending(p=>p.Item2.ConferralSI);
			return internal_FilterActions(prp, a.Select(p=>p.Item1)).FirstOrDefault();
		}

		/// <summary>
		/// Filters a set of actions using the defined Social Importance Claims.
		/// </summary>
		/// <param name="perspective">
		/// From which perspective do we want to filter the actions.
		/// If the perspective is diferent from "self", it will be like the asset is
		/// evaluating desirable action from another entity's point of view.
		/// </param>
		/// <param name="actionsToFilter">
		/// The set of actions we want to filter.
		/// </param>
		/// <returns>
		/// The set of filtered actions.
		/// No action returned will have a Claim value higher that the Social Importance
		/// attributed to the target of the action.
		/// </returns>
		/// <see cref="ClaimDTO"/>
		public IEnumerable<IAction> FilterActions(string perspective, IEnumerable<IAction> actionsToFilter)
		{
			ValidateKBLink();

			var p = Name.BuildName(perspective);
			return internal_FilterActions(p, actionsToFilter);
		}

		private IEnumerable<IAction> internal_FilterActions(Name perspective, IEnumerable<IAction> actionsToFilter)
		{
			perspective = m_kB.AssertPerspective(perspective);
			foreach (var a in actionsToFilter)
			{
				uint minSI;
				if (m_claimTree.TryGetValue(a.Name, out minSI))
					if (internal_GetSocialImportance(a.Target, perspective) < minSI)
						continue;

				yield return a;
			}
		}

		private bool ValidateConferral(Conferral c,Name perspective, SubstitutionSet set)
		{
			var t = c.Target.MakeGround(set);
			if (!t.IsGrounded)
				return false;

			var si = internal_GetSocialImportance(t, perspective);
			return si >= c.ConferralSI;
		}

		private uint CalculateSI(Name target, Name perspective)
		{
			long value = 0;
			foreach (var a in m_attributionRules)
			{
				var sub = new Substitution(a.Target, target);
				if (a.Conditions.Evaluate(m_kB, perspective, new[] { new SubstitutionSet(sub) }))
					value += a.Value;
			}
			return value<1?1:(uint)value;
		}

		/// @cond DOXYGEN_SHOULD_SKIP_THIS

		protected override string OnAssetLoaded()
		{
			return null;
		}

		/// @endcond

		#region DTO operations

		#region Attribution Rules

		public IEnumerable<AttributionRuleDTO> GetAttributionRules()
		{
			return m_attributionRules.Select(r => r.ToDTO());
		}

		public AttributionRuleDTO AddAttributionRule(AttributionRuleDTO ruleDefinition)
		{
			var at = new AttributionRule(ruleDefinition);
			if (m_attributionRules.Add(at))
				return at.ToDTO();

			return null;
		}

		public void UpdateAttributionRule(AttributionRuleDTO ruleDefinition)
		{
			var rule = m_attributionRules.FirstOrDefault(a => a.GUID == ruleDefinition.Id);
			if(rule==null)
				throw new Exception("Attribution rule not found");

			rule.SetData(ruleDefinition);
		}

		public void RemoveAttributionRuleById(Guid id)
		{
			var rule = m_attributionRules.FirstOrDefault(a => a.GUID == id);
			if (rule == null)
				throw new Exception("Attribution rule not found");

			m_attributionRules.Remove(rule);
		}

		#endregion

		#region Claims

		public IEnumerable<ClaimDTO> GetClaims()
		{
			return m_claimTree.Select(pair => new ClaimDTO() {ActionTemplate = pair.Key.ToString(), ClaimSI = pair.Value});
		}

		public void AddClaim(ClaimDTO claim)
		{
			Name n = null;
			try
			{
				n = (Name)claim.ActionTemplate;
				m_claimTree.Add(n, claim.ClaimSI);
			}
			catch (DuplicatedKeyException)
			{
				throw new Exception($"There is already a claim associated to the action template \"{n}\"");
			}
		}

		public void ReplaceClaim(ClaimDTO oldClaim, ClaimDTO newClaim)
		{
			var oldN = (Name)oldClaim.ActionTemplate;
			if(!m_claimTree.ContainsKey(oldN))
				throw new ArgumentException($"No claim for \"{oldN}\" action template was found.");

			var newN = (Name) newClaim.ActionTemplate;
			if (oldN == newN)
			{
				m_claimTree[oldN] = newClaim.ClaimSI;
				return;
			}

			if(m_claimTree.ContainsKey(newN))
				throw new ArgumentException($"There is already a claim associated to \"{newN}\" action template.");

			m_claimTree.Remove(oldN);
			m_claimTree.Add(newN,newClaim.ClaimSI);
		}

		public bool RemoveClaim(string actionTemplate)
		{
			var n = (Name) actionTemplate;
			return m_claimTree.Remove(n);
		}

		#endregion

		#region Conferrals

		public IEnumerable<ConferralDTO> GetConferrals()
		{
			return m_conferalActions.GetAllActionDefinitions().Select(c => c.ToDTO());
		}

		public ConferralDTO AddConferral(ConferralDTO conferral)
		{
			var c = new Conferral(conferral);
			m_conferalActions.AddActionDefinition(c);
			return c.ToDTO();
		}

		public void UpdateConferralData(ConferralDTO conferral)
		{
			var c = m_conferalActions.GetActionDefinition(conferral.Id);
			m_conferalActions.RemoveActionDefinition(c);
			c.SetData(conferral);
			m_conferalActions.AddActionDefinition(c);
		}

		public void RemoveConferralById(Guid id)
		{
			var c = m_conferalActions.GetActionDefinition(id);
			m_conferalActions.RemoveActionDefinition(c);
		}

		#endregion

		/// <summary>
		/// Load a Social Importance Asset definition from a DTO object.
		/// </summary>
		/// <remarks>
		/// Use this to procedurally configure the asset.
		/// </remarks>
		/// <param name="dto">
		/// The DTO containing the data to load
		/// </param>
		public void LoadFromDTO(SocialImportanceDTO dto)
		{
			m_attributionRules.Clear();
			m_attributionRules.UnionWith(dto.AttributionRules.Select(adto => new AttributionRule(adto)));

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

		/// <summary>
		/// Returns a DTO containing all the asset's configurations.
		/// </summary>
		public SocialImportanceDTO GetDTO()
		{
			var at = m_attributionRules.Select(a => a.ToDTO()).ToArray();
			var claims = m_claimTree.Select(p => new ClaimDTO() { ActionTemplate = p.Key.ToString(), ClaimSI = p.Value }).ToArray();
			var conferrals = m_conferalActions.GetAllActionDefinitions().Select(c => c.ToDTO()).ToArray();
			return new SocialImportanceDTO() { AttributionRules = at, Claims = claims, Conferral = conferrals };
		}

		#endregion

		#region Dynamic Properties

		private static readonly Name SI_DYNAMIC_PROPERTY_NAME = Name.BuildName("SI");
		private IEnumerable<DynamicPropertyResult> SIPropertyCalculator(IQueryContext context, Name target)
		{

            foreach (var t in context.AskPossibleProperties(target))
			{
				var si = internal_GetSocialImportance(t.Item1, context.Perspective);
				foreach (var s in t.Item2)
					yield return new DynamicPropertyResult(Name.BuildName(si), s);
			}
		}

		#endregion

		#region Serialization

		/// @cond DOXYGEN_SHOULD_SKIP_THIS

		public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			dataHolder.SetValue("AttributionRules",m_attributionRules.ToArray());
			dataHolder.SetValue("Claims",m_claimTree.Select(p=>new ClaimDTO() {ActionTemplate = p.Key.ToString(),ClaimSI = p.Value}).ToArray());
			dataHolder.SetValue("Conferrals",m_conferalActions.GetAllActionDefinitions().ToArray());
		}

		public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			m_attributionRules = new HashSet<AttributionRule>(dataHolder.GetValue<AttributionRule[]>("AttributionRules"));
			
			var claims = dataHolder.GetValue<ClaimDTO[]>("Claims");
			m_claimTree = new NameSearchTree<uint>();
			foreach (var c in claims)
				m_claimTree.Add(Name.BuildName(c.ActionTemplate), c.ClaimSI);

			var conferrals = dataHolder.GetValue<Conferral[]>("Conferrals");
			m_conferalActions = new ActionSelector<Conferral>(ValidateConferral);
			foreach (var c in conferrals)
				m_conferalActions.AddActionDefinition(c);

			m_cachedSI = new NameSearchTree<NameSearchTree<float>>();
		}

		/// @endcond

		#endregion
	}
}