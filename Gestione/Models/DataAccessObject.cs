using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using Interfaces;
using LibreriaDB;

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
		Giorno VisualizzaGiorno(DateTime data, int idUtente);
		List<Giorno> GiorniCommessa(int idCommessa, int idUtente);
		Commessa CercaCommessa(string nomeCommessa);
        //Aggiungi nuovo corso. Lo puo fare solo l'admin
        void AddCorso(Corso corso);
        //Aggiungi una lezione a un determinato corso. Lo puo fare solo il prof
        void AddLezione(int idCorso, Lezione lezione);
        //Iscrizione di uno studente a un determinato corso. Lo puo fare solo lo studente specifico
        void Iscriviti (int idCorso, string idStudente);

        //Cerca un determinato corso 
        Corso SearchCorsi(int idCorso);
        //Cerca tutti i corsi che contine la "descrizione" nei suoi attributi(nome,descrizione)
        List<Corso> SearchCorsi(string descrizione);
        //Cerca tutti i corsi che contiene la "descrizione" di un determinato studente(idStudente)
        List<Corso>SearchCorsi(string descrizione, string idUtente);
        //Mostra tutti i corsi presenti nel db
        List<Corso>ListaCorsi();
        //Mostra tutti i corsi a cui è iscritto un determinato studente(idStudente)
        List<Corso>ListaCorsi(string idUtente);
    }
	public partial class DataAccesObject : IDao {
		public void AddCorso(Corso corso) {
			SqlConnection connection = new SqlConnection(GetConnection());
			int IdCo = 0;
			try{
			connection.Open();
			SqlCommand command = new SqlCommand("AddCorso",connection);
			command.CommandType = System.Data.CommandType.StoredProcedure;
			command.Parameters.Add("@nome",System.Data.SqlDbType.NVarChar).Value= corso.Nome;
			command.Parameters.Add("@descrizione",System.Data.SqlDbType.NVarChar).Value= corso.Descrizione;
			command.Parameters.Add("@dInizio",System.Data.SqlDbType.DateTime).Value= corso.Inizio;
			command.Parameters.Add("@dFine",System.Data.SqlDbType.DateTime).Value= corso.Fine;
			SqlDataReader reader = command.ExecuteReader();
				while(reader.Read()){
				IdCo = (int)reader.GetDecimal(0);
				}
				if(IdCo == 0) {
						throw new CorsoNonAggiuntaException("Corso non aggiunto") ;
				} else { 
						reader.Close();
						command.Dispose();
						connection.Close();
				}
			} catch (Exception e) {
				throw e;
			} finally {
				connection.Dispose();
			}
		}

		public void AddLezione(int idCorso,Lezione lezione) {
			SqlConnection connection = new SqlConnection(GetConnection());
			int IdLez = 0;
			try { 
				connection.Open();
				SqlCommand command = new SqlCommand("AddLezioni",connection);
				command.Parameters.Add("@idCorsi",System.Data.SqlDbType.Int).Value= idCorso;
				command.Parameters.Add("@descrizione",System.Data.SqlDbType.NVarChar).Value= lezione.Descrizione;
				command.Parameters.Add("@durata",System.Data.SqlDbType.NVarChar).Value= lezione.Durata;
				SqlDataReader reader = command.ExecuteReader();
					while(reader.Read()){
						IdLez = (int)reader.GetDecimal(0);
					} 
					if(IdLez == 0) {
						throw new LezioneNonAggiuntaException("Lezione non aggiunta") ;
					} else { 
						reader.Close();
						command.Dispose();
						connection.Close();
					}
			} catch (Exception e) {
				throw e;
			} finally {
				connection.Dispose();
			}
		}

		public void AggiungiCV(CV a) {
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
			throw new NotImplementedException();
		}

		public List<Giorno> GiorniCommessa(int idCommessa,int idUtente) {
			throw new NotImplementedException();
		}


		public List<Corso> ListaCorsi() {
			Corso c = new Corso {
				Nome = "c#",
				Descrizione = "Corso di programmazione su Asp.Net",
				Id = 1
			};
			Corso d = new Corso {
				Nome = "Java",
				Descrizione = "Corso alla proggrammazione OO",
				Id = 2
			};
			Corso e = new Corso {
				Nome = "Javascripppto",
				Descrizione = "Corso alla programazione su javascripttto",
				Id = 3
			};
			List<Corso> result = new List<Corso> {
				c,
				d,
				e
			};
			return result;
		}

		public List<Corso> ListaCorsi(string idUtente) {
			Corso c = new Corso {
				Nome = "c#",
				Descrizione = "Corso di cerca idutente programmazione su Asp.Net",
				Id = 1
			};
			Corso d = new Corso {
				Nome = "Java",
				Descrizione = "Corso alla c proggrammazione OO cerca idutente",
				Id = 2
			};
			Corso e = new Corso {
				Nome = "Javascripppto",
				Descrizione = "Corso alla programazione su javascripttto cerca idutente",
				Id = 3
			};
			List<Corso> result = new List<Corso> {
				c,
				d,
				e
			};
			return result;
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
		public Corso TrasformInCorso(SqlDataReader data){
			Corso output = new Corso {
				Nome = data.GetString(1),
				Descrizione = data.GetString(2),
				Inizio = data.GetDateTime(3),
				Fine = data.GetDateTime(4)
			};
			return output;
		}
        public string GetConnection(){ 
            SqlConnectionStringBuilder reader = new SqlConnectionStringBuilder();
            reader.DataSource=@"(localdb)\MSSQLLocalDB";
            reader.InitialCatalog = "GeCorsi";
            return reader.ToString();
        }
		public Corso SearchCorsi(int idCorso) {
			SqlParameter[] param = {new SqlParameter("@IdCorso",idCorso)};
			return DB.ExecQProcedureReader("SearchCorso", TrasformInCorso,param);
		}	
		
		public void Iscriviti(int idCorso,string idStudente) {
			SqlParameter[] param = {new SqlParameter("@IdCorso",idCorso), new SqlParameter("@matr",idStudente)};
			DB.ExecNonQProcedure("Iscrizione",param);
		}

		public List<Corso> SearchCorsi(string descrizione) {
			List<Corso> corsi = null;
            SqlConnection con = new SqlConnection(GetConnection());
            try{ 
                corsi = new List<Corso>();
                con.Open();
                SqlCommand cmd = new SqlCommand("SearchCorso",con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idCorso",SqlDbType.Int).Value = descrizione;
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.TableMappings.Add("table","Corsi");
                da.SelectCommand = cmd;
                da.Fill(ds);
                foreach(DataRow dr in ds.Tables["Corsi"].Rows){ 
                    int _id = (int)dr["id"];
                    string _nome = (string) dr["nome"];
                    string _desc = (string)dr["descrizione"];
                    DateTime _dInizio = (DateTime)dr["dInizio"];
                    DateTime _dFine = (DateTime)dr["dFine"];
                    corsi.Add(new Corso{Id=_id,Nome=_nome,Descrizione=_desc,Inizio=_dInizio,Fine=_dFine});
                }
                ds.Dispose();
                da.Dispose();
                cmd.Dispose();
                return corsi;
            }catch(Exception e){
                throw e;    
            }finally{ 
                con.Close();    
            }

		}

		public List<Corso> SearchCorsi(string descrizione,string idUtente) {
			List<Corso> corsi = null;
            SqlConnection con = new SqlConnection(GetConnection());
            try{ 
                corsi = new List<Corso>();
                con.Open();
                SqlCommand cmd = new SqlCommand("SearchCorsiStud",con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idCorso",SqlDbType.Int).Value = descrizione;
                cmd.Parameters.Add("@idStudente",SqlDbType.NVarChar).Value = idUtente;
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.TableMappings.Add("table","Corsi");
                da.SelectCommand = cmd;
                da.Fill(ds);
                foreach(DataRow dr in ds.Tables["Corsi"].Rows){ 
                    int _id = (int)dr["id"];
                    string _nome = (string) dr["nome"];
                    string _desc = (string)dr["descrizione"];
                    DateTime _dInizio = (DateTime)dr["dInizio"];
                    DateTime _dFine = (DateTime)dr["dFine"];
                    corsi.Add(new Corso{Id=_id,Nome=_nome,Descrizione=_desc,Inizio=_dInizio,Fine=_dFine});
                }
                ds.Dispose();
                da.Dispose();
                cmd.Dispose();
                return corsi;
            }catch(Exception e){
                throw e;    
            }finally{ 
                con.Close();    
            }
		}

		public List<CV> SearchEta(int eta) {
			throw new NotImplementedException();
		}

		public List<CV> SearchRange(int etmin,int etmax) {
			throw new NotImplementedException();
		}

		public Giorno VisualizzaGiorno(DateTime data,int idUtente) {
			throw new NotImplementedException();
		}

		[Serializable]
		private class LezioneNonAggiuntaException : Exception {
			public LezioneNonAggiuntaException() {}
			public LezioneNonAggiuntaException(string message) : base(message) {}
			public LezioneNonAggiuntaException(string message,Exception innerException) : base(message,innerException){}
			protected LezioneNonAggiuntaException(SerializationInfo info,StreamingContext context) : base(info,context){
			}
		}

		[Serializable]
		private class CorsoNonAggiuntaException : Exception {
			public CorsoNonAggiuntaException() {}
			public CorsoNonAggiuntaException(string message) : base(message) { }
			public CorsoNonAggiuntaException(string message,Exception innerException) : base(message,innerException) {}
			protected CorsoNonAggiuntaException(SerializationInfo info,StreamingContext context) : base(info,context) {
			}
		}
	}
}