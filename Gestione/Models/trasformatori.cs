using Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace DAO{
	public interface ITrasformer {
		List<Lezione> TrasformInLezione(SqlDataReader data);
		Corso TrasformInCorso(SqlDataReader data);
		List<Corso> TrasformInListaCorso(SqlDataReader data);
        Commessa TrasformInCommessa(SqlDataReader data);
        List<Giorno> TrasformInGiorno(SqlDataReader data);
    }
	public class Trasformator :ITrasformer{
        public List<Giorno> TrasformInGiorno(SqlDataReader data) {
            List<Giorno> list = new List<Giorno>();
            while (data.Read()) {
                Giorno giorno = new Giorno(data.GetDateTime(1));
                giorno.IdGiorno = data.GetInt32(0);
                giorno.AddOreLavorative(new OreLavorative(data.GetInt32(3), data.GetInt32(2), data.GetString(4), data.GetString(5)));
                list.Add(giorno);
            }
            return list;
        }
        public Commessa TrasformInCommessa(SqlDataReader data) {
            Commessa commessa = null;
            if (data.Read()) {
                commessa = new Commessa(data.GetInt32(0), data.GetString(1), data.GetString(2), data.GetInt32(3), data.GetInt32(4));
            }
            return commessa;
        }
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