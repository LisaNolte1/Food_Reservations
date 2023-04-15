using FoodApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodApp.Controllers
{
    public class PreferencesController : Controller
    {

        public ActionResult Preferences()
        {
           List<string> food = new List<string>(); //go to where and get the current menu
            food.Add("Curry");
            food.Add("Salad");
            food.Add("Pizza");
            ViewData["Food"] = food;
            return View();
        }

        public ActionResult SavePreferences(Preferences model)
        {
            //Fetch currently logged in user and save this model against their email
            if(model != null)
            {
                Console.Beep();
                //Save data to database
                if(model.Food != null)
                {
                    Console.Beep();
                }
            }
            ////////////////////////////////////////////////////////
            

            //Redirect to menu
            return RedirectToAction("/SubmitPreferences");
        }

        public ActionResult SubmitPreferences()
        {
            return View();
        }
    }
}
