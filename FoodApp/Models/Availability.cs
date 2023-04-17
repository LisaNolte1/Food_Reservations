namespace FoodApp.Models
{
    public class Availability
    {
        public int AvailabilityId { get; set; }
        public int UserId { get; set; }
        public int DayId { get; set; }

        public virtual User User { get; set; }
        public virtual Day Day { get; set; } 
    }
}