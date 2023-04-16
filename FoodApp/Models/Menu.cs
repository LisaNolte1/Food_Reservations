using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodApp.Models
{
    public class Menu
    {
        public int CuisineIdThursday { get; set; }
        public int CuisineIdWednesday { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }
    }
}