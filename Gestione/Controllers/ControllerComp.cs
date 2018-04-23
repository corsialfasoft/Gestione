using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gestione.Models;
using Interfaces;

namespace Gestione.Controllers{
	 public partial class HomeController : Controller{
        [HttpPost]
        public ActionResult AddComp(string tipo,string livello){
			Competenza comp = new Competenza();
            Profilo p = Session["profile"] as Profilo; //ATTENZIONE DA RIVEDERE QUANDO CI SARA' LA PROFILATURA
			comp.Titolo=tipo;
			comp.Livello=int.Parse(livello);
			DomainModel dm = new  DomainModel();
			dm.AddCompetenze(p.Matricola,comp);
            ViewBag.CV = dm.Search(P.Matricola);
			ModelState.Clear();
			return View("DettaglioCurriculum");
        }
	 }
}