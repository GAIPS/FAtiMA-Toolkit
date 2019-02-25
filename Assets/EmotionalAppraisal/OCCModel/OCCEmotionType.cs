using System.Collections.Generic;

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
		public static readonly OCCEmotionType Pitty = new OCCEmotionType("Pitty", EmotionValence.Negative, true, FortuneOfOthers);
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
            Shame.Name
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
	}
}
