using System;
using System.IO;
using EmotionalDecisionMaking;
using SerializationUtilities;
using ActionLibrary.DTOs;
using NUnit.Framework;

namespace Tests.EmotionalDecisionMaking
{
	[TestFixture]
	public class SerializationTests
	{
		private EmotionalDecisionMakingAsset BuildTestAsset()
		{
			var asset = new EmotionalDecisionMakingAsset();

			asset.AddReaction(new ActionDefinitionDTO() {Action = "Speak([speachType])", Target = "[x]"});
			asset.AddReaction(new ActionDefinitionDTO() { Action = "Speak(formal)", Target = "[x]" });
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