using Conditions.DTOs;
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
						Target = (Name)"[target]",
						Value = (Name)"[v]",
						Conditions = new ConditionSetDTO()
						{
							ConditionSet = new []
							{
								"IsPerson([target]) = true",
								"[target] != Self",
                                "[v] = 20"
							}
						}
					},
					new AttributionRuleDTO()
					{
						Target =  (Name)"[target]",
						Value =  (Name)"-1",
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
						Target = (Name)"[target]",
						Value = (Name)"15",
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
						Target = (Name)"[target]",
						Value = (Name)"10",
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
						Target = (Name)"[target]",
						Value = (Name)"1",
						Conditions = new ConditionSetDTO()
						{
							ConditionSet = new []
							{
								"IsElder([target]) = true",
								"IsElder(Self) = false"
							}
						}
					}
				}
			};
			#endregion
			var si = new SocialImportanceAsset();
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
            var asset = BuildAsset();
            asset.InvalidateCachedSI();

			var ab = asset.GetSocialImportance(b, a); 
			var ba = asset.GetSocialImportance(a, b);

			Assert.AreEqual(abSiValue,ab);
			Assert.AreEqual(baSiValue,ba);
		}

	}
}