using System.Text.Json;
using System.Text.Json.Serialization;

namespace ContosoCrafts.WebSite.Models
{
    public class ProductModel
    {
        public string Id { get; set; }
        public string Owner { get; set; }
        public string Phone { get; set; }
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