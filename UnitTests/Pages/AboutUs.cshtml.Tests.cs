using System.Diagnostics;

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

// NUnit testing framework
using NUnit.Framework;

// Moq mocking framework
using Moq;

// Application specific namespaces
using ContosoCrafts.WebSite.Pages;
using ContosoCrafts.WebSite.Services;

// Namespace for unit tests related to the AboutUs page
namespace UnitTests.Pages.AboutUs
{
    /// <summary>
    /// This class contains unit tests for the About_UsModel class in the AboutUs namespace.
    /// </summary>
    public class AboutUsTests
    {
        #region TestSetup
        // Static variables to store various components needed for testing
        public static IUrlHelperFactory urlHelperFactory;
        public static DefaultHttpContext httpContextDefault;
        public static IWebHostEnvironment webHostEnvironment;
        public static ModelStateDictionary modelState;
        public static ActionContext actionContext;
        public static EmptyModelMetadataProvider modelMetadataProvider;
        public static ViewDataDictionary viewData;
        public static TempDataDictionary tempData;
        public static PageContext pageContext;

        public static About_UsModel pageModel;

        /// <summary>
        /// Initialises the product services and parameters for the test
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            // Setting up the HttpContext, ActionContext, ViewData, TempData, and other required components
            // for simulating a page request.
            httpContextDefault = new DefaultHttpContext()
            {
                TraceIdentifier = "trace",
                //RequestServices = serviceProviderMock.Object,
            };
            // Arrange necessary context and services for the test.
            httpContextDefault.HttpContext.TraceIdentifier = "trace";

            // Act
            // Initialize the About_UsModel and related components.
            modelState = new ModelStateDictionary();

            actionContext = new ActionContext(httpContextDefault, httpContextDefault.GetRouteData(), new PageActionDescriptor(), modelState);

            modelMetadataProvider = new EmptyModelMetadataProvider();
            viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
            tempData = new TempDataDictionary(httpContextDefault, Mock.Of<ITempDataProvider>());

            pageContext = new PageContext(actionContext)
            {
                ViewData = viewData,
                HttpContext = httpContextDefault
            };

            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

            var MockLoggerDirect = Mock.Of<ILogger<PrivacyModel>>();
            JsonFileProductService productService;

            productService = new JsonFileProductService(mockWebHostEnvironment.Object);

            // Assert
            // Ensure that the test components are properly initialized.
            pageModel = new About_UsModel()
            {
                PageContext = pageContext,
                TempData = tempData,
            };
        }

        #endregion TestSetup

        /// <summary>
        /// Test for OnGet for the aboutus page
        /// </summary>
        #region OnGet
        [Test]
        /// <summary>
        /// Unit test method to validate the behavior of the OnGet method.
        /// </summary>
        public void OnGet_Valid_Test_Should_Return_True()
        {
            // Arrange
            // No specific arrangement needed for this test.

            // Act
            // Call the OnGet method to simulate the GET request.
            pageModel.OnGet();

            // Reset
            // No reset action needed for this test.

            // Assert
            // Ensure that the ModelState of the pageModel is valid after executing OnGet.
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
        }

        #endregion OnGet
    }
}