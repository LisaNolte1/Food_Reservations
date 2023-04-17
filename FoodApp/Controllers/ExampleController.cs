using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//TODO: Delete this file. Only for demostration purposes on how to use the DbContext
namespace FoodApp.Controllers
{
    public class ExampleController : Controller
    {

        public ActionResult Index()
        {
            var db = new DbContext();
            //Read from DB 
            string readStatement = "SELECT * FROM PREFERENCES";
            var result = db.ExecuteQuery(readStatement, null);
            Console.WriteLine(result);

            //Insert/Update/Delete from DB
            var insertResult = db.ExecuteNonQuery("INSERT INTO PREFERENCES (preference_id,preference_type) VALUES (200, test);", null);

            //Insert/Update/Delete with parameters from DB
            var parameters = new[]
            {
                new SqlParameter("@Id", 700),
                new SqlParameter("@Type", "Test Preference Type"),
            };
            var res = db.ExecuteNonQuery("INSERT INTO PREFERENCES (preference_id,preference_type) VALUES (@Id, @Type);", parameters);
            
            return View();
        }
    }
}