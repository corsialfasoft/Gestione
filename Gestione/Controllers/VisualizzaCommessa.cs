using Gestione.Models;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gestione.Controllers {
	public partial class HomeController{
		public ActionResult VisulizzaCommessa() {
			return View("VisulizzaCommessa");
		}
		[HttpPost]
		public ActionResult VisulizzaCommessa(string nCom) {
			DomainModel model = new DomainModel();
			Commessa commessa = model.CercaCommessa(nCom);
			if(commessa!=null){ 
				List<Giorno> giorni = model.GiorniCommessa(commessa.Id, idUtente);
				if(giorni!=null && giorni.Count>0){
					ViewBag.Giorni = giorni;
				}else
					ViewBag.Message = "Non è stato trovata nessuna commessa con questo nome";
			}
			return View("VisulizzaCommessa");
		}

	}
}