using System;
using WellFormedNames;
using SerializationUtilities;
using WorldModel.DTOs;

namespace WorldModel
{
    [Serializable]
   public  class Effect : ICustomSerialization
    {
        public Guid Id { get; private set; }

        public Name PropertyName { get; set; }

        public Name NewValue { get; set; }

        public Name ObserverAgent { get; set; }

        public int Priority { get; set; }

        public Effect(EffectDTO ef)
        {
            Id = ef.Id;
            PropertyName = ef.PropertyName;
            NewValue = ef.NewValue;

            if (ef.ObserverAgent != null)
                ObserverAgent = ef.ObserverAgent;
            else ObserverAgent = (Name)"*";

            Priority = ef.Priority;

        }

        public override string ToString()
        {
            return PropertyName + " | " + NewValue + " | " + ObserverAgent + "|" + "|" + Priority + "|"+ this.Id + "\n";
        }


        public EffectDTO ToDTO()
        {
            return new EffectDTO()
            {
                PropertyName = this.PropertyName,
                NewValue = this.NewValue,
                Id = this.Id,
                ObserverAgent = this.ObserverAgent,
                Priority = this.Priority,
            };
        }

        public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
    {
            dataHolder.SetValue("PropertyName", this.PropertyName);
            dataHolder.SetValue("NewValue", this.NewValue);
            dataHolder.SetValue("ObserverAgent", this.ObserverAgent);
            dataHolder.SetValue("Priority", this.Priority);
        }

        public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            PropertyName = dataHolder.GetValue<Name>("PropertyName");
            NewValue = dataHolder.GetValue<Name>("NewValue");
            ObserverAgent = dataHolder.GetValue<Name>("ObserverAgent");
            Priority = dataHolder.GetValue<int>("Priority");
        }
    }
}
