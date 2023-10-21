using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// Srishti Adkar
    /// </summary>

    ///<summary>
    ///Yashashree Deshpande
    ///</summary>

    ///<summary>
    ///Rupa Dinesh
    ///</summary>

    ///<summary>
    ///John Vikas Kotaru
    ///</summary>


    /// <summary>
    /// The IndexModel represents the Razor Pages model for the index page.
    /// It handles the display of products on the homepage.
    /// </summary>
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        /// Constructor for IndexModel
        public IndexModel(ILogger<IndexModel> logger,
            JsonFileProductService productService)
        {
            _logger = logger;
            ProductService = productService;
        }
 
        // Gets the product service used to fetch product data.

        public JsonFileProductService ProductService { get; }

        /// Gets or sets the list of products to be displayed on the index page.
        public IEnumerable<ProductModel> Products { get; private set; }

        // Handles the HTTP GET request for the index page.
        // Retrieves product data using the ProductService and sets the Products property.
        public void OnGet()
        {
            Products = ProductService.GetProducts();
        }
    }
}