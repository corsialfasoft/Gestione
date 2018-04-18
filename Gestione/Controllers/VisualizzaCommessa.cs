﻿using Gestione.Models;
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
				DTCommessa dTCommessa = model.CercaCommessa(commessa);
				if(dTCommessa != null){ 
					List<DTGiorno> giorni = model.GiorniCommessa(dTCommessa.Id, P.Matricola);
					if(giorni!=null && giorni.Count>0){
						ViewBag.NomeCommessa= dTCommessa.Nome;
						ViewBag.Giorni = giorni;
					}else
						ViewBag.Message = "Non è stato trovata nessuna commessa con questo nome";
				
				}
			}
			return View("VisualizzaCommessa");
		}

	}
	
}