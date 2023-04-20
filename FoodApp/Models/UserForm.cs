using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodApp.Models
{
    public class UserForm
    {
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string RoleName { get; set; }
        public int RoleId { get; set; }
    }
}