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
        public ActionResult ModEspLav(int annoInizioVEsp,int annoFineVEsp,string qualificaV,string descrizioneVEsp, int annoInizioEsp, int annoFineEsp, string qualifica, string descrizioneEsp){ 
            EspLav espV = new EspLav{ AnnoInizio=annoInizioVEsp,AnnoFine=annoFineVEsp,Qualifica=qualificaV,Descrizione=descrizioneVEsp};
            EspLav esp = new EspLav{ AnnoInizio=annoInizioEsp,AnnoFine=annoFineEsp,Qualifica=qualifica,Descrizione=descrizioneEsp};
            Profilo p = Session["profile"] as Profilo;
            return View($"DettCv?id={P.Matricola}");
        }
        public ActionResult AddEspLav(int annoInizioEsp, int annoFineEsp, string qualifica, string descrizioneEsp){
            EspLav esp = new EspLav{ AnnoInizio=annoInizioEsp,AnnoFine=annoFineEsp,Qualifica=qualifica,Descrizione=descrizioneEsp};
            Profilo p = Session["profile"] as Profilo;
            dm.AddEspLav(p.Matricola,esp);
            return View($"DettCv?id={P.Matricola}");
        }

        
    }
}