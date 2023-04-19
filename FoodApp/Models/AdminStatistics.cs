using System;

namespace FoodApp.Models
{
    public class AdminStatistics
    {
        public int[] eventIds { get; set; }
        public DateTime[] eventDates { get; set; }
        public string[] userEmails { get; set; }
        public string[] cuisineNames { get; set; }
        public string[] cuisineOptions { get; set; }
        public string[] dietaryPreferences { get; set; }

        public string[] barChartLabels { get; set; }
        public int[] barChartData { get; set; }

        public string[] pieChartLabels { get; set; }
        public int[] pieChartData { get; set; }

        public string[] lineChartLabels { get; set; }
        public int[] lineChartData { get; set; }
    }
}