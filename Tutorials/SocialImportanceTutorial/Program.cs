using System;
using SocialImportance;
using KnowledgeBase;
using WellFormedNames;

namespace SocialImportanceTutorial
{
    class Program
    {
        //This is a small console program to exemplify the main functionality of the Social Importance Asset
        static void Main(string[] args)
        {
			var siTarget = "Player";

            //First, we load the asset from an existing profile
            var siAsset = SocialImportanceAsset.CreateInstance(new GAIPS.Rage.AssetStorage());
            var rules = siAsset.GetAttributionRules();
            //We then register a knowledge base
            var kb = new KB((Name)"John");
            kb.Tell((Name)"IsFriend(Player)", (Name)"False");
            siAsset.RegisterKnowledgeBase(kb);
            
            Console.WriteLine("The SI attributed to "+siTarget+" is:" + siAsset.GetSocialImportance(siTarget));

            Console.ReadKey();
        }
    }
}
