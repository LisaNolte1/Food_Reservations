using FoodApp.Controllers.Utility;
using System.Web.Mvc;

namespace FoodApp.Controllers
{
    [Authorize]
    [Route("/Form")]
    public class FormController : Controller
    {

        // GET: Create
        [Route("/createForm")]
        [HttpGet]
        public ActionResult createForm()
        {
            ViewData["title"] = "form not created, because we haven't implemented that yet...";
            MainUtility.GetUserPreferences("lisan@bbd.co.za");
            return View();
        }
    }
}