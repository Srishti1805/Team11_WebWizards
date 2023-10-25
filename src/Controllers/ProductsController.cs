using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Controllers
{
    /// <summary>
    /// The ProductsController class contains the mechanism for managing the information about the product
    /// It is a subclass of ControllerBase
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        /// <summary>
        /// Contructor for initializing the services for handling the product
        /// </summary>
        /// <param name="productService"></param>
        public ProductsController(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        // Property representing the service handler for JSON operations
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Retrieves a collection of products from the JSON file.
        /// Returns: IEnumerable<ProductModel> - Collection of products.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<ProductModel> Get()
        {
            return ProductService.GetProducts();
        }

        /// <summary>
        /// Produces the action status of a request to add rating to a product
        /// request (RatingRequest) - ratingrequest object containing information of the product id and its rating 
        /// Returns: ActionResult - status code for HTTP ok.
        /// </summary>
        ///<param name="request"></param>
        /// <returns></returns>
        [HttpPatch]
        public ActionResult Patch([FromBody] RatingRequest request)
        {
            ProductService.AddRating(request.ProductId, request.Rating);
            
            return Ok();
        }

        /// <summary>
        /// The RatingRequest class contains the information about the product id and its rating
        /// </summary>
        public class RatingRequest
        {
            // property representing the product id of the product
            public string ProductId { get; set; }
            // property representing the rating of the product
            public int Rating { get; set; }
        }
    }
}