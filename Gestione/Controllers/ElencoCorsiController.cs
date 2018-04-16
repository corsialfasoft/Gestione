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
			ViewBag.Controllo=null;	
			List<Corso> ris =dm.ListaCorsi(); 
			if(ris != null){
				ViewBag.Corsi = ris;		
			}else{
				ViewBag.Message="Elenco vuoto";
			}
			return View();
		}
		[HttpPost]
		public ActionResult ElencoCorsi(bool mieiCorsi ,string descrizione ){
			DomainModel dm = new DomainModel();
			if(int.TryParse(descrizione,out int id) && !mieiCorsi) {    // Cerca Per iD corso
				Corso c = dm.SearchCorsi(id);
				ViewBag.Corso = c;
				ViewBag.Lezioni = c.Lezioni;
				return View("Corso");
			} else if(descrizione != "" && mieiCorsi) {
				ViewBag.Controllo = true;
				ViewBag.Message = "Ecco i tuoi risultati della ricerca";
				ViewBag.Corsi = dm.SearchCorsi(descrizione,P.Matricola);
				return View("ElencoCorsi");
			} else if(descrizione == "" && mieiCorsi) {
				ViewBag.Controllo = true;
				ViewBag.Message = "Ecco i tuoi risultati della ricerca";
				ViewBag.Corsi = dm.ListaCorsi(P.Matricola);
				return View("ElencoCorsi");
			} else if(descrizione != "" && !mieiCorsi) {
				ViewBag.Controllo = true;
				ViewBag.Message = "Ecco i tuoi risultati della ricerca";
				ViewBag.Corsi = dm.SearchCorsi(descrizione);
				return View("ElencoCorsi");
			} else if(descrizione == "" && !mieiCorsi) {
				ViewBag.Controllo = false;
				ViewBag.Message = "Casso hai sbagliato";
				ViewBag.Corsi = dm.ListaCorsi();
				return View("ElencoCorsi");
			} else {
				ViewBag.Messagge = "Errore non gestito!";
				return View();
			}
		}
		
		public ActionResult ElencoCorso(int id){
			DomainModel dm = new DomainModel();
			Corso c = dm.SearchCorsi(id);
			List<Corso> res = new List<Corso> {
				c
			};
			ViewBag.Corsi = res;
			return View("ElencoCorsi");
		}
    }
}