using RolePlayCharacter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTSTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            //Loading the asset
            var rpc = RolePlayCharacterAsset.LoadFromFile("../../../Examples/RPCTest.rpc");
            rpc.LoadAssociatedAssets();
            Console.WriteLine("Starting Mood: " + rpc.Mood);
            var actions = rpc.Decide();
            var action = actions.FirstOrDefault();
            foreach (var a in actions)
            {
                Console.WriteLine(a.Name.ToString() + " p: " + a.Utility);
            }
            Console.ReadKey();
        }
    }
}
