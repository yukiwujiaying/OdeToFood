using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OdeToFood.Data;
using OdetoFoof.Core;

namespace OdeToFood.Pages.Shared.Restaurants
{
    public class ListModel : PageModel
    {
        private readonly IConfiguration config;

        private readonly IRestaurantData restaurantData;

        public string Message { get; set; }
        public IEnumerable<Restaurant> Restaurants { get; set; }

        //function can be an output and input model
        //bindproperty tell the ASP.Net frame work when
        // you are instantiating this class 
        //and you are getting ready to execute a method on this class to process an HTTP request
        //this property should recieve information from the request.
        //This only support POST method we need to add (SupportsGet=true) to make it works on GET
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        
        public ListModel(IConfiguration config, IRestaurantData restaurantData)
        {
            this.config = config;
            this.restaurantData = restaurantData;
        }
        public void OnGet()
        {        
            Message=config["Message"];
            Restaurants = restaurantData.GetRestaurantsByName(SearchTerm);
        }
    }
}
