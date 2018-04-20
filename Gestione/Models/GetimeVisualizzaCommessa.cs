using System;
using System.Collections.Generic;
using Interfaces;

namespace Gestione.Models {
	public partial class DomainModel {
		public List<DTGiorno> GiorniCommessa(int idCommessa, string idUtente){
			try{ 
				List<Giorno> giorni = DAO.GiorniCommessa(idCommessa, idUtente);
				List<DTGiorno> dTGiorni = new List<DTGiorno>();
				if (giorni != null && giorni.Count > 0) {
					foreach (Giorno giorno in giorni) {
						if (giorno.OreLavorate != null && giorno.OreLavorate.Count > 0) 
							dTGiorni.Add(new DTGiorno { Data = giorno.Data, OreLavorate = giorno.OreLavorate[0].Ore });
					}
				}
				return dTGiorni;
			}catch(Exception e){
				throw e;
			}
		}
		public DTCommessa CercaCommessa(string nomeCommessa) {
			try{ 
				Commessa commessa = DAO.CercaCommessa(nomeCommessa);
				if(commessa!=null)
					return new  DTCommessa(commessa.Id,commessa.Nome,commessa.Descrizione,commessa.Capienza,commessa.OreLavorate);
				return null;
			}catch(Exception e){
					throw e;
			}
		}
	}
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

}