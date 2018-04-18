using Gestione.Models;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gestione.Controllers {
    public partial class HomeController{
        [HttpPost]
		public ActionResult AddLezione(string LezNome, string LezDescrizione, int LezDurata, int idCorso) {
			DomainModel Dm = new DomainModel();
			Lezione lez = new Lezione {
				Nome = LezNome,
				Descrizione = LezDescrizione,
				Durata = LezDurata
			};
			Dm.AddLezione(idCorso, lez);
			ViewBag.CorsoId= idCorso;
			ViewBag.Message = "Lezione aggiunta correttamente";
            return View();
        }
    }
}