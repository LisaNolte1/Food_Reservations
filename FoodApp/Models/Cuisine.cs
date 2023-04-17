using System.Collections.Generic;

namespace FoodApp.Models
{
    public class Cuisine
    {
        public int CuisineId { get; set; }
        public string CuisineName { get; set; }
        public int PreferenceId { get; set; }

        public virtual Preference Preference { get; set; }
        public virtual ICollection<Event> Events { get; set; }  
    }
}