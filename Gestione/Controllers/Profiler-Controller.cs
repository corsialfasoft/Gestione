using System;
using System.Web.Mvc;
using Gestione.Models;

namespace Gestione.Controllers {
    public partial class HomeController : Controller {
        public ActionResult Login() {
            return View();
        }
        public ActionResult Iscriviti() {
            return View();
        }
        public ActionResult Logout() {
            Session["profile"] = null;
            return View("Index");
        }

        [HttpPost]
        public ActionResult Iscriviti(string nome,string cognome,string usr,string psw) {
            ProfileModel pm = new ProfileModel();
            try{
                pm.IscrizioneAlPortale(nome,cognome,usr,psw);
                ViewBag.Message = "Utente registrato con successo";
                return View("Index");
            } catch(Exception) {
                ViewBag.Message = "User esistente, inserire un user valido";
                return View("Iscriviti");
            }
        }

        [HttpPost]
        public ActionResult Login(string usr, string psw) {
            ProfileModel pm = new ProfileModel();
            try{
                Session["profile"] = pm.GetProfile(usr,psw);
                return View("MyPage");
            } catch(Exception) {
                ViewBag.Message = "Login e password non validi";
                return View("Login");
            }
        }
    }
}