using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Data;
using OdetoFoof.Core;

namespace OdeToFood.Pages.Shared.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData restaurantData;
        private readonly IHtmlHelper htmlHelper;

        [BindProperty]
        public Restaurant Restaurant { get; set; }

        public IEnumerable<SelectListItem> Cuisines{get; set;}
        public EditModel(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            this.htmlHelper = htmlHelper;
        }
        public IActionResult OnGet(int? restaurantId)
        {
            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();

            if (restaurantId.HasValue)
            {
                Restaurant = restaurantData.GetByID(restaurantId.Value);
            }
            else
            {
                Restaurant = new Restaurant();
            }
       
            if (Restaurant == null)
            {
                return RedirectToPage("./ NotFound");
            }

            return Page();

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
                return Page();
                
            }
            if(Restaurant.Id > 0)
            {
                restaurantData.Update(Restaurant);
            }
            // any restaurand with id<=0 mwan is a new restaurant need to add it to the datasource
            else
            {
                restaurantData.Add(Restaurant);
            }
            restaurantData.Commit();

            TempData["Message"] = "Restaurant Saved!";

            ////second parameter is object contain route value
            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });



        }
    }
}
