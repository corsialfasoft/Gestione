using System;
using System.Collections.Generic;

namespace Gestione.Controllers {
    public class DTGGiorno {
        public DateTime data {get; set;}
        public int TotOreLavorate {get; set;}
        public int OrePermesso {get; set;}
        public int OreMalattia {get; set;}
        public int OreFerie {get; set;}
        public DTGGiorno() {}
        private List<OreLavorate> OreLavorates = new List<OreLavorate>();
        public List<OreLavorate> OreLavorate { get {return OreLavorates;}}
    }

    public class OreLavorate {
        public string nome {get; set;}
        public int oreGiorno {get; set;}
        public string descrizione {get; set;}
    }
}