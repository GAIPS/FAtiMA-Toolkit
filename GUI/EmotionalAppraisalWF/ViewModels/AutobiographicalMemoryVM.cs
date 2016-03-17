using System.Collections.Generic;
using System.Linq;
using EmotionalAppraisal;
using EmotionalAppraisal.DTOs;
using Equin.ApplicationFramework;

namespace EmotionalAppraisalWF.ViewModels
{
    public class AutobiographicalMemoryVM
    {
        private EmotionalAppraisalAsset _emotionalAppraisalAsset;

        public BindingListView<EventDTO> Events {get;}
        
        public AutobiographicalMemoryVM(EmotionalAppraisalAsset ea)
        {
            _emotionalAppraisalAsset = ea;
            this.Events = new BindingListView<EventDTO>(ea.EventRecords.ToList());
        }

        public void AddEventRecord(EventDTO newEvent)
        {
            var id = _emotionalAppraisalAsset.AddEventRecord(newEvent);
            newEvent.Id = id;
            Events.DataSource.Add(newEvent);
            Events.Refresh();
        }

        
    }
}
