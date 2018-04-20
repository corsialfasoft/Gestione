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
		public void ModificaCv()
		{
			HomeController controller = new HomeController();
			ViewResult result = controller.ModificaCv("Sotto","Caga",66,"iCazzi@mia.fuck","Via leMani dal Naso","9999") as ViewResult;
			Assert.IsTrue(result.ViewBag.Message == "Dati anagrafici modificati modificato");
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
			controller.AddComp("Inglese","10","GGGGG");
		}
		 [TestMethod]
		 public void ModificaComp(){
			HomeController controller = new HomeController();
			controller.AddComp("Maiale","4","GGGGG");
			controller.ModComp("Maiale","4","Mucca","5","GGGGG");
		 }
		 [TestMethod]
		 public void AddPersStud(){
			HomeController controller = new HomeController();
			DomainModel dm = new DomainModel();
			ViewResult vr = controller.DettaglioCurriculum() as ViewResult;
			controller.AddPerStudi(3,4,"Licenza Media","Ho imparato a parlare","GGGGG");
		 }
		 [TestMethod]
		 public void ModPerStudi(){
			HomeController controller = new HomeController();
			controller.AddPerStudi(5,6,"Elementare","Inizio","GGGGG");
			controller.ModPerStudi(5,6,"Elementare","Inizio",9,20,"Medie","Fine","GGGGG" );
		 }
		 [TestMethod]
		 public void AddEspLav(){
			HomeController controller = new HomeController();
			controller.AddEspLav(5,6,"Muratore","Fatto I muri","GGGGG" );
		 }
		 [TestMethod]
		 public void ModEspLav(){
			HomeController controller= new HomeController();
			controller.AddEspLav(9,10,"Mangia","Mangio Panini","GGGGG");
			controller.ModEspLav(9,10,"Mangia","Mangio Panini",12 ,15,"Carrucola" ,"DelP","GGGGG");
		 }
    }
}
