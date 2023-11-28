using Bunit;
using NUnit.Framework;

using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Components;
using ContosoCrafts.WebSite.Services;
using System;

namespace UnitTests.Components.Tests
{
    /// <summary>
    /// This class contains unit tests for the ProductModel
    /// </summary>
    public class ProductListTests : BunitTestContext
    {
        #region TestSetup
        public ProductModel[] Products;
        /// <summary>
        /// Test Setup
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {

        }
        #endregion TestSetup

        #region ProductList
        /// <summary>
        /// Test for getting the product list
        /// </summary>
        [Test]
        public void ProductList_Default_Should_Return_Content()
        {
            // Arrange
            Services.AddSingleton(TestHelper.ProductService);

            // Act
            // Render the ProductList component
            var page = RenderComponent<ProductList>();

            // Get the Cards retrned
            var result = page.Markup;

            // Assert
            // Check if the markup contains a specific content
            Assert.AreEqual(true, result.Contains("Gridder"));
        }
        #endregion ProductList

        /// <summary>
        /// Test for testing the enabling the filter function
        /// </summary>
        #region FilterData
        [Test]
        public void Enable_Filter_Data_Set_to_True_Should_Return_True()
        {
            // Arrange
            Services.AddSingleton(TestHelper.ProductService);
            var filterButton = "Filter";

            var page = RenderComponent<ProductList>();
            var buttonList = page.FindAll("Button");
            var button = buttonList.First(m => m.OuterHtml.Contains(filterButton));

            // Act
            button.Click();
            var pageMarkup = page.Markup;

            // Assert
            Assert.AreEqual(true, page.Instance.FilterData);
        }

        /// <summary>
        /// Test for updating the filter
        /// </summary>
        [Test]
        public void UpdateFilterType_ShouldUpdateFilterDataType()
        {
            // Arrange
            Services.AddSingleton(TestHelper.ProductService);
            var page = RenderComponent<ProductList>();

            // Act
            var inputField = page.Find("input[type='text']");
            inputField.Change("NewFilterText");

            // Assert
            Assert.AreEqual("NewFilterText", page.Instance.FilterDataString);
        }

        /// <summary>
        /// Test for clearing out the filter text
        /// </summary>
        [Test]
        public void Clear_Filter_Data_Set_to_False_Should_Return_False()
        {
            // Arrange
            Services.AddSingleton(TestHelper.ProductService);
            var clearButton = "Clear";

            var page = RenderComponent<ProductList>();

            // Find the Buttons (Clear)
            var buttonList = page.FindAll("Button");

            // Find the one that matches the button name looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(clearButton));

            // Act
            button.Click();

            // Get the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert
            Assert.AreEqual(false, page.Instance.FilterData);
        }
        #endregion FilterData

        #region submitRating
        /// <summary>
        /// Test for submit rating for product without any ratings
        /// </summary>
        [Test]
        public void SubmitRating_Valid_ID_Click_Unstared_Should_Increment_Count_And_Check_Star()
        {
            /*
             This test tests that the SubmitRating will change the vote as well as the Star checked
             Because the star check is a calculation of the ratings, using a record that has no stars and checking one makes it clear what was changed
            The test needs to open the page
            Then open the popup on the card
            Then record the state of the count and star check status
            Then check a star
            Then check again the state of the cound and star check status

            */
            // Arrange
            Services.AddSingleton(TestHelper.ProductService);
            var Title = "MoreInfoButton_9";

            var page = RenderComponent<ProductList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(Title));
            button.Click();

            // Get the markup of the page post the Click action
            var buttonMarkup = page.Markup;

            // Get the Star Buttons
            var starButtonList = page.FindAll("span");

            // Get the Vote Count
            // Get the Vote Count, the List should have 7 elements, element 2 is the string for the count
            var preVoteCountSpan = starButtonList[1];
            var preVoteCountString = preVoteCountSpan.OuterHtml;

