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
        public ActionResult ModEspLav(int annoInizioV,int annoFineV,string qualificaV,string descrizioneV, int annoInizio, int annoFine, string qualifica, string descrizione, string matricola){ 
            EspLav espV = new EspLav{ AnnoInizio=annoInizioV,AnnoFine=annoFineV,Qualifica=qualificaV,Descrizione=descrizioneV};
            EspLav esp = new EspLav{ AnnoInizio=annoInizio,AnnoFine=annoFine,Qualifica=qualifica,Descrizione=descrizione};
            dm.ModEspLav(matricola,espV,esp);
            return View("DettalioCurriculum");
        }
        public ActionResult ModEspLav(int annoInizio, int annoFine, string qualifica, string descrizione, string matricola){
            EspLav esp = new EspLav{ AnnoInizio=annoInizio,AnnoFine=annoFine,Qualifica=qualifica,Descrizione=descrizione};
            dm.AddEspLav(matricola,esp);
            return View("DettalioCurriculum");
        }

        
    }
}