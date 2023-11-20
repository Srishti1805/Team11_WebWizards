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

namespace UnitTests.Pages.Product.Read
{
    /// <summary>
    /// This class contains unit tests for the ReadModel class in the "Pages/Product/Read" namespace.
    /// </summary>
    public class ReadTests
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
        /// Test initialisation
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

        #endregion TestSetup

        /// <summary>
        /// Valid test for OnGet
        /// </summary>
        #region OnGet
        [Test]
        public void OnGet_Valid_Test_Should_Return_Product_Identifier()
        {
            // Arrange

            // Act
            pageModel.OnGet("1");
            var result = pageModel.Product.Title;

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual("Gridder", result);
        }

        /// <summary>
        /// Invalid test for OnGet
        /// </summary>
        [Test]
        public void OnGet_Null_Should_Return_False()
        {
            // Arrange

            // Act
            var redirectResult = (RedirectToPageResult)pageModel.OnGet("12");
            var result = pageModel.Product;

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.IsNull(result);
            Assert.AreEqual("./Index", redirectResult.PageName);
        }
        #endregion OnGet
    }
}