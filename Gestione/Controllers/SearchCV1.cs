using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Interfaces;
using  Gestione.Models;

namespace Gestione.Controllers {
	public partial class HomeController : Controller {
		public ActionResult MyPage()
		{
			return View();
		}
		[HttpPost]
		public ActionResult MyPage(string id)
		{
			DomainModel dm = new DomainModel();
			if (id == P.Matricola || id == null) {
				id = P.Matricola;
			}
			CV trovato = dm.Search(id);
			if (trovato == null) {
				ViewBag.Message=$"Non è stato trovato alcun Curriculum con questo codice";
				return View("");
			}
			ViewBag.CV = trovato;
			return View("DettaglioCurriculum");
		}
		public ActionResult RicercaCurriculum()
		{
			return View();
		}
		
		[HttpPost]
		public ActionResult RicercaCurriculum(string chiava,string eta,string etaMin,string etaMax,string cognome)
		{
			int codice;
			int etaMinima;
			int etaMassima;
			List<CV> trovati = new List<CV>();
			if (chiava != "") {
				trovati = dm.SearchChiava(chiava);
				if (trovati.Count > 0) {
					ViewBag.ListaCV= trovati;
					return View("ListaCurriculum");
				}
				ViewBag.Message="Non è stato trovato nessun elemento";
				return View();
			}else if(eta != "" && int.TryParse(eta,out codice)) {
				trovati = dm.SearchEta(codice);
				if (trovati.Count > 0) {
					ViewBag.ListaCV= trovati;
					return View("ListaCurriculum");
				}
				ViewBag.Message="Non è stato trovato nessun elemento";
				return View();
			}else if(etaMin!= "" && etaMax!="" && int.TryParse(etaMin,out etaMinima) && int.TryParse(etaMax,out etaMassima)) {
				if(etaMassima < etaMinima) {
					ViewBag.Message="L'età massima non può essere minore dell'età minima";
					return View();
				}else if (etaMassima == etaMinima) {
					ViewBag.Message="Età minima e massima sono uguali";
					return View();
				}
				trovati = dm.SearchRange(etaMinima,etaMassima);
				if (trovati.Count > 0) {
					
					ViewBag.ListaCV= trovati;
					return View("ListaCurriculum");
				}
				ViewBag.Message="Non è stato trovato nessun elemento";
				return View();
			}else if(cognome!="") {
				trovati = dm.SearchCognome(cognome);
				if(trovati.Count > 0) {
					ViewBag.ListaCV=trovati;
					return View("ListaCurriculum");
				} else {
					ViewBag.Message="Non è stato trovato nessun elemento";
					return View();
				}
			}
			ViewBag.Message="Inserire dei parametri di ricerca validi";
			return View();
		}
	}
}