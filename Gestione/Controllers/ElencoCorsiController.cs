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
			ViewBag.Message =" Ciao ecco i corsi disponibili!";
			DomainModel dm = new DomainModel();
			Corso c = new Corso();
			c.Id= 2;
			c.Descrizione="Mi piacciono i treni!";
			c.Nome=" Corso sui Treni";
			List<Corso> res = new List<Corso>();
			res.Add(c);
			//ViewBag.Corsi = dm.ListaCorsi();
			ViewBag.Corsi = res;
			return View();
		}
		[HttpPost]
		public ActionResult ElencoCorsiMieiCorsi(){
			DomainModel dm = new DomainModel();
			ViewBag.Corsi = dm.ListaCorsi(p.Matricola);
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
		[HttpPost]
		public ActionResult ElencoCorsi(string descrizione){
			DomainModel dm = new DomainModel();
			ViewBag.Corsi= dm.SearchCorsi(descrizione);
			return View();
		}
    }
}