using System.Collections.Generic;

namespace FoodApp.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public int CuisineId { get; set; }
        public int DayId { get; set; }

        public virtual Cuisine Cuisine { get; set; }
        public virtual Day Day { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; } 
    }
}