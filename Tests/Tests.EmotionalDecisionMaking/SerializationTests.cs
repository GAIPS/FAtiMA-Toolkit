using System;
using System.IO;
using EmotionalDecisionMaking;
using SerializationUtilities;
using ActionLibrary.DTOs;
using NUnit.Framework;
using WellFormedNames;

namespace Tests.EmotionalDecisionMaking
{
	[TestFixture]
	public class SerializationTests
	{
		private EmotionalDecisionMakingAsset BuildTestAsset()
		{
			var asset = new EmotionalDecisionMakingAsset();

			asset.AddActionRule(new ActionRuleDTO() {Action = Name.BuildName("Speak([speachType])"), Target = Name.BuildName("[x]")});
			asset.AddActionRule(new ActionRuleDTO() { Action = Name.BuildName("Speak(formal)"), Target = Name.BuildName("[x]") });
			return asset;
		}

		[TestCase]
		public void EmotionalAppraisal_Serialization_Test()
		{
			var asset = BuildTestAsset();

			using (var stream = new MemoryStream())
			{
				var formater = new JSONSerializer();
				formater.Serialize(stream, asset);
				stream.Seek(0, SeekOrigin.Begin);
				Console.WriteLine(new StreamReader(stream).ReadToEnd());
			}
		}

		[TestCase]
		public void EmotionalAppraisal_Deserialization_Test()
		{
			var asset = BuildTestAsset();

			using (var stream = new MemoryStream())
			{
				var formater = new JSONSerializer();
				formater.Serialize(stream, asset);
				stream.Seek(0, SeekOrigin.Begin);
				Console.WriteLine(new StreamReader(stream).ReadToEnd());
				stream.Seek(0, SeekOrigin.Begin);
				var obj = formater.Deserialize(stream);
			}
		}
	}
}