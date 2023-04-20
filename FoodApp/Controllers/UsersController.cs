using FoodApp.Controllers.Utility;
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
        public ViewResult Index()
        {
            //Query to get the roles

            return View();
        }

        public void saveUser(UserForm model)
        {
            Debug.WriteLine(model.UserEmail);

            var db = new DbContext();
            string userQuery = $"INSERT INTO USERS (user_email, role_id) VALUES ('{model.UserEmail}', 1);";
            var userResult = db.ExecuteQuery(userQuery, null);
            Debug.WriteLine(userResult);
        }
       
        public bool getAdmin()
        {
            //this will need to be dynamic (query that finds and return admin's email)
            return MainUtility.GetAdmin();
        }
    }
}