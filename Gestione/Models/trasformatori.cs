using Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace DAO{
	public interface ITrasformer {
		List<Lezione> TrasformInLezione(SqlDataReader data);
		Corso TrasformInCorso(SqlDataReader data);
		List<Corso> TrasformInListaCorso(SqlDataReader data);
	}
	public class Trasformator :ITrasformer{
		public List<Lezione> TrasformInLezione(SqlDataReader data){
			List<Lezione> output = new List<Lezione>();
			while(data.Read()){
				Lezione tmp = new Lezione {
					Nome = data.GetString(1),
					Durata = int.Parse(data.GetString(2)),
					Descrizione = data.GetString(3)
				};
				output.Add(tmp);
			}
			return output;
		}
		public Corso TrasformInCorso(SqlDataReader data){
			Corso output = null;
			if(data.Read()){
					 output = new Corso();
					output.Id = data.GetInt32(0);
					output.Nome = data.GetString(1);
					output.Descrizione = data.GetString(2);
					output.Inizio = data.GetDateTime(3);
					output.Fine = data.GetDateTime(4);					
					}
		return output;
		}	
		public List<Corso> TrasformInListaCorso(SqlDataReader data){
			List<Corso> output = new List<Corso>();
			do{
				Corso tmp = TrasformInCorso(data);
				if(tmp==null)
					break;
				output.Add(tmp);
			}while(true);
			return output;
		}	
	}
}