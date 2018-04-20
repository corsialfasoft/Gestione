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
        public ActionResult ModPerStud(int annoInizioV,int annoFineV, string titoloV,string descrizioneV,
            int annoInizio, int annoFine, string titolo, string descrizione) {
            if(annoFine> annoInizio && titolo.Length>0 && descrizione.Length > 0){
			    DomainModel dm = new DomainModel();
                Profilo p = Session["profile"] as Profilo;
                PerStud perSV = new PerStud { AnnoInizio = annoInizioV, AnnoFine= annoFineV,Titolo= titoloV,Descrizione= descrizioneV };
                PerStud perSN = new PerStud { AnnoInizio = annoInizio, AnnoFine= annoFine,Titolo= titolo,Descrizione= descrizione };
                dm.ModPerStudi(p.Matricola, perSV, perSN);
            }else
                ViewBag.Message ="Formato inserito non corretto";
            return View($"DettCv?id={P.Matricola}");
        }
        [HttpPost]
        public ActionResult AddPerStud(int annoInizio, int annoFine, string titolo, string descrizione) {
            if (annoFine > annoInizio && titolo.Length > 0 && descrizione.Length > 0) {
                DomainModel dm = new DomainModel();
                Profilo p = Session["profile"] as Profilo;
                PerStud perS = new PerStud { AnnoInizio = annoInizio, AnnoFine = annoFine, Titolo = titolo, Descrizione = descrizione };
                dm.AddCvStudi(p.Matricola, perS);
            } else
                ViewBag.Message = "Formato inserito non corretto";
            return View($"DettCv?id={P.Matricola}");
        }
    }
}