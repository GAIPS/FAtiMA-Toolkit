using System;
using Conditions.DTOs;
using WellFormedNames;

namespace WorldModel.DTOs
{
    /// <summary>
    /// Data Type Object Class for the representation of an action effect.
    /// </summary>
    [Serializable]
    public class EffectDTO 
    {
		public Guid Id { get; set; }
      
        public Name PropertyName { get; set; }
        
        public Name NewValue { get; set; }

        public Name ObserverAgent { get; set; }
    }
}
