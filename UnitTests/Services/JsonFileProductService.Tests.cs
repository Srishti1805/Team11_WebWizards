using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

using Moq;

using NUnit.Framework;

using ContosoCrafts.WebSite.Pages.Product;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Models;
using System.Linq;

namespace UnitTests.Services.JsonFileProductServiceTest
{
    /// <summary>
    /// This class contains unit tests for JsonFileProductService
    /// </summary>
    public class JsonFileProductServiceTests
    {
        #region TestSetup
        public static IUrlHelperFactory urlHelperFactory;
        public static DefaultHttpContext httpContextDefault;
        public static IWebHostEnvironment webHostEnvironment;
        public static ModelStateDictionary modelState;
        public static ActionContext actionContext;
        public static EmptyModelMetadataProvider modelMetadataProvider;
        public static ViewDataDictionary viewData;
        public static TempDataDictionary tempData;
        public static PageContext pageContext;

        public static ReadModel pageModel;

        [SetUp]
        public void TestInitialize()
        {
            httpContextDefault = new DefaultHttpContext()
            {
                //RequestServices = serviceProviderMock.Object,
            };

            modelState = new ModelStateDictionary();

            actionContext = new ActionContext(httpContextDefault, httpContextDefault.GetRouteData(), new PageActionDescriptor(), modelState);

            modelMetadataProvider = new EmptyModelMetadataProvider();
            viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
            tempData = new TempDataDictionary(httpContextDefault, Mock.Of<ITempDataProvider>());

            pageContext = new PageContext(actionContext)
            {
                ViewData = viewData,
            };

            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

            var MockLoggerDirect = Mock.Of<ILogger<IndexModel>>();
            JsonFileProductService productService;

            productService = new JsonFileProductService(mockWebHostEnvironment.Object);

            pageModel = new ReadModel(productService)
            {
            };
        }

        #endregion TestSetup

        #region AddRating
        /// <summary>
        /// REST Get Products data
        /// POST a valid rating
        /// Test that the last data that was added was added correctly
        /// </summary>
        [Test]
        public void AddRating_Valid_Product_Id_Rating_null_Should_Return_new_Array()
        {
            // Arrange
            // Get the Last data item
            var data = pageModel.ProductService.GetAllData().Last();

            // Act
            // Store the result of the AddRating method (which is being tested)
            var result = pageModel.ProductService.AddRating(data.Id, 0);

            // Assert
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// REST POST data that doesn't fit the constraints defined in function
        /// Test if it Adds
        /// Returns False because it wont add
        /// </summary>
        [Test]
        public void AddRating_Invalid_Product_ID_Not_Present_Should_Return_False()
        {
            // Arrange

            // Act
            // Store the result of the AddRating method (which is being tested)
            var result = pageModel.ProductService.AddRating("1000", 5);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// REST get result of false ID entered data
        /// Checks if the result equals the added data
        /// Should return false
        /// </summary>
        [Test]
        public void AddRating_InValid_Product_ID_Null_Should_Return_False()
        {
            // Arrange

            // Act
            // Store the result of the AddRating method (which is being tested)
            var result = pageModel.ProductService.AddRating(null, 1);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// REST Gets First Node of original data
        /// Caches the length of how many votes were made
        /// POST a new rating of 5 stars
        /// Gets first node of new data
        /// Checks origional data length against the new data length +1
        /// Checks if last data point was the one that was added
        /// </summary>
        [Test]
        public void AddRating_Valid_Product_Rating_5_Should_Return_True()
        {
            // Arrange
            // Get the First data item
            var data = pageModel.ProductService.GetAllData().First();
            // Store the original Rating list length
            var countOriginal = data.Ratings.Length;

            // Act
            // Store the result of the AddRating method (which is being tested)
            var result = pageModel.ProductService.AddRating(data.Id, 5);
            // Get the updated First data item
            var dataNewList = pageModel.ProductService.GetAllData().First();

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(countOriginal + 1, dataNewList.Ratings.Length);
            Assert.AreEqual(5, dataNewList.Ratings.Last());
        }

        /// <summary>
        /// REST get original data list
        /// Post rating to the data where number of stars are invalid
        /// Resturns false for invalid data point
        /// </summary>
        [Test]
        public void AddRating_InValid_Product_Rating_more_5_Should_Return_False()
        {
            // Arrange
            // Get the First data item
            var data = pageModel.ProductService.GetAllData().First();

            // Act
            // Store the result of the AddRating method (which is being tested)
            var result = pageModel.ProductService.AddRating(data.Id, 6);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// REST get original ratings
        /// POST a rating against the constraint <=0
        /// Compares rating to see if added corrctly
        /// Should return false
        /// </summary>
        [Test]
        public void AddRating_InValid_Product_Rating_less_than_0_Should_Return_False()
        {
            // Arrange
            // Get the First data item
            var data = pageModel.ProductService.GetAllData().First();

            // Act
            // Store the result of the AddRating method (which is being tested)
            var result = pageModel.ProductService.AddRating(data.Id, -2);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// REST get original data
        /// Cache length of data
        /// POST new valid data point
        /// GET new data
        /// Test if equal count is original + 1, and new data should be equal
        /// Test if the correct valid data point was added
        /// </summary>
        [Test]
        public void AddRating_Valid_Product_Rating_greater_than_0_Should_Return_True()
        {
            // Arrange
            // Get the First data item
            var data = pageModel.ProductService.GetAllData().First();

            // Store the original Rating list length for comparison later
            var countOriginal = data.Ratings.Length;

            // Act
            // Store the result of the AddRating method (which is being tested)
            var result = pageModel.ProductService.AddRating(data.Id, 1);
            // Get the updated First data item for comparison
            var dataNewList = pageModel.ProductService.GetAllData().First();

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(countOriginal + 1, dataNewList.Ratings.Length);
            Assert.AreEqual(1, dataNewList.Ratings.Last());
        }
        #endregion AddRating


    }
}