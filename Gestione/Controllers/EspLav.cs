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
        public ActionResult ModEspLav(int annoInizioVEsp,int annoFineVEsp,string qualificaV,string descrizioneVEsp, int annoInizioEsp, int annoFineEsp, string qualifica, string descrizioneEsp, string matricola){ 
            EspLav espV = new EspLav{ AnnoInizio=annoInizioVEsp,AnnoFine=annoFineVEsp,Qualifica=qualificaV,Descrizione=descrizioneVEsp};
            EspLav esp = new EspLav{ AnnoInizio=annoInizioEsp,AnnoFine=annoFineEsp,Qualifica=qualifica,Descrizione=descrizioneEsp};
            dm.ModEspLav(matricola,espV,esp);
            return View("DettalioCurriculum");
        }
        public ActionResult AddEspLav(int annoInizioEsp, int annoFineEsp, string qualifica, string descrizioneEsp, string matricola){
            EspLav esp = new EspLav{ AnnoInizio=annoInizioEsp,AnnoFine=annoFineEsp,Qualifica=qualifica,Descrizione=descrizioneEsp};
            dm.AddEspLav(matricola,esp);
            return View("DettalioCurriculum");
        }

        
    }
}