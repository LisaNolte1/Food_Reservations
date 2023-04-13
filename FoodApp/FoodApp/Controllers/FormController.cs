using System;
using System.Web.Mvc;
using Google.Apis.Services;
using Google.Apis.Forms.v1;
using Google.Apis.Forms.v1.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;



using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace FoodApp.Controllers
{
    [Route("/Form")]
    public class FormController : Controller
    {

        // GET: Create
        [Route("/createForm")]
        [HttpGet]
        public ActionResult createForm()
        {
            string clientId = "467113264566-epc6c0nmntn33abcr2a067l9nhgkpces.apps.googleusercontent.com";
            string clientSecret = "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";



            // Authenticate your app with the Google Forms API.
            UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
             new ClientSecrets { ClientId = clientId, ClientSecret = clientSecret },
             new[] { FormsService.Scope.FormsBody },
             "user",
             System.Threading.CancellationToken.None).Result;



            // Create a FormsService instance to make API calls.
            FormsService service = new FormsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "FoodApp"
            });


            try
            {
                service.Forms.Create(new Form());
                ViewData["title"] = "form created";
            }
            catch(Exception ex)
            {
                ViewData["title"] = ex.ToString();
            }



            return View();
        }
    }
}