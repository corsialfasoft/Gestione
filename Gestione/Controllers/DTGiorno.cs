using System;

namespace Gestione.Controllers {
    internal class DTGiorno {
        public DateTime data {get; set;}
        public int OrePermesso {get; set;}
        public int OreMalattia {get; set;}
        public int OreFerie {get; set;}
        public DTGiorno() {
        }
        public OreLavoro OreLavoro { get; internal set; }
    }

    public class OreLavoro {
        //public List<OreCommessa>
    }
}