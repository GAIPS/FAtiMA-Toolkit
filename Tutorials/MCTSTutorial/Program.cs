using RolePlayCharacter;
using System;
using System.Linq;

namespace MCTSTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            //Loading the asset
            var rpc = RolePlayCharacterAsset.LoadFromFile("../../../Examples/RPC-MCTS.rpc");
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
