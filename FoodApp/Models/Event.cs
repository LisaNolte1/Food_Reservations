using System;
using System.ComponentModel.DataAnnotations;

namespace FoodApp.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public int CuisineId { get; set; }
        public int DayId { get; set; }

        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        public int Active { get; set; }
    }
}