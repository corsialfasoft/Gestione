using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Interfaces;
using Gestione.Models;

namespace Gestione.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
		 public ActionResult Corso(int id) {
			DomainModel dm = new DomainModel();
			Interfaces.Corso scelto = dm.SearchCorsi(id);
			List<Lezione> lezions = new List<Lezione>();
			foreach(Lezione l in scelto.Lezioni){
			lezions.Add(l);
			}
			ViewBag.Lezioni = lezions;
           return View();
        }
		public ActionResult Iscrizione(int idCorso,int idStudente){
			DomainModel dm = new DomainModel();
			dm.Iscriviti(idCorso,idStudente);
			return View();
		}
    }
}