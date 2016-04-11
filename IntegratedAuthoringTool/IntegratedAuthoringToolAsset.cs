using System;
using System.Collections.Generic;
using System.IO;
using GAIPS.Serialization;

namespace IntegratedAuthoringTool
{
    [Serializable]
    public class IntegratedAuthoringToolAsset
    {
        public string ScenarioName { get; set; }
        public IList<string> CharacterFiles { get; set; }
        

        public static IntegratedAuthoringToolAsset LoadFromFile(string filename)
        {
            IntegratedAuthoringToolAsset iat;

            using (var f = File.Open(filename, FileMode.Open, FileAccess.Read))
            {
                var serializer = new JSONSerializer();
                iat = serializer.Deserialize<IntegratedAuthoringToolAsset>(f);
            }
            return iat;
        }

        #region Serialization
        public void SaveToFile(Stream file)
        {
            var serializer = new JSONSerializer();
            serializer.Serialize(file, this);
        }

        public void GetObjectData(ISerializationData dataHolder)
        {
            dataHolder.SetValue("ScenarioName", ScenarioName);
            dataHolder.SetValue("CharacterFiles",CharacterFiles);
        }

        public void SetObjectData(ISerializationData dataHolder)
        {
            ScenarioName = dataHolder.GetValue<string>("ScenarioName");
            CharacterFiles = dataHolder.GetValue<List<string>>("CharacterFiles");
        }
        #endregion
    }
}
