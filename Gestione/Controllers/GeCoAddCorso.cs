using System.Web.Mvc;
using System;
using Interfaces;
using Gestione.Models;
namespace Gestione.Controllers {
    public partial class HomeController : Controller {
        [HttpPost]
        public ActionResult AddCorso(string _nome, string _descrizione, DateTime _inizio, DateTime _fine){
            DomainModel db = new DomainModel();
            string prossimo;
            Corso temp = new Corso{Nome = _nome, Descrizione = _descrizione, Inizio = _inizio, Fine = _fine};
            try{ 
                db.AddCorso(temp);
                ViewBag.Message ="Corso inserito correttamente";
                prossimo = "AddCorso";    
            }catch(Exception){ 
                ViewBag.Message ="Qualcosa è andato storto";     
                prossimo = "AddCorso";
            }
            return View(prossimo);
        }
        public ActionResult AddCorso(){
            return View();
        }
    }
}