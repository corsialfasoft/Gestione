using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    public class HomeController : Controller {
        Profilo P;
        public HomeController() {
            P = new Profilo();

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
            DTGiorno giorno = new DTGiorno();
            giorno.data = data;
            giorno.OreLavoro = new OreLavoro();
            giorno.OrePermesso = 2;
            giorno.OreMalattia = 2;
            giorno.OreFerie = 0;

            ViewBag.giorno = giorno;
            return View();
        }
    }
}