using Interfaces;
using LibreriaDB;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace DAO{
	public interface ITrasformer {
		Corso TrasformInCorso(SqlDataReader data);
        List<Corso> TrasformInCorsi(SqlDataReader data);
        Commessa TrasformInCommessa(SqlDataReader data);
        List<Giorno> TrasformInGiorni(SqlDataReader data);
        Giorno TrasformInGiorno(SqlDataReader reader);
		CV TRansfInCv0(SqlDataReader data);
		List<CV> TransfListCV0(SqlDataReader data);
		Lezione TrasformInLezione(SqlDataReader data);
        List<Lezione> TrasformInLezioni(SqlDataReader data);
    }
	public class Trasformator :ITrasformer{
        public Lezione TrasformInLezione(SqlDataReader data) {
            Lezione output = null;
            if (data.Read()) {
                output = new Lezione {
                    Id = data.GetInt32(0),
                    Nome = data.GetString(1),
                    Durata = int.Parse(data.GetString(2)),
                    Descrizione = data.GetString(3)
                };
            }
            return output;
        }
        public List<Lezione> TrasformInLezioni(SqlDataReader data) {
            return DB.TrasformInList(data, TrasformInLezione);
        }

        public Corso TrasformInCorso(SqlDataReader data) {
            Corso output = null;
            if (data.Read()) {
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
        public List<Corso> TrasformInCorsi(SqlDataReader data) {
            return DB.TrasformInList(data, TrasformInCorso);
        }

        public Giorno TrasformInGiorno(SqlDataReader reader) {
            Giorno result = null;
            if (reader.Read()) {
                result = new Giorno(DateTime.Today);
                do {
                    switch (reader.GetInt32(0)) {
                        case 1:
                            result.HMalattia = reader.GetInt32(1);
                            break;
                        case 2:
                            result.HPermesso = reader.GetInt32(1);
                            break;
                        case 3:
                            result.HFerie = reader.GetInt32(1);
                            break;
                        case 4:
                            result.AddOreLavorative(new OreLavorative(reader.GetInt32(4), reader.GetInt32(1), reader.GetString(2), reader.GetString(3)));
                            break;
                    }
                } while (reader.Read());
            }
            return result;
        }
        public List<Giorno> TrasformInGiorni(SqlDataReader data) {
           return DB.TrasformInList(data,TrasformInGiornoOreLavorative);
        }
        public Giorno TrasformInGiornoOreLavorative(SqlDataReader data) {
            Giorno giorno = null;
            if (data.Read()) {
                giorno = new Giorno(data.GetDateTime(1)) {
                    IdGiorno = data.GetInt32(0)
                };
                giorno.AddOreLavorative(new OreLavorative(data.GetInt32(3), data.GetInt32(2), data.GetString(4), data.GetString(5)));
            }
            return giorno;
        }

		public CV TRansfInCv0(SqlDataReader data){			
			CV output = null;
			if(data.Read()){
				output = new CV{
					Matricola = data.GetString(0)
				};					
			}
		return output;
		}
		public List<CV> TransfListCV0(SqlDataReader data){
            return DB.TrasformInList(data, TRansfInCv0);			
		}

        public Commessa TrasformInCommessa(SqlDataReader data) {
            Commessa commessa = null;
            if (data.Read()) {
                commessa = new Commessa(data.GetInt32(0), data.GetString(1), data.GetString(2), data.GetInt32(3), data.GetInt32(4));
            }
            return commessa;
        }

	}
}