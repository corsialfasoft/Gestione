using System;
using System.Collections.Generic;

namespace Gestione.Controllers {
    public class DTGiorno {
		public DateTime Data { get; set; }
		public int OreLavorate { get; set; }
	}
	public class DTCommessa {
		public int Id { get; set; }
		public string Descrizione { get; set; }
		public string Nome { get; set; }
		public int Capienza { get; set; }
		public int OreLavorate { get; set; }
		public DTCommessa(int id, string nome, string descrizione, int capienza, int oreLavorate) {
			Id = id;
			OreLavorate = oreLavorate;
			Nome = nome;
			Descrizione = descrizione;
			Capienza = capienza;
		}
	}
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