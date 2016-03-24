using System;
using System.IO;
using GAIPS.Serialization;
using RolePlayCharacter.Utilities;
using EmotionalAppraisal;
using EmotionalDecisionMaking;
using AutobiographicMemory;
using KnowledgeBase.WellFormedNames;
using System.Collections.Generic;
//using MyUnityBridge;
//using UnityEngine;

namespace RolePlayCharacter
{
    public enum LOADTYPE
    {
        FROMDATABASE = 0,
        FROMFILE,
        NONE
    }

    public enum FILETYPE
    {
        XML = 0,
        JSON,
        NONE
    }

    public static class FILETYPEEXTENSION
    {
        public static string XML { get { return ".xml"; } }
        public static string JSON { get { return ".json"; } }
    }

    [Serializable]
    public class RPCEvent : IEventRecord
    {
        public string Target { get; set; }
        public string Subject { get; set; }
        public ulong Timestamp { get; set; }

        public uint Id { get; set; }
        public string EventType { get; set; }

        public Name Action { get; set; }

        public Name EventName { get; set; }
		
        public IEnumerable<string> LinkedEmotions { get; set; }

        public Name EventObject { get; set; }

        public void LinkEmotion(string emotionType) {}

		//NOTE: Custom serialization is not needed in this case.
		//      Just use System.NonSerializedAttribute on the fields you don't require serialization (if they are serialized by default)
		//public void GetObjectData(ISerializationData dataHolder)
  //      {
  //          Target = dataHolder.GetValue<string>("Target");
  //          Subject = dataHolder.GetValue<string>("Subject");
  //          Timestamp = dataHolder.GetValue<DateTime>("TimeStamp");
  //          Id = dataHolder.GetValue<uint>("ID");
  //          EventType = dataHolder.GetValue<string>("EventType");
  //          EventName = dataHolder.GetValue<Name>("EventName");
  //          Action = dataHolder.GetValue<Name>("Action");
  //      }

  //      public void SetObjectData(ISerializationData dataHolder)
  //      {
  //          dataHolder.SetValue<string>("Target", Target);
  //          dataHolder.SetValue<string>("Subject", Subject);
  //          dataHolder.SetValue<DateTime>("TimeStamp", Timestamp);
  //          dataHolder.SetValue<uint>("ID", Id);
  //          dataHolder.SetValue<string>("EventType", EventType);
  //          dataHolder.SetValue<Name>("EventName", EventName);
  //          dataHolder.SetValue<Name>("Action", Action);
  //      }
    }

    public class EmptyAction : IAction
    {
        public Name ActionName { get; set; }

        public IList<Name> Parameters { get; set; }

        public Name Target { get; set; }
    }

    public interface IRolePlayCharacterBody
    {
        void SetExpression(string emotion, float amount);
        void LoadObject(string name);
    }

    public class RolePlayCharacterBodyController
    {
        IRolePlayCharacterBody m_Controller;

        public IRolePlayCharacterBody Controller
        {
            get { return m_Controller; }

            set { m_Controller = value; }
        }

        public RolePlayCharacterBodyController(IRolePlayCharacterBody bodyControl)
        {
            this.m_Controller = bodyControl;
        }

        public void SetExpression(string emotion, float amount)
        {
            m_Controller.SetExpression(emotion, amount);
        }

        public void LoadObject(string name)
        {
            m_Controller.LoadObject(name);
        }
    }

    [Serializable]
    public sealed partial class RolePlayCharacterAsset
    {
        #region RolePlayCharater Fields
        [NonSerialized]
        private EmotionalAppraisalAsset _emotionalAppraisalAsset;

        [NonSerialized]
        private EmotionalDecisionMakingAsset _emotionalDecisionMakingAsset;

        private List<RPCEvent> _rpcEvents = new List<RPCEvent>();
        private List<Name> _rpcEventsName = new List<Name>();

        private RolePlayCharacterBodyController _bodyController;

        private IEnumerable<IAction> _rpcActions;

        private static LOADTYPE _loadType = LOADTYPE.NONE;
        private static FILETYPE _fileType = FILETYPE.NONE;

        private string _bodyPath;

        private string _assetId;

        private float _mockMood = 69;

        private string _characterName;

        public string CharacterName
        {
            get
            {
                if (_emotionalAppraisalAsset != null)
                    return _emotionalAppraisalAsset.Perspective;
                else return _characterName;
            }

            set
            {
                if (_emotionalAppraisalAsset != null)
                    _emotionalAppraisalAsset.Perspective = value;
                 else   _characterName = value;
            }
        }

