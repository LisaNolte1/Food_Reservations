using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security;
using System.Web;
using System.Web.Mvc;

namespace FoodApp.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult sendEmails(string email, string parameters, string formLink)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("johnnyblancnoreply@gmail.com");
                mail.To.Add(email);
                mail.Subject = "Hello World";
                mail.Body = "<h1>Hello</h1>";
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    SecureString theSecureString = new NetworkCredential("", "phvjwlngmzyrpqml").SecurePassword;
                    smtp.Credentials = new NetworkCredential("johnnyblancnoreply@gmail.com", theSecureString);
                    smtp.EnableSsl = true;
                    try
                    {
                        smtp.Send(mail);
                        ViewData["Title"] = "Email sent!";
                    }
                    catch (System.Net.Mail.SmtpException error)
                    {
                        ViewData["Title"] = "Email Failed to send!";
                        ViewData["error"] = error.ToString();
                    }

                }
            }
            return View();

        }
    }
}