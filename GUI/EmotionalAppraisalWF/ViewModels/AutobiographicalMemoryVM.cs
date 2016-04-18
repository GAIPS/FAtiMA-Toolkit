using System.Collections.Generic;
using System.Linq;
using AutobiographicMemory.DTOs;
using EmotionalAppraisal;
using EmotionalAppraisal.DTOs;
using Equin.ApplicationFramework;

namespace EmotionalAppraisalWF.ViewModels
{
    public class AutobiographicalMemoryVM
    {
        private readonly EmotionalAppraisalAsset _emotionalAppraisalAsset;

        public BindingListView<EventDTO> Events {get;}
        
        public string[] EventTypes => _emotionalAppraisalAsset.EventTypes;

        public AutobiographicalMemoryVM(EmotionalAppraisalAsset ea)
        {
            _emotionalAppraisalAsset = ea;
            this.Events = new BindingListView<EventDTO>(ea.EventRecords.ToList());
        }
     
        public void AddEventRecord(EventDTO newEvent)
        {
            _emotionalAppraisalAsset.AddEventRecord(newEvent);
            Events.DataSource = _emotionalAppraisalAsset.EventRecords.ToList();
            Events.Refresh();
        }

        public void UpdateEventRecord(EventDTO existingEvent)
        {
            _emotionalAppraisalAsset.UpdateEventRecord(existingEvent);
            Events.DataSource = _emotionalAppraisalAsset.EventRecords.ToList();
            Events.Refresh();
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
        }
    }
}
