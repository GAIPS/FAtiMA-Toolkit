using SerializationUtilities;
using System;
using System.Collections.Generic;
using System.IO;
using Utilities.Json;

namespace FAtiMA.AssetStorage
{
    [Serializable]
    public class AssetStorage
    {
        private Dictionary<string, string> componentConfigurations;

        public AssetStorage()
        {
            this.componentConfigurations = new Dictionary<string, string>();
        }

        public void StoreComponent(string componentName, string configuration)
        {
            if (!componentConfigurations.ContainsKey(componentName))
            {
                componentConfigurations.Add(componentName, configuration);
            }
        }

        public void SaveToFile(string path)
        {
            var json = new JSONSerializer();
            var res = json.SerializeToJson(this);
            
            using (var writer = File.CreateText(path))
            {
                writer.Write(res.ToString(true));
            }
        }
    }
}
