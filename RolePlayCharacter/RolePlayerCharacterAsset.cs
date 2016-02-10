using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionalAppraisal;
using EmotionalDecisionMaking;
using Utilities.Json;
using GAIPS.Serialization;

namespace RolePlayCharacter
{
    [Serializable]
    public sealed partial class RolePlayCharacterAsset
    {
        private EmotionalAppraisalAsset _emotionalAppraisalAsset;
        private EmotionalDecisionMakingAsset _emotionalDecisionMakingAsset;

        public static RolePlayCharacterAsset LoadFromFile(string filename)
        {
            RolePlayCharacterAsset rpc;
            using (var f = File.Open(filename, FileMode.Open, FileAccess.Read))
            {
                var serializer = new JSONSerializer();
                rpc = serializer.Deserialize<RolePlayCharacterAsset>(f);
            }
            return rpc;
        }

        public RolePlayCharacterAsset (string name)
        {
            if (name == null)
                _emotionalAppraisalAsset = new EmotionalAppraisalAsset("SELF");
            else _emotionalAppraisalAsset = EmotionalAppraisalAsset.LoadFromFile(name);
            //_emotionalDecisionMakingAsset = EmotionalDecisionMakingAsset.LoadFromFile(name);


 
        }

        public void SaveToFile(Stream file)
        {
            var serializer = new JSONSerializer();
            serializer.Serialize(file, this);
        }

        public void AddGameEvent()
        {



        }

        public void ReceiveEvent()
        {
            if (_emotionalDecisionMakingAsset == null)
                SimpleAppraisalEvent();

            else AppraisalAndDecideEvent();
        }

        private void SimpleAppraisalEvent()
        {



        }

        private void AppraisalAndDecideEvent()
        {



        }

        public float GetCharacterMood() { return _emotionalAppraisalAsset.EmotionalState.Mood; }

        public IActiveEmotion GetCharacterStrongEmotion() { return _emotionalAppraisalAsset.EmotionalState.GetStrongestEmotion(); }
    }
}
