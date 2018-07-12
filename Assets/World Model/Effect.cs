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

        public Effect(EffectDTO ef)
        {
            Id = ef.Id;
            PropertyName = ef.PropertyName;
            NewValue = ef.NewValue;

            if (ef.ObserverAgent != null)
                ObserverAgent = ef.ObserverAgent;
            else ObserverAgent = (Name)"*";
        }

        public override string ToString()
        {
            return PropertyName + " | " + NewValue + " | " + ObserverAgent + "|"+ this.Id + "\n";
        }


        public EffectDTO ToDTO()
        {
            return new EffectDTO()
            {
                PropertyName = this.PropertyName,
                NewValue = this.NewValue,
                Id = this.Id,
                ObserverAgent = this.ObserverAgent,
            };
        }

        public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
    {
            dataHolder.SetValue("PropertyName", this.PropertyName);
            dataHolder.SetValue("NewValue", this.NewValue);
            dataHolder.SetValue("ObserverAgent", this.ObserverAgent);
        }

        public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            PropertyName = dataHolder.GetValue<Name>("PropertyName");
            NewValue = dataHolder.GetValue<Name>("NewValue");
            ObserverAgent = dataHolder.GetValue<Name>("ObserverAgent");
        }
    }
}
