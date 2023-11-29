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

namespace UnitTests.Pages.Product.Update
{
    /// <summary>
    /// This class contains unit tests for the UpdateModel class in the "Pages/Product/Update" namespace.
    /// </summary>
    public class UpdateTests
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

        public static UpdateModel pageModel;

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

            pageModel = new UpdateModel(productService)
            {
            };
        }

        #endregion TestSetup

        /// <summary>
        /// Test for OnGet for the update page
        /// </summary>
        #region OnGet
        [Test]
        public void OnGet_Valid_Should_Return_Product_Identifier()
        {
            // Arrange
            var temp = "b068534c-f862-4618-9aac-02ae5d6d872f";
            // Act
            pageModel.OnGet(temp);
            var result = pageModel.Product;

            // Reset

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.NotNull(result);
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

        /// <summary>
        /// Test for OnPost for the update page
        /// </summary>
        #region OnPost
        [Test]
        public void OnPost_Valid_Should_Update_and_Return_Product_Details()
        {
            // Arrange
            var temp = "b068534c-f862-4618-9aac-02ae5d6d872f";
            pageModel.OnGet(temp);
            float tempPrice = pageModel.Product.Price;

            // Act
            pageModel.Product.Price = 20;
            pageModel.OnPost();

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(20, pageModel.Product.Price);

            // Reset
            pageModel.Product.Price = tempPrice;
        }

        /// <summary>
        /// Test for valid OnPost for the update page
        /// </summary>
        [Test]
        public void OnPost_Valid_Should_Return_Correct_Page()
        {
            // Arrange
            var temp = "b068534c-f862-4618-9aac-02ae5d6d872f";
            pageModel.OnGet(temp);

            // Act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, result.PageName.Contains("Index"));
        }

        /// <summary>
        /// Test for Invalid OnPost for the update page
        /// </summary>
        [Test]
        public void OnPost_InValid_Should_Return_Error_Page()
        {
            // Arrange
            pageModel.Product = new ProductModel();

            // Act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, result.PageName.Contains("Error"));
        }

        /// <summary>
        /// Test for invalid OnPost for the update page
        /// </summary>
        [Test]
        public void OnPost_InValid_Model_NotValid_Should_not_Return_Page()
        {
            // Arrange (preparing the necessary objects or data for the test)
            var temp = "b068534c-f862-4618-9aac-02ae5d6d872f";
            pageModel.OnGet(temp);

            // Force an invalid error state
            pageModel.ModelState.AddModelError("bogus", "bogus error");

            // Act (performing the actual operation being tested)
            var result = pageModel.OnPost() as ActionResult;

            // Assert (verifying the expected outcome)
            Assert.AreEqual(false, pageModel.ModelState.IsValid);
        }
        #endregion OnPost
    }
}