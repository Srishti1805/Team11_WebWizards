using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

// Namespace declaration for the current class
namespace ContosoCrafts.WebSite.Pages.Product
{
    // Declaration of the Razor Pages model class, inheriting from PageModel
    public class Index_SnowEquipmentModel : PageModel
    {
        // Constructor for the class, accepting a JsonFileProductService instance as a dependency
        public Index_SnowEquipmentModel(JsonFileProductService productService)
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
            Products = ProductService.GetProductsByCategory("Snow Equipments");
        }
    }
}
