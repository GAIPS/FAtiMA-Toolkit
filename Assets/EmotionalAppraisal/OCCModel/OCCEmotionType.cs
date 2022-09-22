using EmotionalAppraisal.DTOs;
using System.Collections.Generic;
using WellFormedNames;

namespace EmotionalAppraisal.OCCModel
{
    public sealed class OCCEmotionType
	{
		#region internal constants

		private static readonly string[] Attribution = { OCCAppraisalVariables.PRAISEWORTHINESS };

		private static readonly string[] WellBeing = { OCCAppraisalVariables.DESIRABILITY };

		private static readonly string[] FortuneOfOthers = { OCCAppraisalVariables.DESIRABILITY,
															   OCCAppraisalVariables.DESIRABILITY_FOR_OTHER};

		private static readonly string[] Attraction = { OCCAppraisalVariables.LIKE };

		private static readonly string[] Composed = { OCCAppraisalVariables.DESIRABILITY,
														OCCAppraisalVariables.PRAISEWORTHINESS};

		private static readonly string[] PositiveProspect = {  OCCAppraisalVariables.GOALSUCCESSPROBABILITY};

		private static readonly string[] NegativeProspect = { OCCAppraisalVariables.GOALSUCCESSPROBABILITY};

		#endregion

		public static readonly OCCEmotionType Admiration = new OCCEmotionType("Admiration",EmotionValence.Positive, true, Attribution);
		public static readonly OCCEmotionType Anger = new OCCEmotionType("Anger", EmotionValence.Negative, true, Composed);
		public static readonly OCCEmotionType Gratitude = new OCCEmotionType("Gratitude", EmotionValence.Positive, true, Composed);
		public static readonly OCCEmotionType Disappointment = new OCCEmotionType("Disappointment", EmotionValence.Negative, true, PositiveProspect);
		public static readonly OCCEmotionType Distress = new OCCEmotionType("Distress", EmotionValence.Negative, true, WellBeing);
		public static readonly OCCEmotionType Fear = new OCCEmotionType("Fear", EmotionValence.Negative, false, NegativeProspect);
		public static readonly OCCEmotionType FearsConfirmed = new OCCEmotionType("Fears-Confirmed", EmotionValence.Negative, true, NegativeProspect);
		public static readonly OCCEmotionType Gratification = new OCCEmotionType("Gratification", EmotionValence.Positive, true, Composed);
		public static readonly OCCEmotionType Gloating = new OCCEmotionType("Gloating", EmotionValence.Positive, true, FortuneOfOthers);
		public static readonly OCCEmotionType HappyFor = new OCCEmotionType("Happy-For", EmotionValence.Positive, true, FortuneOfOthers);
		public static readonly OCCEmotionType Hate = new OCCEmotionType("Hate", EmotionValence.Negative, true, Attraction);
		public static readonly OCCEmotionType Hope = new OCCEmotionType("Hope", EmotionValence.Positive, false, PositiveProspect);
		public static readonly OCCEmotionType Joy = new OCCEmotionType("Joy", EmotionValence.Positive, true, WellBeing);
		public static readonly OCCEmotionType Love = new OCCEmotionType("Love", EmotionValence.Positive, true, Attraction);
		public static readonly OCCEmotionType Pity = new OCCEmotionType("Pity", EmotionValence.Negative, true, FortuneOfOthers);
		public static readonly OCCEmotionType Pride = new OCCEmotionType("Pride", EmotionValence.Positive, true, Attribution);
		public static readonly OCCEmotionType Relief = new OCCEmotionType("Relief", EmotionValence.Positive, true, NegativeProspect);
		public static readonly OCCEmotionType Remorse = new OCCEmotionType("Remorse", EmotionValence.Negative, true, Composed);
		public static readonly OCCEmotionType Reproach = new OCCEmotionType("Reproach", EmotionValence.Negative, true, Attribution);
		public static readonly OCCEmotionType Resentment = new OCCEmotionType("Resentment", EmotionValence.Negative, true, FortuneOfOthers);
		public static readonly OCCEmotionType Satisfaction = new OCCEmotionType("Satisfaction", EmotionValence.Positive, true, PositiveProspect);
		public static readonly OCCEmotionType Shame = new OCCEmotionType("Shame", EmotionValence.Negative, true, Attribution);


