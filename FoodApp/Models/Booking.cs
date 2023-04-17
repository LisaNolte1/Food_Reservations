namespace FoodApp.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }

        public virtual User User { get; set; }
        public virtual Event Event { get; set; } 
    }
}