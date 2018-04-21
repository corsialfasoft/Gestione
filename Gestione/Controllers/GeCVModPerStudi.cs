using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gestione.Models;
using Interfaces;

namespace Gestione.Controllers {
    public partial class HomeController : Controller {
        [HttpPost]
        public ActionResult ModPerStud(int annoInizio, int annoFine, string titolo, string descrizione) {
            if(annoFine> annoInizio && titolo.Length>0 && descrizione.Length > 0){
			    DomainModel dm = new DomainModel();
                Profilo p = Session["profile"] as Profilo;//ATTENZIONE DA RIVEDERE QUANDO CI SARA' LA PROFILATURA
                PerStud perSN = new PerStud { AnnoInizio = annoInizio, AnnoFine= annoFine,Titolo= titolo,Descrizione= descrizione };
                PerStud perSV = Session["percorso"] as PerStud;
                dm.ModPerStudi(p.Matricola, perSV, perSN);
                ViewBag.Message = "Il percorso studi è stato modificato con successo, corri a controllare!";
            }else{
                ViewBag.Message ="Formato inserito non corretto";
                return View("MyPage");
            }
            return View($"MyPage");
        }
        [HttpPost]
        public ActionResult AddPerStud(string annoinizio, string annofine, string titolo, string descrizione) {
            int annoFine = int.Parse(annofine);
            int annoInizio = int.Parse(annoinizio);
            if (annoFine > annoInizio && titolo.Length > 0 && descrizione.Length > 0) {
                DomainModel dm = new DomainModel();
                Profilo p = Session["profile"] as Profilo; //ATTENZIONE DA RIVEDERE QUANDO CI SARA' LA PROFILATURA
                PerStud perS = new PerStud { AnnoInizio = annoInizio, AnnoFine = annoFine, Titolo = titolo, Descrizione = descrizione };
                dm.AddCvStudi(p.Matricola, perS);
                ViewBag.Message="Il percorso studi è stato inserito con successo nel tuo Curriculum!";
            } else{
                ViewBag.Message = "Formato inserito non corretto";
                return View("MyPage");
            }
            return View($"MyPage");
        }
    }
}