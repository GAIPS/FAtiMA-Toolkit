using System.Collections.Generic;
using System.Linq;
using AutobiographicMemory;
using AutobiographicMemory.DTOs;
using Equin.ApplicationFramework;
using RolePlayCharacter;

namespace RolePlayCharacterWF.ViewModels
{
    public class AutobiographicalMemoryVM
    {
        private RolePlayCharacterAsset _rpcAsset;

        public BindingListView<EventDTO> Events {get;}
        public WellFormedNames.Name CharName { get; }

		public static readonly string[] EventTypes = { AMConsts.ACTION_END, AMConsts.ACTION_START, AMConsts.PROPERTY_CHANGE };

		public AutobiographicalMemoryVM(RolePlayCharacterAsset asset)
		{
            _rpcAsset = asset;
            this.Events = new BindingListView<EventDTO>(_rpcAsset.EventRecords.ToList());
            this.CharName = _rpcAsset.CharacterName;
        }
     
        public void AddEventRecord(EventDTO newEvent)
        {
            _rpcAsset.AddEventRecord(newEvent);
            Events.DataSource = _rpcAsset.EventRecords.ToList();
            Events.Refresh();
        }

        public void UpdateEventRecord(EventDTO existingEvent)
        {
            _rpcAsset.UpdateEventRecord(existingEvent);
            Events.DataSource = _rpcAsset.EventRecords.ToList();
            Events.Refresh();
		}

        public EventDTO RetrieveEventRecord(uint id)
        {
            return _rpcAsset.GetEventDetails(id);
        }

        public void RemoveEventRecords(IEnumerable<EventDTO> events)
        {
            foreach (var eventDto in events)
            {
                _rpcAsset.ForgetEvent(eventDto.Id);
            }

            Events.DataSource = _rpcAsset.EventRecords.ToList();
            Events.Refresh();
		}
    }
}
