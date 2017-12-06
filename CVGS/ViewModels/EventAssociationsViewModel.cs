using CVGS.Models;

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
    }
}