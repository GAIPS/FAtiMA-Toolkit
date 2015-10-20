using EmotionalAppraisal;
using System.Collections.Generic;

namespace OCCModelAppraisal
{
	public sealed class OCCEmotionType
	{
		#region internal constants

		private static readonly string[] attribution = { OCCAppraisalVariables.PRAISEWORTHINESS };

		private static readonly string[] wellBeing = { OCCAppraisalVariables.DESIRABILITY };

		private static readonly string[] fortuneOfOthers = { OCCAppraisalVariables.DESIRABILITY,
															   OCCAppraisalVariables.DESIRABILITY_FOR_OTHER};

		private static readonly string[] attraction = { OCCAppraisalVariables.LIKE };

		private static readonly string[] composed = { OCCAppraisalVariables.DESIRABILITY,
														OCCAppraisalVariables.PRAISEWORTHINESS};

		private static readonly string[] positiveProspect = { OCCAppraisalVariables.GOALCONDUCIVENESS,
																OCCAppraisalVariables.GOALSTATUS,
																OCCAppraisalVariables.FAILUREPROBABILITY};

		private static readonly string[] negativeProspect = { OCCAppraisalVariables.GOALCONDUCIVENESS,
																OCCAppraisalVariables.GOALSTATUS,
																OCCAppraisalVariables.SUCCESSPROBABILITY};

		#endregion

		public static readonly OCCEmotionType ADMIRATION = new OCCEmotionType("Admiration",EmotionValence.Positive, true, attribution);
		public static readonly OCCEmotionType ANGER = new OCCEmotionType("Anger", EmotionValence.Positive, true, composed);
		public static readonly OCCEmotionType GRATITUDE = new OCCEmotionType("Gratitude", EmotionValence.Positive, true, composed);
		public static readonly OCCEmotionType DISAPPOINTMENT = new OCCEmotionType("Disappointment", EmotionValence.Negative, true, positiveProspect);
		public static readonly OCCEmotionType DISTRESS = new OCCEmotionType("Distress", EmotionValence.Negative, true, wellBeing);
		public static readonly OCCEmotionType FEAR = new OCCEmotionType("Fear", EmotionValence.Negative, false, negativeProspect);
		public static readonly OCCEmotionType FEARS_CONFIRMED = new OCCEmotionType("Fears-Confirmed", EmotionValence.Negative, true, negativeProspect);
		public static readonly OCCEmotionType GRATIFICATION = new OCCEmotionType("Gratification", EmotionValence.Positive, true, composed);
		public static readonly OCCEmotionType GLOATING = new OCCEmotionType("Gloating", EmotionValence.Positive, true, fortuneOfOthers);
		public static readonly OCCEmotionType HAPPY_FOR = new OCCEmotionType("Happy-For", EmotionValence.Positive, true, fortuneOfOthers);
		public static readonly OCCEmotionType HATE = new OCCEmotionType("Hate", EmotionValence.Negative, true, attraction);
		public static readonly OCCEmotionType HOPE = new OCCEmotionType("Hope", EmotionValence.Positive, false, positiveProspect);
		public static readonly OCCEmotionType JOY = new OCCEmotionType("Joy", EmotionValence.Positive, true, wellBeing);
		public static readonly OCCEmotionType LOVE = new OCCEmotionType("Love", EmotionValence.Positive, true, attraction);
		public static readonly OCCEmotionType PITTY = new OCCEmotionType("Pitty", EmotionValence.Negative, true, fortuneOfOthers);
		public static readonly OCCEmotionType PRIDE = new OCCEmotionType("Pride", EmotionValence.Positive, true, attribution);
		public static readonly OCCEmotionType RELIEF = new OCCEmotionType("Relief", EmotionValence.Positive, true, negativeProspect);
		public static readonly OCCEmotionType REMORSE = new OCCEmotionType("Remorse", EmotionValence.Negative, true, composed);
		public static readonly OCCEmotionType REPROACH = new OCCEmotionType("Reproach", EmotionValence.Negative, true, attribution);
		public static readonly OCCEmotionType RESENTMENT = new OCCEmotionType("Resentment", EmotionValence.Negative, true, fortuneOfOthers);
		public static readonly OCCEmotionType SATISFACTION = new OCCEmotionType("Satisfaction", EmotionValence.Positive, true, positiveProspect);
		public static readonly OCCEmotionType SHAME = new OCCEmotionType("Shame", EmotionValence.Negative, true, attribution);

		#region Class Definition

		public readonly string Name;
		public readonly EmotionValence Valence;
		public readonly IEnumerable<string> AppraisalVariables;
		public readonly bool InfluencesMood;

		private OCCEmotionType(string name, EmotionValence valence, bool influencesMood, string[] appraisalVariables)
		{
			this.Name = name;
			this.Valence = valence;
			this.InfluencesMood = influencesMood;
			this.AppraisalVariables = appraisalVariables;
		}

		#endregion
	}
}
