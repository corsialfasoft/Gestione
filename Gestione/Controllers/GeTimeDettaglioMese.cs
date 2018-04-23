using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gestione.Controllers {
    public partial class HomeController{
        [HttpPost]
        public ViewResult VisualizzaMese(string anno,string mese) { 
            if(anno !="" && mese !="") {
               ViewBag.Mese = dm.DettaglioMese(int.Parse(anno),int.Parse(mese), P.Matricola);
            } else
                ViewBag.Message ="Inserire anno e mese";
            return View();
        }
        public ViewResult VisualizzaMese() { 
            return View();
        }
    }
}