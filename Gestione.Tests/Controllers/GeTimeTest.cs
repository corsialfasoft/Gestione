using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gestione;
using Gestione.Controllers;

namespace Gestione.Tests.Controllers {
    public partial class HomeControllerTest {
        [TestMethod]
        public void TestVisulizzaGiorno() {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestCompilaOreLavrative() {
            HomeController controller = new HomeController(new Profilo(new Guid().ToString(),"studente",null,"test", "OreLavrative"));
            ViewResult result = controller.AddGiorno(DateTime.Today, "Ore di lavoro",2, "GeTime TestCompilaOreL") as ViewResult;
            Assert.IsNull(result.ViewBag.Message);
            result = controller.VisualizzaGiorno(DateTime.Today) as ViewResult;
            Assert.IsTrue(result.ViewBag.giorno.TotOreLavorate==2);
        }

        [TestMethod]
        public void TestCompilaNonLavrative() {
            HomeController controller = new HomeController(new Profilo(new Guid().ToString(), "studente", null, "test", "OreNonLavrative"));
            ViewResult result = controller.AddGiorno(DateTime.Today, "Ore di permesso", 2, "") as ViewResult;
            Assert.IsNull(result.ViewBag.Message);
            Assert.IsNotNull(result.ViewBag.EsitoAddGiorno);
            result = controller.AddGiorno(DateTime.Today, "Ore di malattia", 2, "") as ViewResult;
            Assert.IsNull(result.ViewBag.Message);
            Assert.IsNotNull(result.ViewBag.EsitoAddGiorno);
            result = controller.VisualizzaGiorno(DateTime.Today) as ViewResult;
            Assert.IsTrue(result.ViewBag.giorno.OrePermesso == 2);
            Assert.IsTrue(result.ViewBag.giorno.OreMalattia == 2);
        }
	}
}
