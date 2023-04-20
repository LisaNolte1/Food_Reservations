using FoodApp.Controllers.Utility;
using FoodApp.EventHandlers;
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
    public class UsersController : Controller
    {
        public static event NewEmailAddedEventHandler NewEmailAdded;

        private string didError = null;
        protected virtual void OnNewEmailAdded(NewEmailAddedEventArgs e)
        {
            NewEmailAdded?.Invoke(this, e);
        }

        public ViewResult Index()
        {
            //Query to get the roles
            if (didError != null)
            {
                ViewData["error"] = didError;
                didError = null;
            }
            return View();
        }

        public ActionResult saveUser(UserForm model)
        {
            try
            {
                var db = new DbContext();
                string userQuery = $"INSERT INTO USERS (user_email, role_id) VALUES ('{model.UserEmail}', 1);";
                var userResult = db.ExecuteQuery(userQuery, null);
                NewEmailAddedEventArgs args = new NewEmailAddedEventArgs();
                args.Email = model.UserEmail;
                OnNewEmailAdded(args);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewData["error"] = ex.ToString();
                didError = ex.ToString();
                return RedirectToAction("Index", "Home");

            }

            
        }
       
        public bool getAdmin()
        {
            //this will need to be dynamic (query that finds and return admin's email)
            return MainUtility.GetAdmin();
        }
    }
}