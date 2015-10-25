using EmotionalAppraisal;
using GAIPS.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OCCModelAppraisal.OCCModel;
using System.IO;

namespace UnitTest
{
	[TestClass]
	public class SerializationTests
	{
		private static EmotionalAppraisalAsset BuildBaseAsset()
		{
			var asset = new EmotionalAppraisalAsset();
			asset.AddComponent(new OCCAffectDerivationComponent());

			//Dispositions
			var loveDisposition = new EmotionDisposition(OCCEmotionType.LOVE.Name, 5, 3);
			asset.EmotionalState.AddEmotionDisposition(loveDisposition);

			var hateDisposition = new EmotionDisposition(OCCEmotionType.HATE.Name, 5, 3);
			asset.EmotionalState.AddEmotionDisposition(hateDisposition);

			var hopeDisposition = new EmotionDisposition(OCCEmotionType.HOPE.Name, 8, 4);
			asset.EmotionalState.AddEmotionDisposition(hopeDisposition);

			var fearDisposition = new EmotionDisposition(OCCEmotionType.FEAR.Name, 2, 1);
			asset.EmotionalState.AddEmotionDisposition(fearDisposition);

			var satisfactionDisposition = new EmotionDisposition(OCCEmotionType.SATISFACTION.Name, 8, 5);
			asset.EmotionalState.AddEmotionDisposition(satisfactionDisposition);

			var reliefDisposition = new EmotionDisposition(OCCEmotionType.RELIEF.Name, 5, 3);
			asset.EmotionalState.AddEmotionDisposition(reliefDisposition);

			var fearsConfirmedDisposition = new EmotionDisposition(OCCEmotionType.FEARS_CONFIRMED.Name, 2, 1);
			asset.EmotionalState.AddEmotionDisposition(fearsConfirmedDisposition);

			var disapointmentDisposition = new EmotionDisposition(OCCEmotionType.DISAPPOINTMENT.Name, 5, 2);
			asset.EmotionalState.AddEmotionDisposition(disapointmentDisposition);

			var joyDisposition = new EmotionDisposition(OCCEmotionType.JOY.Name, 2, 3);
			asset.EmotionalState.AddEmotionDisposition(joyDisposition);

			var distressDisposition = new EmotionDisposition(OCCEmotionType.DISTRESS.Name, 2, 1);
			asset.EmotionalState.AddEmotionDisposition(distressDisposition);

			var happyForDisposition = new EmotionDisposition(OCCEmotionType.HAPPY_FOR.Name, 5, 2);
			asset.EmotionalState.AddEmotionDisposition(happyForDisposition);

			var pittyDisposition = new EmotionDisposition(OCCEmotionType.PITTY.Name, 2, 2);
			asset.EmotionalState.AddEmotionDisposition(pittyDisposition);

			var resentmentDisposition = new EmotionDisposition(OCCEmotionType.RESENTMENT.Name, 2, 3);
			asset.EmotionalState.AddEmotionDisposition(resentmentDisposition);

			var gloatingDisposition = new EmotionDisposition(OCCEmotionType.GLOATING.Name, 8, 5);
			asset.EmotionalState.AddEmotionDisposition(gloatingDisposition);

			var prideDisposition = new EmotionDisposition(OCCEmotionType.PRIDE.Name, 5, 5);
			asset.EmotionalState.AddEmotionDisposition(prideDisposition);

			var shameDisposition = new EmotionDisposition(OCCEmotionType.SHAME.Name, 2, 1);
			asset.EmotionalState.AddEmotionDisposition(shameDisposition);

			var gratificationDisposition = new EmotionDisposition(OCCEmotionType.GRATIFICATION.Name, 8, 5);
			asset.EmotionalState.AddEmotionDisposition(gratificationDisposition);

			var remorseDisposition = new EmotionDisposition(OCCEmotionType.REMORSE.Name, 2, 1);
			asset.EmotionalState.AddEmotionDisposition(remorseDisposition);

			var admirationDisposition = new EmotionDisposition(OCCEmotionType.ADMIRATION.Name, 5, 3);
			asset.EmotionalState.AddEmotionDisposition(admirationDisposition);

			var reproachDisposition = new EmotionDisposition(OCCEmotionType.REPROACH.Name, 8, 2);
			asset.EmotionalState.AddEmotionDisposition(reproachDisposition);

			var gratitudeDisposition = new EmotionDisposition(OCCEmotionType.GRATITUDE.Name, 5, 3);
			asset.EmotionalState.AddEmotionDisposition(gratitudeDisposition);

			var angerDisposition = new EmotionDisposition(OCCEmotionType.ANGER.Name, 5, 3);
			asset.EmotionalState.AddEmotionDisposition(angerDisposition);

			return asset;
		}

		[TestMethod]
		public void BasicSerializationTest()
		{
			var asset = BuildBaseAsset();

			using (var stream = new MemoryStream())
			{
				var formater = new JSONSerializer();
				formater.Serialize(stream, asset);

				stream.Seek(0, SeekOrigin.Begin);
				System.Diagnostics.Trace.Write(new StreamReader(stream).ReadToEnd());
			}
		}

		[TestMethod]
		public void BasicDeserializationTest()
		{
			var asset = BuildBaseAsset();

			using (var stream = new MemoryStream())
			{
				var formater = new JSONSerializer();
				formater.Serialize(stream, asset);
				stream.Seek(0, SeekOrigin.Begin);
				System.Diagnostics.Trace.Write(new StreamReader(stream).ReadToEnd());
				stream.Seek(0, SeekOrigin.Begin);
				var obj = formater.Deserialize(stream);
			}
		}
	}
}
