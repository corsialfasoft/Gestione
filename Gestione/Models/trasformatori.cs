using Gestione.Controllers;
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
        List<Commessa> TrasformInListaCommesse(SqlDataReader data);
        List<Giorno> TrasformInGiorni(SqlDataReader data);
        Giorno TrasformInGiorno(SqlDataReader reader);
		CV TRansfInCv0(SqlDataReader data);
		List<CV> TransfListCV0(SqlDataReader data);
		CV TransfInCv(SqlDataReader data);
		Competenza TransfInCompetenza(SqlDataReader data);
		List<Competenza> TransfInCompetenze(SqlDataReader data);
		PerStud TransfInPerStud(SqlDataReader data);
		List<PerStud> TransfInListPerstud(SqlDataReader data);
		EspLav TransEspLav (SqlDataReader data);
		List<EspLav> TransfInListEspLav(SqlDataReader data);
		Lezione TrasformInLezione(SqlDataReader data);
        List<Lezione> TrasformInLezioni(SqlDataReader data);
        List<Giorno> TransfInGiorni(SqlDataReader data);
        DTGiornoDMese ConvertGiornoInDTGDMese(Giorno giorno);
    }
	public class Trasformator :ITrasformer{
        //GeCo
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
        //GeTime
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

        public Commessa TrasformInCommessa(SqlDataReader data) {
            Commessa commessa = null;
            if (data.Read()) {
                commessa = new Commessa(data.GetInt32(0), data.GetString(1), data.GetString(2), data.GetValue(3)==DBNull.Value?0:data.GetInt32(3), data.GetInt32(4));
            }
            return commessa;
        }

        public List<Commessa> TrasformInListaCommesse(SqlDataReader data) {
            return DB.TrasformInList(data, TrasformInCommessa);
        }

        //GeCV
		public CV TRansfInCv0(SqlDataReader data){			
			CV output = null;
			if(data.Read()){
				output = new CV{
					Matricola = data.GetString(0)
				};
				DataAccesObject dao = new DataAccesObject();
				output=dao.Search(output.Matricola);
			}
			return output;
		}
		public List<CV> TransfListCV0(SqlDataReader data){
            return DB.TrasformInList(data, TRansfInCv0);			
		}


		public CV TransfInCv(SqlDataReader data){
			CV output = null;
			if(data.Read()){
				output = new CV{
					Nome = data.GetString(0) ?? "",
					Cognome = data.GetString(1)?? "",
					Eta = data.GetInt32(2),
					Matricola = data.GetString(3) ?? "",
					Email = data.GetValue(4)==DBNull.Value ? "" : data.GetString(4),
					Residenza = data.GetValue(5)==DBNull.Value ? "" : data.GetString(5),
					Telefono =  data.GetValue(6)==DBNull.Value ? "" : data.GetString(6)
				};
			}
			return output;
		}
		public Competenza TransfInCompetenza(SqlDataReader data){
			Competenza output = null;
			if(data.Read()){
				output = new Competenza{
					Livello = data.GetValue(0) == DBNull.Value ? 0 : data.GetInt32(0),
                    Titolo = data.GetValue(1) == DBNull.Value ? "" : data.GetString(1)
				};
			}
			return output;
		}
		public List<Competenza> TransfInCompetenze (SqlDataReader data){
			return DB.TrasformInList(data, TransfInCompetenza);		
		}

		public PerStud TransfInPerStud(SqlDataReader data){
			PerStud output = null;
			if(data.Read()){
				output = new PerStud{
					AnnoInizio = data.GetValue(0) == DBNull.Value ? 0 : data.GetInt32(0),
                    AnnoFine = data.GetValue(1) == DBNull.Value ? 0 : data.GetInt32(1),
                    Titolo = data.GetValue(2) == DBNull.Value ? "" : data.GetString(2),
                    Descrizione = data.GetValue(3) == DBNull.Value ? "" : data.GetString(3)
				};
			}
			return output;
		}
		public List<PerStud> TransfInListPerstud(SqlDataReader data){
            return DB.TrasformInList(data, TransfInPerStud);
        }

		public EspLav TransEspLav (SqlDataReader data){
			EspLav output = null;
			if(data.Read()){
				output = new EspLav{
					AnnoInizio = data.GetValue(0) == DBNull.Value ? 0 : data.GetInt32(0),
					AnnoFine = data.GetValue(1) == DBNull.Value ? 0 : data.GetInt32(1),
                    Qualifica = data.GetValue(2)==DBNull.Value ? "" : data.GetString(2),
                    Descrizione = data.GetValue(3)==DBNull.Value ? "" : data.GetString(3)
                    };
			}
			return output;
		}
		public List<EspLav> TransfInListEspLav(SqlDataReader data){
			return DB.TrasformInList(data, TransEspLav);
		}

        public List<Giorno> TransfInGiorni(SqlDataReader reader) {
            List<Giorno> giorni = new List<Giorno>();
            DateTime oldData = default(DateTime);
            Giorno result =null;
            while (reader.Read()) {
                DateTime data= reader.GetDateTime(2);//data indice
                if (data != oldData) {
                    result = new Giorno(data);
                    oldData= data;
                    giorni.Add(result);
                }
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
                        result.TotOreLavorate = reader.GetInt32(1);
                        break;
                }
            }
            return giorni;
        }
        public DTGiornoDMese ConvertGiornoInDTGDMese(Giorno giorno) {
            return new DTGiornoDMese {
                data = giorno.Data, OreFerie = giorno.HFerie, OreMalattia = giorno.HMalattia,
                OrePermesso = giorno.HPermesso, TotOreLavorate = giorno.TotOreLavorate
            };
        }
    }
}	