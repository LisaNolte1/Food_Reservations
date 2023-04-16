using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodApp.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public int CuisineId { get; set; }
        public int DayId { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        //public virtual Cuisine Cuisine { get; set; }
        //public virtual Day Day { get; set; }
        //public virtual ICollection<Booking> Bookings { get; set; }

        //public Event(int eventId, int cuisineId, int dayId, DateTime date)
        //{
        //    this.EventId = eventId;
        //    this.CuisineId = cuisineId;
        //    this.DayId = dayId;
        //    this.Date = date;
        //}
    }
}