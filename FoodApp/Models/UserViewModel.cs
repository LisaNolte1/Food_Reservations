using System.Collections.Generic;
using System.Web.Mvc;

namespace FoodApp.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }

        public string UserEmail { get; set; }

        public int PreferenceId { get; set; }

        public string PreferenceType { get; set; }

        public IEnumerable<SelectListItem> PreferenceTypes { get; set; }
    }

}