using NUnit.Framework;
using ContosoCrafts.WebSite.Models;

namespace UnitTests.Model.Tests
{
    /// <summary>
    /// Unit tests for ProductTypeEnum
    /// </summary>
    class ProductTypeEnumTests
    {
        #region DisplayName

        /// <summary>
        /// Tests that DisplayName returns a valid string
        /// </summary>
        [Test]
        public void DisplayName_Should_Return_Valid_String()
        {
            // Arrange

            // Act
            string cat1 = ProductTypeEnum.HomeAppliance.DisplayName();
            string cat2 = ProductTypeEnum.GardeningEquipments.DisplayName();
            string cat3 = ProductTypeEnum.SnowEquipments.DisplayName();
            string cat4 = ProductTypeEnum.CampingEquipments.DisplayName();
            string cat5 = ProductTypeEnum.Undefined.DisplayName();

            // Assert
            Assert.AreEqual("Home Appliances", cat1);
            Assert.AreEqual("Gardening Equipments", cat2);
            Assert.AreEqual("Snow Equipments", cat3);
            Assert.AreEqual("Camping Equipments", cat4);
            Assert.AreEqual("Undefined", cat5);
        }

        #endregion DisplayName

        [Test]
        public void DisplayName_Enum_Out_Of_Range_Return_Default()
        {
            // Arrange

            // Act
            var result = ProductTypeEnumExtensions.DisplayName((ProductTypeEnum)8);

            // Assert
            Assert.AreEqual("", result);
        }
    }
}