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
    }
}