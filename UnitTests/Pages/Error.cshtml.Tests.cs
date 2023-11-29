using System.Diagnostics;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Moq;
using ContosoCrafts.WebSite.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using NUnit.Framework.Internal;
using System.Collections.Generic;

// Namespace for unit tests related to the Error page
namespace UnitTests.Pages.Error
{
    /// <summary>
    /// This class contains unit tests for the errortesting for the website
    /// </summary>
    public class ErrorTests
    {
        #region TestSetup
        public static ErrorModel pageModel;
        /// <summary>
        /// Initializes the necessary services and parameters for the test.
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            var logger = new Mock<ILogger<ErrorModel>>();
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            var httpRequest = new Mock<HttpRequest>();
            httpRequest.Setup(r => r.Query).Returns(new QueryCollection(new Dictionary<string, StringValues>()));
            httpContextAccessor.Setup(h => h.HttpContext.Request).Returns(httpRequest.Object);

            pageModel = new ErrorModel(logger.Object, httpContextAccessor.Object)
            {
                PageContext = TestHelper.PageContext,
                TempData = TestHelper.TempData,
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
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
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
            pageModel.OnGet();

            // Reset

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual("trace", pageModel.RequestId);
            Assert.AreEqual(true, pageModel.ShowRequestId);
        }

        /// <summary>
        /// Unit test method to validate the behavior of the OnGet method when errorLocation is null.
        /// </summary>
        [Test]
        public void OnGet_NoErrorLocation_ShouldSetDefaultMessage()
        {
            // Arrange
            var logger = new Mock<ILogger<ErrorModel>>();
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            var httpRequest = new Mock<HttpRequest>();
            httpRequest.Setup(r => r.Query).Returns(new QueryCollection(new Dictionary<string, StringValues>()));
            httpContextAccessor.Setup(h => h.HttpContext.Request).Returns(httpRequest.Object);
            Activity activity = new Activity("activity");
            activity.Start();

            var pageModel = new ErrorModel(logger.Object, httpContextAccessor.Object)
            {
                Message = null // Ensure Message is initially null
            };

            // Act
            pageModel.OnGet();

            // Reset
            activity.Stop();

            // Assert
            Assert.True(pageModel.ShowRequestId);
            Assert.AreEqual("The resource you're trying to find is either missing or moved.", pageModel.Message);
        }

        /// <summary>
        /// Unit test method to validate the behavior of the OnGet method when errorLocation is Read.
        /// </summary>
        [TestCase("Read")]
        public void OnGet_WithValidErrorLocation_Read_ShouldSetCorrectMessage(string errorLocation)
        {
            // Arrange
            var logger = new Mock<ILogger<ErrorModel>>();
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            var httpRequest = new Mock<HttpRequest>();
            httpRequest.Setup(r => r.Query).Returns(new QueryCollection(new Dictionary<string, StringValues> { { "errorLocation", errorLocation } }));
            httpContextAccessor.Setup(h => h.HttpContext.Request).Returns(httpRequest.Object);
            Activity activity = new Activity("activity");
            activity.Start();

            var pageModel = new ErrorModel(logger.Object, httpContextAccessor.Object)
            {
                Message = null // Ensure Message is initially null
            };

            // Act
            pageModel.OnGet();

            // Reset
            activity.Stop();

            // Assert
            Assert.True(pageModel.ShowRequestId);
            Assert.AreEqual("Invalid product selected for Read operation.", pageModel.Message);
        }

        /// <summary>
        /// Unit test method to validate the behavior of the OnGet method when errorLocation is Update.
        /// </summary>
        [TestCase("Update")]
        public void OnGet_WithValidErrorLocation_Update_ShouldSetCorrectMessage(string errorLocation)
        {
            // Arrange
            var logger = new Mock<ILogger<ErrorModel>>();
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            var httpRequest = new Mock<HttpRequest>();
            httpRequest.Setup(r => r.Query).Returns(new QueryCollection(new Dictionary<string, StringValues> { { "errorLocation", errorLocation } }));
            httpContextAccessor.Setup(h => h.HttpContext.Request).Returns(httpRequest.Object);
            Activity activity = new Activity("activity");
            activity.Start();

            var pageModel = new ErrorModel(logger.Object, httpContextAccessor.Object)
            {
                Message = null // Ensure Message is initially null
            };

            // Act
            pageModel.OnGet();

            // Reset
            activity.Stop();

            // Assert
            Assert.True(pageModel.ShowRequestId);
            Assert.AreEqual("Invalid product selected for Update operation.", pageModel.Message);
        }

        /// <summary>
        /// Unit test method to validate the behavior of the OnGet method when errorLocation is Delete.
        /// </summary>
        [TestCase("Delete")]
        public void OnGet_WithValidErrorLocation_Delete_ShouldSetCorrectMessage(string errorLocation)
        {
            // Arrange
            var logger = new Mock<ILogger<ErrorModel>>();
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            var httpRequest = new Mock<HttpRequest>();
            httpRequest.Setup(r => r.Query).Returns(new QueryCollection(new Dictionary<string, StringValues> { { "errorLocation", errorLocation } }));
            httpContextAccessor.Setup(h => h.HttpContext.Request).Returns(httpRequest.Object);
            Activity activity = new Activity("activity");
            activity.Start();

            var pageModel = new ErrorModel(logger.Object, httpContextAccessor.Object)
            {
                Message = null // Ensure Message is initially null
            };

            // Act
            pageModel.OnGet();

            // Reset
            activity.Stop();

            // Assert
            Assert.True(pageModel.ShowRequestId);
            Assert.AreEqual("Invalid product selected for Delete operation.", pageModel.Message);
        }
        #endregion OnGet

        #region OnPost
        /// <summary>
        /// Unit test method to validate the behavior of the OnPost method when clicked on Go Home
        /// </summary>
        [Test]
        public void OnPost_Valid_Activity_Should_Return_True()
        {
            // Arrange

            // Act
            var redirectResult = (RedirectToPageResult)pageModel.OnPost();

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual("/Index", redirectResult.PageName);

        }
        #endregion OnPost
    }
}