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

// Namespace for unit tests related to the Error page
namespace UnitTests.Pages.Error
{
    public class ErrorTests
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

        public static ErrorModel pageModel;

        /// <summary>
        /// Initializes the necessary services and parameters for the test.
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
            // Initialize the ErrorModel and related components.
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

            // Assert
            // Ensure that the test components are properly initialized.
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

            var MockLoggerDirect = Mock.Of<ILogger<ErrorModel>>();
            JsonFileProductService productService;

            productService = new JsonFileProductService(mockWebHostEnvironment.Object);

            pageModel = new ErrorModel(MockLoggerDirect)
            {
                PageContext = pageContext,
                TempData = tempData,
            };
        }

        #endregion TestSetup

        /// <summary>
        /// Unit test method to validate the behavior of the OnGet method when activity is valid.
        /// </summary>
        #region OnGet
        [Test]
        public void OnGet_Valid_Activity_Set_Should_Return_RequestId()
        {
            // Arrange
            // Start a new activity for testing purposes
            Activity activity = new Activity("activity");
            activity.Start();

            // Act
            // Call the OnGet method to simulate the GET request.
            pageModel.OnGet();

            // Reset
            // Stop the activity after the test.
            activity.Stop();

            // Assert
            // Ensure that the ModelState of the pageModel is valid after executing OnGet.
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            // Ensure that the RequestId in pageModel matches the Id of the started activity.
            Assert.AreEqual(activity.Id, pageModel.RequestId);
        }

        /// <summary>
        /// Unit test method to validate the behavior of the OnGet method when activity is null.
        /// </summary>
        [Test]
        public void OnGet_InValid_Activity_Null_Should_Return_TraceIdentifier()
        {
            // Arrange

            // Act
            // Call the OnGet method to simulate the GET request.
            pageModel.OnGet();

            // Reset

            // Assert
            // Ensure that the ModelState of the pageModel is valid after executing OnGet.
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            // Ensure that the RequestId in pageModel matches the TraceIdentifier of the HttpContext.
            Assert.AreEqual("trace", pageModel.RequestId);
            // Ensure that ShowRequestId property is set to true.
            Assert.AreEqual(true, pageModel.ShowRequestId);
        }
        #endregion OnGet
    }
}