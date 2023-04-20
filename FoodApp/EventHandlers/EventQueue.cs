using FoodApp.Controllers;
using FoodApp.Controllers.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.IdentityModel.Protocols.WSTrust;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace FoodApp.EventHandlers
{
    public sealed class EventQueue
    {
        private EventQueue() {
            EventHearer();
            UsersController.NewEmailAdded += q_NewEmailAdded;
            EmailController.NewEmailAdded += q_NewEmailAdded;
        }

        private static EventQueue _instance;
        public static event NewEmailAddedEventHandler NewEmailAdded;
        private static Queue<string> emails = new Queue<string>();

        public static EventQueue GetInstance()
        {
            if (_instance == null)
            {
                _instance = new EventQueue();
            } else { return _instance; }

            return _instance;
        }

        static void q_NewEmailAdded(object sender, NewEmailAddedEventArgs e)
        {
            emails.Enqueue(e.Email);
        }

        public static async void EventHearer()
        {
            while (true)
            {
                if(emails.Count != 0)
                {
                   var em = emails.Dequeue();
                   MailMessage mailMessage = MainUtility.GetMailMessage(em);
                    MainUtility.SendEmail(mailMessage);
                }
                await Task.Delay(1000);
            }
        } 


    }
}