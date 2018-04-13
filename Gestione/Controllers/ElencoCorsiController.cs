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
		public ActionResult ElencoCorsi(bool mieiCorsi ,string descrizione ){
			DomainModel dm = new DomainModel();
			int id ;
			if(int.TryParse(descrizione,out id) && !mieiCorsi){		// Cerca Per iD corso
				Corso c = dm.SearchCorsi(id);
				ViewBag.Corso = c;
				ViewBag.Lezioni = c.Lezioni;
				return View("Corso");
			}else if(descrizione!="" && mieiCorsi){
				ViewBag.Corsi = dm.SearchCorsi(descrizione , p.Matricola);
				return View("ElencoCorsi");
			}else if(descrizione=="" && mieiCorsi){
				ViewBag.Corsi = dm.ListaCorsi(p.Matricola);
				return View("ElencoCorsi");
			}else if(descrizione!="" && !mieiCorsi){
				ViewBag.Corsi= dm.SearchCorsi(descrizione);
				return View("ElencoCorsi");
			}else if(descrizione=="" && !mieiCorsi){
				ViewBag.Corsi= dm.ListaCorsi();
				return View("ElencoCorsi");
			}else{
				ViewBag.Messagge="Errore non gestito!";
				return View();
			}
		}
		[HttpPost]
		public ActionResult ElencoCorso(int idCorso){
			DomainModel dm = new DomainModel();
			Corso c = dm.SearchCorsi(idCorso);
			ViewBag.Corso = c;
			ViewBag.Lezioni = c.Lezioni;
			return View("Corso");
		}
		/*
		[HttpPost]
		public ActionResult ElencoCorsi(string idUtente , string descrizione){
			DomainModel dm = new DomainModel();
			ViewBag.Corsi= dm.SearchCorsi(descrizione,idUtente);
			return View();
		}
		*/
		/*
		[HttpPost]
		public ActionResult ElencoCorsi(string descrizione ){
			DomainModel dm = new DomainModel();
			ViewBag.Corsi= dm.SearchCorsi(descrizione);
			return View();
		}
		public ActionResult ElencoCorsiMieiCorsi(){
			return View("ElencoCorsi");
		}
		*/
    }
}