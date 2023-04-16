using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodApp.Models;

namespace FoodApp.Controllers
{
    public class AdminController : Controller
    {

        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult CreateEvent()
        {
            var db = new DbContext();
            string readStatement = $"SELECT * FROM CUISINES";
            Debug.WriteLine(readStatement);
            var results = db.ExecuteQuery(readStatement, null).Rows;
            List<KeyValuePair<int,string>> Cuisines = new List<KeyValuePair<int,string>>();
            foreach (DataRow result in results)
            {
                Debug.WriteLine(result[0]);
                Debug.WriteLine(result[1]);
                Cuisines.Add(new KeyValuePair<int, string>((int)result[0], result[1].ToString()));
            }
            ViewData["Cuisines"] = Cuisines;

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
            ViewData["Days"] = Days;
            return View();
        }

        public ActionResult SaveEvent(Menu model)
        {
            Debug.WriteLine(model.Date);
            Debug.WriteLine(model.CuisineIdThursday);
            Debug.WriteLine(model.CuisineIdWednesday);
            var resp = SaveInternal(model);
            if (!resp)
            {
                ViewData["title"] = "Fail";
            }
            return View();
        }

        private bool SaveInternal(Menu menu)
        {
            try 
            {
                var db = new DbContext();
                db.ExecuteQuery($"INSERT INTO EVENTS (cuisine_id,day_id,date) VALUES ({menu.CuisineIdWednesday}, {1}, {menu.Date});", null);
                db.ExecuteQuery($"INSERT INTO EVENTS (cuisine_id,day_id,date) VALUES ({menu.CuisineIdThursday}, {2}, {menu.Date});", null);
                return true;
            }
            catch(Exception ex)
            {
                ViewData["error"] = ex.Message;
                return false;
            }
        }
    }
}
