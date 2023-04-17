//using FoodApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodApp.Controllers.Utility
{
    [Authorize]
    public partial class MainUtility
    {


        //GET DB CONNECTION HERE
        private void GetCurrentUser()
        {
            
        }

        public static bool GetAdmin()
        {
            var db = new DbContext();
            string readStatement = $"SELECT role_id FROM USERS WHERE user_email = '{System.Security.Claims.ClaimsPrincipal.Current.FindFirst("preferred_username").Value}'";
            var result = db.ExecuteQuery(readStatement, null).Rows;
            List<string> emails = new List<string>();
            foreach (DataRow row in result)
            {
                return (int)row[0] == 2;
                
            }
            return false;
        }
    }
}
