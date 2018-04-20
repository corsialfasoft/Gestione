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
        public ActionResult ModPerStudi(int annoInizioV,int annoFineV, string titoloV,string descrizioneV,
            int annoInizio, int annoFine, string titolo, string descrizione,string matricola) {
            if(annoFine> annoInizio && titolo.Length>0 && descrizione.Length > 0){
			    DomainModel dm = new DomainModel();
                PerStud perSV = new PerStud { AnnoInizio = annoInizioV, AnnoFine= annoFineV,Titolo= titoloV,Descrizione= descrizioneV };
                PerStud perSN = new PerStud { AnnoInizio = annoInizio, AnnoFine= annoFine,Titolo= titolo,Descrizione= descrizione };
                dm.ModPerStudi(matricola, perSV, perSN);
            }else
                ViewBag.Message ="Formato inserito non corretto";
            return View("DettaglioCurriculum");
        }
        [HttpPost]
        public ActionResult AddPerStudi(int annoInizio, int annoFine, string titolo, string descrizione,string matricola) {
            if (annoFine > annoInizio && titolo.Length > 0 && descrizione.Length > 0) {
                DomainModel dm = new DomainModel();
                PerStud perS = new PerStud { AnnoInizio = annoInizio, AnnoFine = annoFine, Titolo = titolo, Descrizione = descrizione };
                dm.AddCvStudi(matricola, perS);
            } else
                ViewBag.Message = "Formato inserito non corretto";
            return View("DettaglioCurriculum");
        }
    }
}