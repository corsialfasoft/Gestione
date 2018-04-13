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
        [HttpPost]
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

        private CV InitCV(string nome,string cognome,string eta,
            string email,string residenza,string telefono,string annoinizio,string annofine,
            string titolo, string descrizione, string annoinizioesp, string annofinesp,string qualifica,
            string descrizionesp,string tipo,string livello) {
                try{
                    CV cv = new CV();
                    cv.nome = nome;
                    cv.cognome = cognome;
                    cv.eta = int.Parse(eta);
                    cv.email = email;
                    cv.residenza = residenza;
                    cv.telefono = telefono;
                    EspLav esp = new EspLav();
                    esp.AnnoInizio = int.Parse(annoinizioesp);
                    esp.AnnoFine = int.Parse(annofinesp);
                    esp.qualifica = qualifica;
                    esp.descrizione = descrizionesp;
                    cv.esperienze.Add(esp);
                    PerStud percorso = new PerStud();
                    percorso.AnnoInizio = int.Parse(annoinizio);
                    percorso.AnnoFine = int.Parse(annofine);
                    percorso.titolo = titolo;
                    percorso.descrizione = descrizione;
                    cv.percorsostudi.Add(percorso);
                    Competenza comp = new Competenza();
                    comp.titolo = tipo;
                    comp.livello = int.Parse(livello);
                    cv.competenze.Add(comp);
                    return cv;
            } catch(Exception e) {
                throw e;
            }
        }
        
        [HttpPost]
        public ActionResult AggiungiCurriculum(string nome,string cognome,string eta,
            string email,string residenza,string telefono,string annoinizio,string annofine,
            string titolo, string descrizione, string annoinizioesp, string annofinesp,string qualifica,
            string descrizionesp,string tipo,string livello
            ) {
            try{
                if (!String.IsNullOrEmpty(nome) && !String.IsNullOrEmpty(cognome)
                    && !String.IsNullOrEmpty(eta) && !String.IsNullOrEmpty(email)
                    && !String.IsNullOrEmpty(telefono) && !String.IsNullOrEmpty(residenza)){
                    if(int.TryParse(eta, out int Eta)){
                        dm.AggiungiCV(InitCV(nome,cognome,eta,email,residenza,telefono,annoinizio,
                            annofine,titolo,descrizione,annoinizioesp,annofinesp,qualifica,descrizionesp,tipo,livello));
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
            } catch(Exception) {
                ViewBag.Message = "Qualcosa è andato storto";
                return View("DettaglioCurriculum");
            }
        }

        [HttpPost]
        public ActionResult ModificaCurriculum(string nome,string cognome,string eta,
            string email,string residenza,string telefono,string annoinizio,string annofine,
            string titolo, string descrizione, string annoinizioesp, string annofinesp,string qualifica,
            string descrizionesp,string tipo,string livello,string id
            ) {
            try{
                if (!String.IsNullOrEmpty(nome) && !String.IsNullOrEmpty(cognome)
                    && !String.IsNullOrEmpty(eta) && !String.IsNullOrEmpty(email)
                    && !String.IsNullOrEmpty(telefono) && !String.IsNullOrEmpty(residenza)){
                    if(int.TryParse(eta, out int Eta)){
                        dm.ModificaCV(InitCV(ViewBag.CV.nome,ViewBag.CV.cognome,
                            ViewBag.CV.eta,ViewBag.CV.email,ViewBag.CV.residenza,ViewBag.CV.telefono,ViewBag.CV.annoinizio,
                            ViewBag.CV.annofine,ViewBag.CV.titolo,ViewBag.CV.descrizione,ViewBag.CV.annoinizioesp,
                            ViewBag.CV.annofinesp,ViewBag.CV.qualifica,ViewBag.CV.descrizionesp,ViewBag.CV.tipo,ViewBag.CV.livello),
                            InitCV(nome,cognome,eta,email,residenza,telefono,annoinizio,annofine,titolo,descrizione,
                            annoinizioesp,annofinesp,qualifica,descrizionesp,tipo,livello));
                        ViewBag.Message = "Curriculum Modificato";
                        return View("MyPage");
                    } else {
		        	    CV trovato = dm.Search(P.Matricola);
                         ViewBag.CV = trovato;
                        ViewBag.Message = "Eta' non valida";
                        return View("DettaglioCurriculum");
                    }
                } else {
			        CV trovato = dm.Search(P.Matricola);
                    ViewBag.CV = trovato;
                    ViewBag.Message = "Campi obbligatori da inserire...";
                    return View("DettaglioCurriculum");
                }
            } catch(Exception) {
			    CV trovato = dm.Search(P.Matricola);
                ViewBag.CV = trovato;
                ViewBag.Message = "Qualcosa è andato storto.";
                return View("DettaglioCurriculum");
            }
        }
    }
}