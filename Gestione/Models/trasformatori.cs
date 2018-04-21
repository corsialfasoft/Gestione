﻿using Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace DAO{
	public interface ITrasformer {
		List<Lezione> TrasformInLezione(SqlDataReader data);
		Corso TrasformInCorso(SqlDataReader data);
		List<Corso> TrasformInListaCorso(SqlDataReader data);
        Commessa TrasformInCommessa(SqlDataReader data);
        List<Giorno> TrasformInListGiorno(SqlDataReader data);
        Giorno TrasformInGiorno(SqlDataReader reader);
		CV TRansfInCv0(SqlDataReader data);
		List<CV> TransfListCV0(SqlDataReader data);
		CV TransfInCv(SqlDataReader data);
		Competenza TransfInCompetenza(SqlDataReader data);
		List<Competenza> TransfInLIstCompetenza (SqlDataReader data);
		PerStud TransfInPerStud(SqlDataReader data);
		List<PerStud> TransfInListPerstud(SqlDataReader data);
		EspLav TransEspLav (SqlDataReader data);
		List<EspLav> TransfInListEspLav(SqlDataReader data);
    }
	public class Trasformator :ITrasformer{
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
        public List<Giorno> TrasformInListGiorno(SqlDataReader data) {
            List<Giorno> list = new List<Giorno>();
            while (data.Read()) {
				Giorno giorno = new Giorno(data.GetDateTime(1)) {
					IdGiorno = data.GetInt32(0)
				};
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
			List<CV> output = new List<CV>();
			do{
				CV tmp = TRansfInCv0(data);
				if(tmp==null)
					break;
				output.Add(tmp);
			}while(true);
			return output;			
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
		public List<Competenza> TransfInLIstCompetenza (SqlDataReader data){
			List<Competenza> output = new List<Competenza>();
			do{
				Competenza tmp = TransfInCompetenza(data);
			if(tmp==null)
					break;
				output.Add(tmp);
			}while(true);
			return output;			
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
			List<PerStud> output = new List<PerStud>();
			do{
				PerStud tmp = TransfInPerStud(data);
			if(tmp==null)
					break;
				output.Add(tmp);
			}while(true);
			return output;		
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
			List<EspLav> output = new List<EspLav>();
			do{
				EspLav tmp = TransEspLav(data);
			if(tmp==null)
					break;
				output.Add(tmp);
			}while(true);
			return output;		
		}
	}
}	