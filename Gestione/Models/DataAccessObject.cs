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
		List<Lezione> ListaLezioni(Corso corso);
    }
	public partial class DataAccesObject : IDao {
		private List<Lezione> TrasformInLezione(SqlDataReader data){
			List<Lezione> output = new List<Lezione>();
			while(data.Read()){
				Lezione tmp = new Lezione {
					Durata = int.Parse(data.GetString(1)),
					Descrizione = data.GetString(2)
				};
				output.Add(tmp);
			}
			return output;
		}
		public List<Lezione> ListaLezioni(Corso corso){
			SqlParameter[] param = {new SqlParameter("@IdCorso",corso.Id)};
			return DB.ExecQProcedureReader("ListaLezioni",TrasformInLezione,param);
		}
		public void AddCorso(Corso corso) {
			SqlConnection connection = new SqlConnection(GetConnection());
			int IdCo = 0;
			try{
				connection.Open();
				SqlCommand command = new SqlCommand("AddCorso",connection) {
					CommandType = CommandType.StoredProcedure
				};
				command.Parameters.Add("@nome",SqlDbType.NVarChar).Value= corso.Nome;
				command.Parameters.Add("@descrizione",SqlDbType.NVarChar).Value= corso.Descrizione;
				command.Parameters.Add("@dInizio",SqlDbType.DateTime).Value= corso.Inizio;
				command.Parameters.Add("@dFine",SqlDbType.DateTime).Value= corso.Fine;
				SqlDataReader reader = command.ExecuteReader();
				while(reader.Read()){
					IdCo = (int)reader.GetDecimal(0);
				}
				reader.Close();
				command.Dispose();
				if(IdCo == 0) {
					throw new CorsoNonAggiuntaException("Corso non aggiunto") ;
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
				SqlCommand command = new SqlCommand("AddLezione",connection) {
					CommandType = CommandType.StoredProcedure
				};
				command.Parameters.Add("@idCorsi",SqlDbType.Int).Value= idCorso;
				command.Parameters.Add("@nome",SqlDbType.NVarChar).Value=lezione.Nome;
				command.Parameters.Add("@descrizione",SqlDbType.NVarChar).Value= lezione.Descrizione;
				command.Parameters.Add("@durata",SqlDbType.NVarChar).Value= lezione.Durata.ToString();
				SqlDataReader reader = command.ExecuteReader();
					while(reader.Read()){
						IdLez = (int)reader.GetDecimal(0);
					} 
					reader.Close();
					command.Dispose();
					if(IdLez == 0) {
						throw new LezioneNonAggiuntaException("Lezione non aggiunta") ;
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
		    List<Corso> result = new List<Corso>();
            SqlConnection con = new SqlConnection(GetConnection());
			SqlCommand cmd = new SqlCommand("dbo.ListaCorsi",con) {
				CommandType = CommandType.StoredProcedure
			};
			try {
				DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.TableMappings.Add("Table","Corsi");
                da.SelectCommand = cmd;
                da.Fill(ds);
                foreach(DataRow dr in ds.Tables["Corsi"].Rows){ 
                    Int32 _id = (Int32) dr[0];
                    String _nome = (String) dr[1];
                    String _desc = (String)dr[2];
                    DateTime _dInizio = (DateTime)dr[3];
                    DateTime _dFine = (DateTime)dr[4];
                    result.Add(new Corso{Id=_id,Nome=_nome,Descrizione=_desc,Inizio=_dInizio,Fine=_dFine});
                }
                ds.Dispose();
                da.Dispose();
                cmd.Dispose();
		        return result;
              }catch(Exception e){
                throw e;    
            }finally{ 
                con.Close();    
            }
		}

		public List<Corso> ListaCorsi(string idUtente) {
			List<Corso> result = new List<Corso> ();
		    SqlConnection con = new SqlConnection(GetConnection());
			SqlCommand cmd = new SqlCommand("dbo.ListaCorsiStudenti",con) {
				CommandType = CommandType.StoredProcedure
			};
			con.Open();
			cmd.Parameters.Add("@idStudente",SqlDbType.NVarChar).Value = idUtente;
           try{
            DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.TableMappings.Add("Table","Corsi");
                da.SelectCommand = cmd;
                da.Fill(ds);
                foreach(DataRow dr in ds.Tables["Corsi"].Rows){ 
                    int _id = (int)dr["id"];
                    string _nome = (string) dr["nome"];
                    string _desc = (string)dr["descrizione"];
                    DateTime _dInizio = (DateTime)dr["dInizio"];
                    DateTime _dFine = (DateTime)dr["dFine"];
                    result.Add(new Corso{Id=_id,Nome=_nome,Descrizione=_desc,Inizio=_dInizio,Fine=_dFine});
                }
                ds.Dispose();
                da.Dispose();
                cmd.Dispose();
			    return result;
              }catch(Exception e){
                throw e;    
            }finally{ 
                con.Close();    
            }
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
		private Corso TrasformInCorso(SqlDataReader data){
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
		
        private string GetConnection(){
			SqlConnectionStringBuilder reader = new SqlConnectionStringBuilder {
				DataSource = @"(localdb)\MSSQLLocalDB",
				InitialCatalog = "GeCorsi"
			};
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
				SqlCommand cmd = new SqlCommand("SearchCorso",con) {
					CommandType = CommandType.StoredProcedure
				};
				cmd.Parameters.Add("@idCorso",SqlDbType.Int).Value = descrizione;
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.TableMappings.Add("Table","Corsi");
                da.SelectCommand = cmd;
                da.Fill(ds);
                foreach(DataRow dr in ds.Tables["Corsi"].Rows){ 
                    int _id = (int)dr[0];
                    string _nome = (string) dr[1];
                    string _desc = (string)dr[2];
                    DateTime _dInizio = (DateTime)dr[3];
                    DateTime _dFine = (DateTime)dr[4];
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

		public List<Corso> SearchCorsi(string descrizione,string idUtente)  {
			List<Corso> corsi = null;
            SqlConnection con = new SqlConnection(GetConnection());
            try{ 
                corsi = new List<Corso>();
                con.Open();
				SqlCommand cmd = new SqlCommand("SearchCorsiStud",con) {
					CommandType = CommandType.StoredProcedure
				};
				cmd.Parameters.Add("@idCorso",SqlDbType.Int).Value = descrizione;
                cmd.Parameters.Add("@idStudente",SqlDbType.NVarChar).Value = idUtente;
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.TableMappings.Add("Table","Corsi");
                da.SelectCommand = cmd;
                da.Fill(ds);
                foreach(DataRow dr in ds.Tables["Corsi"].Rows){ 
                    int _id = (int)dr[0];
                    string _nome = (string) dr[1];
                    string _desc = (string)dr[2];
                    DateTime _dInizio = (DateTime)dr[3];
                    DateTime _dFine = (DateTime)dr[4];
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