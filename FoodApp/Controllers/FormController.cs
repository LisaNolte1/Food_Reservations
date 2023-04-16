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
            return View();
        }
    }
}