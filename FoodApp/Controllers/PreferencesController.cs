using FoodApp.Models;
using System;
using System.Collections.Generic;
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
           List<string> food = new List<string>(); //go to where and get the current menu
            food.Add("Curry");
            food.Add("Salad");
            food.Add("Pizza");
            ViewData["Food"] = food;
            return View();
        }

        [Authorize]
        public ActionResult SavePreferences(FormSelection model)
        {
            string userfullname = System.Security.Claims.ClaimsPrincipal.Current.FindFirst("name").Value;
            string emailAddress = System.Security.Claims.ClaimsPrincipal.Current.FindFirst("preferred_username").Value;
            Debug.WriteLine(userfullname);
            Debug.WriteLine(emailAddress);
            if (model != null)
            {
                //Console.Beep();
                //Save data to database
                if(model.Food != null)
                {
                    //Console.Beep();
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
