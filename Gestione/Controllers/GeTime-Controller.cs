﻿using Gestione.Models;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gestione.Controllers {
	public partial class HomeController{
		 public ActionResult VisualizzaGiorno() {
            return View();
        }
        [HttpPost]
        public ActionResult VisualizzaGiorno(DateTime data) {
            DomainModel dm = new DomainModel();
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
			DomainModel model = new DomainModel();
			if(commessa.Length==0)
				ViewBag.Message = "Inserire un nome di commessa";
			else{ 
				try{ 
					DTCommessa dTCommessa = model.CercaCommessa(commessa);
					if(dTCommessa != null){ 
						List<DTGiorno> giorni = model.GiorniCommessa(dTCommessa.Id, P.Matricola);
						if(giorni!=null && giorni.Count>0){
							ViewBag.NomeCommessa= dTCommessa.Nome;
							ViewBag.Giorni = giorni;
						}else
							ViewBag.Message = "Non è stato trovata nessuna commessa con questo nome";
				
					}
				}catch(Exception e){
					ViewBag.Message = "Errore del server";
				}
			}
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
			DomainModel dm = new DomainModel();
            DTGGiorno giorno = dm.VisualizzaGiorno(dateTime, P.Matricola);
			try{
                if (giorno != null) {
                    if (giorno.OreFerie > 0) {
                        ViewBag.Giorno = giorno;
                        ViewBag.Message = $"Il giorno {dateTime.ToString("yyyy-MM-dd")} eri in ferie";
                        return View("AddGiorno");
                    } else if (giorno.OreMalattia + giorno.OrePermesso + giorno.TotOreLavorate + (int)ore > 8) {
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
					DTCommessa commessa =dm.CercaCommessa(Commessa);
					if (commessa == null){
						ViewBag.Message ="Commessa non trovata";
						return View("AddGiorno");
					}
					dm.CompilaHLavoro(dateTime,(int) ore, commessa.Id, P.Matricola);
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
			}catch(Exception e){
                ViewBag.Message = "Ci sono gia presenti altri tipi di ore";
            }
			return View("AddGiorno");
		}
        public ActionResult Modifica() {
            return View();
        }
    }
    public class DTGiorno {
        public DateTime Data { get; set; }
        public int OreLavorate { get; set; }
    }
    public class DTCommessa {
        public int Id { get; set; }
        public string Descrizione { get; set; }
        public string Nome { get; set; }
        public int Capienza { get; set; }
        public int OreLavorate { get; set; }
        public DTCommessa(int id, string nome, string descrizione, int capienza, int oreLavorate) {
            Id = id;
            OreLavorate = oreLavorate;
            Nome = nome;
            Descrizione = descrizione;
            Capienza = capienza;
        }
    }
    public class DTGGiorno {
        public DateTime data { get; set; }
        public int TotOreLavorate { get; set; }
        public int OrePermesso { get; set; }
        public int OreMalattia { get; set; }
        public int OreFerie { get; set; }
        public DTGGiorno() { }
        private List<OreLavorate> OreLavorates = new List<OreLavorate>();
        public List<OreLavorate> OreLavorate { get { return OreLavorates; } }
    }
    public class OreLavorate {
        public string nome { get; set; }
        public int oreGiorno { get; set; }
        public string descrizione { get; set; }
    }
}