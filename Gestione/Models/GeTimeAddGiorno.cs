using System;
using System.Collections.Generic;
using DAO;
using Interfaces;
using DAO;

namespace Gestione.Models {
	public partial class DomainModel : IGeCo, IGeCV, IGeTime {
		
		public void CompilaHLavoro(DateTime data, int ore, int idCommessa, string idUtente){
			DataAccesObject dao = new DataAccesObject();
			dao.CompilaHLavoro(data, ore, idCommessa, idUtente);
		}
		public void Compila(DateTime data, int ore, HType tipoOre, string idUtente){
			IDao dao = new DataAccesObject();
			try {
				dao.Compila(data, ore, tipoOre,idUtente);
			} catch (Exception e) {
				throw e;
			}
		}
	}
}