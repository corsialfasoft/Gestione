using Gestione.Models;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gestione.Controllers {
	public partial class HomeController {

		public ActionResult AddCommessa() {
			return View("AddCommessa");
		}
		[HttpGet]       //AGGIUSTARE //FINIRE
		public ActionResult AddCommessa(string commessa, string descrCommessa, string stimaOre) {
			try {
				int oreStimate = 0;
				bool oreBool = int.TryParse(stimaOre, out oreStimate);
				if (commessa != null && descrCommessa != null && oreBool != false && oreStimate > 0) {
					DTCommessa comm = dm.CercaCommessa(commessa);
					if (comm != null) {
						dm.AddCommessa(comm);
						ViewBag.AddCommessa = comm;
						//ViewBag.EsitoAddGiorno = stateGiorno.Ore + " ore di lavoro aggiunte!";
						//Session["stateGiorno"] = null;
					} else {
						ViewBag.Message = "Operazione non consentita";
					}
				} else
					ViewBag.Message = "Operazione non consentita";
			} catch (Exception e) {
				ViewBag.Message = e.Message;
			}
			return View("AddCommessa");
		}
	}
}