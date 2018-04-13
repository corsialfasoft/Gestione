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
		public ActionResult RicercaCurriculum(string chiava,string eta)
		{
			List<CV> trovati = new List<CV>();
			int etagiusta;
			if(chiava != "") {
				trovati = dm.SearchChiava(chiava);
				if (trovati.Count > 0) {
					ViewBag.CVtrovati = trovati;
					return View("ListaCurriculum");
				}
			}else if (eta != "" && int.TryParse(eta,out etagiusta)) {
				trovati=dm.SearchEta(etagiusta);
					ViewBag.CVtrovati = trovati;
					return View("ListaCurriculum");
			}
			ViewBag.Message=$"Non è stato trovato nessun elemento";
			return View();
		}
	}
}