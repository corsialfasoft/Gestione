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
        public void Contact() {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
		[TestMethod]
		public void VisualizzaCommessa() {
			HomeController controller = new HomeController();
			ViewResult result = controller.VisualizzaCommessa("GeTime") as ViewResult;
			Assert.IsNotNull(result.ViewBag.Giorni);
		}
        [TestMethod]
        public void VisualizzaGiorno() {
           
            HomeController controller = new HomeController();
            string value = "2000-01-01"; //non funziona se nn sono presenti nel DB questi valori!!!!!!!!!
            DateTime convertedDate ;
            convertedDate = Convert.ToDateTime(value); //non funziona se nn sono presenti nel DB questi valori!!!!!!!!!
            ViewResult result = controller.VisualizzaGiorno(convertedDate) as ViewResult;
            
            Assert.IsTrue(result.ViewBag.giorno.data == convertedDate);
            Assert.IsTrue(result.ViewBag.giorno.OreLavorate [0].oreGiorno == 4); //non funziona se nn sono presenti nel DB questi valori!!!!!!!!!
            Assert.IsTrue(result.ViewBag.giorno.TotOreLavorate == 4); //non funziona se nn sono presenti nel DB questi valori!!!!!!!!!
            Assert.IsTrue(result.ViewBag.giorno.OreMalattia == 0); //non funziona se nn sono presenti nel DB questi valori!!!!!!!!!
        }
    }
}
