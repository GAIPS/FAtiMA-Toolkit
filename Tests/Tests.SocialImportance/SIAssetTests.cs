using ActionLibrary;
using Conditions.DTOs;
using EmotionalAppraisal;
using EmotionalAppraisal.DTOs;
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
			var ea = new EmotionalAppraisalAsset("Matt");
#region Set KB

			ea.AddOrUpdateBelief(new BeliefDTO()
			{
				Name = "IsPerson(Matt)",
				Perspective = "*",
				Value = "true"
			});
			ea.AddOrUpdateBelief(new BeliefDTO()
			{
				Name = "IsPerson(Mary)",
				Perspective = "*",
				Value = "true"
			});
			ea.AddOrUpdateBelief(new BeliefDTO()
			{
				Name = "IsPerson(Diego)",
				Perspective = "*",
				Value = "true"
			});
			ea.AddOrUpdateBelief(new BeliefDTO()
			{
				Name = "IsPerson(Thomas)",
				Perspective = "*",
				Value = "true"
			});
			ea.AddOrUpdateBelief(new BeliefDTO()
			{
				Name = "IsPerson(Robot)",
				Perspective = "Diego",
				Value = "true"
			});
			ea.AddOrUpdateBelief(new BeliefDTO()
			{
				Name = "IsOutsider(Diego)",
				Perspective = "*",
				Value = "true"
			});
			ea.AddOrUpdateBelief(new BeliefDTO()
			{
				Name = "IsOutsider(Diego)",
				Perspective = "Robot",
				Value = "false"
			});
			ea.AddOrUpdateBelief(new BeliefDTO()
			{
				Name = "AreFriends(Self,Mary)",
				Perspective = "Self",
				Value = "true"
			});
			ea.AddOrUpdateBelief(new BeliefDTO()
			{
				Name = "AreFriends(Self,Matt)",
				Perspective = "Mary",
				Value = "true"
			});
			ea.AddOrUpdateBelief(new BeliefDTO()
			{
				Name = "AreFriends(Self,Thomas)",
				Perspective = "Self",
				Value = "true"
			});
			ea.AddOrUpdateBelief(new BeliefDTO()
			{
				Name = "IsBartender(Matt)",
				Perspective = "*",
				Value = "true"
			});

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

			//si.BindEmotionalAppraisalAsset(ea);
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
			var n = new BeliefDTO()
			{
				Name = $"AskedDrink({name})",
				Perspective = "Self",
				Value = "true"
			};
			//ASSET_TO_TEST.LinkedEA.AddOrUpdateBelief(n);
			var a = ASSET_TO_TEST.DecideConferral(Name.SELF_STRING);
			//ASSET_TO_TEST.LinkedEA.RemoveBelief($"AskedDrink({name})","self");

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