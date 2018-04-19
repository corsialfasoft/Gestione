using Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace DAO{
	public interface ITrasformer {
		List<Lezione> TrasformInLezione(SqlDataReader data);
		Corso TrasformInCorso(SqlDataReader data);
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
			Corso output = new Corso();
			if(data.Read()){
					output.Id = data.GetInt32(0);
					output.Nome = data.GetString(1);
					output.Descrizione = data.GetString(2);
					output.Inizio = data.GetDateTime(3);
					output.Fine = data.GetDateTime(4);					
					}
		return output;
		}		
	}
}