	    public static OCCEmotionType Parse(string type)
	    {
	        switch (type)
	        {
                case "Admiration": return Admiration;
                case "Anger": return Anger;
                case "Gratitude": return Gratitude;
                case "Distress": return Distress;
                case "Joy": return Joy;
                case "Pride": return Pride;
                case "Remorse": return Remorse;
                case "Reproach": return Reproach;
                case "Shame": return Shame;
				case "Pity": return Pity;
				case "Disappointment": return Disappointment;
				case "Satisfaction": return Satisfaction;
				case "Resentment": return Resentment;
				case "Love": return Love;
				case "Hate": return Hate;
				case "Gloating": return Gloating;
				case "Fear": return Fear;
				case "FearsConfirmed": return FearsConfirmed;
				case "Relief": return Relief;
				case "Hope": return Hope;
				case "HappyFor": return HappyFor;
				case "Gratification": return Gratification;
				default: return null;
	        }
	    }

	    public static string[] Types =
	    {
	        Admiration.Name,
            Anger.Name,
            Gratitude.Name,
            Distress.Name,
            Gratification.Name,
            Joy.Name,
            Pride.Name,
            Reproach.Name,
            Shame.Name,
			HappyFor.Name,
			Hope.Name,
			Relief.Name,
			FearsConfirmed.Name,
			Fear.Name,
			Gloating.Name,
			Love.Name,
			Hate.Name,
			Resentment.Name,
			Satisfaction.Name,
			Disappointment.Name,
			Pity.Name

		};

        
		#region Class Definition

		public readonly string Name;
		public readonly EmotionValence Valence;
		public readonly IEnumerable<string> AppraisalVariables;
		public readonly bool InfluencesMood;

		private OCCEmotionType(string name, EmotionValence valence, bool influencesMood, string[] appraisalVariables)
		{
			Name = name;
			Valence = valence;
			InfluencesMood = influencesMood;
			AppraisalVariables = appraisalVariables;
		}

		#endregion


