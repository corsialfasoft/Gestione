using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gestione.Models;
using Interfaces;

namespace Gestione.Controllers {
    
    public partial class HomeController : Controller {
        public ActionResult Login() {
            return View();
        }
        public ActionResult Iscriviti() {
            return View();
        }

        [HttpPost]
        public ActionResult Iscriviti(string nome,string cognome,string usr,string psw) {
            DomainModel dm = new DomainModel();
            try{
                //dm.IscrizioneAlPortale(nome,cognome,usr,psw);
                ViewBag.Message = "Utente registrato con successo";
                return View("Index");
            } catch(Exception) {
                ViewBag.Message = "User esistente, inserire un user valido";
                return View("Iscriviti");
            }
        }

        [HttpPost]
        public ActionResult Login(string usr, string psw) {
            DomainModel dm = new DomainModel();
            try{
                //Session["profile"] = dm.GetProfile(usr,psw);
                return View("MyPage");
            } catch(Exception) {
                ViewBag.Message = "Login e password non validi";
                return View("Login");
            }
        }
    }
}