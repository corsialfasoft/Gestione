using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gestione.Models;
using Interfaces;

namespace Gestione.Controllers {
    public partial class  HomeController : Controller {
		public ActionResult ElencoCorsi(){
			DomainModel dm = new DomainModel();
			ViewBag.Corsi = dm.ListaCorsi();
			return View();
		}
		[HttpPost]
		public ActionResult ElencoCorsi(int idUtente){
			DomainModel dm = new DomainModel();
			ViewBag.Corsi = dm.ListaCorsi(idUtente);
			return View();
		}
		[HttpPost]
		public ActionResult ElencoCorso(int idCorso){
			DomainModel dm = new DomainModel();
			Corso c = dm.SearchCorsi(idCorso);
			ViewBag.Corso = c;
			ViewBag.Lezioni = c.Lezioni;
			return View("Corso");
		}
		[HttpPost]
		public ActionResult ElencoCorsi(int idUtente , string descrizione){
			DomainModel dm = new DomainModel();
			ViewBag.Corsi= dm.SearchCorsi(descrizione,idUtente);
			return View();
		}
    }
}