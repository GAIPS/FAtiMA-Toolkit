using System.Collections.Generic;
using System.Linq;
using AutobiographicMemory;
using AutobiographicMemory.DTOs;
using EmotionalAppraisal;
using Equin.ApplicationFramework;

namespace EmotionalAppraisalWF.ViewModels
{
    public class AutobiographicalMemoryVM
    {
	    private readonly BaseEAForm _mainForm;
	    private EmotionalAppraisalAsset _emotionalAppraisalAsset => _mainForm.CurrentAsset;

        public BindingListView<EventDTO> Events {get;}

		public static readonly string[] EventTypes = { Constants.ACTION_START_EVENT.ToString(),Constants.ACTION_FINISHED_EVENT.ToString(), Constants.PROPERTY_CHANGE_EVENT.ToString() };

		public AutobiographicalMemoryVM(BaseEAForm form)
		{
			_mainForm = form;
            this.Events = new BindingListView<EventDTO>(_emotionalAppraisalAsset.EventRecords.ToList());
        }
     
        public void AddEventRecord(EventDTO newEvent)
        {
            _emotionalAppraisalAsset.AddEventRecord(newEvent);
            Events.DataSource = _emotionalAppraisalAsset.EventRecords.ToList();
            Events.Refresh();
			_mainForm.SetModified();
        }

        public void UpdateEventRecord(EventDTO existingEvent)
        {
            _emotionalAppraisalAsset.UpdateEventRecord(existingEvent);
            Events.DataSource = _emotionalAppraisalAsset.EventRecords.ToList();
            Events.Refresh();
			_mainForm.SetModified();
		}

        public EventDTO RetrieveEventRecord(uint id)
        {
            return _emotionalAppraisalAsset.GetEventDetails(id);
        }

        public void RemoveEventRecords(IEnumerable<EventDTO> events)
        {
            foreach (var eventDto in events)
            {
                _emotionalAppraisalAsset.ForgetEvent(eventDto.Id);
            }

            Events.DataSource = _emotionalAppraisalAsset.EventRecords.ToList();
            Events.Refresh();
			_mainForm.SetModified();
		}
    }
}
