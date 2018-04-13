﻿using System;
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
		public void MyPage()
		{
			HomeController controller = new HomeController();
			ViewResult result = controller.MyPage() as ViewResult;
			controller.MyPage("801130");
			Assert.IsTrue(result.ViewBag.CV != null);
			Assert.IsTrue(result.ViewBag.CV.nome == "Massimo");
			Assert.IsTrue(result.ViewBag.CV.cognome == "franzoso");
			Assert.IsTrue(result.ViewBag.CV.telefono == "3391627441");
			Assert.IsTrue(result.ViewBag.CV.eta == 33);
		}
        [TestMethod]
		public void EliminaCVTest()
		{
			HomeController controller = new HomeController();
			ViewResult result = controller.EliminaCV("ciao") as ViewResult;
			Assert.IsTrue(result.ViewBag.Message == "Non siamo riusciti a eliminare il curriculum selezionato");
		}
    }
}