		public static List<AppraisalVariableDTO> getVariableFromEmotion(string emotion)
        {

			switch (emotion)
			{
				case nameof(OCCEmotionType.Admiration):

					return new List<AppraisalVariableDTO>(){

						new AppraisalVariableDTO()
						{
							Name = OCCAppraisalVariables.PRAISEWORTHINESS,
							Value = WellFormedNames.Name.BuildName(5),
							Target = WellFormedNames.Name.BuildName("[target]")
						}
					};

				case nameof(OCCEmotionType.Anger):

					return new List<AppraisalVariableDTO>(){ 
						
						new AppraisalVariableDTO()
						{
							Name= OCCAppraisalVariables.PRAISEWORTHINESS,
							Value = WellFormedNames.Name.BuildName(-5),
							Target = WellFormedNames.Name.BuildName("[target]")
						},
						new AppraisalVariableDTO()
						{
							Name= OCCAppraisalVariables.DESIRABILITY,
							Value = WellFormedNames.Name.BuildName(-5),
							Target = WellFormedNames.Name.NIL_SYMBOL
						}
					};

				case nameof(OCCEmotionType.Gratitude):

					return new List<AppraisalVariableDTO>(){

						new AppraisalVariableDTO()
						{
							Name= OCCAppraisalVariables.PRAISEWORTHINESS,
							Value = WellFormedNames.Name.BuildName(5),
							Target = WellFormedNames.Name.BuildName("[target]")
						},
						new AppraisalVariableDTO()
						{
							Name= OCCAppraisalVariables.DESIRABILITY,
							Value = WellFormedNames.Name.BuildName(5),
							Target = WellFormedNames.Name.NIL_SYMBOL
						}
					};
				case nameof(OCCEmotionType.Distress):

					return new List<AppraisalVariableDTO>(){

						new AppraisalVariableDTO()
						{
							Name= OCCAppraisalVariables.DESIRABILITY,
							Value = WellFormedNames.Name.BuildName(-5),
							Target = WellFormedNames.Name.NIL_SYMBOL
						}
					};

				case nameof(OCCEmotionType.Gratification):

					return new List<AppraisalVariableDTO>(){

						new AppraisalVariableDTO()
						{
							Name= OCCAppraisalVariables.PRAISEWORTHINESS,
							Value = WellFormedNames.Name.BuildName(5),
							Target = WellFormedNames.Name.SELF_SYMBOL
						},
						new AppraisalVariableDTO()
						{
							Name= OCCAppraisalVariables.DESIRABILITY,
							Value = WellFormedNames.Name.BuildName(5),
							Target = WellFormedNames.Name.NIL_SYMBOL
						}
					};

				case nameof(OCCEmotionType.Joy):

					return new List<AppraisalVariableDTO>(){

						new AppraisalVariableDTO()
						{
							Name= OCCAppraisalVariables.DESIRABILITY,
							Value = WellFormedNames.Name.BuildName(5),
							Target = WellFormedNames.Name.NIL_SYMBOL
						}
					};


				case nameof(OCCEmotionType.Pride):

					return new List<AppraisalVariableDTO>(){

						new AppraisalVariableDTO()
						{
							Name= OCCAppraisalVariables.PRAISEWORTHINESS,
							Value = WellFormedNames.Name.BuildName(5),
							Target = WellFormedNames.Name.SELF_SYMBOL
						}
					};

				case nameof(OCCEmotionType.Shame):

					return new List<AppraisalVariableDTO>(){

						new AppraisalVariableDTO()
						{
							Name= OCCAppraisalVariables.PRAISEWORTHINESS,
							Value = WellFormedNames.Name.BuildName(5),
							Target = WellFormedNames.Name.SELF_SYMBOL
						}
					};
				case nameof(OCCEmotionType.Reproach):

					return new List<AppraisalVariableDTO>(){

						new AppraisalVariableDTO()
						{
							Name= OCCAppraisalVariables.PRAISEWORTHINESS,
							Value = WellFormedNames.Name.BuildName(5),
							Target = WellFormedNames.Name.SELF_SYMBOL
						}
					};
				case nameof(OCCEmotionType.HappyFor):

					return new List<AppraisalVariableDTO>(){

							new AppraisalVariableDTO()
						{
							Name= OCCAppraisalVariables.DESIRABILITY,
							Value = WellFormedNames.Name.BuildName(5),
							Target = WellFormedNames.Name.NIL_SYMBOL
						},
						new AppraisalVariableDTO()
						{
							Name= OCCAppraisalVariables.DESIRABILITY_FOR_OTHER,
							Value = WellFormedNames.Name.BuildName(5),
							Target = WellFormedNames.Name.BuildName("[target]")
						}
					};
				case nameof(OCCEmotionType.Resentment):

					return new List<AppraisalVariableDTO>(){

							new AppraisalVariableDTO()
						{
							Name= OCCAppraisalVariables.DESIRABILITY,
							Value = WellFormedNames.Name.BuildName(-5),
							Target = WellFormedNames.Name.SELF_SYMBOL
						},
						new AppraisalVariableDTO()
						{
							Name= OCCAppraisalVariables.DESIRABILITY_FOR_OTHER,
							Value = WellFormedNames.Name.BuildName(5),
							Target = WellFormedNames.Name.BuildName("[target]")
						}
					};

				case nameof(OCCEmotionType.Gloating):

					return new List<AppraisalVariableDTO>(){

							new AppraisalVariableDTO()
						{
							Name= OCCAppraisalVariables.DESIRABILITY,
							Value = WellFormedNames.Name.BuildName(5),
							Target = WellFormedNames.Name.NIL_SYMBOL
						},
						new AppraisalVariableDTO()
						{
							Name= OCCAppraisalVariables.DESIRABILITY_FOR_OTHER,
							Value = WellFormedNames.Name.BuildName(-5),
							Target = WellFormedNames.Name.BuildName("[target]")
						}
					};

				case nameof(OCCEmotionType.Pity):

					return new List<AppraisalVariableDTO>(){

							new AppraisalVariableDTO()
						{
							Name= OCCAppraisalVariables.DESIRABILITY,
							Value = WellFormedNames.Name.BuildName(-5),
							Target = WellFormedNames.Name.NIL_SYMBOL
						},
						new AppraisalVariableDTO()
						{
							Name= OCCAppraisalVariables.DESIRABILITY_FOR_OTHER,
							Value = WellFormedNames.Name.BuildName(-5),
							Target = WellFormedNames.Name.BuildName("[target]")
						}
					};

				case nameof(OCCEmotionType.Hope):

					return new List<AppraisalVariableDTO>(){

							new AppraisalVariableDTO()
						{
							Name= OCCAppraisalVariables.GOALSUCCESSPROBABILITY,
							Value = WellFormedNames.Name.BuildName(0.6f),
							Target = WellFormedNames.Name.BuildName("GoodGoalName")
						},
					};

				case nameof(OCCEmotionType.Fear):

					return new List<AppraisalVariableDTO>(){

							new AppraisalVariableDTO()
						{
							Name= OCCAppraisalVariables.GOALSUCCESSPROBABILITY,
							Value = WellFormedNames.Name.BuildName(0.4f),
							Target = WellFormedNames.Name.BuildName("BadGoalName")
						},
					};

				case nameof(OCCEmotionType.Satisfaction):

					return new List<AppraisalVariableDTO>(){

							new AppraisalVariableDTO()
						{
							Name= OCCAppraisalVariables.GOALSUCCESSPROBABILITY,
							Value = WellFormedNames.Name.BuildName(1),
							Target = WellFormedNames.Name.BuildName("GoodGoalName")
						},
					};
				case nameof(OCCEmotionType.FearsConfirmed):

					return new List<AppraisalVariableDTO>(){

							new AppraisalVariableDTO()
						{
							Name= OCCAppraisalVariables.GOALSUCCESSPROBABILITY,
							Value = WellFormedNames.Name.BuildName(1),
							Target = WellFormedNames.Name.BuildName("BadGoalName")
						},
					};
				case nameof(OCCEmotionType.Relief):

					return new List<AppraisalVariableDTO>(){

							new AppraisalVariableDTO()
						{
							Name= OCCAppraisalVariables.GOALSUCCESSPROBABILITY,
							Value = WellFormedNames.Name.BuildName(0),
							Target = WellFormedNames.Name.BuildName("BadGoalName")
						},
					};
				case nameof(OCCEmotionType.Disappointment):

					return new List<AppraisalVariableDTO>(){

							new AppraisalVariableDTO()
						{
							Name= OCCAppraisalVariables.GOALSUCCESSPROBABILITY,
							Value = WellFormedNames.Name.BuildName(0),
							Target = WellFormedNames.Name.BuildName("GoodGoalName")
						},
					};
				case nameof(OCCEmotionType.Love):

					return new List<AppraisalVariableDTO>(){

							new AppraisalVariableDTO()
						{
							Name= OCCAppraisalVariables.LIKE,
							Value = WellFormedNames.Name.BuildName(5),
						},
					};

				case nameof(OCCEmotionType.Hate):

					return new List<AppraisalVariableDTO>(){

							new AppraisalVariableDTO()
						{
							Name= OCCAppraisalVariables.GOALSUCCESSPROBABILITY,
							Value = WellFormedNames.Name.BuildName(-5)
						},
					};


				default:

					return new List<AppraisalVariableDTO>(){

						new AppraisalVariableDTO()
						{
							Name= OCCAppraisalVariables.DESIRABILITY,
							Value = WellFormedNames.Name.BuildName(2),
						}
					};


			}



		}

