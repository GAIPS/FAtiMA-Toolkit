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
using AutobiographicMemory;
using KnowledgeBase.WellFormedNames;

namespace RolePlayCharacter
{
    [Serializable]
    public class RPCEvent : IEventRecord
    {
        public string Target { get; }
        public string Subject { get;  }
        public DateTime Timestamp { get; }

        public uint Id { get; }
        public string EventType { get; }

        public Name Action { get; }

        public Name EventName { get; }
        public IEnumerable<string> LinkedEmotions { get; }
        public void LinkEmotion(string emotionType) {}
    }

    [Serializable]
    public sealed partial class RolePlayCharacterAsset
    {
        private EmotionalAppraisalAsset _emotionalAppraisalAsset;
        private EmotionalDecisionMakingAsset _emotionalDecisionMakingAsset;

        private List<RPCEvent> _rpcEvents = new List<RPCEvent>();
        private List<Name> _rpcEventsName = new List<Name>();

        private IEnumerable<IAction> _rpcActions;

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

        public RolePlayCharacterAsset ()
        {
          
        }

        public RolePlayCharacterAsset (string fileEmotionalAppraisalAsset, string fileEmotionalDecisionMakingAsset)
        {
           
        }

        public void SetEmotionAppraisalModulePath()
        {


        }

        public void SetEmotionalAppraisalModule()
        {
            _emotionalAppraisalAsset = EmotionalAppraisalAsset.LoadFromFile("");
        }

        public void SetEmotionalDecisionModulePath()
        {



        }

        public void SetEmotionalDecisionModule()
        {
            _emotionalDecisionMakingAsset = new EmotionalDecisionMakingAsset(_emotionalAppraisalAsset);
        }

        public void SaveToFile(Stream file)
        {
            var serializer = new JSONSerializer();
            serializer.Serialize(file, this);
        }

        public void AddRPCEvent(RPCEvent evt)
        {
            _rpcEvents.Add(evt);
            _rpcEventsName.Add(evt.EventName);
        }

        public void PerceiveEvent()
        {
            if (_emotionalAppraisalAsset != null)
                _emotionalAppraisalAsset.AppraiseEvents(_rpcEventsName);

            if (_emotionalDecisionMakingAsset != null)
               _rpcActions =  _emotionalDecisionMakingAsset.Decide();     
        }

        private void SimpleAppraisalEvents()
        {
          
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
                _emotionalAppraisalAsset = EmotionalAppraisalAsset.LoadFromFile(name);
            }
        }

        private void LoadEmotionDecisionMakingAssetFromFile(string name)
        {
            if(name != null)
            {
               // _emotionalDecisionMakingAsset = EmotionalDecisionMakingAsset.LoadFromFile(name);
            }
        }

        private void LoadEmotionDecisionMakingAsset(EmotionalAppraisalAsset eaa)
        {
            _emotionalDecisionMakingAsset = new EmotionalDecisionMakingAsset(eaa);

        }
        public float GetCharacterMood() { return _emotionalAppraisalAsset.Mood; }

        public void GetObjectData(ISerializationData dataHolder)
        {
            dataHolder.SetValue("RPC_Events", _rpcEvents);
            dataHolder.SetValue("RPC_Actions", _rpcActions);
            dataHolder.SetValue("RPC_Events_Names", _rpcEventsName);
        }

        public void SetObjectData(ISerializationData dataHolder)
        {
            _rpcEvents = dataHolder.GetValue<List<RPCEvent>>("RPC_Events");
            _rpcEventsName = dataHolder.GetValue<List<Name>>("RPC_Events_Names");
            _rpcActions = dataHolder.GetValue<IEnumerable<IAction>>("RPC_Actions");
        }
    }
}
