
using System;
using System.IO;
using EmotionalDecisionMaking;
using SerializationUtilities;
using ActionLibrary.DTOs;
using NUnit.Framework;
using Conditions;
using WellFormedNames;
using System.Collections.Generic;
using Conditions.DTOs;

namespace Tests.EmotionalDecisionMaking
{
	[TestFixture]
	public class EDMTests
	{
		private EmotionalDecisionMakingAsset BuildTestAsset()
		{
			var asset = new EmotionalDecisionMakingAsset();

			asset.AddActionRule(new ActionRuleDTO() {Action = Name.BuildName("Speak([speachType])"), Target = Name.BuildName("[x]")});
			asset.AddActionRule(new ActionRuleDTO() { Action = Name.BuildName("Speak(formal)"), Target = Name.BuildName("[x]") });
			return asset;
		}

		[TestCase]
		public void Test_EDM_AddRuleCondition()
		{
			var asset = BuildTestAsset();

            Guid id = new Guid();
            foreach( var rule in asset.GetAllActionRules())
            {
                id = rule.Id;
                break;
            }

            asset.AddRuleCondition(id, "[x] != SELF");

            Assert.IsNotEmpty(asset.GetActionRule(id).Conditions.ConditionSet);

			
		}

        [TestCase]
        public void Test_EDM_UpdateRuleCondition()
        {
            var asset = BuildTestAsset();

            Guid id = new Guid();
            foreach (var rule in asset.GetAllActionRules())
            {
                id = rule.Id;
                break;
            }

            asset.AddRuleCondition(id, "[x] != SELF");

            Assert.AreEqual(asset.GetActionRule(id).Conditions.ConditionSet[0], "[x] != SELF");

            asset.UpdateRuleCondition(id, "[x] != SELF", "Has(Floor) = SELF");

            Assert.AreEqual(asset.GetActionRule(id).Conditions.ConditionSet[0], "Has(Floor) = SELF");

        }

        [TestCase]
        public void Test_EDM_UpdateRuleConditions()
        {
            var asset = BuildTestAsset();

            Guid id = new Guid();
            foreach (var rule in asset.GetAllActionRules())
            {
                id = rule.Id;
                break;
            }

            asset.AddRuleCondition(id, "[x] != SELF");

            Assert.AreEqual(asset.GetActionRule(id).Conditions.ConditionSet[0], "[x] != SELF");

            Conditions.DTOs.ConditionSetDTO condSet = new ConditionSetDTO() { ConditionSet = new string[] { "[x] != Start", "Has(Floor) = SELF" } };

            asset.UpdateRuleConditions(id, condSet);

            Assert.AreEqual(asset.GetActionRule(id).Conditions.ConditionSet.Length, 2);

        }

        [TestCase]
        public void Test_EDM_RemoveRuleConditions()
        {
            var asset = BuildTestAsset();

            Guid id = new Guid();
            foreach (var rule in asset.GetAllActionRules())
            {
                id = rule.Id;
                break;
            }

            asset.AddRuleCondition(id, "[x] != SELF");

            Assert.IsNotEmpty(asset.GetActionRule(id).Conditions.ConditionSet);

            asset.RemoveRuleConditions(id, new List<string> { "[x] != SELF"});

            Assert.IsEmpty(asset.GetActionRule(id).Conditions.ConditionSet);

        }

        [TestCase]
        public void Test_EDM_RemoveActionRules()
        {
            var asset = BuildTestAsset();

            List<Guid> ids = new List<Guid>();

            foreach (var rule in asset.GetAllActionRules())
            {
                ids.Add(rule.Id);
            }

            asset.RemoveActionRules(ids);

            Assert.IsEmpty(asset.GetAllActionRules());

        }

        [TestCase]
        public void Test_EDM_UpdateActionRule()
        {
            var asset = BuildTestAsset();

            Guid id = new Guid();
            foreach (var rule in asset.GetAllActionRules())
            {
                id = rule.Id;
                break;
            }

            var toEdit = asset.GetActionRule(id);

            ActionRuleDTO newAction = new ActionRuleDTO()
            {
                Action = (Name)"EnterRoom",
                Id = id,
                Target = (Name)"[x]",
                Conditions = new ConditionSetDTO()
            };

            asset.UpdateActionRule(toEdit, newAction);

            id = new Guid();
            foreach (var rule in asset.GetAllActionRules())
            {
                id = rule.Id;
                break;
            }

            Assert.AreEqual(asset.GetActionRule(id).Action, (Name)"EnterRoom");

        }

    }
}