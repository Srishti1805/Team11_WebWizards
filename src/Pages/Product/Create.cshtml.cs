using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// Create Page
    /// </summary>
    public class CreateModel : PageModel
    {
        // Data middle tier
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Default Construtor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="productService"></param>
        public CreateModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        //// Model binding property for the product to be updated.
        [BindProperty]
        public ProductModel Product { get; set; }

        /// <summary>
        /// REST Get request
        /// </summary>
        /// <param name="id"></param>
        public void OnGet()
        {
            Product = ProductService.CreateProduct();
            // Redirect the webpage to the Update page populated with the data so the user can fill in the fields
            //return RedirectToPage("./Update", new { Id = Product.Id });
        }

        public IActionResult OnPost()
        {
            ProductService.CreateData(Product);

            // Redirect to the product index page after successful update.
            return RedirectToPage("./Index");
        }
    }
}