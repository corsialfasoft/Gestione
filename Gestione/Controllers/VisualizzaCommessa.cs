using Gestione.Models;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gestione.Controllers {
	public partial class HomeController{
		public ActionResult VisualizzaCommessa() {
			return View("VisualizzaCommessa");
		}
		[HttpPost]
		public ActionResult VisualizzaCommessa(string commessa) {
			DomainModel model = new DomainModel();
			if(commessa.Length==0)
				ViewBag.Message = "Inserire un nome di commessa";
			else{ 
				try{ 
					DTCommessa dTCommessa = model.CercaCommessa(commessa);
					if(dTCommessa != null){ 
						List<DTGiorno> giorni = model.GiorniCommessa(dTCommessa.Id, P.Matricola);
						if(giorni!=null && giorni.Count>0){
							ViewBag.NomeCommessa= dTCommessa.Nome;
							ViewBag.Giorni = giorni;
						}else
							ViewBag.Message = "Non hai mai lavorato su questa commessa!";
					}
				}catch(Exception e){
					ViewBag.Message = "Errore del server";
				}
			}
			return View("VisualizzaCommessa");
		}
        public ActionResult GeTimeHome() {
            return View();
        }

    }
	
}