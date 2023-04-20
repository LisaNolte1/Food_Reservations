using FoodApp.Controllers.Utility;
using FoodApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace FoodApp.Controllers
{
    [Authorize]
    public class PreferencesController : Controller
    {

        [Authorize]
        public ActionResult Preferences()
        {
            
            string emailAddress = System.Security.Claims.ClaimsPrincipal.Current.FindFirst("preferred_username").Value;
            var db = new DbContext();
            string userQuery = $"SELECT user_id FROM Users WHERE user_email = '{emailAddress}'";
            var userResult = db.ExecuteQuery(userQuery, null);
            DataRow res1 = userResult.Rows[0];
            int userId = res1.Field<int>("user_id");

            string prefQuery = $"SELECT user_id, day_id, preference_id from Settings WHERE user_id = {userId}";
            var prefResult = db.ExecuteQuery(prefQuery, null);

            // Create the view model and set any existing preferences
            var viewModel = new FormSelection();
            if (prefResult.Rows.Count > 0)
            {
                DataRow prefRow = prefResult.Rows[0];
                viewModel.DietaryRequirements = prefRow.Field<int>("preference_id");
                viewModel.Days = prefRow.Field<int>("day_id");
            }else
            {
                viewModel.DietaryRequirements = 4;
                viewModel.Days = 4;
            }

            // Get the weekly menu
            var menu = MainUtility.GetWeeklyMenu();

            // Populate the options for the dropdown list
            var daysList = new List<SelectListItem>();
            daysList.Add(new SelectListItem { Text = "Wednesday", Value = "1" });
            daysList.Add(new SelectListItem { Text = "Thursday", Value = "2" });
            daysList.Add(new SelectListItem { Text = "Both", Value = "3" });
            daysList.Add(new SelectListItem { Text = "None", Value = "4" });
            ViewData["DaysList"] = daysList;

            var preferenceList = new List<SelectListItem>();
            daysList.Add(new SelectListItem { Text = "Vegetarian", Value = "1" });
            daysList.Add(new SelectListItem { Text = "Halal", Value = "2" });
            daysList.Add(new SelectListItem { Text = "Vegan", Value = "3" });
            daysList.Add(new SelectListItem { Text = "None", Value = "4" });
            ViewData["PreferencesList"] = preferenceList;

            // Set the selected value for the dropdown list
            if (viewModel.Days != null )
            {
                ViewData["SelectedDay"] = daysList.FirstOrDefault(x => x.Value == viewModel.Days.ToString());
            }
            else
            {
                ViewData["SelectedDay"] = daysList.LastOrDefault();
            }
            if (viewModel.DietaryRequirements != null)
            {
                ViewData["SelectedPreference"] = preferenceList.FirstOrDefault(x => x.Value == viewModel.DietaryRequirements.ToString());
            }
            else
            {
                ViewData["SelectedPreference"] = preferenceList.LastOrDefault();
            }

            // Pass the options for the menu to the view
            ViewData["wednesdayOptions"] = menu[0];
            ViewData["thursdayOptions"] = menu[1];

            return View(viewModel);

        }
        

     

        [Authorize]
        public ActionResult SavePreferences(FormSelection model)
        {

            string userfullname = System.Security.Claims.ClaimsPrincipal.Current.FindFirst("name").Value;
            string emailAddress = System.Security.Claims.ClaimsPrincipal.Current.FindFirst("preferred_username").Value;

            var db = new DbContext();
            string userQuery = $"SELECT user_id FROM Users WHERE user_email = '{emailAddress}'";
            var userResult = db.ExecuteQuery(userQuery, null);
            DataRow res1 = userResult.Rows[0];
            int userId = res1.Field<int>("user_id");
            int preferenceId = model.DietaryRequirements;
            int dayId = model.Days;

            // check if we should update or insert
            string checkQuery = $"SELECT COUNT(*) FROM Settings WHERE user_id = {userId}";
            var checkResult = db.ExecuteQuery(checkQuery, null);
            if (checkResult.Rows[0][0] != DBNull.Value && Convert.ToInt32(checkResult.Rows[0][0]) > 0)
            {
                string updateQuery = $"UPDATE Settings SET preference_id = {preferenceId}, day_id = {dayId} WHERE user_id = {userId}";
                db.ExecuteQuery(updateQuery, null);
            }
            else
            {
                string insertQuery = $"INSERT INTO Settings VALUES ({userId}, {dayId}, {preferenceId})";
                db.ExecuteQuery(insertQuery, null);
            }     
            
            //Redirect to menu
            MainUtility.SetUserPreferences(model);
            return RedirectToAction("/SubmitPreferences");
        }

        public ActionResult SubmitPreferences()
        {
            return View();
        }
    }
}
