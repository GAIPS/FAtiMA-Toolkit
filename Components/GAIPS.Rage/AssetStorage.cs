using SerializationUtilities;
using System.Collections.Generic;
using Utilities.Json;

namespace GAIPS.Rage
{
    public class AssetStorage
    {
        private Dictionary<string, string> componentConfigurations;

        public AssetStorage()
        {
            this.componentConfigurations = new Dictionary<string, string>();
        }

        public static AssetStorage FromJson(string json)
        {
            var res = new AssetStorage();
            if (json == string.Empty) return res;
            var serializer = new JSONSerializer();
            string assetName = string.Empty;
            foreach (var el in (JsonArray)JsonParser.Parse(json))
            {
                if (el.GetType() == typeof(JsonString)) 
                    assetName = el.ToString().Trim(new char[]{'\"'});
                else
                {
                    res.componentConfigurations[assetName] = el.ToString(true);
                }

            }

            return res;
        }

        public void StoreComponent(string componentName, string configuration)
        {
            if (!componentConfigurations.ContainsKey(componentName))
            {
                componentConfigurations.Add(componentName, configuration);
            }
            else
            {
                componentConfigurations[componentName] = configuration;
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


        public string ToJson()
        {
            if (componentConfigurations.Count == 0) return null;

            string result = "[\n";
            var i = 0;
            foreach (var c in componentConfigurations.Keys)
            {
                result += "\"" + c + "\", \n";

                if (i < componentConfigurations.Keys.Count - 1)
                {
                    result += componentConfigurations[c] + ",\n";
                }
                else
                {
                    result += componentConfigurations[c] + "\n]\n";
                }
                i++;
            }
            return result;
        }
    }
}
