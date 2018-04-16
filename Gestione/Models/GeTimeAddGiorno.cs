using System;
using System.Collections.Generic;
using Interfaces;

namespace Gestione.Models {
	public partial class DomainModel : IGeCo, IGeCV, IGeTime {
		
		public void CompilaHLavoro(DateTime data, int ore, int idCommessa, string idUtente){
			
		}
		public void Compila(DateTime data, int ore, HType tipoOre, string idUtente){

		}
	}
}