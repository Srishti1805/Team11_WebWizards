using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

using ContosoCrafts.WebSite.Models;

using Microsoft.AspNetCore.Hosting;

namespace ContosoCrafts.WebSite.Services
{
   /// <summary>
   /// Data Middle tier service handling JSON operations
   /// </summary>
   public class JsonFileProductService
    {
        /// <summary>
        /// Contructor for initializing the service with the web host environment.
        /// </summary>
        /// <param name="webHostEnvironment"></param>
        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        // Property representing the web host environment
        public IWebHostEnvironment WebHostEnvironment { get; }

        // Private property representing the full path to the JSON file
        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json"); }
        }

        /// <summary>
        /// Retrieves a collection of products from the JSON file.
        // Returns: IEnumerable<ProductModel> - Collection of products.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductModel> GetProducts()
        {
            using(var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<ProductModel[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        /// <summary>
        /// Adds a rating to the specified product
        /// productId (string): The unique identifier of the product.
        /// rating (int): The rating to be added to the product.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="rating"></param>
        public void AddRating(string productId, int rating)
        { 
            var products = GetProducts();

            // If the product's ratings array is null, create a new array with the given rating.
            if (products.First(x => x.Id == productId).Ratings == null)
            {
                products.First(x => x.Id == productId).Ratings = new int[] { rating };
            }
            else
            {
                // If ratings array exists, add the new rating to the existing ratings.
                var ratings = products.First(x => x.Id == productId).Ratings.ToList();
                ratings.Add(rating);
                products.First(x => x.Id == productId).Ratings = ratings.ToArray();
            }

            // // Serialize the updated products collection and write it back to the JSON file.
            using (var outputStream = File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<ProductModel>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }), 
                    products
                );
            }
        }
    }
}