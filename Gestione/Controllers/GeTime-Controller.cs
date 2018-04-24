using Gestione.Models;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gestione.Controllers {
	public partial class HomeController{
        DomainModel dm = new DomainModel();
 
		public ActionResult VisualizzaGiorno() {
            return View();
        }
        [HttpPost]
        public ActionResult VisualizzaGiorno(DateTime data) {
            DTGGiorno giorno = dm.VisualizzaGiorno(data, P.Matricola);
            if (giorno!=null) {
                ViewBag.giorno = giorno;
            } else {
                ViewBag.Message = "Data non trovata!";
            }
            return View();
        }
		public ActionResult VisualizzaCommessa() {
			return View("VisualizzaCommessa");
		}
		[HttpPost]
		public ActionResult VisualizzaCommessa(string commessa) {
			if(commessa.Length==0)
				ViewBag.Message = "Inserire il nome della commessa!";
			else{ 
				try{
					List<DTCommessa> dTCommesse = dm.CercaCommessa(commessa);
					if(dTCommesse.Count>0) {
                        if (dTCommesse.Count==1) {
                            List<DTGiorno> giorni = dm.GiorniCommessa(dTCommesse[0].Id, P.Matricola);
						    ViewBag.NomeCommessa= dTCommesse[0].Nome;
                            ViewBag.Commesse = dTCommesse;
							ViewBag.Giorni = giorni;
                        } else {
                            ViewBag.Commesse = dTCommesse;
                        }
                    } else {
						ViewBag.Message = "Non è stato trovata nessuna commessa con questo nome";
                    }
				} catch(Exception) {
					ViewBag.Message = "Errore del server";
				}
			}
			return View("VisualizzaCommessa");
		}
        [HttpGet]
        public ActionResult DettaglioCommessa(string nome) {
            ViewBag.Commesse = VisualizzaCommessa(nome);
            return View("VisualizzaCommessa");
        }
        public ActionResult GeTimeHome() {
            return View();
        }
		public ActionResult AddGiorno() {
			return View("AddGiorno");
		}
		[HttpPost]
		public ActionResult AddGiorno(DateTime dateTime, string tipoOre, int ?ore, string Commessa) {
			ViewBag.GeCoDataTime = dateTime;
            DTGGiorno giorno = dm.VisualizzaGiorno(dateTime, P.Matricola);
			try{
                if (giorno != null) {
                    if (giorno.OreFerie > 0) {
                        ViewBag.Giorno = giorno;
                        ViewBag.Message = $"Il giorno {dateTime.ToString("yyyy-MM-dd")} eri in ferie";
                        return View("AddGiorno");
                    } else if (giorno.OreMalattia + giorno.OrePermesso + giorno.TotOreLavorate + (tipoOre != "Ore di ferie" ?  ore==null ? 0 : ore : 8) > 8) {
                        ViewBag.Giorno = giorno;
                        ViewBag.Message = $"Il giorno {dateTime.ToString("yyyy-MM-dd")} stai superando le 8 ore";
                        return View("AddGiorno");
                    }
                }
				if (tipoOre == "Ore di lavoro"){
                    if (ore == null) {
                        ViewBag.Message = "Inserire le ore";
                        return View();
                    }
					List<DTCommessa> commesse = dm.CercaCommessa(Commessa);
					if (commesse == null){
						ViewBag.Message ="Commessa non trovata";
						return View("AddGiorno");
					}
					dm.CompilaHLavoro(dateTime,(int) ore, commesse[0].Id, P.Matricola);
				} else if (tipoOre == "Ore di permesso"){
                    if (ore == null) {
                        ViewBag.Message = "Inserire le ore";
                        return View();
                    }
                    HType tOre = (HType) 2;
					dm.Compila(dateTime, (int)ore, tOre, P.Matricola);
				} else if (tipoOre == "Ore di malattia") {
                    if (ore == null) {
                        ViewBag.Message = "Inserire le ore";
                        return View();
                    }
                    HType tOre = (HType) 1;
				    dm.Compila(dateTime, (int)ore, tOre, P.Matricola);
				} else {
					HType tOre = (HType) 3;
                    dm.Compila(dateTime, 8, tOre, P.Matricola);
				}
				ViewBag.EsitoAddGiorno = ore + " " + tipoOre + " aggiunte!";
			}catch(Exception){
                ViewBag.Message = "Errore server";
            }
			return View("AddGiorno");
		}
        public ActionResult Modifica() {
            return View();
        }
    }
}