using System;
using System.Collections.Generic;
using System.Linq;
using GAIPS.Rage;
using SerializationUtilities;
using KnowledgeBase;
using RolePlayCharacter;
using WellFormedNames;
using Utilities;
using System.Text;
using System.Security.Cryptography;
using ActionLibrary;
using Utilities.Json;

namespace CommonSense
{
    [Serializable]
    public class CommonSenseAsset
    {
        private KB commonSenseDatabase;


        public CommonSenseAsset()
        {
            commonSenseDatabase = new KB(Name.BuildName("CommonSense"));
        }


        public void LoadDefaultDatabase()
        {
            commonSenseDatabase.Tell((Name)"is(Animal)", (Name)"Entity");
            commonSenseDatabase.Tell((Name)"is(Liquor)", (Name)"Alcohol");
        }
    }
}
