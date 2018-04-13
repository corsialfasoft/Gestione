using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Interfaces;
using Gestione.Models;

namespace Gestione.Controllers {
    public class Profilo {
        public string Matricola { get; set; }
        public string Ruolo { get; set; }
        public List<String> Funzioni { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }

		public Profilo(){}

        public Profilo(string matricola, string ruolo, List<String> funzioni, string nome, string cognome) {
            Matricola = matricola;
            Ruolo = ruolo;
            Funzioni = funzioni;
            Nome = nome;
            Cognome = cognome;
        }
    }
    public partial class HomeController : Controller {
        Profilo P;
        public HomeController() {
            P = new Profilo("qwerty","admin",null,"ciao","mazzo");

		}
		public ActionResult Index() {
			return View();
		}

        public ActionResult AddLezione() {
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
		public ActionResult Corso(int id=1) {
			DomainModel dm = new DomainModel();
			Corso scelto = dm.SearchCorsi(id);
			List<Lezione> lezions = new List<Lezione>();
			foreach(Lezione l in scelto.Lezioni) {
				lezions.Add(l);
			}
			ViewBag.Lezioni = lezions;
			return View();
		}
		public ActionResult Iscrizione(int idCorso=1,int idStudente=1) {
			DomainModel dm = new DomainModel();
			try {
				dm.Iscriviti(idCorso,idStudente);
			} catch(Exception e) {
				ViewBag.Message = e.Message;
			}
			return View();
		}
	}
}