        public float Mood
        {
            get
            {
                if (_emotionalAppraisalAsset != null)
                    return _emotionalAppraisalAsset.Mood;

                else return _mockMood;
            }

            set
            {
                if (_emotionalAppraisalAsset != null)
                    _emotionalAppraisalAsset.Mood = value;

                else _mockMood = value;
            }
        }

        public string AssetId
        {
            get { return _assetId; }

            set { _assetId = value; }
        }

        private static string _currentFileAsset;

        private string _emotionalAppraisalPath;

        public string EmotionalAppraisalPath
        {
            get { return _emotionalAppraisalPath; }
            set { _emotionalAppraisalPath = value; }
        }

        private string _emotionalDecisionMakingPath;

        public string EmotionalDecisionMakingPath
        {
            get { return _emotionalDecisionMakingPath; }

            set { _emotionalDecisionMakingPath = value; }
        }

        public string BodyPath
        {
            get { return _bodyPath; }

            set { _bodyPath = value; }
        }

        public RolePlayCharacterBodyController BodyController
        {
            get
            {
                return _bodyController;
            }

            set
            {
                _bodyController = value;
            }
        }
        #endregion

        #region Load Methods
        public static RolePlayCharacterAsset LoadFromFile(string idAsset)
        {
            RolePlayCharacterAsset rpc;
           string filename = GetFileToLoad(idAsset);
           
            using (var f = File.Open(filename, FileMode.Open, FileAccess.Read))
            {
                var serializer = new JSONSerializer();
                rpc = serializer.Deserialize<RolePlayCharacterAsset>(f);
            }
            return rpc;
        }

        public static RolePlayCharacterAsset LoadFromDatabase()
        {
            RolePlayCharacterAsset rpc = new RolePlayCharacterAsset();
            return rpc;
        }

        public static RolePlayCharacterAsset LoadAsset(LOADTYPE loadType)
        {
            RolePlayCharacterAsset rpc = new RolePlayCharacterAsset() ;

            switch(loadType)
            {
                case LOADTYPE.FROMFILE:
                    try
                    {
                       return LoadFromFile(_currentFileAsset);
                    }

                    catch(FileNotFoundException exception)
                    {
                        Console.WriteLine(exception.Message);
                    }
                    break;
                case LOADTYPE.FROMDATABASE:
                    break;
                default:
                    break;
            }

            return rpc;
        }

        #endregion

        private static string GetFileToLoad(string idAsset)
        {
            try
            {
                if (_fileType == FILETYPE.NONE)
                    throw new ParameterNotDefiniedException(new Messages().FILEEXTENSIONNOTDEFINED().ShowMessage());

                if (_loadType == LOADTYPE.FROMFILE)
                    throw new ParameterNotDefiniedException(new Messages().LOADTYPEINCORRECT().ShowMessage());

                return _currentFileAsset;
            }

            catch (ParameterNotDefiniedException exception)
            {
                Console.WriteLine(exception.Message);
            }

            return "Default";
        }

        #region LoadAndSet Methods
        private void LoadEmotionAppraisalAssetFromFile(string name)
        {
            if (name != null)
            {
                _emotionalAppraisalAsset = EmotionalAppraisalAsset.LoadFromFile(name);
            }
        }

        private void LoadEmotionalAppraisalAssetFromType(LOADTYPE loadType)
        {
            if (loadType == LOADTYPE.FROMFILE)
            {
                LoadEmotionAppraisalAssetFromFile(_currentFileAsset);
            }
        }

        private void LoadEmotionDecisionMakingAssetFromFile(string name)
        {
            if (name != null)
            {
                _emotionalAppraisalAsset = EmotionalAppraisalAsset.LoadFromFile(name);
                LoadEmotionDecisionMakingAsset(_emotionalAppraisalAsset);
            }
        }

        private void LoadEmotionDecisionMakingAsset(EmotionalAppraisalAsset eaa)
        {
            _emotionalDecisionMakingAsset = new EmotionalDecisionMakingAsset(eaa);
        }


        public void SetEmotionAppraisalModulePath(string emotionalAppraisalPath)
        {
            _emotionalAppraisalPath = emotionalAppraisalPath;
        }

        public void SetEmotionalAppraisalModule(string name)
        {
            LoadEmotionAppraisalAssetFromFile(name);
        }

        public void SetEmotionalDecisionModulePath(string emotionalDecisionPath)
        {
            _emotionalDecisionMakingPath = emotionalDecisionPath;
        }

