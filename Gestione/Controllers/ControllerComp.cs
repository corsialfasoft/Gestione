using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gestione.Models;
using Interfaces;

namespace Gestione.Controllers{
	 public partial class HomeController : Controller{
		public void ModComp(string tipoV,string livelloV , string tipo,string livello,string matricola){
			DomainModel dm = new DomainModel();
			Competenza daMod = new Competenza();
			daMod.Titolo=tipoV;
			daMod.Livello=int.Parse(livelloV);
			Competenza Mod = new Competenza();
			Mod.Titolo=tipo;
			Mod.Livello=int.Parse(livello);
			dm.ModComp(daMod,Mod,matricola);
		}
		public void AddComp(string tipo,string livello , string matricola){
			Competenza comp = new Competenza();
			comp.Titolo=tipo;
			comp.Livello=int.Parse(livello);
			DomainModel dm = new  DomainModel();
			dm.AddCompetenze(matricola,comp);
		}
	 }
}