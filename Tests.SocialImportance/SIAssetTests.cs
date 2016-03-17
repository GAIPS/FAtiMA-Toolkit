using EmotionalAppraisal;
using KnowledgeBase.DTOs.Conditions;
using KnowledgeBase.WellFormedNames;
using NUnit.Framework;
using SocialImportance;
using SocialImportance.DTOs;

namespace Tests.SocialImportance
{
	[TestFixture]
	public class SIAssetTests
	{
		private static SocialImportanceAsset ASSET_TO_TEST = BuildAsset();

		private static SocialImportanceAsset BuildAsset()
		{
			var ea = new EmotionalAppraisalAsset("Matt");

#region Set KB

			ea.Kb.Tell((Name)"IsPerson(Matt)",true,Name.UNIVERSAL_SYMBOL);
			ea.Kb.Tell((Name)"IsPerson(Mary)", true, Name.UNIVERSAL_SYMBOL);
			ea.Kb.Tell((Name)"IsPerson(Diego)", true, Name.UNIVERSAL_SYMBOL);
			ea.Kb.Tell((Name)"IsPerson(Thomas)", true, Name.UNIVERSAL_SYMBOL);
			ea.Kb.Tell((Name)"IsPerson(Robot)", true, (Name)"Diego");
			ea.Kb.Tell((Name)"IsOutsider(Diego)", true, Name.UNIVERSAL_SYMBOL);
			ea.Kb.Tell((Name)"IsOutsider(Diego)", false, (Name)"Robot");
			ea.Kb.Tell((Name)"AreFriends(Self,Mary)", true, Name.SELF_SYMBOL);
			ea.Kb.Tell((Name)"AreFriends(Self,Matt)", true, (Name)"Mary");
			ea.Kb.Tell((Name)"AreFriends(Self,Thomas)", true, Name.SELF_SYMBOL);

			#endregion

			var si = new SocialImportanceAsset(ea);
#region SI DTO especification
			var siDTO = new SocialImportanceDTO
			{
				AttributionRules = new[]
				{
					new AttributionRuleDTO()
					{
						Target = "[target]",
						Value = 20,
						Conditions = new ConditionSetDTO()
						{
							Set = new []
							{
								new ConditionDTO()
								{
									Condition = "IsPerson([target]) = true"
								},
								new ConditionDTO()
								{
									Condition = "[target] != Self"
								}
							}
						}
					},
					new AttributionRuleDTO()
					{
						Target = "[target]",
						Value = -1,
						Conditions = new ConditionSetDTO()
						{
							Set = new []
							{
								new ConditionDTO()
								{
									Condition = "IsOutsider([target]) = true"
								},
								new ConditionDTO()
								{
									Condition = "[target] != Self"
								}
							}
						}
					},
					new AttributionRuleDTO()
					{
						Target = "[target]",
						Value = 15,
						Conditions = new ConditionSetDTO()
						{
							Set = new []
							{
								new ConditionDTO()
								{
									Condition = "AreFriends(Self,[target]) = true"
								}
							}
						}
					},
					new AttributionRuleDTO()
					{
						Target = "[target]",
						Value = 10,
						Conditions = new ConditionSetDTO()
						{
							Set = new []
							{
								new ConditionDTO()
								{
									Condition = "IsClient([target]) = true"
								},
								new ConditionDTO()
								{
									Condition = "IsBartender(Self) = true"
								},
								new ConditionDTO()
								{
									Condition = "[target] != Self"
								}
							}
						}
					},
					new AttributionRuleDTO()
					{
						Target = "[target]",
						Value = 1,
						Conditions = new ConditionSetDTO()
						{
							Set = new []
							{
								new ConditionDTO()
								{
									Condition = "IsElder([target]) = true"
								},
								new ConditionDTO()
								{
									Condition = "IsElder(Self) = false"
								}
							}
						}
					},
					new AttributionRuleDTO()
					{
						Target = "[target]",
						Value = -1,
						Conditions = new ConditionSetDTO()
						{
							Set = new []
							{
								new ConditionDTO()
								{
									Condition = "IsElder([target]) = false"
								},
								new ConditionDTO()
								{
									Condition = "IsElder(Self) = true"
								}
							}
						}
					}
				}
			};
#endregion
			si.LoadFromDTO(siDTO);
			return si;
		}

		[TestCase("Matt","Mary",35,35)]
		[TestCase("Matt", "Diego", 19, 20)]
		[TestCase("Matt", "Thomas", 35, 20)]
		[TestCase("Matt", "Robot", 0, 20)]
		[TestCase("Mary", "Diego", 19, 20)]
		[TestCase("Mary", "Thomas", 20, 20)]
		[TestCase("Mary", "Robot", 0, 20)]
		[TestCase("Diego", "Thomas", 20, 19)]
		[TestCase("Diego", "Robot", 20, 20)]
		[TestCase("Thomas", "Robot", 0, 20)]
		public static void Test_SI_Values(string a, string b, float abSiValue, float baSiValue)
		{
			var ab = ASSET_TO_TEST.GetSocialImportance((Name) a, (Name) b);
			var ba = ASSET_TO_TEST.GetSocialImportance((Name)b, (Name)a);

			Assert.AreEqual(ab,abSiValue);
			Assert.AreEqual(ba, baSiValue);
		}
	}
}