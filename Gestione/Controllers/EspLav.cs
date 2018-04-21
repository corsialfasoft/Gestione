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
        public ActionResult ModEspLav(int annoInizioEsp, int annoFineEsp, string qualifica, string descrizioneEsp){ 
            //EspLav espV = new EspLav{ AnnoInizio=annoInizioVEsp,AnnoFine=annoFineVEsp,Qualifica=qualificaV,Descrizione=descrizioneVEsp};
            EspLav esp = new EspLav{ AnnoInizio=annoInizioEsp,AnnoFine=annoFineEsp,Qualifica=qualifica,Descrizione=descrizioneEsp};
            Profilo p = Session["profile"] as Profilo; //ATTENZIONE DA RIVEDERE QUANDO CI SARA' LA PROFILATURA
            EspLav espV = Session["esperienza"] as EspLav;
            dm.ModEspLav(p.Matricola,espV,esp);
            ViewBag.Message = "Funziona";
            return View($"MyPage");
        }

        public ActionResult AddEspLav(int annoinizioesp, int annofinesp, string qualifica, string descrizionesp){
            EspLav esp = new EspLav{ AnnoInizio=annoinizioesp,AnnoFine= annofinesp,Qualifica=qualifica,Descrizione=descrizionesp};
            Profilo p = Session["profile"] as Profilo; //ATTENZIONE DA RIVEDERE QUANDO CI SARA' LA PROFILATURA
            dm.AddEspLav(p.Matricola,esp);
            ViewBag.Message="Esperienza aggiunta nel curriculum,corri a controllare!";
            return View($"MyPage");
        }

        
    }
}