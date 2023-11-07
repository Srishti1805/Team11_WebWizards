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
using ContosoCrafts.WebSite.Controllers;
using System.Linq;

namespace UnitTests.Controller.Tests
{
    /// <summary>
    /// This class contains unit tests for the ProductModel
    /// </summary>
    public class ProductsControllerTests
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
        /// <summary>
        /// Test Setup
        /// </summary>
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
        #endregion

        #region
        /// <summary>
        /// Creates a default datapoint of ProductServices
        /// Creates a new datapoint of the a ProductsController with datapoint
        /// Gets the whole datapoint
        /// Tests if Equal
        /// </summary>
        [Test]
        public void get_AllData_Present_Should_Return_True()
        {
            //arrange
            

            //Act
            //store datapoint as a ProductController datapoint
            var newData = new ProductsController(pageModel.ProductService).Get().First();

            var response = pageModel.ProductService.GetAllData().First();

            //Assert
            Assert.AreEqual(newData.Id, response.Id);
        }
        #endregion

        #region
        /// <summary>
        /// Creates a default datapoint of ProductServices
        /// Creates a new datapoint of the a ProductsController with datapoint
        /// Gets the whole datapoint
        /// Tests if Added dataPoint equals the created one
        /// </summary>
        [Test]
        public void Patch_AddValid_Rating_Should_Return_True()
        {
            //arrange
            

            //Act
            //store datapoint as a ProductController datapoint
            var newData = new ProductsController(pageModel.ProductService);
            //Create a newRating datapoint to "Patch to theDataController"
            var newRating = new ProductsController.RatingRequest();
            {
                newRating.ProductId = newData.ProductService.GetAllData().Last().Id;
                newRating.Rating = 5;
            }

            //Act
            newData.Patch(newRating);

            //Assert
            Assert.AreEqual(newData.ProductService.GetAllData().Last().Id, newRating.ProductId);
        }
        #endregion
    }
}