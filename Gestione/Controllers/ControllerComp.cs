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
		public ActionResult ModComp(string tipoV,string livelloV , string tipo,string livello){
			DomainModel dm = new DomainModel();
			Competenza daMod = new Competenza();
            Profilo p = Session["profile"] as Profilo;
			daMod.Titolo=tipoV;
			daMod.Livello=int.Parse(livelloV);
			Competenza Mod = new Competenza();
			Mod.Titolo=tipo;
			Mod.Livello=int.Parse(livello);
			dm.ModComp(daMod,Mod,p.Matricola);
            return View($"MyPage");
        }
        [HttpPost]
        public ActionResult AddComp(string tipo,string livello){
			Competenza comp = new Competenza();
            Profilo p = Session["profile"] as Profilo;
			comp.Titolo=tipo;
			comp.Livello=int.Parse(livello);
			DomainModel dm = new  DomainModel();
			dm.AddCompetenze(p.Matricola,comp);
            return View($"MyPage");
        }
	 }
}