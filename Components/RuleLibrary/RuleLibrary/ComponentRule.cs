using Conditions;
using Conditions.DTOs;
using SerializationUtilities.Attributes;
using Utilities.Json;
using System.Collections.Generic;
using WellFormedNames;

namespace RuleLibraryComponent
{
    [Serializable]
    public class ComponentRule
    {
        public Name Name { get; set; }
        public List<string> Identities { get; set; }
        public ConditionSetDTO Conditions { get; set; }
        public Dictionary<string, string> Properties { get; set; }
    }
}