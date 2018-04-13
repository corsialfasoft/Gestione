﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gestione.Models;
using Interfaces;

namespace Gestione.Controllers {
    public class Profilo {
        public string Matricola { get; set; }
        public string Ruolo { get; set; }
        public List<String> Funzioni { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }

        public Profilo(string matricola, string ruolo, List<String> funzioni, string nome, string cognome) {
            Matricola = matricola;
            Ruolo = ruolo;
            Funzioni = funzioni;
            Nome = nome;
            Cognome = cognome;
        }
        public Profilo(){ }
    }
    public partial class HomeController : Controller {
        DomainModel dm = new DomainModel();
        Profilo P;
        public HomeController() {
            P = new Profilo{Matricola="801130"};

        }
        public ActionResult Index() {
            return View ();
        }
        public ActionResult DettaglioCurriculum(){
            return View();
        }
        public ActionResult EliminaCV(string idCV){ 
            CV temp = dm.Search(idCV); 
            string prossimo ;
            try{ 
                dm.EliminaCV(temp);
                ViewBag.Message = "Curriculum eliminato con successo";
                if(P.Ruolo=="admin"){ 
                    prossimo = "ListaCurriculum";
                }else{
                    prossimo = "MyPage";  
                }
            }catch(Exception){ 
                ViewBag.Message = "Non siamo riusciti a eliminare il curriculum selezionato"; 
                prossimo = "MyPage";
            }
            return View(prossimo);
        }
        [HttpPost]
        public ActionResult AggiungiCurriculum(string nome,string cognome,string eta,
            string email,string residenza,string telefono,string annoinizio,string annofine,
            string titolo, string descrizione, string annoinizioesp, string annofinesp,string qualifica,
            string descrizionesp,string tipo,string livello
            ) {
            if (!String.IsNullOrEmpty(nome) && !String.IsNullOrEmpty(cognome)
                && !String.IsNullOrEmpty(eta) && !String.IsNullOrEmpty(email)
                && !String.IsNullOrEmpty(telefono) && !String.IsNullOrEmpty(residenza)){
                if(int.TryParse(eta, out int Eta)){
                    CV cv = new CV();
                    cv.nome = nome;
                    cv.cognome = cognome;
                    cv.eta = int.Parse(eta);
                    cv.email = email;
                    cv.residenza = residenza;
                    cv.telefono = telefono;
                    EspLav esp = new EspLav();
                    esp.AnnoInizio = Convert.ToDateTime(annoinizioesp);
                    esp.AnnoFine = Convert.ToDateTime(annofinesp);
                    esp.qualifica = qualifica;
                    esp.descrizione = descrizionesp;
                    cv.esperienze.Add(esp);
                    PerStud percorso = new PerStud();
                    percorso.AnnoInizio = Convert.ToDateTime(annoinizio);
                    percorso.AnnoFine = Convert.ToDateTime(annofine);
                    percorso.titolo = titolo;
                    percorso.descrizione = descrizione;
                    cv.percorsostudi.Add(percorso);
                    Competenza comp = new Competenza();
                    comp.titolo = tipo;
                    comp.livello = int.Parse(livello);
                    cv.competenze.Add(comp);
                    dm.AggiungiCV(cv);
                    ViewBag.Message = "Curriculum Aggiunto";
                    return View("MyPage");
                } else {
                    ViewBag.Message = "Eta' non valida";
                    return View("DettaglioCurriculum");
                }
            } else{
                ViewBag.Message = "Campi obbligatori da inserire...";
                return View("DettaglioCurriculum");
            }
        }
    }
}