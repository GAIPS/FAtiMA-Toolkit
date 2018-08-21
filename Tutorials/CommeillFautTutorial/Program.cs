using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ActionLibrary;
using ActionLibrary.DTOs;
using AssetManagerPackage;
using CommeillFaut.DTOs;
using CommeillFaut;
using Conditions.DTOs;
using EmotionalAppraisal;
using EmotionalAppraisal.DTOs;
using IntegratedAuthoringTool;
using KnowledgeBase;
using Microsoft.CSharp.RuntimeBinder;
using RolePlayCharacter;
using WellFormedNames;
using WorldModel;
using WorldModel.DTOs;

namespace CommeillFautTutorial
{
    class Program
    {
        static List<RolePlayCharacterAsset> rpcList;

        static void Main(string[] args)
        {

            var iat = IntegratedAuthoringToolAsset.LoadFromFile("../../../Examples/CiF-Tutorial/JobInterview.iat");
            rpcList = new List<RolePlayCharacterAsset>();
        
            foreach (var source in iat.GetAllCharacterSources())
            {

                var rpc = RolePlayCharacterAsset.LoadFromFile(source.Source);


                //rpc.DynamicPropertiesRegistry.RegistDynamicProperty(Name.BuildName("Volition"),cif.VolitionPropertyCalculator);
                rpc.LoadAssociatedAssets();

                iat.BindToRegistry(rpc.DynamicPropertiesRegistry);

                rpcList.Add(rpc);

            }
          

            foreach (var actor in rpcList)
            {


                foreach (var anotherActor in rpcList)
                {
                    if (actor != anotherActor)
                    {


                        var changed = new[] { EventHelper.ActionEnd(anotherActor.CharacterName.ToString(), "Enters", "Room") };
                        actor.Perceive(changed);
                    }
                    
                }
              
            }


            foreach(var actor in rpcList)
            {
                if(actor.Decide().FirstOrDefault() != null)
                Console.WriteLine(actor.CharacterName.ToString() + " decided to perform  " + actor.Decide().FirstOrDefault().Name);
            }


            Console.ReadKey();


        

        }
    }
}

    





