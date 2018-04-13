using Gestione.Models;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gestione.Controllers {
	public partial class HomeController{
		public ActionResult VisualizzaCommessa() {
			return View("VisualizzaCommessa");
		}
		[HttpPost]
		public ActionResult VisualizzaCommessa(string nCom) {
			DomainModel model = new DomainModel();
			Commessa commessa = model.CercaCommessa(nCom);
			if(commessa!=null){ 
				List<Giorno> giorni = model.GiorniCommessa(commessa.Id, P.Matricola);
				if(giorni!=null && giorni.Count>0){
					List<DTGiorno> dTGiorni = new List<DTGiorno>();
					foreach(Giorno giorno in giorni){
						if(giorno.OreLavorate!=null && giorno.OreLavorate.Count>0){ 
							dTGiorni.Add(new DTGiorno{ Data=giorno.Data,OreLavorate= giorno.OreLavorate[0].Ore});
						}
					}
					ViewBag.NomeCommessa= commessa.Nome;
					ViewBag.Giorni = dTGiorni;
				}else
					ViewBag.Message = "Non è stato trovata nessuna commessa con questo nome";
				
			}
			
			return View("VisualizzaCommessa");
		}

	}
	public class DTGiorno{
		public DateTime Data { get;set;}
		public int OreLavorate{ get;set;}
	}
}