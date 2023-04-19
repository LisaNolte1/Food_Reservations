using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using FoodApp.Controllers.Utility;
using FoodApp.Models;

namespace FoodApp.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Admin()
        {
            var dbContext = new DbContext();

            //Table Data
            var tableQuery = "SELECT e.event_id, e.event_date, u.user_email,c.cuisine_name, co.cuisine_option_name, p.preference_type FROM BOOKINGS b JOIN EVENTS e ON b.event_id = e.event_id AND e.active = 1 " +
                                "JOIN USERS u ON b.user_id = u.user_id " +
                                "JOIN CUISINES_OPTIONS co ON b.cuisine_options_id = co.cuisine_options_id " +
                                "JOIN CUISINES c ON co.cuisine_id = c.cuisine_id " +
                                "JOIN PREFERENCES p ON co.preference_id = p.preference_id";

            var tableResult = dbContext.ExecuteQuery(tableQuery, null).Rows;
            var eventIds = new List<int>();
            var eventDates = new List<DateTime>();
            var userEmails = new List<string>();
            var cuisineNames = new List<string>();
            var cuisineOptions = new List<string>();
            var dietaryPreferences = new List<string>();
            foreach (DataRow row in tableResult)
            {
                eventIds.Add(Convert.ToInt32(row["event_id"]));
                eventDates.Add(((DateTime)row["event_date"]).Date);
                userEmails.Add(row["user_email"].ToString());
                cuisineNames.Add(row["cuisine_name"].ToString());
                cuisineOptions.Add(row["cuisine_option_name"].ToString());
                dietaryPreferences.Add(row["preference_type"].ToString());
            }

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
                eventIds = eventIds.ToArray(),
                eventDates = eventDates.ToArray(),
                userEmails = userEmails.ToArray(),
                cuisineNames = cuisineNames.ToArray(),
                cuisineOptions = cuisineOptions.ToArray(),
                dietaryPreferences = dietaryPreferences.ToArray(),
                barChartLabels = barChartLabels.ToArray(),
                barChartData = barChartData.ToArray(),
                pieChartLabels = pieChartLabels.ToArray(),
                pieChartData = pieChartData.ToArray(),
                lineChartLabels = lineChartLabels.ToArray(),
                lineChartData = lineChartData.ToArray()
            };

            return View(adminStatistics);
        }

        public ActionResult CreateEvent()
        {
            var newEvent = MainUtility.CreateEvent();
            ViewData["Cuisines"] = newEvent[0];
            ViewData["Days"] = newEvent[1];
            return View();
        }

        public ActionResult SaveEvent(Models.Menu model)
        {
            Debug.WriteLine(model.ExpiryDate);
            Debug.WriteLine(model.CuisineIdThursday);
            Debug.WriteLine(model.CuisineIdWednesday);
            var resp = SaveInternal(model);
            if (!resp)
            {
                ViewData["itle"] = "Fail";
            }
            return View();
        }

        private bool SaveInternal(Models.Menu menu)
        {
            try
            {
                MainUtility.SaveEventUtil(menu);
                ViewData["success"] = true;
                return true;
            }
            catch (Exception ex)
            {
                ViewData["error"] = ex;
                return false;
            }
        }
    }
}
