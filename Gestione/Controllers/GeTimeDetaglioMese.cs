using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gestione.Controllers {
    public partial class HomeController{
        [HttpPost]
        public ViewResult DetaglioMese(int anno,int mese) { 
            if(anno > 0 && mese >0) {
               ViewBag.Mese = dm.DettaglioMese(anno,mese);
            } else
                ViewBag.Message ="Inserire anno e mese";
            return View();
        }
    }
}