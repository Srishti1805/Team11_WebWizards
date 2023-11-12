using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

 
namespace ContosoCrafts.WebSite.Pages.Product
{
    public class Index_gardeningModel : PageModel
    {
        public Index_gardeningModel(JsonFileProductService productService)
        {

            ProductService = productService;

        }

        // Data Service
        public JsonFileProductService ProductService { get; }
        // Collection of the Data

        public IEnumerable<ProductModel> Products { get; private set; }

        /// <summary>
        /// REST OnGet, return all data
        /// </summary>

        public void OnGet()
        {
            Products = ProductService.GetFirstTwoProducts();
        }

    }

}


