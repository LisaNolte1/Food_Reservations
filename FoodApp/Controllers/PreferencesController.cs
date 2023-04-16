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
            Utility.MainUtility.SetUserPreferences(model);
            return RedirectToAction("/SubmitPreferences");
        }

        public ActionResult SubmitPreferences()
        {
            return View();
        }
    }
}
