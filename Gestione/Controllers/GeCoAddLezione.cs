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
		public ActionResult AddLezione(String LezNome, string LezDescrizione, int LezDurata, int idCorso) {
           // int idCorso = (int)Session["idCorso"];
			//int idd = int.Parse(idCorso);
            DomainModel Dm = new DomainModel();
            Lezione lez = new Lezione();
            lez.Nome = LezNome;
            lez.Descrizione = LezDescrizione;
            lez.Durata = LezDurata;
            Dm.AddLezione(idCorso, lez);
			ViewBag.CorsoId= idCorso;
            return View();
        }
    }
}