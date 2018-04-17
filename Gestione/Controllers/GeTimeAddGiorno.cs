using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Interfaces;
using Gestione.Models;

namespace Gestione.Controllers {
	public partial class HomeController : Controller {
		public ActionResult AddGiorno() {
			return View("AddGiorno");
		}
		[HttpPost]
		public ActionResult AddGiorno(DateTime dateTime, string tipoOre, int ore, string Commessa) {
			ViewBag.GeCoDataTime = dateTime;
			DomainModel dm = new DomainModel();
			try{ 
				if (tipoOre == "Ore di lavoro"){
					DTCommessa commessa =dm.CercaCommessa(Commessa);
					if (commessa == null){
						ViewBag.Message ="Commessa non trovata";
						return View("AddGiorno");
					}
					dm.CompilaHLavoro(dateTime, ore, commessa.Id, P.Matricola);
				} else if (tipoOre == "Ore di permesso"){
					HType tOre = (HType) 2;
					dm.Compila(dateTime, ore, tOre, P.Matricola);
				} else if (tipoOre == "Ore di malattia") {
					HType tOre = (HType) 3;
					try{ 
						dm.Compila(dateTime, 8, tOre, P.Matricola);
					}catch(Exception){
						ViewBag.Message = "Ci sono gia presenti altri tipi di ore";
						return View("AddGiorno");
					}
				} else {
					HType tOre = (HType) 1;
					dm.Compila(dateTime, ore, tOre, P.Matricola);
				}
				ViewBag.EsitoAddGiorno = ore + " " + tipoOre + " aggiunte!";
			}catch(Exception e){
				ViewBag.Message = "Errore del server";
			}
			return View("AddGiorno");
		}
	}
}