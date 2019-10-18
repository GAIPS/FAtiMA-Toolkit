using SerializationUtilities;
using SerializationUtilities.Attributes;
using System.Collections.Generic;
using System.IO;

namespace RuleLibraryComponent
{
    [Serializable]
    public class RuleLibrary
    {
        
        private Dictionary<string, List<ComponentRule>> componentRules { get; set; }

        public RuleLibrary()
        {
            this.componentRules = new Dictionary<string, List<ComponentRule>>();
        }


        public void AddComponentRule(string componentName, ComponentRule rule )
        {
            if (!componentRules.ContainsKey(componentName))
            {
                componentRules.Add(componentName, new List<ComponentRule>());
            }
            componentRules[componentName].Add(rule);
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
