using System;
using System.Collections.Generic;
using System.Net;
using System.Security;
using System.Web;

namespace FoodApp.Controllers.Utility
{
    public partial class Authinator
    {

		private static SecureString myVar = new NetworkCredential("", "phvjwlngmzyrpqml").SecurePassword;

        public static SecureString MyAuth
        {
            get { return myVar; }
        }

	}
}