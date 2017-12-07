using CVGS.Models;
using System.Linq;
using System.Collections.Generic;

namespace CVGS.ViewModels
{
    public class EventAssociationsViewModel : EVENT
    {
        /// <summary>
        /// Whether the Member is registered for Event
        /// </summary>
        public bool IsRegistered;

        /// <summary>
        /// Count of Members registered for Event
        /// </summary>
        public int RegisteredCount;


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