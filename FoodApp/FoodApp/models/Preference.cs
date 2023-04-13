using System.Collections.Generic;

namespace FoodApp.Models
{
    public class Preference
    {
        public int PreferenceId { get; set; }
        public string PreferenceType { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Cuisine> Cuisines { get; set; } 
    }
}