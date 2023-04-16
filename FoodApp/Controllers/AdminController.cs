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

        public ActionResult SaveEvent(Models.Menu model)
        {
            Debug.WriteLine(model.Date);
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
                var db = new DbContext();
                var parameters = getSQLParameters(menu);
                db.ExecuteNonQuery("UPDATE EVENTS SET active = 0", null);

                db.ExecuteNonQuery("INSERT INTO EVENTS (cuisine_id,day_id,event_date,active) VALUES (@CuisineIdWednesday, @DayIdWednesday, CONVERT (date, @EventDate, 101),1)", parameters[0]);
                db.ExecuteNonQuery("INSERT INTO EVENTS (cuisine_id,day_id,event_date,active) VALUES (@CuisineIdThursday, @DayIdThursday, CONVERT (date, @EventDate, 101),1)", parameters[1]);
                ViewData["success"] = true;
                return true;
            }
            catch(Exception ex)
            {
                ViewData["error"] = ex;
                return false;
            }
        }

        private List<SqlParameter[]> getSQLParameters(Models.Menu menu)
        {
            List<SqlParameter[]> parameters = new List<SqlParameter[]>();
            var parameters0 = new[]
               {
                new SqlParameter("@CuisineIdWednesday", menu.CuisineIdWednesday),
                new SqlParameter("@DayIdWednesday", 1),
                new SqlParameter("@EventDate", menu.Date)
                };
            parameters.Add(parameters0);
            var parameters1 = new[]
            {
                new SqlParameter("@CuisineIdThursday", menu.CuisineIdThursday),
                new SqlParameter("@DayIdThursday", 2),
                new SqlParameter("@EventDate", menu.Date)
                };
            parameters.Add(parameters1);

            return parameters;
        }
    }
}
