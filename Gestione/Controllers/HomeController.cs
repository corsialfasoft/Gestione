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
            return View ();
        }
        public ActionResult AggiungiCurriculum(){
            return View();
        }

        [HttpPost]
        public ActionResult AggiungiCurriculum(string nome,string cognome,string eta,
            string email,string residenza,string telefono,string annoinizio,string annofine,
            string titolo, string descrizione, string annoinizioesp, string annofinesp,string qualifica,
            string descrizionesp,string tipo,string livello
            ) {
            if (!String.IsNullOrEmpty(nome) && !String.IsNullOrEmpty(cognome)
                && !String.IsNullOrEmpty(eta) && !String.IsNullOrEmpty(email)
                && !String.IsNullOrEmpty(telefono) && !String.IsNullOrEmpty(residenza)){
                if(int.TryParse(eta, out int Eta)){
                    dm.AggiungiCV(new CV());
                    ViewBag.Message = "Curriculum Aggiunto";
                    return View("MyPage");
                } else {
                    ViewBag.Message = "Eta' non valida";
                    return View("AggiungiCurriculum");
                }
            } else{
                ViewBag.Message = "Campi obbligatori da inserire...";
                return View("AggiungiCurriculum");
            }
        }
    }
}