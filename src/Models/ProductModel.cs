using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// Represents a model for a product.
    /// </summary>
    public class ProductModel
    {
        // Properties representing various aspects of a product

        /// <summary>
        /// Gets or sets the unique identifier of the product.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the owner of the product.
        /// </summary>
        public string Owner { get; set; }
        /// <summary>
        /// Gets or sets the phone number associated with the product.
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Gets or sets the name associated with the product.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the email address associated with the product.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the image URL of the product.
        /// </summary>
        [JsonPropertyName("img")]
        public string Image { get; set; }
        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// Gets or sets the URL of the tutorial video associated with the product.
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Gets or sets the title of the product.
        /// </summary>
        [Required]
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the review of the product.
        /// </summary>
        public string review { get; set; }
        /// <summary>
        /// Gets or sets the location information of the product.
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// Gets or sets an array of ratings associated with the product.
        /// </summary>
        public int[] Ratings { get; set; }
        /// <summary>
        /// Converts the product model to its JSON representation.
        /// </summary>
        /// <returns>A JSON string representing the product model.</returns>
        public override string ToString() => JsonSerializer.Serialize<ProductModel>(this);


    }
}