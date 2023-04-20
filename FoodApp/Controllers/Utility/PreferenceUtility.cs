using FoodApp;
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
        public static bool SetUserPreferences(FormSelection model)
        {
            try
            {
                string emailAddress = System.Security.Claims.ClaimsPrincipal.Current.FindFirst("preferred_username").Value;
                var db = new DbContext();
                string readStatement = $"SELECT * FROM EVENTS WHERE active = 1";
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
            foreach (DataRow row in result)
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
        public static AdminStatistics GetAdminStatistics()
        {
            var dbContext = new DbContext();

            // Bar chart data
            var barChartQuery = "SELECT CUISINES_OPTIONS.cuisine_option_name, COUNT(*) as num_bookings FROM BOOKINGS JOIN CUISINES_OPTIONS ON BOOKINGS.cuisine_options_id = CUISINES_OPTIONS.cuisine_options_id GROUP BY CUISINES_OPTIONS.cuisine_option_name";
            var barChartResult = dbContext.ExecuteQuery(barChartQuery, null).Rows;
            var barChartLabels = new List<string>();
            var barChartData = new List<int>();
            foreach (DataRow row in barChartResult)
            {
                barChartLabels.Add(row["cuisine_option_name"].ToString());
                barChartData.Add(Convert.ToInt32(row["num_bookings"]));
            }

            // Pie chart data
            var pieChartQuery = "SELECT PREFERENCES.preference_type, COUNT(BOOKINGS.booking_id) AS bookings_count FROM BOOKINGS JOIN USERS ON BOOKINGS.user_id = USERS.user_id JOIN CUISINES_OPTIONS ON BOOKINGS.cuisine_options_id = CUISINES_OPTIONS.cuisine_options_id JOIN PREFERENCES ON USERS.preference_id = PREFERENCES.preference_id GROUP BY PREFERENCES.preference_type";
            var pieChartResult = dbContext.ExecuteQuery(pieChartQuery, null).Rows;
            var pieChartLabels = new List<string>();
            var pieChartData = new List<int>();
            foreach (DataRow row in pieChartResult)
            {
                pieChartLabels.Add(row["preference_type"].ToString());
                pieChartData.Add(Convert.ToInt32(row["bookings_count"]));
            }

            // Line chart data
            var lineChartQuery = "SELECT [event_id], COUNT(*) AS [num_bookings] FROM [dbo].[BOOKINGS] GROUP BY [event_id]";
            var lineChartResult = dbContext.ExecuteQuery(lineChartQuery, null).Rows;
            var lineChartLabels = new List<string>();
            var lineChartData = new List<int>();
            foreach (DataRow row in lineChartResult)
            {
                lineChartLabels.Add(row["event_id"].ToString());
                lineChartData.Add(Convert.ToInt32(row["num_bookings"]));
            }

            var adminStatistics = new AdminStatistics
            {
                barChartLabels = barChartLabels.ToArray(),
                barChartData = barChartData.ToArray(),
                pieChartLabels = pieChartLabels.ToArray(),
                pieChartData = pieChartData.ToArray(),
                lineChartLabels = lineChartLabels.ToArray(),
                lineChartData = lineChartData.ToArray()
            };
            return adminStatistics;
        }
    }
}