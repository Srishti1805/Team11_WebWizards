using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

using NUnit.Framework;

namespace UnitTests.Pages.Startup
{
    /// <summary>
    /// The StartupTests class contains the unit tests for testing the startup configuration of the website
    /// </summary>
    public class StartupTests
    {
        #region TestSetup

        /// <summary>
        /// Initialises the Test
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
        }

        /// <summary>
        /// Initialises the startup configuartions for the test
        /// </summary>
        public class Startup : ContosoCrafts.WebSite.Startup
        {
            public Startup(IConfiguration config) : base(config) { }
        }
        #endregion TestSetup

        /// <summary>
        /// Test for valid start of website
        /// </summary>
        #region ConfigureServices
        [Test]
        public void Startup_ConfigureServices_Valid_Defaut_Should_Pass()
        {
            var webHost = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            Assert.IsNotNull(webHost);
        }
        #endregion ConfigureServices

        /// <summary>
        /// Test for valid webhost
        /// </summary>
        #region Configure
        [Test]
        public void Startup_Configure_Valid_Defaut_Should_Pass()
        {
            var webHost = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            Assert.IsNotNull(webHost);
        }

        #endregion Configure
    }
}