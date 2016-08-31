using System;
using AutobiographicMemory.DTOs;
using SerializationUtilities;
using WellFormedNames;

namespace AutobiographicMemory
{
	public sealed partial class AM
	{
		[Serializable]
		private class PropertyChangeEvent: BaseEvent, IPropertyChangedEvent
		{
		    public static bool IsPropertyChangeEvent(Name eventName)
		    {
		        return eventName.GetNTerm(1) == Constants.PROPERTY_CHANGE_EVENT;
		    }

			public Name Property { get; private set; }
    	    public Name NewValue { get; private set; }
    
			public PropertyChangeEvent(uint id, Name eventName, ulong timestamp) : base(id, eventName, timestamp)
			{
                Property = eventName.GetNTerm(3);
				NewValue = eventName.GetNTerm(4);
			}

			public override EventDTO ToDTO()
			{
				return new PropertyChangeEventDTO
				{
					Property = Property.ToString(),
					Event = EventName.ToString(),
					Id = Id,
					Subject = Subject.ToString(),
					NewValue = NewValue.ToString(),
					Time = Timestamp
				};
			}

			protected override Name BuildEventName()
			{
				return Name.BuildName(EVT_NAME, Type, Subject, Property, NewValue);
			}

			public override BaseEvent SwapPerspective(Name oldPerspective, Name newPerspective)
			{
				Property = Property.SwapTerms(oldPerspective, newPerspective);
				NewValue = NewValue.SwapTerms(oldPerspective, newPerspective);
				base.SwapPerspective(oldPerspective, newPerspective);
				return this;
			}

			public override void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
			{
				base.GetObjectData(dataHolder,context);
				dataHolder.SetValue("Property", Property);
				dataHolder.SetValue("NewValue", NewValue);
			}

			public override void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
			{
				Property = dataHolder.GetValue<Name>("Property");
				NewValue = dataHolder.GetValue<Name>("NewValue");
				base.SetObjectData(dataHolder,context);
			}
		}
	}
}