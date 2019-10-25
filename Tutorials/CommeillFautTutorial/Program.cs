using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IntegratedAuthoringTool;
using RolePlayCharacter;

namespace CommeillFautTutorial
{
    class Program
    {
        static List<RolePlayCharacterAsset> rpcList;

        static void Main(string[] args)
        {

             // This tutorial has become a testing ground for the method "LoadFromString"


            rpcList = new List<RolePlayCharacterAsset>();

          //  Load Player from STRING

            var path = "C:\\Users\\Manue\\Documents\\Work\\FAtiMA\\Tutorials\\Examples\\CiF-Tutorial\\rpc\\player.rpc";
            
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            

            
          //  var playerString = File.OpenRead(path);
            
              string fileContents;
            using (StreamReader reader = new StreamReader(fs))
            {
                fileContents = reader.ReadToEnd();
            }
            
           
            /*
          
           
            johnRPC.LoadAssociatedAssetsFromString(johnAppraisal, johnEDM, johnSI, "");
            
            iat.BindToRegistry(johnRPC.DynamicPropertiesRegistry);

            Console.WriteLine("Asset loaded from text correctly");

            // Letting each other know they are here
            var hi = new[] { EventHelper.ActionEnd(player.CharacterName.ToString(), "Enters", "Room") };
            sarahRPC.Perceive(hi);

            var hi2 = new[] { EventHelper.ActionEnd(johnRPC.CharacterName.ToString(), "Enters", "Room") };
            sarahRPC.Perceive(hi2);

            var handshake = new[] { EventHelper.ActionEnd(sarahRPC.CharacterName.ToString(), "Enters", "Room") };
            player.Perceive(handshake);

            var handshake2 = new[] { EventHelper.ActionEnd(johnRPC.CharacterName.ToString(), "Enters", "Room") };
            player.Perceive(handshake2);

            var welcome = new[] { EventHelper.ActionEnd(sarahRPC.CharacterName.ToString(), "Enters", "Room") };
            johnRPC.Perceive(welcome);

            var welcome2 = new[] { EventHelper.ActionEnd(player.CharacterName.ToString(), "Enters", "Room") };
            johnRPC.Perceive(welcome2);


            rpcList.Add(johnRPC);
            rpcList.Add(sarahRPC);
            rpcList.Add(player);


            foreach (var rpc in rpcList)
            {
                foreach (var b in rpc.GetAllBeliefs())
                {
                    Console.WriteLine("I: " + rpc.CharacterName.ToString() + " believe " + b.Name + " " + b.Value);
                }
            }


            if(sarahRPC.Decide().FirstOrDefault() != null)
                Console.WriteLine(sarahRPC.CharacterName.ToString() + " decided to perform  " + sarahRPC.Decide().FirstOrDefault().Name);

            if(johnRPC.Decide().FirstOrDefault() != null)
                Console.WriteLine(johnRPC.CharacterName.ToString() + " decided to perform  " + johnRPC.Decide().FirstOrDefault().Name);

            if(player.Decide().FirstOrDefault() != null)
                Console.WriteLine(player.CharacterName.ToString() + " decided to perform  " + player.Decide().FirstOrDefault().Name);
            Console.ReadKey();


        */

        }
    }
}

    





