using System;
using System.IO;
using EmotionalDecisionMaking;
using GAIPS.Serialization;
using KnowledgeBase.WellFormedNames;
using NUnit.Framework;

namespace Tests.EmotionalDecisionMaking
{
	[TestFixture]
	public class SerializationTests
	{
		private ReactiveActions BuildTestAsset()
		{
			var r = new ReactiveActions();

			var d = new ActionTendency((Name)"Speak([speachType])",(Name)"[x]");
			d.ActivationCooldown = 2;
			r.AddActionTendency(d);

			d = new ActionTendency((Name)"Speak(formal)",(Name)"[x]");
			d.ActivationCooldown = 5;
			r.AddActionTendency(d);
			return r;
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