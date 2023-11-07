using ContosoCrafts.WebSite.Models;
using NUnit.Framework;

namespace UnitTests.Model.Tests
{
    /// <summary>
    /// This class contains unit tests for the ProductModel
    /// </summary>
    public class ProductModelTests
    {
        private const string MockId = "1";

        private const string MockTitle = "Gridder";

        private const string MockOwner = "Srishti";

        private const string MockDescription = "The Gridder is a gardening tool that rolls out perfectly spaced grids for planting.";

        private const string MockUrl = "https://www.youtube.com/watch?v=SxygEWYwAB8";

        private const string MockImage = "https://i.pinimg.com/564x/b2/4a/4e/b24a4e95612802454143fa54fd999741.jpg";

        private const string MockLocation = "Seattle, Redmond, Belleve";

        /// <summary>
        /// Tests the get set functionality of Product Model
        /// </summary>
        [Test]
        public void ToString_ReturnsJson()
        {
            // Arrange
            var product = new ProductModel
            {
                Id = MockId,
                Title = MockTitle,
                Owner = MockOwner,
                Url = MockUrl,
                Image = MockImage,
                Location = MockLocation
            };

            // Act
            var jsonString = product.ToString();

            // Assert
            Assert.IsNotEmpty(jsonString);
            Assert.IsTrue(jsonString.Contains(product.Id));
            Assert.IsTrue(jsonString.Contains(product.Title));
            Assert.IsTrue(jsonString.Contains(product.Owner));
            Assert.IsTrue(jsonString.Contains(product.Url));
            Assert.IsTrue(jsonString.Contains(product.Image));
            Assert.IsTrue(jsonString.Contains(product.Location));

        }
    }
}