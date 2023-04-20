namespace FoodApp.Models
{
    public class AdminStatistics 
    {
        public string[] barChartLabels { get; set; }
        public int[] barChartData { get; set; }

        public string[] pieChartLabels { get; set; }
        public int[] pieChartData { get; set; }

        public string[] lineChartLabels { get; set; }
        public int[] lineChartData { get; set; }
    }
}