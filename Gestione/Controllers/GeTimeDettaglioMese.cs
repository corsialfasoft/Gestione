using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gestione.Controllers {
    public partial class HomeController{
        [HttpPost]
        public ViewResult VisualizzaMese(string anno,string mese) { 
            if(int.TryParse(anno,out int annoI) && int.TryParse(mese,out int meseI)) {
               ViewBag.Mese = dm.DettaglioMese(annoI,meseI, P.Matricola);
                if (ViewBag.Mese.Count > 0) {
                    ViewBag.Year = annoI;
                    ViewBag.Month = meseI;
                    ViewBag.TOreL = ((List<DTGiornoDMese>)ViewBag.Mese).Sum<DTGiornoDMese>(dTGiornoDMese => dTGiornoDMese.TotOreLavorate);
                    ViewBag.TOreP = ((List<DTGiornoDMese>)ViewBag.Mese).Sum<DTGiornoDMese>(dTGiornoDMese => dTGiornoDMese.OrePermesso);
                    ViewBag.TOreM = ((List<DTGiornoDMese>)ViewBag.Mese).Sum<DTGiornoDMese>(dTGiornoDMese => dTGiornoDMese.OreMalattia);
                    ViewBag.TOreF = ((List<DTGiornoDMese>)ViewBag.Mese).Sum<DTGiornoDMese>(dTGiornoDMese => dTGiornoDMese.OreFerie);
                }
            } else
                ViewBag.Message ="Inserire anno e mese";
            return View();
        }
        public ViewResult VisualizzaMese() { 
            return View();
        }
        [HttpGet]
        public ViewResult AddGiornoSelectCommessa(string nome) {
             StateGiorno stateGiorno = Session["stateGiorno"] as StateGiorno;
            if (stateGiorno != null && nome!="") {
                List<DTCommessa> commesse= dm.CercaCommessa(nome);
                if(commesse.Count == 1) {
                    dm.CompilaHLavoro(stateGiorno.Data, stateGiorno.Ore, commesse[0].Id, P.Matricola);
                    ViewBag.GeCoDataTime = stateGiorno.Data;//.ToString("yyyy-MM-dd");
                    ViewBag.EsitoAddGiorno = stateGiorno.Ore + " ore di lavoro aggiunte!";
                    Session["stateGiorno"] = null;
                } else {
                    ViewBag.Message = "Operazione non consentita";
                }
            }else
                ViewBag.Message="Operazione non consentita";
            return View("AddGiorno");
        }
        public class StateGiorno {
            public DateTime Data { get; set; }
            public int Ore { get; set; }
        }
    }
}