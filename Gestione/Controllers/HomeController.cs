using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gestione.Models;
using Interfaces;

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
            P = new Profilo("admin","direttore",new List<string>{"Visualizza commessa"},"nauman","aziz");

        }
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

        public ActionResult VisualizzaGiorno() {
            return View();
        }

        [HttpPost]
        public ActionResult VisualizzaGiorno(DateTime data) {
            DomainModel dm = new DomainModel();
            DTGGiorno giorno = dm.VisualizzaGiorno(data, P.Matricola);
            if (giorno!=null) {
                ViewBag.giorno = giorno;
            } else {
                ViewBag.Message = "Data non trovata!";
            }
            return View();
        }
    }
}