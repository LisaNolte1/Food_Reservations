using FoodApp.Controllers.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodApp.Controllers
{
    public class UsersController : Controller
    {
       
        public bool getAdmin()
        {
            //this will need to be dynamic (query that finds and return admin's email)
            return MainUtility.GetAdmin();
        }
    }
}