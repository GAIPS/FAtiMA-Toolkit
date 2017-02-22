using System;
using System.Linq;
using NUnit.Framework;
using RolePlayCharacter;
using WellFormedNames;

namespace Tests.RolePlayerCharacterAsset
{
	[TestFixture]
	public class RPCAssetTests
	{
		[Test]
		public void TestIsAgentWithVariable()
		{
			var rpc = new RolePlayCharacterAsset();
			rpc.LoadAssociatedAssets();

			rpc.Perceive(new [] {(Name)"Event(Action-Start,Mary,*,*)", (Name)"Event(Action-Start,John,*,*)" });
			var result = rpc.Queryable.AskPossibleProperties((Name) "IsAgent([x])", Name.SELF_SYMBOL, null).ToArray();
			if(result.Length!=1)
				Assert.Fail();

			if(result[0].Item2.Count() != 3)
				Assert.Fail();
		}
	}
}