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
			return View("GeTimeAddGiorno");
		}
		[HttpPost]
		public ActionResult AddGiorno(DateTime dataTime, string tipoOre, int ore, string Commessa) {
			ViewBag.GeCoDataTime = dataTime;
			ViewBag.GeCoDataType = tipoOre;
			ViewBag.GeCoOre = ore;
			DomainModel dm = new DomainModel();
			//ViewBag.GeCoIdCommessa = dm.CercaCommessa(Commessa).Id;
			int u = 8;

			if (tipoOre == "Ore di lavoro"){
				//dm.CompilaHLavoro(dataTime, ore, ViewBag.GeCoIdCommessa, "idUtente");
				dm.CompilaHLavoro(dataTime, ore, u, "idUtente");
			} else if (tipoOre == "Ore di permesso"){
				HType tOre = (HType) 2;
				dm.Compila(dataTime, ore, tOre, "idUtente");
			} else if (tipoOre == "Ore di malattia") {
				HType tOre = (HType) 3;
				dm.Compila(dataTime, ore, tOre, "idUtente");
			} else {
				HType tOre = (HType) 1;
				dm.Compila(dataTime, ore, tOre, "idUtente");
			}
			//ViewBag.GeCoIdCommessa = 3;
			ViewBag.EsitoAddGiorno = ore + " Ore aggiunte!";

			/*< option value = "Ore di ferie" >

				 < option value = "Ore di malattia" >*/

			//ViewBag.GeCoIdCommessa = Commessa;
			//ViewBag.GeCoIdUtente = idUtente;

			/*CompilaHLavoro(DateTime data, int ore, int idCommessa, string idUtente)
					Compila(DateTime data, int ore, HType tipoOre, string idUtente)*/

			return View("GeTimeAddGiorno");
		}
	}
}