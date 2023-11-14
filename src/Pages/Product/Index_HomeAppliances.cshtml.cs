using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ContosoCrafts.WebSite.Pages.Product
{
    public class Index_HomeAppliancesModel : PageModel
    {
        public Index_HomeAppliancesModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }
        public JsonFileProductService ProductService { get; }

        public IEnumerable<ProductModel> Products { get; private set; }
        public void OnGet()
        {
            Products = ProductService.GetLastTwoProducts();
        }
    }
}
