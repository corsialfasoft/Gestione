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
		public ActionResult VisualizzaCommessa(string nCom) {
			DomainModel model = new DomainModel();
			DTCommessa commessa = model.CercaCommessa(nCom);
			if(commessa!=null){ 
				List<DTGiorno> giorni = model.GiorniCommessa(commessa.Id, P.Matricola);
				if(giorni!=null && giorni.Count>0){
					ViewBag.NomeCommessa= commessa.Nome;
					ViewBag.Giorni = giorni;
				}else
					ViewBag.Message = "Non è stato trovata nessuna commessa con questo nome";
				
			}
			return View("VisualizzaCommessa");
		}

	}
	
}