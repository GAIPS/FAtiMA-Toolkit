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
using AutobiographicMemory.Interfaces;

namespace RolePlayCharacter
{

    public class GameEvent : IEvent
    {
        public string Action { get; }
        public string Target { get; }
        public string Subject { get;  }
        public DateTime Timestamp { get; }
        public IEnumerable<IEventParameter> Parameters { get; }
    }

    [Serializable]
    public sealed partial class RolePlayCharacterAsset
    {
        private EmotionalAppraisalAsset _emotionalAppraisalAsset;
        private EmotionalDecisionMakingAsset _emotionalDecisionMakingAsset;

        private List<IEvent> _gameEvents = new List<IEvent>();

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
                LoadEmotionAppraisalAsset("SELF");

            else LoadEmotionAppraisalAsset(name);

            if (_emotionalAppraisalAsset != null)
                LoadEmotionDecisionMakingAsset(_emotionalAppraisalAsset);
        }

        public void SaveToFile(Stream file)
        {
            var serializer = new JSONSerializer();
            serializer.Serialize(file, this);
        }

        public void AddGameEvent(GameEvent evt)
        {
            _gameEvents.Add(evt);
        }

        public void ReceiveEvent()
        {
            if (_emotionalDecisionMakingAsset == null)
                SimpleAppraisalEvents();

            else AppraisalAndDecideEvent();
        }

        private void SimpleAppraisalEvents()
        {
            _emotionalAppraisalAsset.AppraiseEvents(_gameEvents);
        }

        private void AppraisalAndDecideEvent()
        {
            /***/


        }


        public void RetrieveAction()
        {



        }


        private void LoadEmotionAppraisalAsset(string name)
        {
            if(name != null)
                _emotionalAppraisalAsset = new EmotionalAppraisalAsset(name);
        }

        private void LoadEmotionAppraisalAssetFromFile(string name)
        {
            if(name != null)
            {
                _emotionalDecisionMakingAsset = new EmotionalDecisionMakingAsset(_emotionalAppraisalAsset);
            }
        }

        private void LoadEmotionDecisionMakingAssetFromFile(string name)
        {
            if(name != null)
            {



            }
        }

        private void LoadEmotionDecisionMakingAsset(EmotionalAppraisalAsset eaa)
        {
            _emotionalDecisionMakingAsset = new EmotionalDecisionMakingAsset(eaa);

        }
        public float GetCharacterMood() { return _emotionalAppraisalAsset.EmotionalState.Mood; }

        public IActiveEmotion GetCharacterStrongEmotion() { return _emotionalAppraisalAsset.EmotionalState.GetStrongestEmotion(); }
    }
}
