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

        [JsonPropertyName("img")]
        public string Image { get; set; }
        public string Price { get; set; }

        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string review { get; set; }
        public string Location { get; set; }
        public int[] Ratings { get; set; }

        public override string ToString() => JsonSerializer.Serialize<ProductModel>(this);


    }
}