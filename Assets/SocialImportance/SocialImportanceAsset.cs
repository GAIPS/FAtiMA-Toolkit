using System;
using System.Collections.Generic;
using System.Linq;
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
	public sealed class SocialImportanceAsset: Asset<SocialImportanceAsset>, ICustomSerialization
	{
		private KB m_kB;
		private HashSet<AttributionRule> m_attributionRules;

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
			registry.RegistDynamicProperty(SI_DYNAMIC_PROPERTY_NAME, "", SIPropertyCalculator);
		}

		public void UnbindToRegistry(IDynamicPropertiesRegistry registry)
		{
			registry.UnregistDynamicProperty((Name)"SI([target])");
		}

		private void ValidateKBLink()
		{
			if(m_kB==null)
				throw new InvalidOperationException($"Cannot execute operation as an instance of KB was not registed in this asset.");
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

		private uint CalculateSI(Name target, Name perspective)
		{
			long result = 0;

			foreach (var a in m_attributionRules)
			{
				var sub = new Substitution(a.Target, new ComplexValue(target));
                var resultSubSet = a.Conditions.Unify(m_kB, perspective, new[] { new SubstitutionSet(sub) });
                foreach(var set in resultSubSet)
                {
                    var v = a.Value.MakeGround(set);
                    result += int.Parse(v.ToString());
                }
			}
			return result<1?1:(uint)result;
		}


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

		private static readonly Name SI_DYNAMIC_PROPERTY_NAME = Name.BuildName("SI");
		private IEnumerable<DynamicPropertyResult> SIPropertyCalculator(IQueryContext context, Name target)
		{
            foreach (var t in context.AskPossibleProperties(target))
			{
				var si = internal_GetSocialImportance(t.Item1.Value, context.Perspective);
				foreach (var s in t.Item2)
					yield return new DynamicPropertyResult(new ComplexValue(Name.BuildName(si)), s);
			}
		}

		/// @cond DOXYGEN_SHOULD_SKIP_THIS

		public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			dataHolder.SetValue("AttributionRules",m_attributionRules.ToArray());
		}

		public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
		{
			m_attributionRules = new HashSet<AttributionRule>(dataHolder.GetValue<AttributionRule[]>("AttributionRules"));
            foreach(var rule in m_attributionRules)
            {
                rule.GUID = Guid.NewGuid();
            }
			m_cachedSI = new NameSearchTree<NameSearchTree<float>>();
		}

		/// @endcond
	}
}