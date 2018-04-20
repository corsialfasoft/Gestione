using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gestione;
using Gestione.Controllers;
using Gestione.Models;

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
		public void RicercaCurriculum()
		{
			HomeController controller = new HomeController();
			ViewResult result = controller.RicercaCurriculum() as ViewResult;
			controller.RicercaCurriculum("truzzotunztunz","","","","");
			Assert.IsTrue(result.ViewBag.CV != null);
			Assert.IsTrue(result.ViewBag.CV.Count == 3);
			controller.RicercaCurriculum("","22","","","");
			Assert.IsTrue(result.ViewBag.CV.Count==3);
			controller.RicercaCurriculum("","","18","24","");
			Assert.IsTrue(result.ViewBag.CV.Count==3);
			controller.RicercaCurriculum("","","","","Franzoso");
			Assert.IsTrue(result.ViewBag.CV.Count==1);
		}
        [TestMethod]
		public void EliminaCVTest()
		{
			HomeController controller = new HomeController();
			ViewResult result = controller.EliminaCV("ciao") as ViewResult;
			Assert.IsTrue(result.ViewBag.Message == "Non siamo riusciti a eliminare il curriculum selezionato");
		}
		 [TestMethod]
		public void AddCompTest(){
			HomeController controller = new HomeController();
			DomainModel dm = new DomainModel();
			dm.AddCompetenze("GGGGG" , new Interfaces.Competenza{Titolo="Mangiaspade" , Livello=32});
		}
		 [TestMethod]
		 public void ModificaComp(){
			DomainModel dm = new DomainModel();
			dm.AddCompetenze("GGGGG" , new Interfaces.Competenza{Titolo="Porcospino" , Livello=32});
			dm.ModComp(new Interfaces.Competenza{Titolo="Porcospino" , Livello=32}, new Interfaces.Competenza{Titolo="Mangiapanino" , Livello=9999} , "GGGGG");
		 }
		 [TestMethod]
		 public void AddPersStud(){
			HomeController controller = new HomeController();
			DomainModel dm = new DomainModel();
			ViewResult vr = controller.DettaglioCurriculum() as ViewResult;
			controller.AddPerStudi(3,4,"Licenza Media","Ho imparato a parlare","GGGGG");

		 }
    }
}