            // Get the First star item from the list, it should not be checked
            var starButton = starButtonList.First(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star"));

            // Save the html for it to compare after the click
            var preStarChange = starButton.OuterHtml;

            // Act

            // Click the star button
            starButton.Click();

            // Get the markup to use for the assert
            buttonMarkup = page.Markup;

            // Get the Star Buttons
            starButtonList = page.FindAll("span");

            // Get the Vote Count, the List should have 7 elements, element 2 is the string for the count
            var postVoteCountSpan = starButtonList[1];
            var postVoteCountString = postVoteCountSpan.OuterHtml;

            // Get the Last stared item from the list
            starButton = starButtonList.First(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star checked"));

            // Save the html for it to compare after the click
            var postStarChange = starButton.OuterHtml;

            // Assert
            Assert.AreEqual(true, preVoteCountString.Contains("Be the first to vote!"));
            Assert.AreEqual(true, postVoteCountString.Contains("1"));
            Assert.AreEqual(false, preVoteCountString.Equals(postVoteCountString));


            // Reset
            Products = (ProductModel[])TestHelper.ProductService.GetProducts();
            Products[9].Ratings = null;
            TestHelper.ProductService.SaveModifiedData(Products);
        }

        /// <summary>
        /// Test for submit rating
        /// </summary>
        [Test]
        public void SubmitRating_Valid_ID_Click_Stared_Should_Increment_Count()
        {
            /*
             This test tests that the SubmitRating will change the vote as well as the Star checked
             Because the star check is a calculation of the ratings, using a record that has no stars and checking one makes it clear what was changed
            The test needs to open the page
            Then open the popup on the card
            Then record the state of the count and star check status
            Then check a star
            Then check again the state of the cound and star check status

            */
            // Arrange
            Services.AddSingleton(TestHelper.ProductService);
            var Title = "MoreInfoButton_10";

            var page = RenderComponent<ProductList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(Title));
            button.Click();

            // Get the markup of the page post the Click action
            var buttonMarkup = page.Markup;

            // Get the Star Buttons
            var starButtonList = page.FindAll("span");

            // Get the Vote Count
            // Get the Vote Count, the List should have 7 elements, element 2 is the string for the count
            var preVoteCountSpan = starButtonList[1];
            var preVoteCountString = ((preVoteCountSpan.InnerHtml).Split(' '))[0];

            // Get the First star item from the list, it should not be checked
            var starButton = starButtonList.First(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star"));

            // Save the html for it to compare after the click
            var preStarChange = starButton.OuterHtml;

            // Act

            // Click the star button
            starButton.Click();

            // Get the markup to use for the assert
            buttonMarkup = page.Markup;

            // Get the Star Buttons
            starButtonList = page.FindAll("span");

            // Get the Vote Count, the List should have 7 elements, element 2 is the string for the count
            var postVoteCountSpan = starButtonList[1];
            var postVoteCountString = ((postVoteCountSpan.InnerHtml).Split(' '))[0];

            // Get the Last stared item from the list
            starButton = starButtonList.First(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star checked"));

            // Save the html for it to compare after the click
            var postStarChange = starButton.OuterHtml;
            int preVoteCount = Int32.Parse(preVoteCountString);
            int postVoteCount = Int32.Parse(postVoteCountString);
            var result = postVoteCount - preVoteCount;

            // Assert
            Assert.AreEqual(1, result);

            //Reset
            //Products = (ProductModel[])TestHelper.ProductService.GetProducts();
            //Products[0].Ratings = [5,5,3,2,1,5,4,4];

        }
        #endregion submitRating

        #region SelectProduct
        /// <summary>
        /// Test for getting content of selected product
        /// </summary>
        [Test]
        public void SelectProduct_Valid_ID_Should_Return_Content()
        {
            // Arrange
            Services.AddSingleton(TestHelper.ProductService);
            var id = "10";

            // Render the ProductList component
            var page = RenderComponent<ProductList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));

            // Act
            button.Click();

            // Get the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert
            // Check if the markup contains a specific content
            Assert.AreEqual(true, pageMarkup.Contains("Gridder"));
        }
        #endregion SelectProduct
    }
}