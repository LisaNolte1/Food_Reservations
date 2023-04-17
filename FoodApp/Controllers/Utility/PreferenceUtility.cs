using FoodApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;

namespace FoodApp.Controllers.Utility
{
    public partial class MainUtility
    {
        private void GetUserMenuChoice()
        {
            throw new NotImplementedException();
        }

        public static bool SetUserPreferences(FormSelection model)
        {
            try
            {
                string emailAddress = System.Security.Claims.ClaimsPrincipal.Current.FindFirst("preferred_username").Value;
                var db = new DbContext();
                string readStatement = $"SELECT * FROM EVENTS WHERE active = 1";
                Debug.WriteLine(readStatement);
                var result = db.ExecuteQuery(readStatement, null).Rows;
                var userIDResult = db.ExecuteQuery($"SELECT user_id FROM USERS WHERE user_email = '{emailAddress}'", null).Rows;
                int userID = (int)userIDResult[0][0];
                if (model.Days == 4)
                {
                    return true;
                }
                foreach (DataRow row in result)
                {
                    if ((int)row[2] == 1 && (model.Days == 1 || model.Days == 3))
                    {
                        db.ExecuteNonQuery($"INSERT INTO BOOKINGS (user_id,event_id,cuisine_options_id) VALUES ({userID}, {(int)row[0]}, {model.wedFood})", null);
                    }
                    else if ((int)row[2] == 2 && (model.Days == 2 || model.Days == 3))
                    {
                        db.ExecuteNonQuery($"INSERT INTO BOOKINGS (user_id,event_id,cuisine_options_id) VALUES ({userID}, {(int)row[0]}, {model.thursFood})", null);
                    }
                }


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
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

        public static List<List<KeyValuePair<int, string>>> GetWeeklyMenu()
        {
            var db = new DbContext();
            string readStatement = $"SELECT * FROM EVENTS WHERE active = 1";
            Debug.WriteLine(readStatement);
            var result = db.ExecuteQuery(readStatement, null).Rows;
            List<KeyValuePair<int, string>> wedOptions = new List<KeyValuePair<int, string>>();
            List<KeyValuePair<int, string>> thursOptions = new List<KeyValuePair<int, string>>();
            foreach (DataRow row in result)
            {
                Debug.WriteLine(row[1]);
                readStatement = $"SELECT * FROM CUISINES_OPTIONS WHERE cuisine_id = {row[1]}";
                var cResults = result = db.ExecuteQuery(readStatement, null).Rows;
                foreach (DataRow c in cResults)
                {
                    if ((int)row[2] == 1)
                    {
                        wedOptions.Add(new KeyValuePair<int, string>((int)c[0], c[3].ToString()));
                    }
                    else if ((int)row[2] == 2)
                    {
                        thursOptions.Add(new KeyValuePair<int, string>((int)c[0], c[3].ToString()));
                    }

                }
            }
            return new List<List<KeyValuePair<int, string>>> { wedOptions, thursOptions };
        }

        public static List<List<KeyValuePair<int, string>>> CreateEvent()
        {
            DbContext db = new DbContext();
            string readStatement = $"SELECT * FROM CUISINES";
            Debug.WriteLine(readStatement);
            var results = db.ExecuteQuery(readStatement, null).Rows;
            List<KeyValuePair<int, string>> Cuisines = new List<KeyValuePair<int, string>>();
            foreach (DataRow result in results)
            {
                Debug.WriteLine(result[0]);
                Debug.WriteLine(result[1]);
                Cuisines.Add(new KeyValuePair<int, string>((int)result[0], result[1].ToString()));
            }

            readStatement = $"SELECT * FROM DAYS";
            Debug.WriteLine(readStatement);
            results = db.ExecuteQuery(readStatement, null).Rows;
            List<KeyValuePair<int, string>> Days = new List<KeyValuePair<int, string>>();
            foreach (DataRow result in results)
            {
                Debug.WriteLine(result[0]);
                Debug.WriteLine(result[1]);
                Days.Add(new KeyValuePair<int, string>((int)result[0], result[1].ToString()));
            }

            return new List<List<KeyValuePair<int, string>>> { Cuisines, Days };
        }

        public static void SaveEventUtil(Models.Menu menu)
        {
            try
            {
                var db = new DbContext();
                var parameters = getSQLParameters(menu);
                db.ExecuteNonQuery("UPDATE EVENTS SET active = 0", null);

                db.ExecuteNonQuery("INSERT INTO EVENTS (cuisine_id,day_id,event_date,active) VALUES (@CuisineIdWednesday, @DayIdWednesday, CONVERT (date, @EventDate, 101),1)", parameters[0]);
                db.ExecuteNonQuery("INSERT INTO EVENTS (cuisine_id,day_id,event_date,active) VALUES (@CuisineIdThursday, @DayIdThursday, CONVERT (date, @EventDate, 101),1)", parameters[1]);
            }
            catch (Exception ex) 
            {
                throw;
            }
        }

        private static List<SqlParameter[]> getSQLParameters(Models.Menu menu)
        {
            List<SqlParameter[]> parameters = new List<SqlParameter[]>();
            var parameters0 = new[]
               {
                new SqlParameter("@CuisineIdWednesday", menu.CuisineIdWednesday),
                new SqlParameter("@DayIdWednesday", 1),
                new SqlParameter("@EventDate", menu.ExpiryDate)
                };
            parameters.Add(parameters0);
            var parameters1 = new[]
            {
                new SqlParameter("@CuisineIdThursday", menu.CuisineIdThursday),
                new SqlParameter("@DayIdThursday", 2),
                new SqlParameter("@EventDate", menu.ExpiryDate)
                };
            parameters.Add(parameters1);

            return parameters;
        }
    }
}