using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Data;
using OdetoFoof.Core;

namespace OdeToFood.Pages.Shared.Restaurants
{
    public class DetailModel : PageModel
    {
        private readonly IRestaurantData restaurantData;
        public Restaurant Restaurant { get; set; }
        public DetailModel(IRestaurantData restaurantdata)
        {
            this.restaurantData = restaurantdata;
        }
        public IActionResult OnGet(int restaurantId)
        {
            Restaurant = restaurantData.GetByID(restaurantId);
            if(Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }
    }
}
