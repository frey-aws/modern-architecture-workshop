using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sample_webapi;
using sample_webapi.Controllers;

namespace sample_webapi_tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
