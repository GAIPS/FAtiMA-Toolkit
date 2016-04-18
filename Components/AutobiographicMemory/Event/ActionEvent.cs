using System;
using AutobiographicMemory.DTOs;
using GAIPS.Serialization;
using KnowledgeBase.WellFormedNames;

namespace AutobiographicMemory
{
	public sealed partial class AM
	{
		[Serializable]
		private class ActionEvent : BaseEvent
		{
            public static bool IsActionEvent(Name eventName)
            {
                return eventName.GetNTerm(1) == Constants.ACTION_EVENT;
            }

			public Name Action { get; private set; }
            public Name Target { get; private set; }

		    public ActionEvent(uint id, Name eventName, ulong timestamp) : base(id, eventName, timestamp)
			{
				Action = eventName.GetNTerm(3);
                Target = eventName.GetNTerm(4);
	        }

			public override EventDTO ToDTO()
			{
				return new ActionEventDTO
				{
					Action = Action.ToString(),
					Event = EventName.ToString(),
					Id = Id,
					Subject = Subject.ToString(),
					Target = Target.ToString(),
					Time = Timestamp
				};
			}

			protected override Name BuildEventName()
			{
				return Name.BuildName(EVT_NAME, (Name)Type, (Name)Subject, Action, (Name)Target);
			}

			public override BaseEvent SwapPerspective(Name oldPerspective, Name newPerspective)
			{
				Action = Action.SwapPerspective(oldPerspective, newPerspective);
				Target = Target.SwapPerspective(oldPerspective, newPerspective);
				base.SwapPerspective(oldPerspective, newPerspective);
				return this;
			}

			public override void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
			{
				base.GetObjectData(dataHolder,context);
				dataHolder.SetValue("Action", Action);
				dataHolder.SetValue("Target", Target);
			}

			public override void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
			{
				Action = dataHolder.GetValue<Name>("Action");
				Target = dataHolder.GetValue<Name>("Target");
				base.SetObjectData(dataHolder,context);
			}
		}
	}
}