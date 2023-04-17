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
