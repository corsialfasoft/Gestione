using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gestione.Models;
using Interfaces;

namespace Gestione.Controllers {
    public partial class HomeController : Controller {
       

        public ActionResult DettCv(string id){
            DomainModel dm = new DomainModel();
            ViewBag.CV = dm.Search(id);
            return View("DettaglioCurriculum");
        }
       // [HttpPost] Commentato per risolvere bug su elimina da lista
        public ActionResult EliminaCV(string id){ 
            CV temp = dm.Search(id); 
            string prossimo ;
            try{ 
                dm.EliminaCV(temp);
                ViewBag.Message = "Curriculum eliminato con successo";
				Profilo P = new Profilo();
				P.Ruolo="admin"; // ATTENZIONE SETTATO SUL CONTROLLER!!!!
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
        public ActionResult ModEspLav(int annoInizioV,int annoFineV,string qualificaV,string descrizioneV, int annoInizio, int annoFine, string qualifica, string descrizione, string matricola){ 
            EspLav espV = new EspLav{ AnnoInizio=annoInizioV,AnnoFine=annoFineV,Qualifica=qualificaV,Descrizione=descrizioneV};
            EspLav esp = new EspLav{ AnnoInizio=annoInizio,AnnoFine=annoFine,Qualifica=qualifica,Descrizione=descrizione};
            dm.ModEspLav(matricola,espV,esp);
            return View("DettalioCurriculum");
        }
        public ActionResult ModEspLav(int annoInizio, int annoFine, string qualifica, string descrizione, string matricola){
            EspLav esp = new EspLav{ AnnoInizio=annoInizio,AnnoFine=annoFine,Qualifica=qualifica,Descrizione=descrizione};
            dm.AddEspLav(matricola,esp);
            return View("DettalioCurriculum");
        }

        
    }
}