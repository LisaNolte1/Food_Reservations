using System.Collections.Generic;

namespace FoodApp.Models
{
    public class Day
    {
        public int DayId { get; set; }
        public string DayName { get; set; }

        public virtual ICollection<Availability> Availabilities { get; set; }
        public virtual ICollection<Event> Events { get; set; } 
    }
}