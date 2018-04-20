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
					Id = data.GetInt32(0),
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
				output = new Corso {
					Id = data.GetInt32(0),
					Nome = data.GetString(1),
					Descrizione = data.GetString(2),
					Inizio = data.GetDateTime(3),
					Fine = data.GetDateTime(4)
				};
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