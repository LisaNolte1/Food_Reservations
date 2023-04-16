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

        private const string noreplyEmail = "johnnyblancnoreply@gmail.com";
        private const string subject = "Lunch Mailer";
        private const string formLink = "https://localhost:44337/Preferences/Preferences";
        // GET: Email
        

        [Route("/sendEmails")]
        [HttpGet]
        public ActionResult sendEmails()
        {
            List<string> emails = MainUtility.GetMailingList();
            ViewData["MailingList"] = emails;
            foreach (string email in emails)
            {
                Debug.WriteLine("===================");
                Debug.WriteLine(email);
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(noreplyEmail);
                    mail.To.Add(email);
                    mail.Subject = subject;
                    mail.IsBodyHtml = true;
                    string attachmentName = @"Resources\email.jpeg"; // note lowercase extension
                    string attachmentPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, attachmentName);
                    Attachment picture = new Attachment(attachmentPath, MediaTypeNames.Image.Jpeg);
                    string contentID = "test001@host";
                    picture.ContentId = contentID;
                    mail.Attachments.Add(picture);
                    mail.Body = $"<html>" +
                                $"<body>" +
                                $"<a href=\"{formLink}\">" +
                                $"<img src=\"cid:{contentID}\">" +
                                "</a>" +
                                "</body>" +
                                "</html>";
                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        SecureString theSecureString = Authinator.MyAuth;
                        smtp.Credentials = new NetworkCredential(noreplyEmail, theSecureString);
                        smtp.EnableSsl = true;
                        try
                        {
                            smtp.Send(mail);
                            ViewData["Title"] = "Email sent!";
                        }
                        catch (Exception error)
                        {
                            ViewData["Title"] = "Email Failed to send!";
                            ViewData["error"] = ViewData["error"].ToString() + '\n' + email;
                        }

                    }
                }
            }
            
            return View();

        }
    }
}