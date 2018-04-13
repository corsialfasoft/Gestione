using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gestione;
using Gestione.Controllers;


namespace Gestione.Tests.Controllers {
    [TestClass]
    public class HomeControllerTest {
        [TestMethod]
        public void Index() {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About() {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.",result.ViewBag.Message);
        }

        [TestMethod]
        public void AddCorso() {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.AddCorso("ciao","corso inerente al saluto", new DateTime(2015,02,15),new DateTime(2015,02,16)) as ViewResult;
            
            // Assert
            Assert.IsTrue(result.ViewBag.Message=="Qualcosa è andato storto");
        }
		[TestMethod]
        public void Corso() {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Corso() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
			Assert.IsNotNull(result.ViewBag.Lezioni);
        }
    }
}
