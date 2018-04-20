using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gestione;
using Gestione.Controllers;

namespace Gestione.Tests.Controllers {
    public partial class HomeControllerTest {

        [TestMethod]
        public void TestCompilaOreLavrative() {
            HomeController controller = new HomeController(new Profilo(Guid.NewGuid().ToString().Substring(0, 10), "studente",null,"test", "OreLavrative"));
            ViewResult result = controller.AddGiorno(DateTime.Today, "Ore di lavoro",2, "GeTime TestCompilaOreL") as ViewResult;
            Assert.IsNull(result.ViewBag.Message);
            Assert.IsNotNull(result.ViewBag.EsitoAddGiorno);
            result = controller.VisualizzaGiorno(DateTime.Today) as ViewResult;
            Assert.IsTrue(result.ViewBag.giorno.TotOreLavorate==2);
        }

        [TestMethod]
        public void TestCompilaNonLavrative() {
            HomeController controller = new HomeController(new Profilo(Guid.NewGuid().ToString().Substring(0,10), "studente", null, "test", "OreNonLavrative"));
            ViewResult result = controller.AddGiorno(DateTime.Today, "Ore di permesso", 2, "") as ViewResult;
            Assert.IsNull(result.ViewBag.Message);
            Assert.IsNotNull(result.ViewBag.EsitoAddGiorno);
            result = controller.AddGiorno(DateTime.Today, "Ore di malattia", 2, "") as ViewResult;
            Assert.IsNull(result.ViewBag.Message);
            Assert.IsNotNull(result.ViewBag.EsitoAddGiorno);
            result = controller.VisualizzaGiorno(DateTime.Today) as ViewResult;
            Assert.IsTrue(result.ViewBag.giorno.OrePermesso == 2);
            Assert.IsTrue(result.ViewBag.giorno.OreMalattia == 2);
        }
        [TestMethod]
        public void TestVisualizzaCommessa() {
            HomeController controller = new HomeController(new Profilo("GTCommessa", "studente", null, "test", "OreNonLavrative"));
            ViewResult result = controller.VisualizzaCommessa("GeTime TestVisualizzaCommessa") as ViewResult;
            Assert.IsNotNull(result.ViewBag.Giorni);
            Assert.IsTrue(result.ViewBag.Giorni.Count==2);
        }
        [TestMethod]
        public void TestVisualizzaGiorno() {
            HomeController controller = new HomeController(new Profilo("GTVGiorno", "studente", null, "test", "OreNonLavrative"));
            string value = "2000-01-01"; //non funziona se nn sono presenti nel DB questi valori!!!!!!!!!
            DateTime convertedDate;
            convertedDate = Convert.ToDateTime(value); //non funziona se nn sono presenti nel DB questi valori!!!!!!!!!
            ViewResult result = controller.VisualizzaGiorno(convertedDate) as ViewResult;

            Assert.IsTrue(result.ViewBag.giorno.data.ToString("yyyy-MM-dd") == convertedDate.ToString("yyyy-MM-dd"));
            Assert.IsTrue(result.ViewBag.giorno.OreLavorate[0].oreGiorno == 4); //non funziona se nn sono presenti nel DB questi valori!!!!!!!!!
            Assert.IsTrue(result.ViewBag.giorno.TotOreLavorate == 4); //non funziona se nn sono presenti nel DB questi valori!!!!!!!!!
            Assert.IsTrue(result.ViewBag.giorno.OreMalattia == 4); //non funziona se nn sono presenti nel DB questi valori!!!!!!!!!
        }
    }
}
