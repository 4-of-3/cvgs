using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CVGS.ViewModels;

namespace CVGS.Models
{
    [MetadataType(typeof(EventMeta))]
    public partial class EVENT
    {
    }

    public class EventMeta
    {
        [DisplayName("ID")]
        public int EventId { get; set; }

        [DisplayName("Title")]
        [Required]
        public string EventTitle { get; set; }

        [DisplayName("Description")]
        [Required, MaxLength(1024)]
        public string Description { get; set; }

        [DisplayName("Date")]
        [Required, DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MMM-dd-yyyy h:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime EventDate { get; set; }

        [DisplayName("Active")]
        public bool ActiveStatus { get; set; }

        [DisplayName("Date Created")]
        public DateTime? DateCreated { get; set; }

        [DisplayName("Location")]
        [Required, MaxLength(64)]
        public string Location { get; set; }


        /// <summary>
        /// Extend the base Event class with basic association information about an event (registration, etc)
        /// </summary>
        /// <param name="event">Event model object to convert</param>
        /// <param name="memberId">Member id that defines several associations</param>
        /// <returns>Extended viewmodel for Event model with associations</returns>
        public static EventAssociationsViewModel CreateEventAssociationsFromModel(EVENT @event, int memberId)
        {
            // Determine associated information
            bool isRegistered = @event.MEMBER_EVENT.Count(e => e.EventId == @event.EventId && e.MemberId == memberId) > 0;

            return new EventAssociationsViewModel()
            {
                EventId = @event.EventId,
                EventTitle = @event.EventTitle,
                Description = @event.Description,
                Location = @event.Location,
                EventDate = @event.EventDate,
                ActiveStatus = @event.ActiveStatus,
                DateCreated = @event.DateCreated,
                // Associated information (added)
                IsRegistered = isRegistered,
                // Associated lists
                MEMBER_EVENT = @event.MEMBER_EVENT,
            };
        }


        /// <summary>
        /// Convert a list of Event models into a list of the extended Events viewmodel with assocations
        /// </summary>
        /// <param name="events">List of Event models</param>
        /// <param name="memberId">Member id that defines several assocations</param>
        /// <returns>List of extended viewmodels for Event model with associations</returns>
        public static List<EventAssociationsViewModel> CreateEventAssociationsListFromModels(IEnumerable<EVENT> events, int memberId)
        {
            List<EventAssociationsViewModel> eventAssociations = new List<EventAssociationsViewModel>();

            foreach (EVENT @event in events)
            {
                eventAssociations.Add(CreateEventAssociationsFromModel(@event, memberId));
            }

            return eventAssociations;
        }
    }
}
