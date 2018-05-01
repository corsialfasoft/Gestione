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
		[HttpPost]
		public ActionResult AddCommessa(string commessa, string descrCommessa, string stimaOre) {
			try {
				int oreStimate = 0;
				bool oreBool = int.TryParse(stimaOre, out oreStimate);
				if (commessa != null && descrCommessa != null && oreBool != false && oreStimate > 0) {
					DTCommessa comm = dm.CercaCommessa(commessa);
					if (comm == null) {
						comm = new DTCommessa();
						comm.Nome = commessa;
						comm.Descrizione = descrCommessa;
						comm.Capienza = oreStimate;
						dm.AddCommessa(comm);
						ViewBag.AddCommessa = comm;
					} else {
						ViewBag.Mex = "Operazione non consentita, commessa già esistente";
					}
				} else
					ViewBag.Mex = "Operazione non consentita, campi non riempiti correttamente";
			} catch (Exception e) {
				ViewBag.Mex = e.Message;
			}
			return View("AddCommessa");
		}
	}
}