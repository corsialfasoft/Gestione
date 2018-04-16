﻿using System;
using System.Collections.Generic;
using Interfaces;
using System.Data.SqlClient;

namespace DAO{
	public interface IDao{
		void ModificaCV(CV a, CV b); //modifica un curriculum nel db
		void AggiungiCV(CV a); //quando sei loggato, puoi aggiungere un curriculum nel db
		void CaricaCV(string path); //quando non sei loggato, puoi spedire un curriuculum
		CV Search(string id); //search di un curriculum per id di un curriculum
		List<CV> SearchChiava(string chiava); //search generale per parole chiava 
		List<CV> SearchEta(int eta); //search solo per quella precisa età
		List<CV> SearchRange(int etmin, int etmax); //search per un range di età minimo e massimo
		void EliminaCV(CV curriculum); //Elimina un CV dal db
		List<CV> SearchCognome(string cognome); //Ricerca solo per cognome
	
	
	
		void CompilaHLavoro(DateTime data, int ore, int idCommessa, int idUtente);
		void Compila(DateTime data, int ore, HType tipoOre, int idUtente);
		Giorno VisualizzaGiorno(DateTime data, string idUtente);
		List<Giorno> GiorniCommessa(int idCommessa, string idUtente);
		Commessa CercaCommessa(string nomeCommessa);
        //Aggiungi nuovo corso. Lo puo fare solo l'admin
        void AddCorso(Corso corso);
        //Aggiungi una lezione a un determinato corso. Lo puo fare solo il prof
        void AddLezione(int idCorso, Lezione lezione);
        //Iscrizione di uno studente a un determinato corso. Lo puo fare solo lo studente specifico
        void Iscriviti (int idCorso, int idStudente);

        //Cerca un determinato corso 
        Corso SearchCorsi(int idCorso);
        //Cerca tutti i corsi che contine la "descrizione" nei suoi attributi(nome,descrizione)
        List<Corso> SearchCorsi(string descrizione);
        //Cerca tutti i corsi che contiene la "descrizione" di un determinato studente(idStudente)
        List<Corso>SearchCorsi(string descrizione, int idUtente);
        //Mostra tutti i corsi presenti nel db
        List<Corso>ListaCorsi();
        //Mostra tutti i corsi a cui è iscritto un determinato studente(idStudente)
        List<Corso>ListaCorsi(int idUtente);
    }
	public partial class DataAccesObject : IDao {
		public void AddCorso(Corso corso) {
			throw new NotImplementedException();
		}

		public void AddLezione(int idCorso,Lezione lezione) {
			throw new NotImplementedException();
		}

		public void AggiungiCV(CV a) {
			throw new NotImplementedException();
		}

		public void CaricaCV(string path) {
			throw new NotImplementedException();
		}

		public Commessa CercaCommessa(string nomeCommessa) {
			return new Commessa(10,"GeTime","Progetto GeTime",50,0);
		}

		public void Compila(DateTime data,int ore,HType tipoOre,int idUtente) {
			throw new NotImplementedException();
		}

		public void CompilaHLavoro(DateTime data,int ore,int idCommessa,int idUtente) {
			throw new NotImplementedException();
		}

		public void EliminaCV(CV curriculum) {
			throw new NotImplementedException();
		}

		public List<Giorno> GiorniCommessa(int idCommessa,string idUtente) {
			List<Giorno> giorni = new List<Giorno>();
			Commessa com = new Commessa(10,"GeTime","Progetto GeTime", 50,0);
			DateTime data = DateTime.Today;
			Giorno giorno = new Giorno(data, 0, 0, 0, "11");
			giorno.AddOreCommessa(new OreCommessa(10,2,com.Nome,com.Descrizione));
			giorni.Add(giorno);
			giorno = new Giorno(data.AddDays(-1), 0, 0, 0, "9");
			giorno.AddOreCommessa(new OreCommessa(11, 2, com.Nome, com.Descrizione));
			giorni.Add(giorno);
			giorni.Add(new Giorno(data.AddDays(-1), 0,0,0,"11"));
			giorno.AddOreCommessa(new OreCommessa(13, 2, com.Nome, com.Descrizione));
			giorni.Add(giorno);
			return giorni;
		}

		public void Iscriviti(int idCorso,int idStudente) {
			throw new NotImplementedException();
		}

		public List<Corso> ListaCorsi() {
			throw new NotImplementedException();
		}

		public List<Corso> ListaCorsi(int idUtente) {
			throw new NotImplementedException();
		}

		public void ModificaCV(CV a,CV b) {
			throw new NotImplementedException();
		}

		public CV Search(string id) {
			throw new NotImplementedException();
		}

		public List<CV> SearchChiava(string chiava) {
			throw new NotImplementedException();
		}

		public List<CV> SearchCognome(string cognome) {
			throw new NotImplementedException();
		}

		public Corso SearchCorsi(int idCorso) {
			throw new NotImplementedException();
		}

		public List<Corso> SearchCorsi(string descrizione) {
			throw new NotImplementedException();
		}

		public List<Corso> SearchCorsi(string descrizione,int idUtente) {
			throw new NotImplementedException();
		}

		public List<CV> SearchEta(int eta) {
			throw new NotImplementedException();
		}

		public List<CV> SearchRange(int etmin,int etmax) {
			throw new NotImplementedException();
		}

		public Giorno VisualizzaGiorno(DateTime data, string idUtente) {
            Giorno result = null;
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
            scsb.DataSource= @"(localdb)\MSSQLLocalDB";
            scsb.InitialCatalog="GeTime";
            SqlConnection connection = new SqlConnection(scsb.ToString());
            try {
                connection.Open();
                SqlCommand command = new SqlCommand("SP_VisualizzaGiorno",connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@Data", System.Data.SqlDbType.Date).Value = data.ToString("yyyy-MM-dd");
                command.Parameters.Add("@IdUtente", System.Data.SqlDbType.NVarChar).Value = idUtente;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) {
                    result = new Giorno(data);
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
                                result.AddOreCommessa(new OreCommessa(reader.GetInt32(4), reader.GetInt32(1), reader.GetString(2), reader.GetString(3)));
                                break;
                        }
                    } while(reader.Read());
                }
                reader.Close();
                command.Dispose();
            } catch (Exception e) {
                throw e;
            } finally {
                connection.Dispose();
            }
            return result;
		}
	}
}