using SerializationUtilities;
using System;
using System.Collections.Generic;
using WellFormedNames;
using KnowledgeBase;
using System.Linq;
using SerializationUtilities.SerializationGraph;

namespace WorkingMemory
{
    [Serializable]
    public sealed class WM : ICustomSerialization
    {
        private Dictionary<string, Name> values = new Dictionary<string, Name>();

        public void BindToRegistry(IDynamicPropertiesRegistry registry)
        {
            registry.RegistDynamicProperty(WORKING_MEMORY_ID_PROPERTY_NAME, WorkingMemoryProperty);
        }

        //Event
        private static readonly Name WORKING_MEMORY_ID_PROPERTY_NAME = Name.BuildName("TellWM");
        private IEnumerable<DynamicPropertyResult> WorkingMemoryProperty(IQueryContext context, Name name, Name value)
        {
            foreach (var c in context.Constraints)
            {
                var n = context.AskPossibleProperties(name).ToList().FirstOrDefault()?.Item1.Value;
                var v = context.AskPossibleProperties(value).ToList().FirstOrDefault()?.Item1.Value;

                if (n == null || v == null)
                    yield return new DynamicPropertyResult(new ComplexValue(Name.BuildName("Fail")), c);
                else if (!n.IsGrounded || !v.IsGrounded)
                    yield return new DynamicPropertyResult(new ComplexValue(Name.BuildName("Fail")), c);
                else if (n.IsComposed || v.IsComposed)
                    yield return new DynamicPropertyResult(new ComplexValue(Name.BuildName("Fail")), c);
                else
                {
                    this.SetValue(n,v);
                    yield return new DynamicPropertyResult(new ComplexValue(Name.BuildName("OK")), c);
                }
                
            }
        }

        public void SetValue(Name valueName, Name value)
        {
            this.values[valueName.ToString().ToLower()] = value;
        }

        public Name GetValue(Name valueName)
        {
            var key = valueName.ToString().ToLower();
            if (this.values.ContainsKey(key))
                return this.values[key];
            else return null;
        }


        public IEnumerable<WMEntryDTO> GetAllWorkingMemoryEntries()
        {
            return this.values.Select(v => new WMEntryDTO { Name = v.Key, Value = v.Value.ToString()});
        }

        public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            var valList = dataHolder.ParentGraph.CreateObjectData();
            dataHolder.SetValueGraphNode("Values", valList);
            foreach (var v in values)
            {
                IGraphNode node;
                if (!valList.TryGetField(v.Key, out node))
                {
                    valList[v.Key] = dataHolder.ParentGraph.BuildNode(v.Value);
                }
            }
        }


        public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            this.values = new Dictionary<string, Name>();
            var valList = dataHolder.GetValueGraphNode("Values");
            var it = ((IObjectGraphNode)valList).GetEnumerator();
            while (it.MoveNext())
            {
                var key = it.Current.FieldName;
                var value = it.Current.FieldNode.RebuildObject<Name>();
                this.SetValue((Name)key,value);
            }
        }
    }
}
