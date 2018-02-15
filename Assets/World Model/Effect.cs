using System;
using System.Collections.Generic;
using System.Linq;
using WellFormedNames;
using Conditions;
using System.Text;
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

        public Name ResponsibleAgent { get; set; }

        public Effect(EffectDTO ef)
        {
            Id = ef.Id;
            PropertyName = ef.PropertyName;
            NewValue = ef.NewValue;
            if (ef.ResponsibleAgent != null)
                ResponsibleAgent = ef.ResponsibleAgent;
            else ResponsibleAgent = (Name) "World";
        }

        public override string ToString()
        {
            return PropertyName + " | " + NewValue + " | "  + ResponsibleAgent + "|" + this.Id + "\n";
        }


        public EffectDTO ToDTO()
        {
            return new EffectDTO()
            {
                PropertyName = this.PropertyName,
                NewValue = this.NewValue,
                Id = this.Id,
                ResponsibleAgent = this.ResponsibleAgent
            };
        }

        public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
    {
            dataHolder.SetValue("PropertyName", this.PropertyName);
            dataHolder.SetValue("NewValue", this.NewValue);
            dataHolder.SetValue("ResponsibleAgent", this.ResponsibleAgent);
        }

        public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            PropertyName = dataHolder.GetValue<Name>("PropertyName");
            NewValue = dataHolder.GetValue<Name>("NewValue");
            ResponsibleAgent = dataHolder.GetValue<Name>("ResponsibleAgent");

        }
    }
}
