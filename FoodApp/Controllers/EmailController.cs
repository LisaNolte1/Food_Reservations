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

namespace FoodApp.Controllers
{
    [Authorize]
    [Route("/Email")]
    public class EmailController : Controller
    {
        // GET: Email
        [Route("/sendEmails")]
        [HttpGet]
        public ActionResult sendEmails()
        {
            List<string> emails = MainUtility.GetMailingList();
            ViewData["MailingList"] = emails;
            foreach (string email in emails)
            {
                MailMessage mail = MainUtility.GetMailMessage(email);
                using (mail)
                {
                    if(MainUtility.SendEmail(mail))
                    {
                        
                        ViewData["Title"] = "Email sent!";
                    }
                    else
                    {
                        ViewData["Title"] = "Email Failed to send!";
                        ViewData["error"] = ViewData["error"].ToString() + '\n' + email;
                    }
                }
            }
            
            return View();

        }
    }
}