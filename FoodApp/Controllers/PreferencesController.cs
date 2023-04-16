using FoodApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodApp.Controllers
{
    public class PreferencesController : Controller
    {

        public ActionResult Preferences()
        {
            var db = new DbContext();
            string readStatement = $"SELECT * FROM EVENTS WHERE active = 1";
            Debug.WriteLine(readStatement);
            var result = db.ExecuteQuery(readStatement, null).Rows;
            List<KeyValuePair<int,string>> wedOptions = new List<KeyValuePair<int,string>>();
            List<KeyValuePair<int,string>> thursOptions = new List<KeyValuePair<int,string>>();
            foreach (DataRow row in result)
            {
                Debug.WriteLine(row[1]);
                readStatement = $"SELECT * FROM CUISINES_OPTIONS WHERE cuisine_id = {row[1]}";
                var cResults = result = db.ExecuteQuery(readStatement, null).Rows;
                foreach(DataRow c in cResults)
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
            ViewData["thursdayOptions"] = thursOptions;
            ViewData["wednesdayOptions"] = wedOptions;
            return View();
        }

        [Authorize]
        public ActionResult SavePreferences(FormSelection model)
        {

            string userfullname = System.Security.Claims.ClaimsPrincipal.Current.FindFirst("name").Value;
            string emailAddress = System.Security.Claims.ClaimsPrincipal.Current.FindFirst("preferred_username").Value;
            Debug.WriteLine(userfullname);
            Debug.WriteLine(emailAddress);

            var db = new DbContext();
            string userQuery = $"SELECT user_id FROM Users WHERE user_email = '{emailAddress}'";
            var userResult = db.ExecuteQuery(userQuery, null);
            DataRow res1 = userResult.Rows[0];
            int userId = res1.Field<int>("user_id");
     
            string prefQuery = $"SELECT preference_id FROM Preferences WHERE preference_type = '{model.DietaryRequirements.ToString()}'";
            var prefResult = db.ExecuteQuery(prefQuery, null);
            DataRow res2 = prefResult.Rows[0];
            int preferenceId = res2.Field<int>("preference_id");

            string daysQuery = $"SELECT day_id FROM Days WHERE day_name = '{model.Days.ToString()}'";
            var daysResult = db.ExecuteQuery(daysQuery, null);
            DataRow res3 = daysResult.Rows[0];
            int dayId = res3.Field<int>("day_id");

            // check if we should update or insert
            string checkQuery = $"SELECT COUNT(*) FROM Settings WHERE user_id = {userId}";
            var checkResult = db.ExecuteQuery(checkQuery, null);
            if (checkResult.Rows[0][0] != DBNull.Value && Convert.ToInt32(checkResult.Rows[0][0]) > 0)
            {
                string updateQuery = $"UPDATE Settings SET preference_id = {preferenceId}, day_id = {dayId} WHERE user_id = {userId}";
                var updateResult = db.ExecuteQuery(updateQuery, null);
            }
            else
            {
                string insertQuery = $"INSERT INTO Settings VALUES ({userId}, {preferenceId}, {dayId})";
                var insertResult = db.ExecuteQuery(insertQuery, null);
            }     
            
            //Redirect to menu
            return RedirectToAction("/SubmitPreferences");
        }

        public ActionResult SubmitPreferences()
        {
            return View();
        }
    }
}
