using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis;

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
        /// <param name = "webHostEnvironment"></param>
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
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<ProductModel[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        /// <summary>
        /// Retrieves a collection of products from the JSON file based on the category passed as argument
        /// </summary>
        /// <param name="Category"></param>
        /// <returns></returns>
        public IEnumerable<ProductModel> GetProductsByCategory(string Category)
        {
            // Open the JSON file for reading
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                // Deserialize the JSON content into an array of ProductModel objects
                var products = JsonSerializer.Deserialize<ProductModel[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        // Ensure case-insensitive property name matching during deserialization
                        PropertyNameCaseInsensitive = true
                    });
                // Filter products based on the provided category (case-insensitive comparison)
                // Note: If needed, consider trimming category strings to handle potential whitespace issues
                //return products.Reverse().Take(2);
                return products.Where(product => product.Category.Equals(Category, StringComparison.OrdinalIgnoreCase));
            }
        }

        /// <summary>
        /// Returns all data stored in json file 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductModel> GetAllData()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
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
        /// <param name = "productId"></param>
        /// <param name = "rating"></param>
        public bool AddRating(string productId, int rating)
        {
            var products = GetProducts();

            // if the productId is invalid, return false
            if (string.IsNullOrEmpty(productId))
            {
                return false;
            }
            // Read the product details by calling GetProducts() method
            var product = GetProducts();

            // Check if the product exist, if not return false
            var data = product.FirstOrDefault(x => x.Id.Equals(productId));

            if (data == null)
            {
                return false;
            }
            // Check Rating for boundries, if rating less than 0 return false
            if (rating < 0)
            {
                return false;
            }
            // Check Rating for boundries, if rating greater than 5 return false
            if (rating > 5)
            {
                return false;
            }
            // Check to see if the rating exist, if there are none, then create the array
            if (data.Ratings == null)
            {
                data.Ratings = new int[] { };
            }
            // Save the ratings to a rating array
            var ratings = data.Ratings.ToList();

            ratings.Add(rating);

            data.Ratings = ratings.ToArray();

            // Save the updated data in the product
            SaveModifiedData(product);
            return true;
        }

        /// <summary>
        /// Update the fields in the product
        /// Save the modified data
        /// </summary>
        /// <param name = "data"></param>
        /// <returns></returns>
        public ProductModel UpdateData(ProductModel data)
        {
            // Read the data for selected product
            var product = GetProducts();

            var productmodeldata = product.FirstOrDefault(x => x.Id.Equals(data.Id));
            //if (productmodeldata == null)
            //{
            //    return null;
            //}

            // Update the selected data into the same fields
            productmodeldata.Id = data.Id;
            productmodeldata.Owner = data.Owner;
            productmodeldata.Phone = data.Phone;
            productmodeldata.Name = data.Name;
            productmodeldata.Email = data.Email;
            productmodeldata.Title = data.Title;
            productmodeldata.Description = data.Description;
            productmodeldata.Category = data.Category;
            productmodeldata.Price = data.Price;
            productmodeldata.AvailableDays = data.AvailableDays;
            productmodeldata.Url = data.Url;
            productmodeldata.Image = data.Image;
            productmodeldata.Location = data.Location;
            
            // Save the updated data in the product
            SaveModifiedData(product);
            //Return the updated product data
            return productmodeldata; 
        }

        /// <summary>
        /// Save the products data to JSON
        /// </summary>
        /// <param name = "products">The collection of products to be saved.</param>
        public void SaveModifiedData(IEnumerable<ProductModel> products)
        {
            // Serialize the updated products collection and write it back to the JSON file
            using (var outputStream = File.Create(JsonFileName))
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

        /// <summary>
        /// Remove the item from the system
        /// </summary>
        /// <returns></returns>
        public ProductModel DeleteData(string id)
        {
            // Get the current set, and append the new record to it
            var dataSet = GetProducts();
            var data = dataSet.FirstOrDefault(m => m.Id.Equals(id));

            var newDataSet = GetProducts().Where(m => m.Id.Equals(id) == false);

            SaveModifiedData(newDataSet);

            return data;
        }
        /// <summary>
        /// Create data to add to the system
        /// </summary>
        /// <returns></returns>
        public ProductModel CreateData(ProductModel data)
        {
            var product = new ProductModel();
            product.Id = data.Id;
            product.Owner = data.Owner;
            product.Phone = data.Phone;
            product.Name = data.Name;
            product.Email = data.Email;
            product.Title = data.Title;
            product.Description = data.Description;
            product.Category = data.Category;
            product.Price = data.Price;
            product.AvailableDays = data.AvailableDays;
            product.Url = data.Url;
            product.Image = data.Image;
            product.Location = data.Location;

            // Get the current set, and append the new record to it becuase IEnumerable does not have Add
            var dataSet = GetAllData();
            dataSet = dataSet.Append(product);

            SaveModifiedData(dataSet);

            return data;
        }

        /// <summary>
        /// Create temp product model object
        /// </summary>
        /// <returns></returns>
        public ProductModel CreateProduct()
        {
            string s = GetAllData().Last().Id;
            int x = Int32.Parse(s);
            var data = new ProductModel()
            {
                Id = (x + 1).ToString(), //System.Guid.NewGuid().ToString(),
                Owner = "Enter Owner Name",
                Phone = "Enter you mobile number",
                Name = null,
                Email = "Enter Email",
                Title = "Enter Title",
                Description = "Enter Description",
                Category = "Enter the product category",
                Price = 0.0f,
                Url = "Enter URL",
                Image = "Enter image url",
                Location = " Enter Location",
                AvailableDays = 0,
                Ratings = new int[] { 0 }
            };
            return data;
        }


    }
}