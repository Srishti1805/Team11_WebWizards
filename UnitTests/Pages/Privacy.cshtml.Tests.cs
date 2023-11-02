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

// Namespace for unit tests related to the Privacy page
namespace UnitTests.Pages.Privacy
{
    /// <summary>
    /// The PrivacyTests class contains the unit tests for the privacy page of the website
    /// </summary>
    public class PrivacyTests
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

        public static PrivacyModel pageModel;

        /// <summary>
        /// Initialises the product services and parameters for the test
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            //// Setting up the HttpContext, ActionContext, ViewData, TempData, and other required components
            // for simulating a page request.
            httpContextDefault = new DefaultHttpContext()
            {
                TraceIdentifier = "trace",
                //RequestServices = serviceProviderMock.Object,
            };
            httpContextDefault.HttpContext.TraceIdentifier = "trace";

            modelState = new ModelStateDictionary();

            actionContext = new ActionContext(httpContextDefault, httpContextDefault.GetRouteData(), new PageActionDescriptor(), modelState);

            modelMetadataProvider = new EmptyModelMetadataProvider();
            viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
            tempData = new TempDataDictionary(httpContextDefault, Mock.Of<ITempDataProvider>());

            // Arrange necessary context and services for the test.
            pageContext = new PageContext(actionContext)
            {
                ViewData = viewData,
                HttpContext = httpContextDefault
            };

            // Act
            // Initialize the PrivacyModel and related components.
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

            // Assert
            // Ensure that the test components are properly initialized.
            var MockLoggerDirect = Mock.Of<ILogger<PrivacyModel>>();
            JsonFileProductService productService;

            productService = new JsonFileProductService(mockWebHostEnvironment.Object);

            pageModel = new PrivacyModel(MockLoggerDirect)
            {
                PageContext = pageContext,
                TempData = tempData,
            };
        }

        #endregion TestSetup

        /// <summary>
        /// Test for OnGet for the privacy page
        /// </summary>
        #region OnGet
        [Test]
        ///<summary>
        ///// This unit test method checks the behavior of the OnGet method
        ///<summary>
        public void OnGet_Valid_Activity_Set_Should_Return_RequestId()
        {
            // Arrange
            // Prepare the necessary data and context for the test.
            // Act
            pageModel.OnGet();

            // Reset
            // Call the OnGet method to simulate the GET request.

            // Assert
            // Ensure that the ModelState of the pageModel is valid after executing OnGet.
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
        }

        #endregion OnGet
    }
}