using FoodReservation.Extensions;

namespace FoodReservation.Models
{
    public class FormViewModel
    {
        public List<DayViewModel>? Days { get; set; }
        public DayViewModel? SelectedDay { get; set; }
        public FormViewModel()
        {
            Days = new List<DayViewModel>();
            SelectedDay = new DayViewModel();
        }
    }

    public class DayViewModel   
    {
        public DateTime Date { get; set; }
        public string DayName { get; set; }
        public bool IsAvailable { get; set; }
        public bool Vegan { get; set; }
        public bool Vegetarian { get; set; }
        public bool GlutenFree { get; set; }
        public bool Selected {get; set;}
        public List<DietaryPreference> DietaryPreferences { get; set; }

        public DayViewModel( )
        {
            DietaryPreferences = new List<DietaryPreference>();
        }
    }

    public class DietaryPreference
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}
