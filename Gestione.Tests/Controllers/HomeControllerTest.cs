using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gestione;
using Gestione.Controllers;
using Gestione.Models;
using Interfaces;

namespace Gestione.Tests.Controllers {
    [TestClass]
    public partial class HomeControllerTest {
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
            ViewResult result = controller.AddCorso("ciao","corso inerente al test", new DateTime(2015,02,15),new DateTime(2015,02,16)) as ViewResult;
            
            // Assert
            Assert.IsTrue(result.ViewBag.Message=="Corso inserito correttamente");
        }
		[TestMethod]
        public void Corso() {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Corso(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
			Assert.IsNotNull(result.ViewBag.Lezioni);
        }
		[TestMethod]
		public void ElencoCorsi(){
			 HomeController controller = new HomeController();			 
			 ViewResult result = controller.ElencoCorsi() as ViewResult;			 
			 Assert.IsNotNull( result.ViewBag.Corsi);
		}
        	[TestMethod]
		public void ElencoCorsiStudenti(){
			 HomeController controller = new HomeController();
			 ViewResult result = controller.ElencoCorsiStudente("prova") as ViewResult;
			 Assert.IsNotNull( result.ViewBag.CorsiStudente);
		}
		[TestMethod]
		public void ElencoCorsiParam(){
			 HomeController controller = new HomeController();
			// ViewResult result = controller.ElencoCorsi(true,"Pasticcione") as ViewResult;
			// Assert.IsNotNull( result.ViewBag.Corsi);
		}
		[TestMethod]
		public void AddLezione(){
			HomeController controller = new HomeController();
			ViewResult result = controller.AddLezione("test","test",1,1) as ViewResult;
			Assert.IsTrue( result.ViewBag.Message=="Lezione aggiunta correttamente");
		}
		[TestMethod]
		public void AddLezione1(){
			int idCorso=1;
			HomeController controller = new HomeController();
			ViewResult	result = controller.AddLezione(idCorso) as ViewResult;
			Assert.IsTrue(result.ViewBag.Message==idCorso);
			Assert.IsTrue(result.ViewBag.CorsoId==idCorso);
		}
		[TestMethod]
		public void ElencoCorsi1(){
			string descrizione = "a";
			bool mieiCorsi =true;
			HomeController controller = new HomeController();
            ViewResult result = controller.AddCorso("ciao","corso inerente al test", new DateTime(2015,02,15),new DateTime(2015,02,16)) as ViewResult;
			//result = controller.ElencoCorsi(mieiCorsi,descrizione) as ViewResult;			
			if((descrizione == "a" && mieiCorsi==true)||(descrizione == "" && mieiCorsi==true)
				||(descrizione != "" && mieiCorsi==false)){
				Assert.IsTrue(result.ViewBag.Controllo == true);
				Assert.IsTrue(result.ViewBag.Message == "Ecco i tuoi risultati della ricerca");
			}else if(descrizione == "a" && mieiCorsi==true){
				Assert.IsTrue(result.ViewBag.Controllo == false);
				Assert.IsTrue(result.ViewBag.Message == "input errato, riprova!");
			}else{
				Assert.IsTrue(result.ViewBag.Messagge == "Errore non gestito!");
			}
		}
		[TestMethod]
		public void ElencoCorso(){
			DomainModel dm = new DomainModel();
			HomeController controller = new HomeController();			
			List<Corso> res = new List<Corso> { dm.SearchCorsi(1) };
			ViewResult result = controller.ElencoCorso(1) as ViewResult;
			result.ViewBag.Corsi = res;
			Assert.IsInstanceOfType(result.ViewBag.Corsi , typeof(List<Corso>));
		}
		[TestMethod]
		public void ElencoCorsiStud(){
			DomainModel dm = new DomainModel();
			HomeController controller = new HomeController();			
			List<Corso> corso = dm.ListaCorsi("prova");
			ViewResult result = controller.ElencoCorsiStudente("prova") as ViewResult;            
			result.ViewBag.CorsiStudente = corso;
			Assert.IsInstanceOfType(result.ViewBag.CorsiStudente, typeof(List<Corso>) );
		}
		//[TestMethod]
		//public void ModificaLezione()
		//{ TO DO, BISOGNA CREARE UN CONTESTO
		//	DomainModel dm = new DomainModel();
		//	HomeController controller = new HomeController();
		//	int idCorso = 1;
		//	Lezione a =new Lezione {Nome="Omnes",Descrizione="java avanzato",Durata=9 };
		//	dm.AddLezione(1,a);
		//	Corso corso = dm.SearchCorsi(idCorso);
		//	List<Lezione> lezioni = dm.ListaLezioni(corso);
		//	var query = from lez in lezioni
		//				where lez.Nome == "Omnes"
		//				select lez;
		//	int idLezione= query.Last().Id;
		//	ViewResult result = controller.ModificaLezione(a.Nome,idLezione,a.Descrizione,a.Durata,1) as ViewResult;
		//	controller.ModificaLezionePost("Servabit","ille",8,idLezione,1);
		//	List<Lezione> lezioniModificata = dm.ListaLezioni(corso);
		//	Assert.IsTrue(lezioniModificata.Last() != a);
		//}
	}
}
