using System;
using AssetManagerPackage;
using CommeillFaut.DTOs;
using CommeillFaut;
using RolePlayCharacter;

namespace CommeillFautTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            AssetManager.Instance.Bridge = new BasicIOBridge();

            var flirt = new SocialExchangeDTO {Action = "Flirt", Instantiation = "You are so beautiful"};
            var compliment = new SocialExchangeDTO { Action = "Compliment", Instantiation = "You are pretty cool" };

            CommeillFautAsset n = new CommeillFautAsset();
            n.AddExchange(flirt);
            n.AddExchange(compliment);

	        var rpc = RolePlayCharacterAsset.LoadFromFile("../../../Examples/john.rpc");
            rpc.Initialize();

			Console.WriteLine(rpc.CharacterName);

       //     CommeillFautAsset cif;
       //     var flirt_id = cif.AddSocialExchange(flirt);
        //   var compliment_id = cif.AddSocialExchange(compliment);

         //   cif.GetSocialMove(Name.BuildName("Player"));
            Console.ReadLine();
        }
    }
}



/* InfluenceRule inf1 = new InfluenceRule("One", 3, true);
 InfluenceRule inf2 = new InfluenceRule("Two", 3, false);
 InfluenceRule inf3 = new InfluenceRule("Three", 1, true);






 SocialExchange flirt = new SocialExchange("Flirt", "toFlirt");
 flirt.InfluenceRules.Add(inf1);
 flirt.InfluenceRules.Add(inf2);
 flirt.InfluenceRules.Add(inf3);

 SocialExchange compliment = new SocialExchange("Insult", " i think you're ugly ");
 compliment.InfluenceRules.Add(inf1);
 compliment.InfluenceRules.Add(inf2);
 compliment.InfluenceRules.Add(inf3);

 Manager man = new Manager();

 man.SocialExchangeList.Add(flirt);
 man.SocialExchangeList.Add(compliment);



 var jonas_ea = EmotionalAppraisalAsset.LoadFromFile("../../../Examples/jonas.ea");


Character john = new Character(jonas_ea.Perspective.ToString(), jonas_ea.GetBeliefValue("isNice(SELF)"), jonas_ea.GetBeliefValue("isDrunk(SELF)"), jonas_ea.GetBeliefValue("Gender(SELF)"));
 Character john = new Character(man, "Sarah", "Nice", "Angry", "Female");

Console.WriteLine(john.ToString() + "\n");

 var sarah_ea = EmotionalAppraisalAsset.LoadFromFile("../../../Examples/sarah.ea");

 Character sarah = new Character(sarah_ea.Perspective.ToString(), sarah_ea.GetBeliefValue("isNice(SELF)"), sarah_ea.GetBeliefValue("isDrunk(SELF)"), sarah_ea.GetBeliefValue("Gender(SELF)"));

 Console.WriteLine(sarah.ToString() + "\n");

 Character peter = new Character("Peter", "Ugly", "Angry", "Male");
 john.SocialExchanges.Add(flirt);
 john.SocialExchanges.Add(compliment);

 sarah.SocialExchanges.Add(flirt);
 sarah.SocialExchanges.Add(compliment);

 peter.SocialExchanges.Add(flirt);
 peter.SocialExchanges.Add(compliment);

 sarah.Targets.Add(peter);
 sarah.Targets.Add(john);
 john.Targets.Add(sarah);
 john.Targets.Add(peter);
 peter.Targets.Add(sarah);
 peter.Targets.Add(john);

 john.Init();
 peter.Init();
 sarah.Init();
 // Console.WriteLine(flirt.ToString());


 Thread oThread = new Thread(new ThreadStart(john.BDI));
 Thread aThread = new Thread(new ThreadStart(peter.BDI));
 Thread bThread = new Thread(new ThreadStart(sarah.BDI));

 Thread mThread = new Thread(new ThreadStart(man.Init));
 mThread.Start();

 oThread.Start();
 int milliseconds = 1000;
 Thread.Sleep(milliseconds);
 aThread.Start();
 Thread.Sleep(1000);
 bThread.Start();

 //       Thread mThread = new Thread(new ThreadStart(man.Init));
 //     mThread.Start();


 */
