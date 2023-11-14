using System.Linq;

//Microsoft.AspNetCore namespaces
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

// Moq mocking framework
using Moq;

// NUnit testing framework
using NUnit.Framework;

// Application specific namespaces
using ContosoCrafts.WebSite.Pages;
using ContosoCrafts.WebSite.Services;

// Namespace for unit tests related to the Index page
namespace UnitTests.Pages.Index
{
    /// <summary>
    /// The IndexTests class contains the unit tests for the index page of the website
    /// </summary>
    public class IndexTests
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

        public static IndexModel pageModel;

        /// <summary>
        /// Initialises the product services and parameters for the test
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            // new http context class
            httpContextDefault = new DefaultHttpContext()
            {
                //RequestServices = serviceProviderMock.Object,
            };

            // new model state
            modelState = new ModelStateDictionary();
            // new action context
            actionContext = new ActionContext(httpContextDefault, httpContextDefault.GetRouteData(), new PageActionDescriptor(), modelState);
            // new model metadata provider
            modelMetadataProvider = new EmptyModelMetadataProvider();
            // new data dictionary view
            viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
            // temp dictionary for data
            tempData = new TempDataDictionary(httpContextDefault, Mock.Of<ITempDataProvider>());
            // new page context
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
            // json handler
            productService = new JsonFileProductService(mockWebHostEnvironment.Object);
            // new page model
            pageModel = new IndexModel(MockLoggerDirect, productService)
            {
            };
        }

        #endregion TestSetup

        /// <summary>
        /// Test for OnGet for the index page
        /// </summary>
        #region OnGet
        [Test]
        ///<summary>
        ///Unit test method to validate the behavior of the OnGet method
        /// by checking if it returns the correct count of products.
        /// </summary>
        public void OnGet_Valid_Test_Should_Return_Product_Count()
        {
            // Arrange
            // No specific arrangement needed for this test.

            // Act
            // Call the OnGet method to simulate the GET request.

            pageModel.OnGet();
            // Get the count of products from the pageModel for assertion.
            var result = pageModel.Products.ToList().Count;

            // Assert
            // Ensure that the ModelState of the pageModel is valid after executing OnGet.
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            // Ensure that the number of products returned by the OnGet method is as expected (6 in this case).
            Assert.AreEqual(10, result);
        }
        #endregion OnGet
    }
}