using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FoodReservation.Models;

namespace FoodReservation.Pages
{
    public class ReservationFormModel : PageModel
    {
        public List<DayViewModel> days = new List<DayViewModel>();
        public List<DayViewModel> Days { get; set; }

        public ReservationFormModel()
        {
            Days = new List<DayViewModel>();
            days = new List<DayViewModel>();

            for (int i = 0; i < 7; i++)
            {
                DateTime date = DateTime.Today.AddDays(i);

                DayViewModel dayViewModel = new DayViewModel()
                {
                    DayName = date.ToString("dddd"),
                    Date = date,
                    IsAvailable = true
                };

                days.Add(dayViewModel);
            }

            // Assign the created list to Days property

            FormViewModel ab = new FormViewModel();
            this.Days = days;
        }

        


        [BindProperty]
        public DayViewModel SelectedDay { get; set; }

        [BindProperty]
        public bool Vegan { get; set; }

        [BindProperty]
        public bool Vegetarian { get; set; }

        [BindProperty]
        public bool GlutenFree { get; set; }

        public void OnGet()
        {
             Days = new List<DayViewModel>()
            {
                new DayViewModel() { DayName = "Monday" },
                new DayViewModel() { DayName = "Tuesday" },
                new DayViewModel() { DayName = "Wednesday" },
                new DayViewModel() { DayName = "Thursday" },
                new DayViewModel() { DayName = "Friday" },
                new DayViewModel() { DayName = "Saturday" },
                new DayViewModel() { DayName = "Sunday" }
            };
        }

        public IActionResult OnPost()
        {
            // Handle the form submission here.
            // You can access the selected day and dietary preferences through the corresponding properties.
            // For example, if the selected day is stored in the SelectedDay.DayName property, you can retrieve it like this:
            string selectedDayName = SelectedDay.DayName;

            // Then you can perform any additional processing or database operations as needed.

            // Finally, you can return a result, such as a redirect to another page.
           
            // return RedirectToPage("Confirmation");
            return Page();
        }
    }
}
