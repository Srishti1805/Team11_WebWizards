
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
using System.Linq;

namespace UnitTests.Pages.Product.Delete
{
    /// <summary>
    /// This class contains unit tests for the DeleteModel class in the "Pages/Product/Delete" namespace.
    /// </summary>
    public class DeleteTests
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

        public static DeleteModel pageModel;

        /// <summary>
        /// Initialises the product services and parameters for the test
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

            pageModel = new DeleteModel(productService)
            {
            };
        }
        #endregion TestSetup

        #region OnGet
        [Test]
        /// <summary>
        /// test for OnGet of delete page
        /// </summary>
        public void OnGet_Valid_Test_Should_Return_Product_Identifier()
        {
            // Arrange
            var temp = "b068534c-f862-4618-9aac-02ae5d6d872f";
            // Act
            pageModel.OnGet(temp);
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
            Assert.AreEqual("/Error", redirectResult.PageName);
        }
        #endregion OnGet

        #region OnPost
        [Test]
        /// <summary>
        /// test for OnGet of delete page
        /// </summary>
        public void OnPost_Valid_Should_Delete_Product()
        {
            // Arrange
            var temp = "b068534c-f862-4618-9aac-02ae5d6d872f";
            var dataSet = pageModel.ProductService.GetProducts();
            pageModel.OnGet(temp);

            // Act
            pageModel.OnPost();
            var result = pageModel.ProductService.GetProducts().FirstOrDefault(m => m.Id.Equals("1"));

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(null, result);

            // Reset
            pageModel.ProductService.SaveModifiedData(dataSet);
        }
        #endregion OnPost
    }
}