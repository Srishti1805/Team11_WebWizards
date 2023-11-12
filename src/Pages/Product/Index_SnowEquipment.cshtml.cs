using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoCrafts.WebSite.Pages.Product
{
	public class Index_SnowEquipmentModel : PageModel
    {
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
            Products = ProductService.GetFirstTwoProducts();
        }
    }
}