        public void SetEmotionalDecisionModule(string fullPath)
        {
            if (_emotionalAppraisalAsset != null)
            {
                _emotionalDecisionMakingAsset = new EmotionalDecisionMakingAsset(_emotionalAppraisalAsset);
                using (var f = File.Open(fullPath, FileMode.Open, FileAccess.Read))
                {
                    var serializer = new JSONSerializer();
                    _emotionalDecisionMakingAsset.ReactiveActions = serializer.Deserialize<ReactiveActions>(f);
                }

            }

        }

        public void SetLoadingType(LOADTYPE loadType)
        {
            _loadType = loadType;
        }

        public void SetBodyController(RolePlayCharacterBodyController rpcBodyController)
        {
            _bodyController = rpcBodyController;
        }

        #endregion

        #region CharacterMethods
        public void AddRPCEvent(RPCEvent evt)
        {
            _rpcEvents.Add(evt);
            _rpcEventsName.Add(evt.EventName);
        }

        public void AddEvent(Name eventName)
        {
            _rpcEventsName.Add(eventName);
        }

        public void PerceiveEvent()
        {
            try
            {
                if (_emotionalAppraisalAsset != null)
                    _emotionalAppraisalAsset.AppraiseEvents(_rpcEventsName);

                else throw new FunctionalityNotImplementedException(new Messages().EMOTIONALAPPRAISALNOTIMPLEMENTED().ShowMessage());

                _rpcEvents.Clear();
                _rpcEventsName.Clear();

               // UpdateCharacterState();
             //   if (_emotionalDecisionMakingAsset != null)
               //     _rpcActions = _emotionalDecisionMakingAsset.Decide();

                //else throw new FunctionalityNotImplementedException(new Messages().EMOTIONDECISIONNOTIMPLEMENTED().ShowMessage());
            }
            
            catch(FunctionalityNotImplementedException exception)
            {
                Console.WriteLine(exception.ExceptionMessage);
            }  
        }

        public IEnumerable<IAction> RetrieveDecidedAction()
        {
            if (_emotionalDecisionMakingAsset != null)
                return _emotionalDecisionMakingAsset.Decide();

            else return _rpcActions;
        }

        public IEnumerable<IAction> RetrieveActions()
        {
            return _rpcActions;

        }

        public float GetCharacterMood() { return Mood; }

        public float GetIntensityStrongestEmotion()
        {
            return GetStrongestActiveEmotion().Intensity;
        }

        public IActiveEmotion GetStrongestActiveEmotion()
        {
            IEnumerable<IActiveEmotion> currentActiveEmotions = _emotionalAppraisalAsset.GetAllActiveEmotions();

            IActiveEmotion activeEmotion = null;

            foreach (IActiveEmotion emotion in currentActiveEmotions)
            {
                if (activeEmotion != null)
                {
                    if (activeEmotion.Intensity < emotion.Intensity)
                        activeEmotion = emotion;
                }

                else activeEmotion = emotion;
            }

            return activeEmotion;
        }

        public IEnumerable<IActiveEmotion> CharacterEmotions()
        {
           return _emotionalAppraisalAsset.GetAllActiveEmotions();
        }

        public void UpdateCharacterState()
        {
            if (_emotionalAppraisalAsset != null)
                _emotionalAppraisalAsset.Update();
        }

        #endregion

        #region Serialization
        public void SaveAsset(string filename)
        {
            using (var file = File.Create(filename))
            {
                SaveToFile(file);
            }
        }

        public void SaveToFile(Stream file)
        {
            var serializer = new JSONSerializer();
            serializer.Serialize(file, this);
        }

        public void GetObjectData(ISerializationData dataHolder)
        {
            dataHolder.SetValue("EmotionalAppraisalPath", _emotionalAppraisalPath);
            dataHolder.SetValue("EmotionalDecisionMakingPath", _emotionalDecisionMakingPath);
            dataHolder.SetValue("RPC_Events", _rpcEvents);
            dataHolder.SetValue("RPC_Actions", _rpcActions);
            dataHolder.SetValue("RPC_Events_Names", _rpcEventsName);
            dataHolder.SetValue("CharacterName", _characterName);
        }

        public void SetObjectData(ISerializationData dataHolder)
        {
            _emotionalDecisionMakingPath = dataHolder.GetValue<string>("EmotionalDecisionMakingPath");
            _emotionalAppraisalPath = dataHolder.GetValue<string>("EmotionalAppraisalPath");
           _rpcEvents = dataHolder.GetValue<List<RPCEvent>>("RPC_Events");
           _rpcEventsName = dataHolder.GetValue<List<Name>>("RPC_Events_Names");
           _rpcActions = dataHolder.GetValue<IEnumerable<IAction>>("RPC_Actions");
            _characterName = dataHolder.GetValue<string>("CharacterName");
        }
        #endregion
    }
}
