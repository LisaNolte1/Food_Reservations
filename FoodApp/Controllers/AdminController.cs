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
            // THIS IS JUST FOR REFERENCE, THIS IS NOT THE FINAL CONTROLLER.
            // Fetch DB context
            var dbContext = new DbContext();

            // Query the database to get the data for the bar chart
            string barChartQuery = "SELECT CUISINES_OPTIONS.cuisine_option_name, COUNT(*) as num_bookings FROM BOOKINGS JOIN CUISINES_OPTIONS ON BOOKINGS.cuisine_options_id = CUISINES_OPTIONS.cuisine_options_id GROUP BY CUISINES_OPTIONS.cuisine_option_name";
            var barChartResult = dbContext.ExecuteQuery(barChartQuery, null).Rows;

            // Create a list of objects to store the data
            var chartData = new List<object>();

            foreach (DataRow row in barChartResult)
            {
                var data = new
                {
                    cuisine_option_name = row["cuisine_option_name"].ToString(),
                    num_bookings = row["num_bookings"]
                };
                chartData.Add(data);
            }

            // Pass data to the view using ViewBag
            ViewBag.ChartData = chartData;

            return View();
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
            catch(Exception ex)
            {
                ViewData["error"] = ex;
                return false;
            }
        }
    }
}
