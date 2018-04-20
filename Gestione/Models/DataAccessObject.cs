using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
	
	
	
		void CompilaHLavoro(DateTime data, int ore, int idCommessa, string idUtente);
		void Compila(DateTime data, int ore, HType tipoOre, string idUtente);
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
			try {
				SqlParameter[] parameter = new SqlParameter[1];
				parameter[0] = new SqlParameter("@nomeCommessa", System.Data.SqlDbType.NVarChar);
				parameter[0].Value = nomeCommessa;
				return DB.ExecQProcedureReader("SP_CercaCommessa", TrasformInCommessa, parameter, "GeTime");
			} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}
		private Commessa TrasformInCommessa(SqlDataReader data) {
			Commessa commessa = null;
			if(data.Read()) {
				commessa = new Commessa(data.GetInt32(0), data.GetString(1), data.GetString(2),data.GetInt32(3), data.GetInt32(4));
			}
			return commessa;
		}

		public void Compila(DateTime data, int ore, HType tipoOre, string idUtente) {
			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = @"(localdb)\MSSQLLocalDB";
            builder.InitialCatalog = "GeTime";
            SqlConnection conn = new SqlConnection(builder.ToString());
            try{ 
				conn.Open();
				SqlCommand cmd = new SqlCommand("SP_Compila", conn);
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.Add("@giorno", System.Data.SqlDbType.Date).Value=data.ToString("yyyy-MM-dd");
				cmd.Parameters.Add("@idUtente", System.Data.SqlDbType.NVarChar).Value=idUtente;
				cmd.Parameters.Add("@ore", System.Data.SqlDbType.Int).Value=ore;
				cmd.Parameters.Add("@TipoOre", System.Data.SqlDbType.Int).Value=(int)tipoOre;
				cmd.ExecuteNonQuery();	
				cmd.Dispose();
            } catch (SqlException e) {
                throw new Exception(e.Message);
            } catch (Exception e) {
                throw e;
            }finally{ 
                conn.Dispose();
            }
        }
		public void CompilaHLavoro(DateTime data,int ore,int idCommessa,string idUtente) {
			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
			builder.DataSource = @"(localdb)\MSSQLLocalDB";
			builder.InitialCatalog = "GeTime";
			SqlConnection connection = new SqlConnection(builder.ToString());
			try {
				connection.Open();
				SqlCommand cmd = new SqlCommand("SP_AddHLavoro", connection);
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.Add("@data", System.Data.SqlDbType.Date).Value = data;
				cmd.Parameters.Add("@ore", System.Data.SqlDbType.Int).Value = ore;
				cmd.Parameters.Add("@idCommessa", System.Data.SqlDbType.Int).Value = idCommessa;
				cmd.Parameters.Add("@idUtente", System.Data.SqlDbType.NVarChar).Value = idUtente;
				cmd.ExecuteNonQuery();
				cmd.Dispose();
			} catch (Exception e){
				throw e;
			}finally{
				connection.Close();
			}
		}

		public void EliminaCV(CV curriculum) {
			throw new NotImplementedException();
		}
		private List<Giorno> TrasformInGiorno(SqlDataReader data) {
			List<Giorno> list = new List<Giorno>();
			while(data.Read()){
				Giorno giorno = new Giorno(data.GetDateTime(1));
				giorno.IdGiorno=data.GetInt32(0);
				giorno.AddOreLavorative(new OreLavorative(data.GetInt32(3),data.GetInt32(2),data.GetString(4),data.GetString(5)));
				list.Add(giorno);
			}
			return list;
		}
		public List<Giorno> GiorniCommessa(int idCommessa,string idUtente) {
			try{
				SqlParameter[] parameter = new SqlParameter[2];
				parameter[0]= new SqlParameter("@idC",System.Data.SqlDbType.Int);
				parameter[0].Value=idCommessa;
				parameter[1] = new SqlParameter("@idU", System.Data.SqlDbType.NVarChar);
				parameter[1].Value=idUtente;
				return DB.ExecQProcedureReader("SP_VisualizzaCommessa", TrasformInGiorno,parameter,"GeTime");
			}catch(SqlException e){
				throw new Exception(e.Message);
			}catch(Exception e){
				throw e;
			}
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
                                result.AddOreLavorative(new OreLavorative(reader.GetInt32(4), reader.GetInt32(1), reader.GetString(2), reader.GetString(3)));
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