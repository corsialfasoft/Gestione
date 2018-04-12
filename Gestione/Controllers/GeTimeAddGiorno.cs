using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Interfaces;

namespace Gestione.Controllers {
	public partial class HomeController : Controller {
		public ActionResult AddGiorno() {
			return View("GeTimeAddGiorno");
		}
		[HttpPost]
		public ActionResult AddGiorno(string dataTime, string ore, string idCommessa, string idUtente) {
			ViewBag.GeCoDataTime = dataTime;
			ViewBag.GeCoOre = ore;
			ViewBag.GeCoIdCommessa = idCommessa;
			ViewBag.GeCoIdUtente = idUtente;

			/*CompilaHLavoro(DateTime data, int ore, int idCommessa, string idUtente)
					Compila(DateTime data, int ore, HType tipoOre, string idUtente)*/

			return View("GeTimeAddGiorno");
		}
	}
}