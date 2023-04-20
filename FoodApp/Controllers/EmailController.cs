using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Security;
using System.Web.Mvc;
using FoodApp.Controllers.Utility;
using System.IO;
using System.Net.Mime;
using System.Diagnostics;
using FoodApp.EventHandlers;
using System.Reflection;

namespace FoodApp.Controllers
{
    [Authorize]
    [Route("/Email")]
    public class EmailController : Controller
    {
        public static event NewEmailAddedEventHandler NewEmailAdded;

        protected virtual void OnNewEmailAdded(NewEmailAddedEventArgs e)
        {
            NewEmailAdded?.Invoke(this, e);
        }

        // GET: Email
        [Route("/sendEmails")]
        [HttpGet]
        public ActionResult sendEmails()
        {
            List<string> emails = MainUtility.GetMailingList();
            ViewData["MailingList"] = emails;
            NewEmailAddedEventArgs args;
            foreach (string email in emails)
            {
                args = new NewEmailAddedEventArgs();
                args.Email = email;
                OnNewEmailAdded(args);
            }

            return View();
        }
    }
}