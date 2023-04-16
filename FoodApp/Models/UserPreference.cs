using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodApp.Models
{
    public class UserPreference
    {
        public string UserEmail { get; set; }
        public string PreferenceType { get; set; }

        public UserPreference(string userEmail, string preferenceType)
        {
            UserEmail = userEmail;
            PreferenceType = preferenceType;
        }
    }
}