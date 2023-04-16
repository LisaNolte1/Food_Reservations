using FoodApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace FoodApp.Controllers.Utility
{
    public partial class MainUtility
    {
        private void GetUserMenuChoice()
        {
            throw new NotImplementedException();
        }

        private void SetUserPreferences()
        {
            throw new NotImplementedException();
        }

        public static List<UserPreference> GetUserPreferences(string userEmail)
        {
            var db = new DbContext();
            string readStatement = $"SELECT user_email, preference_type FROM USERS INNER JOIN SETTINGS ON USERS.user_id = SETTINGS.user_id INNER JOIN PREFERENCES ON SETTINGS.preference_id = PREFERENCES.preference_id WHERE USERS.user_email = '{userEmail}'";
            var result = db.ExecuteQuery(readStatement, null).Rows;
            List<UserPreference> preference = new List<UserPreference>();
            foreach(DataRow row in result) 
            {
                var newRow = new UserPreference(row[0].ToString(), row[1].ToString());
                preference.Add(newRow);
            }
            return preference;
        }

        private void GetWeeklyMenu()
        {
            throw new NotImplementedException();
        }
    }
}