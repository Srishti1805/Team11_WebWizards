using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// Manage the Delete of the data for a single record
    /// </summary>
    public class DeleteModel : PageModel
    {
        // Data middletier
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Default Construtor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="productService"></param>
        public DeleteModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        // The data to show, bind to it for the post
        [BindProperty]
        public ProductModel Product { get; set; }

        /// <summary>
        /// REST Get request
        /// Loads the Data
        /// </summary>
        /// <param name="id"></param>
        public RedirectToPageResult OnGet(string id)
        {
            Product = ProductService.GetProducts().FirstOrDefault(m => m.param.Equals(id));
            if (Product == null)
            {
                return RedirectToPage("/Error", new { errorLocation = "Delete" });
            }
            return null;
        }

        /// <summary>
        /// Call the data layer to Delete that data
        /// Then return to the index page
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {

            ProductService.DeleteData(Product.Id);

            return RedirectToPage("./Index");
        }
    }
}