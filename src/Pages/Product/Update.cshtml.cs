//Microsoft.AspNetCore namespaces
using System.Linq;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

// Namespace for Product related to the Update page
namespace ContosoCrafts.WebSite.Pages.Product

{
    /// <summary>
    /// PageModel for updating product information.
    /// </summary>
    public class UpdateModel : PageModel

    {
        /// <summary>
        /// Service for interacting with JSON data.
        /// </summary>
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Constructor to initialize the page model with a product service.
        /// </summary>
        /// <param name="productService"></param>
        public UpdateModel(JsonFileProductService productService)

        {
            ProductService = productService;
        }

        //// Model binding property for the product to be updated.
        [BindProperty]
        public ProductModel Product { get; set; }

        /// <summary>
        /// Handles HTTP GET requests and retrieves the product details based on the provided ID
        /// </summary>
        /// <param name="id"></param>
        public RedirectToPageResult OnGet(string id)

        {
            // Retrieve the product with the specified ID from the product service.
            Product = ProductService.GetProducts().FirstOrDefault(m => m.param.Equals(id));
            if (Product == null)
            {
                return RedirectToPage("/Error", new { errorLocation = "Update" });
            }
            return null;
        }

        /// <summary>
        /// Handles HTTP POST requests and updates the product information
        /// </summary>
        /// <returns></returns> A redirect result to the product index page after successful update
        public IActionResult OnPost()

        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // Update the product data using the product service
            ProductService.UpdateData(Product);

            // Redirect to the product index page after successful update.
            return RedirectToPage("./Index");
        }

    }

}