using System.Web.Mvc;
using System;
using Interfaces;
using Gestione.Models;
namespace Gestione.Controllers {
    public partial class HomeController : Controller {
        [HttpPost]
        public ActionResult AddCorso(string _nome, string _descrizione, DateTime _inizio, DateTime _fine){
            DomainModel db = new DomainModel();
            Corso temp = new Corso{Nome = _nome, Descrizione = _descrizione, Inizio = _inizio, Fine = _fine};
            db.AddCorso(temp);
            return View("ListaCorsi");
        }
        public ActionResult AddCorso(){
            return View();
        }
    }
}