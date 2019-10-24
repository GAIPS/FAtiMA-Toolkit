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

        public string GetComponentConfiguration(string componentName)
        {
            if (componentConfigurations.ContainsKey(componentName))
            {
                return componentConfigurations[componentName];
            }
            else
            {
                return null;
            }
        }

        public void SaveToFile(string path)
        {


            using (var writer = File.CreateText(path))
            {
                foreach(var c in componentConfigurations.Keys)
                {
                    writer.WriteLine("--"+ c + "--");
                    writer.WriteLine(componentConfigurations[c]);
                }
            }
        }
    }
}
