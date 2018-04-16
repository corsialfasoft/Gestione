using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Interfaces;
using  Gestione.Models;

namespace Gestione.Controllers {
	public partial class HomeController : Controller {
		public ActionResult ListaCurriculum(){
			return View();
        }
	}
}