using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodApp.Controllers.Utility
{
    public partial class MainUtility
    {
        //public ActionResult Index()
        //{
        //    var db = new DbContext();
        //    //Read from DB 
        //    string readStatement = "SELECT * FROM PREFERENCES";
        //    var result = db.ExecuteQuery(readStatement, null);
        //    Console.WriteLine(result);

        //    //Insert/Update/Delete from DB
        //    var insertResult = db.ExecuteNonQuery("INSERT INTO PREFERENCES (preference_id,preference_type) VALUES (200, test);", null);

        //    //Insert/Update/Delete with parameters from DB
        //    var parameters = new[]
        //    {
        //        new SqlParameter("@Id", 700),
        //        new SqlParameter("@Type", "Test Preference Type"),
        //    };
        //    var res = db.ExecuteNonQuery("INSERT INTO PREFERENCES (preference_id,preference_type) VALUES (@Id, @Type);", parameters);

        //    return View();
        //}

        public static void GetMailingList()
        {
            var db = new DbContext();
            string readStatement = "SELECT user_email FROM USERS";
            var result = db.ExecuteQuery(readStatement, null);
            Console.WriteLine(result);
        }
    }
}