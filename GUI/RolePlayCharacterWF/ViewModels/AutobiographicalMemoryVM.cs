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
	    private readonly BaseRPCForm _mainForm;
	    private RolePlayCharacterAsset _rpcAsset => _mainForm.CurrentAsset;

        public BindingListView<EventDTO> Events {get;}

		public static readonly string[] EventTypes = { Constants.ACTION_START_EVENT.ToString(),Constants.ACTION_FINISHED_EVENT.ToString(), Constants.PROPERTY_CHANGE_EVENT.ToString() };

		public AutobiographicalMemoryVM(BaseRPCForm form)
		{
			_mainForm = form;
            this.Events = new BindingListView<EventDTO>(_rpcAsset.EventRecords.ToList());
        }
     
        public void AddEventRecord(EventDTO newEvent)
        {
            _rpcAsset.AddEventRecord(newEvent);
            Events.DataSource = _rpcAsset.EventRecords.ToList();
            Events.Refresh();
			_mainForm.SetModified();
        }

        public void UpdateEventRecord(EventDTO existingEvent)
        {
            _rpcAsset.UpdateEventRecord(existingEvent);
            Events.DataSource = _rpcAsset.EventRecords.ToList();
            Events.Refresh();
			_mainForm.SetModified();
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
			_mainForm.SetModified();
		}
    }
}
