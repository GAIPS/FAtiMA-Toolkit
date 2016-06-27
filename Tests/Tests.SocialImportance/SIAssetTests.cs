using System.Runtime.InteropServices.WindowsRuntime;
using ActionLibrary;
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
			ea.Kb.Tell((Name)"IsBartender(Matt)", true, Name.UNIVERSAL_SYMBOL);

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
					},
					new AttributionRuleDTO()
					{
						Target = "[target]",
						Value = -1,
						Conditions = new ConditionSetDTO()
						{
							ConditionSet = new []
							{
								"IsElder([target]) = false",
								"IsElder(Self) = true"
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

			si.BindEmotionalAppraisalAsset(ea);
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

			Assert.AreEqual(ab,abSiValue);
			Assert.AreEqual(ba, baSiValue);
		}

		[TestCase("Diego","Give(Drink)")]
		[TestCase("Mary", "Give(Best-Drink)")]
		[TestCase("Robot", null)]
		public static void Test_Conferrals(string name, string expectedResult)
		{
			var n = Name.BuildName((Name) "AskedDrink", (Name) name);
			ASSET_TO_TEST.LinkedEA.Kb.Tell(n,true);
			var a = ASSET_TO_TEST.DecideConferral(Name.SELF_STRING);
			ASSET_TO_TEST.LinkedEA.Kb.Tell(n,null);

			if (string.IsNullOrEmpty(expectedResult))
			{
				if(a==null)
					Assert.Pass();
				else
					Assert.Fail();
			}

			var an = (Name)expectedResult;
			Assert.AreEqual(an, a.ToNameRepresentation());
		}
	}
}