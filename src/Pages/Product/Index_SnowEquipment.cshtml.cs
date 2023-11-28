using System.Collections.Generic;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

// Namespace declaration for the current class
namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// Declaration of the Razor Pages model class, inheriting from PageModel
    /// </summary>
    public class Index_SnowEquipmentModel : PageModel
    {
        // Constructor for the class, accepting a JsonFileProductService instance as a dependency
        public Index_SnowEquipmentModel(JsonFileProductService productService)
        {
            // Initializing the ProductService property with the provided instance
            ProductService = productService;
        }
        // Declaration of the data service property
        // It holds an instance of JsonFileProductService
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Declaration of the property to hold a collection of ProductModel objects
        // It is settable only within the class (private set;)
        /// </summary>
        public IEnumerable<ProductModel> Products { get; private set; }

        /// <summary>
        /// HTTP GET handler for the page.
        /// Retrieves products with the category "Snow Equipments" from the ProductService
        /// and assigns them to the Products property.
        /// </summary>
        public void OnGet()
        {
            // Call to the GetProductsByCategory method of the ProductService
            // This fetches products with the specified category
            Products = ProductService.GetProductsByCategory(ProductTypeEnum.SnowEquipments);
        }
    }
}