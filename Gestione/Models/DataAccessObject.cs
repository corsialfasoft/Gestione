using System;
using System.Collections.Generic;
using Interfaces;
using System.Data.SqlClient;
using System.Data;

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
        void AddCvStudi(string matrCv,PerStud studi);
        void AddEspLav(string matrCv, EspLav esp );
        void AddCompetenze(string matrCv, Competenza comp);
	
	
		void CompilaHLavoro(DateTime data, int ore, int idCommessa, int idUtente);
		void Compila(DateTime data, int ore, HType tipoOre, int idUtente);
		Giorno VisualizzaGiorno(DateTime data, int idUtente);
		List<Giorno> GiorniCommessa(int idCommessa, int idUtente);
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
        private string GetConnection(){ 
            SqlConnectionStringBuilder reader = new SqlConnectionStringBuilder();
            reader.DataSource=@"(localdb)\MSSQLLocalDB";
            reader.InitialCatalog = "GECV";
            return reader.ToString();
        }
        public void AddCompetenze(string matrCv,Competenza comp) {
            throw new NotImplementedException();
        }

        public void AddCorso(Corso corso) {
			throw new NotImplementedException();
		}

        public void AddCvStudi(string matrCv,PerStud studi) {
            throw new NotImplementedException();
        }

        public void AddEspLav(string matrCv,EspLav esp) {
			SqlConnection con= new SqlConnection(GetConnection());
			try {
				con.Open();
				SqlCommand command = new SqlCommand("AddEspLav",con);
				command.CommandType=CommandType.StoredProcedure;
				command.Parameters.Add("@AnnoI",SqlDbType.Int).Value=esp.AnnoInizio;
				command.Parameters.Add("@AnnoF",SqlDbType.Int).Value=esp.AnnoFine;
				command.Parameters.Add("@Qualifica",SqlDbType.NVarChar).Value=esp.qualifica;
				command.Parameters.Add("@Descrizione",SqlDbType.NVarChar).Value=esp.descrizione;
				command.Parameters.Add("@matr",SqlDbType.Int).Value=matrCv;
                int x = command.ExecuteNonQuery();
				command.Dispose();
				if (x == 0) { 
					throw new Exception("Nessuna Esperienza Inserita");
					}
				
			}catch(Exception e) {
				throw e;
			}finally {
				con.Dispose();
			}
        }

        public void AddLezione(int idCorso,Lezione lezione) {
			throw new NotImplementedException();
		}

		

		public void CaricaCV(string path) {
			throw new NotImplementedException();
		}

		public Commessa CercaCommessa(string nomeCommessa) {
			throw new NotImplementedException();
		}

		public void Compila(DateTime data,int ore,HType tipoOre,int idUtente) {
			throw new NotImplementedException();
		}

		public void CompilaHLavoro(DateTime data,int ore,int idCommessa,int idUtente) {
			throw new NotImplementedException();
		}

		public void EliminaCV(CV curriculum) {
			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
			builder.DataSource=@"(localdb)\MSSQLLocalDB";
			builder.InitialCatalog="GECV";
			SqlConnection connection = new SqlConnection(builder.ToString());
			int x;
			try {
				connection.Open();
				SqlCommand command = new SqlCommand("dbo.DeleteCurriculum",connection);
				command.CommandType=System.Data.CommandType.StoredProcedure;
				command.Parameters.Add("@parola",System.Data.SqlDbType.NVarChar).Value=curriculum.matricola;
				 x = command.ExecuteNonQuery();
				command.Dispose();
				if (x == 0) { 
					throw new Exception("Nessun curriculum eliminato!");
					}
				
			}catch(Exception e) {
				throw e;
			}finally {
				connection.Dispose();
			}
		}

		public List<Giorno> GiorniCommessa(int idCommessa,int idUtente) {
			throw new NotImplementedException();
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

	

		

		public List<CV> SearchChiava(string chiava) {
			List<CV> trovati = new List<CV>();
			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
			builder.DataSource=@"(localdb)\MSSQLLocalDB";
			builder.InitialCatalog="GECV";
			SqlConnection connection = new SqlConnection(builder.ToString());
			try {
				connection.Open();
				SqlCommand command = new SqlCommand("dbo.CercaParolaChiava",connection);
				command.CommandType=System.Data.CommandType.StoredProcedure;
				command.Parameters.Add("@parola",System.Data.SqlDbType.NVarChar).Value=chiava;
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read()){
					trovati.Add(Search(reader.GetString(0)));
				}
				reader.Close();
				command.Dispose();
				return trovati;
				
			}catch(Exception e) {
				throw e;
			}finally {
				connection.Dispose();
			}
		}

		public List<CV> SearchCognome(string cognome) {
			List<CV> trovati = new List<CV>();
			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
			builder.DataSource=@"(localdb)\MSSQLLocalDB";
			builder.InitialCatalog="GECV";
			SqlConnection connection = new SqlConnection(builder.ToString());
			try {
				connection.Open();
				SqlCommand command = new SqlCommand("dbo.CercaCognome",connection);
				command.CommandType=System.Data.CommandType.StoredProcedure;
				command.Parameters.Add("@cognome",System.Data.SqlDbType.NVarChar).Value=cognome;
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read()){
					trovati.Add(Search(reader.GetString(0)));
				}
				reader.Close();
				command.Dispose();
				return trovati;
				
			}catch(Exception e) {
				throw e;
			}finally {
				connection.Dispose();
			}
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
		List<CV> trovati = new List<CV>();
			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
			builder.DataSource=@"(localdb)\MSSQLLocalDB";
			builder.InitialCatalog="GECV";
			SqlConnection connection = new SqlConnection(builder.ToString());
			try {
				connection.Open();
				SqlCommand command = new SqlCommand("dbo.CercaEta",connection);
				command.CommandType=System.Data.CommandType.StoredProcedure;
				command.Parameters.Add("@eta",System.Data.SqlDbType.Int).Value=eta;
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read()){
					trovati.Add(Search(reader.GetString(0)));
				}
				reader.Close();
				command.Dispose();
				return trovati;
				
			}catch(Exception e) {
				throw e;
			}finally {
				connection.Dispose();
			}
		}

		public List<CV> SearchRange(int etmin,int etmax) {
			List<CV> trovati = new List<CV>();
			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
			builder.DataSource=@"(localdb)\MSSQLLocalDB";
			builder.InitialCatalog="GECV";
			SqlConnection connection = new SqlConnection(builder.ToString());
			try {
				connection.Open();
				SqlCommand command = new SqlCommand("dbo.CercaEtaMinMax",connection);
				command.CommandType=System.Data.CommandType.StoredProcedure;
				command.Parameters.Add("@e_min",System.Data.SqlDbType.Int).Value=etmin;
				command.Parameters.Add("@e_max",System.Data.SqlDbType.Int).Value=etmax;
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read()){
					trovati.Add(Search(reader.GetString(0)));
				}
				reader.Close();
				command.Dispose();
				return trovati;
				
			}catch(Exception e) {
				throw e;
			}finally {
				connection.Dispose();
			}
		}

		public Giorno VisualizzaGiorno(DateTime data,int idUtente) {
			throw new NotImplementedException();
		}
	}
}