		public static List<OCCEmotionType> getEmotionsFromRule(AppraisalRuleDTO rule)
        {
			List<OCCEmotionType> retList = new List<OCCEmotionType>();

			var desirability = rule.AppraisalVariables.appraisalVariables.Find(x => x.Name == OCCAppraisalVariables.DESIRABILITY);

			var praiseworthiness = rule.AppraisalVariables.appraisalVariables.Find(x => x.Name == OCCAppraisalVariables.PRAISEWORTHINESS);

			var desirabilityForOthers = rule.AppraisalVariables.appraisalVariables.Find(x => x.Name == OCCAppraisalVariables.DESIRABILITY_FOR_OTHER);

			var likes = rule.AppraisalVariables.appraisalVariables.Find(x => x.Name == OCCAppraisalVariables.LIKE);

			var goals = rule.AppraisalVariables.appraisalVariables.Find(x => x.Name == OCCAppraisalVariables.GOALSUCCESSPROBABILITY);


			if (desirability != null & praiseworthiness != null)
			{
				int number1;
				int.TryParse(desirability.Value.ToString(), out number1);
				int number2;
				int.TryParse(praiseworthiness.Value.ToString(), out number2);

				if (praiseworthiness.Target == WellFormedNames.Name.SELF_SYMBOL)
				{
					if (number1 > 0)
					{
						retList.Add(Gratification);
						retList.Add(Joy);
						retList.Add(Pride);
					}

					else {
						retList.Add(Remorse);
						retList.Add(Distress);
						retList.Add(Shame);
					}
				} else if (praiseworthiness.Target != WellFormedNames.Name.SELF_SYMBOL)
				{

					if (number1 > 0)
					{
						retList.Add(Gratitude);
						retList.Add(Joy);
						retList.Add(Admiration);

					}

					else
					{
						retList.Add(Anger);
						retList.Add(Distress);
						retList.Add(Reproach);
					}
				}
			}
			else
			{
				if (desirability != null)
				{
					int number1;
					int.TryParse(desirability.Value.ToString(), out number1);

					if (number1 > 0)
					{
						retList.Add(Joy);
					}
					else
					{
						retList.Add(Distress);
					}
				}

				if (praiseworthiness != null)
				{
					int number1;
					int.TryParse(praiseworthiness.Value.ToString(), out number1);

					if (praiseworthiness.Target == WellFormedNames.Name.SELF_SYMBOL)
					{
						if (number1 > 0)
						{
							retList.Add(Pride);
						}
						else
						{
							retList.Add(Shame);
						}
					} else
					{

						if (number1 > 0)
						{
							retList.Add(Admiration);
						}
						else
						{
							retList.Add(Reproach);
						}
					}

				}
			}

				if(likes != null)
                {
				int number1;
				int.TryParse(likes.Value.ToString(), out number1);

				if (number1 > 0)
                    {
						retList.Add(Love);
						
					} else
                    {
						retList.Add(Hate);
					}

				}


				if (goals != null)
				{

				var g = goals.Value;
				var s = goals.Value.ToString();
				int number;
				int.TryParse(s, out number);

				if (number > 0 && number < 1)
					{
						retList.Add(Hope);
						retList.Add(Fear);
						retList.Add(Relief);
						retList.Add(Disappointment);

					}
					else
					{
						retList.Add(Hope);
						retList.Add(Fear);
						retList.Add(Satisfaction);
						retList.Add(FearsConfirmed);
					}

				}

				if(desirability!= null && desirabilityForOthers != null)
                {

				int number1;
				int.TryParse(desirability.Value.ToString(), out number1);
				int number2;
				int.TryParse(desirabilityForOthers.Value.ToString(), out number2);

				if (number1 > 0)
					{
						if (number2 > 0)
						{
							retList.Add(HappyFor);
						}
						else
						{
							retList.Add(Gloating);
						}
						retList.Add(Joy);
					}
					else
					{
						if (number2 > 0)
						{
							retList.Add(Resentment);
						}
						else
						{
							retList.Add(Pity);
						}
						retList.Add(Distress);
					}
				

			}



			return retList;

		}
	}
}
