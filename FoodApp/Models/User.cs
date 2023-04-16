using System.Collections.Generic;

namespace FoodApp.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public int PreferenceId { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual Preference Preference { get; set; }
        public virtual ICollection<Availability> Availabilities { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}