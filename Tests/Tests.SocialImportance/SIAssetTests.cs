using Conditions.DTOs;
using EmotionalAppraisal.DTOs;
using KnowledgeBase;
using NUnit.Framework;
using SocialImportance;
using SocialImportance.DTOs;
using WellFormedNames;

namespace Tests.SocialImportance
{
	[TestFixture]
	public class SIAssetTests
	{
		private static SocialImportanceAsset ASSET_TO_TEST = BuildAsset();

		private static SocialImportanceAsset BuildAsset()
		{
			var kb = new KB((Name)"Matt");
            #region Set KB
            kb.Tell((Name)"IsPerson(Matt)", (Name)"true", (Name)"*");
            kb.Tell((Name)"IsPerson(Mary)", (Name)"true", (Name)"*");
            kb.Tell((Name)"IsPerson(Thomas)", (Name)"true", (Name)"*");
            kb.Tell((Name)"IsPerson(Diego)", (Name)"true", (Name)"*");
            kb.Tell((Name)"IsPerson(Robot)", (Name)"true", (Name)"Diego");
            kb.Tell((Name)"IsOutsider(Diego)", (Name)"true", (Name)"*");
            kb.Tell((Name)"IsOutsider(Diego)", (Name)"false", (Name)"Robot");
            kb.Tell((Name)"AreFriends(SELF,Mary)", (Name)"true", (Name)"SELF");
            kb.Tell((Name)"AreFriends(SELF,Matt)", (Name)"true", (Name)"Mary");
            kb.Tell((Name)"AreFriends(SELF,Thomas)", (Name)"true", (Name)"SELF");
            kb.Tell((Name)"IsBartender(Matt)", (Name)"true", (Name)"*");

			#endregion

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
							ConditionSet = new []
							{
								"IsPerson([target]) = true",
								"[target] != Self"
							}
						}
					},
					new AttributionRuleDTO()
					{
						Target = "[target]",
						Value = -1,
						Conditions = new ConditionSetDTO()
						{
							ConditionSet = new []
							{
								"IsOutsider([target]) = true",
								"[target] != Self"
							}
						}
					},
					new AttributionRuleDTO()
					{
						Target = "[target]",
						Value = 15,
						Conditions = new ConditionSetDTO()
						{
							ConditionSet = new []
							{
								"AreFriends(Self,[target]) = true"
							}
						}
					},
					new AttributionRuleDTO()
					{
						Target = "[target]",
						Value = 10,
						Conditions = new ConditionSetDTO()
						{
							ConditionSet = new []
							{
								"IsClient([target]) = true",
								"IsBartender(Self) = true",
								"[target] != Self"
							}
						}
					},
					new AttributionRuleDTO()
					{
						Target = "[target]",
						Value = 1,
						Conditions = new ConditionSetDTO()
						{
							ConditionSet = new []
							{
								"IsElder([target]) = true",
								"IsElder(Self) = false"
							}
						}
					}
				},
				Conferral = new[]
				{
					new ConferralDTO()
					{
						Action = "Give(Drink)",
						ConferralSI = 10,
						Target = "[x]",
						Conditions = new ConditionSetDTO()
						{
							ConditionSet = new []
							{
								"AskedDrink([x])=true"
							}
						}
					},
					new ConferralDTO()
					{
						Action = "Give(Best-Drink)",
						ConferralSI = 23,
						Target = "[x]",
						Conditions = new ConditionSetDTO()
						{
							ConditionSet = new []
							{
								"AskedDrink([x])=true"
							}
						}
					}
				}
			};
			#endregion
			var si = new SocialImportanceAsset();
			si.LoadFromDTO(siDTO);
            si.RegisterKnowledgeBase(kb);
			return si;
		}

		[TestCase("Matt", "Mary", 35, 35)]
		[TestCase("Matt", "Diego", 19, 20)]
		[TestCase("Matt", "Thomas", 35, 20)]
		[TestCase("Matt", "Robot", 1, 20)]
		[TestCase("Mary", "Diego", 19, 20)]
		[TestCase("Mary", "Thomas", 20, 20)]
		[TestCase("Mary", "Robot", 1, 20)]
		[TestCase("Diego", "Thomas", 20, 19)]
		[TestCase("Diego", "Robot", 20, 20)]
		[TestCase("Thomas", "Robot", 1, 20)]
		public static void Test_SI_Values(string a, string b, float abSiValue, float baSiValue)
		{
			ASSET_TO_TEST.InvalidateCachedSI();
			var ab = ASSET_TO_TEST.GetSocialImportance(b, a); 
			var ba = ASSET_TO_TEST.GetSocialImportance(a, b);

			Assert.AreEqual(abSiValue,ab);
			Assert.AreEqual(baSiValue,ba);
		}

		[TestCase("Diego","Give(Drink)")]
		[TestCase("Mary", "Give(Best-Drink)")]
		[TestCase("Robot", null)]
		public static void Test_Conferrals(string name, string expectedResult)
		{
			var n = new BeliefDTO()
			{
				Name = $"AskedDrink({name})",
				Perspective = "Self",
				Value = "true"
			};
			
            ASSET_TO_TEST.LinkedKB.Tell((Name)n.Name,(Name)n.Value,(Name)n.Perspective);
			var a = ASSET_TO_TEST.DecideConferral(Name.SELF_STRING);
			ASSET_TO_TEST.LinkedKB.Tell((Name)n.Name, null, (Name)n.Perspective); 
			if (string.IsNullOrEmpty(expectedResult))
			{
				if(a==null)
					Assert.Pass();
				else
					Assert.Fail();
			}

			var an = (Name)expectedResult;
			Assert.AreEqual(an, a.Name);
		}
	}
}