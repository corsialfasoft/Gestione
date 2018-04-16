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
			if(dm.CercaCommessa(Commessa) != null){
				if (tipoOre == "Ore di lavoro"){
					dm.CompilaHLavoro(dateTime, ore, dm.CercaCommessa(Commessa).Id, P.Matricola);
				} else if (tipoOre == "Ore di permesso"){
					HType tOre = (HType) 2;
					dm.Compila(dateTime, ore, tOre, P.Matricola);
				} else if (tipoOre == "Ore di malattia") {
					HType tOre = (HType) 3;
					dm.Compila(dateTime, ore, tOre, P.Matricola);
				} else {
					HType tOre = (HType) 1;
					dm.Compila(dateTime, ore, tOre, P.Matricola);
				}
				ViewBag.EsitoAddGiorno = ore + " " + tipoOre + " aggiunte!";
			}
			return View("AddGiorno");
		}
	}
}