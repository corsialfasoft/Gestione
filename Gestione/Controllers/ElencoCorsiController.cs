﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gestione.Models;
using Interfaces;

namespace Gestione.Controllers {
    public partial class  HomeController : Controller {
		public ActionResult ElencoCorsi(){
			//ViewBag.Message =" Ciao ecco i corsi disponibili!";
			ViewBag.Controllo=null;	

			DomainModel dm = new DomainModel();
			Corso c = new Corso {
				Id = 2,
				Descrizione = "Mi piacciono i treni!",
				Nome = " Corso sui Treni"
			};
			List<Corso> res = new List<Corso> {
				c
			};
			ViewBag.Corsi = dm.ListaCorsi();
			
			return View();
		}
		[HttpPost]
		public ActionResult ElencoCorsi(bool mieiCorsi ,string descrizione ){
			DomainModel dm = new DomainModel();
			if(int.TryParse(descrizione,out int id) && !mieiCorsi) {        // Cerca Per iD corso

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
			//ViewBag.Lezioni = c.Lezioni;
			return View("ElencoCorsi");
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