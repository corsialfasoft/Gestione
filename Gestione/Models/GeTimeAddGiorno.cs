using System;
using System.Collections.Generic;
using DAO;
using Interfaces;
using DAO;

namespace Gestione.Models {
	public partial class DomainModel : IGeCo, IGeCV, IGeTime {
		IDao DAO = new DataAccesObject();
		
		public void CompilaHLavoro(DateTime data, int ore, int idCommessa, string idUtente){
            try {
                DAO.CompilaHLavoro(data, ore, idCommessa, idUtente);
            } catch (Exception e) {
                throw e;
            }
        }
		public void Compila(DateTime data, int ore, HType tipoOre, string idUtente){
			try {
                DAO.Compila(data, ore, tipoOre,idUtente);
			} catch (Exception e) {
				throw e;
			}
		}
	}
}