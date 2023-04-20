using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Security;
using System.Web;
using System.Web.Helpers;

namespace FoodApp.Controllers.Utility
{
    public partial class MainUtility
    {
        public const string noreplyEmail = "johnnyblancnoreply@gmail.com";
        private const string subject = "Lunch Mailer";
        private const string formLink = "https://localhost:44337/Preferences/Preferences";

        public static List<string> GetMailingList()
        {
            var db = new DbContext();
            string readStatement = "SELECT user_email FROM USERS";
            var result = db.ExecuteQuery(readStatement, null).Rows;
            List<string> emails = new List<string>();
            foreach (DataRow row in result) 
            {
                emails.Add(row[0].ToString());
            }
            return emails;
        }

        public static MailMessage GetMailMessage(string email)
        {
            MailMessage mail = new MailMessage();
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

            return mail;
        }

        public static bool SendEmail(MailMessage mail)
        {

            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.Credentials = new NetworkCredential(MainUtility.noreplyEmail, MainUtility.MyAuth);
                smtp.EnableSsl = true;
               try
                {
                    smtp.Send(mail);
                    return true;
                }
                catch (Exception ex) 
                {
                    //log the error here
                    Debug.WriteLine(ex.ToString());
                    return false;
                }

            }
        }

        private static SecureString myVar = new NetworkCredential("", "phvjwlngmzyrpqml").SecurePassword;

        public static SecureString MyAuth
        {
            get { return myVar; }
        }
    }
}