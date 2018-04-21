using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using Interfaces;
using LibreriaDB;

namespace DAO {
	public interface IDao{
		void ModificaCV(string nome,string cognome,int eta,string email,string residenza,string telefono,string matr); //modifica un curriculum nel db
		void AggiungiCV(CV a); //quando sei loggato, puoi aggiungere un curriculum nel db
		void CaricaCV(string path); //quando non sei loggato, puoi spedire un curriuculum
		CV Search(string id); //search di un curriculum per id di un curriculum
		List<CV> SearchChiava(string chiava); //search generale per parole chiava 
		List<CV> SearchEta(int eta); //search solo per quella precisa età
		List<CV> SearchRange(int etmin, int etmax); //search per un range di età minimo e massimo
		void EliminaCV(CV curriculum); //Elimina un CV dal db
		List<CV> SearchCognome(string cognome); //Ricerca solo per cognome
        void AddCvStudi(string MatrCv,PerStud studi);
        void AddEspLav(string MatrCv, EspLav esp );
        void AddCompetenze(string MatrCv, Competenza comp);
        void ModEspLav(string MatrCv, EspLav espV, EspLav esp );
		void ModComp( string matricola, Competenza daMod , Competenza Mod ); // Modifica la singola competenza
	
        void ModPerStudi(string matricola, PerStud daMod, PerStud Mod);


        void CompilaHLavoro(DateTime data, int ore, int idCommessa, int idUtente);
		void Compila(DateTime data, int ore, HType tipoOre, int idUtente);
		Giorno VisualizzaGiorno(DateTime data, int idUtente);
		List<Giorno> GiorniCommessa(int idCommessa, int idUtente);
		Commessa CercaCommessa(string nomeCommessa);
        //Aggiungi nuovo corso. Lo puo fare solo l'admin
        void AddCorso(Corso corso);
        //Aggiungi una lezione a un determinato corso. Lo puo fare solo il prof
        void AddLezione(int idCorso, Lezione lezione);
		void ModLezione(Lezione lezione);
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
		//mostra tutte le lezioni associate a un corso
		List<Lezione> ListaLezioni(Corso corso);
    }
	
	public partial class DataAccesObject : IDao {
		ITrasformer transf = new Trasformator();
		public void ModComp (string matricola , Competenza daMod , Competenza Mod){
			try{
				SqlParameter[] parameters = {
					new SqlParameter("@matricola", matricola),
					new SqlParameter("@titoloDaMod", daMod.Titolo),
					new SqlParameter("@livdaMod", daMod.Livello),
					new SqlParameter("@titoloMod", Mod.Titolo),
					new SqlParameter("@livMod", Mod.Livello)					
				};
				int output = DB.ExecNonQProcedure("ModComp", parameters,"GeCV");
				if(output == 0){
					throw new Exception("Nessuna modifica fatta!");
				}
			} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}
		public void ModPerStudi(string matricola , PerStud daMod , PerStud Mod){
			try{
				SqlParameter[] parameters = {
					new SqlParameter("@matricola", matricola),
					new SqlParameter("@annoIdaMod", daMod.AnnoInizio),
					new SqlParameter("@annoFdaMod", daMod.AnnoFine),
					new SqlParameter("@titoloDaMod", daMod.Titolo),
					new SqlParameter("@descrDaMod", daMod.Descrizione),
					new SqlParameter("@annoIMod", Mod.AnnoInizio),
					new SqlParameter("@annoFMod", Mod.AnnoFine),
					new SqlParameter("@titoloMod", Mod.Titolo),
					new SqlParameter("@descrMod", Mod.Descrizione),
				};
			int output = DB.ExecNonQProcedure("ModPerStud", parameters,"GeCV");
				if(output == 0){
					throw new Exception("Nessuna modifica fatta!");
				}
			} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}
		public void ModEspLav(string matricola , EspLav daMod , EspLav Mod){			
			try{
				SqlParameter[] parameters = {
					new SqlParameter("@matricola", matricola),
					new SqlParameter("@annoIdaMod", daMod.AnnoInizio),
					new SqlParameter("@annoFdaMod", daMod.AnnoFine),
					new SqlParameter("@qualificaDaMod", daMod.Qualifica),
					new SqlParameter("@descrDaMod", daMod.Descrizione),
					new SqlParameter("@annoIMod", Mod.AnnoInizio),
					new SqlParameter("@annoFMod", Mod.AnnoFine),
					new SqlParameter("@qualificaMod", Mod.Qualifica),
					new SqlParameter("@descrMod", Mod.Descrizione),
				};
			int output = DB.ExecNonQProcedure("ModEspLav", parameters,"GeCV");
				if(output == 0){
					throw new Exception("Nessuna modifica fatta!");
				}
			} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}
		public void AggiungiCV(CV c) {
			try{
				SqlParameter[] parameters = { 
					new SqlParameter("@Nome",c.Nome),
					new SqlParameter("@Cognome",c.Cognome),
					new SqlParameter("@Eta",c.Eta),
					new SqlParameter("@Matricola",c.Matricola),
					new SqlParameter("@Residenza",c.Residenza),
					new SqlParameter("@Telefono",c.Telefono)
					};
			int output = DB.ExecNonQProcedure("AddCv", parameters,"GeCV");
				if(output == 0){
					throw new Exception("Nessun CV aggiunto!");
				}
			} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}
		private List<EspLav> GetEspLav(string matricola) {
		try{
			SqlParameter[] param = {new SqlParameter("@Matricola",matricola)};
			List<EspLav> output = DB.ExecQReader(transf.TransfInListEspLav,"GetEspLav", "GeCv");
				return output;
			} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}
		private List<PerStud> GetPerStudi(string matricola) {
			try{
				SqlParameter[] param = {new SqlParameter("@Matricola", matricola)};
				List<PerStud> output = DB.ExecQReader(transf.TransfInListPerstud,"GetPerStudi", "GeCv");
				return output;
			} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}
		private List<Competenza> GetComp(string matricola) {
			try{
				SqlParameter[] param = {new SqlParameter("@Matricola", matricola)};
				List<Competenza> output = DB.ExecQReader(transf.TransfInLIstCompetenza,"GetComp", "GeCv");
				return output;
			} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}
		public CV Search(string matr) {
			try{
				SqlParameter[] param = {new SqlParameter("@Matricola",matr)};
				CV output = DB.ExecQReader(transf.TransfInCv,"GetCv","GeCv");
				output.Percorsostudi= GetPerStudi(output.Matricola);
				output.Esperienze = GetEspLav(output.Matricola);
				output.Competenze = GetComp(output.Matricola);
				return output;
			} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}
        public void ModificaCV(CV a,CV b) {
			try{
				SqlParameter[] parameters = {
					new SqlParameter("@matricolaM",a.Matricola),
					new SqlParameter("@nomeM",b.Nome),
					new SqlParameter("@cognomeM",b.Cognome),
					new SqlParameter("@etaM",b.Eta),
					new SqlParameter("@emailM",b.Email),
					new SqlParameter("@residenzaM",b.Residenza),				
					new SqlParameter("@telefonoM",b.Telefono),
				};
				int output = DB.ExecNonQProcedure("ModificaCurriculum", parameters,"GeCV");
				if(output == 0){
					throw new Exception();
				}
				ModEspLav(a.Matricola,a.Esperienze[0],b.Esperienze[0]);
				ModComp(a.Matricola,a.Competenze[0],b.Competenze[0]);
			} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}
		public List<Lezione> ListaLezioni(Corso corso){
			SqlParameter[] param = {new SqlParameter("@IdCorso",corso.Id)};
			return DB.ExecQProcedureReader("ListaLezioni",transf.TrasformInLezione,param,"GeCorsi");
		}
		public void AddCompetenze(string MatrCv,Competenza comp) {
			try{
				SqlParameter[] param = {
					new SqlParameter("@Tipo",comp.Titolo),
					new SqlParameter("@Livello",comp.Livello),
					new SqlParameter("@MatrCv",MatrCv)					
				};
				int output = DB.ExecNonQProcedure("dbo.AddCompetenze", param,"GeCV");
				if(output == 0){
					throw new Exception("Nessuna competenza aggiunta!");
				}
			} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}
  //       SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder {
		//		DataSource = @"(localdb)\MSSQLLocalDB",
		//		InitialCatalog = "GECV"
		//	};
		//	SqlConnection connection = new SqlConnection(builder.ToString());
		//	int x;
		//	try {
		//		connection.Open();
		//		SqlCommand command = new SqlCommand("dbo.AddCompetenze",connection) {
		//			CommandType = CommandType.StoredProcedure
		//		};
		//		command.Parameters.Add("@Tipo",SqlDbType.NVarChar).Value=comp.Titolo;
		//		command.Parameters.Add("@Livello",SqlDbType.Int).Value=comp.Livello;
		//		command.Parameters.Add("@MatrCv",SqlDbType.NVarChar).Value=MatrCv;
		//		x = command.ExecuteNonQuery();
		//		command.Dispose();
		//		if (x == 0) { 
		//			throw new Exception("Nessun curriculum eliminato!");
		//			}				
		//	}catch(Exception e) {
		//		throw e;
		//	}finally {
		//		connection.Dispose();
		//	}
		//} 
        public void AddCorso(Corso corso) {
			try{
				SqlParameter[] param = {
					new SqlParameter("@nome", corso.Nome),
					new SqlParameter("@descrizione", corso.Descrizione),
					new SqlParameter("@dInizio", corso.Inizio),
					new SqlParameter("@dFine", corso.Fine)
				};
				int RowAffected = DB.ExecNonQProcedure("AddCorso", param,"GeCorsi");
				if(RowAffected == 0){
					throw new CorsoNonAggiuntaException("Corso non aggiunto") ;
				}
			} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}
		public void AddLezione(int idCorso,Lezione lezione) {
			try{
				SqlParameter[] param = {
					new SqlParameter ("@idCorsi", idCorso),
					new SqlParameter ("@nome", lezione.Nome),
					new SqlParameter("@descrizione", lezione.Descrizione),
					new SqlParameter("@durata", lezione.Durata)
				};
				int RowAffected = DB.ExecNonQProcedure("AddLezione", param,"GeCorsi");
				if(RowAffected == 0){
					throw new LezioneNonAggiuntaException("Lezione non aggiunta") ;
				}
			} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}
		public void ModLezione(Lezione lezione){
			try{
				SqlParameter[] param = {
					new SqlParameter("@idLezione",lezione.Id),
					new SqlParameter("@nome",lezione.Nome),
					new SqlParameter("@descrizione",lezione.Descrizione),
					new SqlParameter("@durata",lezione.Durata)
				};
				int RowAffected =DB.ExecNonQProcedure("ModLezione",param,"GeCorsi",@"(localdb)\MSSQLLocalDB");
				if (RowAffected == 0) {
					throw new LezionNonModificataException("Non hai modificato la lezione");
				}
			} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}		
        public void AddCvStudi(string MatrCv,PerStud studi) {
			try{
				SqlParameter[] parameters = {
					new SqlParameter("@AnnoI",studi.AnnoInizio),
					new SqlParameter("@AnnoF",studi.AnnoFine),
					new SqlParameter("@Titolo",studi.Titolo),
					new SqlParameter("@Descrizione",studi.Descrizione),
					new SqlParameter("@MatrCv",MatrCv)
				};
				int output = DB.ExecNonQProcedure("dbo.AddCvStudi", parameters, "GeCv");
				if (output == 0) { 
					throw new Exception("Nessun curriculum eliminato!");
				}
			} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}		
		//	SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder {
		//		DataSource = @"(localdb)\MSSQLLocalDB",
		//		InitialCatalog = "GECV"
		//	};
		//	SqlConnection connection = new SqlConnection(builder.ToString());
		//	int x;
		//	try {
		//		connection.Open();
		//		SqlCommand command = new SqlCommand("dbo.AddCvStudi",connection) {
		//			CommandType = CommandType.StoredProcedure
		//		};
		//		command.Parameters.Add("@AnnoI",SqlDbType.Int).Value=studi.AnnoInizio;
		//		command.Parameters.Add("@AnnoF",SqlDbType.Int).Value=studi.AnnoFine;
		//		command.Parameters.Add("@Titolo",SqlDbType.VarChar).Value=studi.Titolo;
		//		command.Parameters.Add("@Descrizione",SqlDbType.VarChar).Value=studi.Descrizione;
		//		command.Parameters.Add("@MatrCv",SqlDbType.NVarChar).Value=MatrCv;
		//		 x = command.ExecuteNonQuery();
		//		command.Dispose();
		//		if (x == 0) { 
		//			throw new Exception("Nessun curriculum eliminato!");
		//			}				
		//	}catch(Exception e) {
		//		throw e;
		//	}finally {
		//		connection.Dispose();
		//	}
		//}
        public void AddEspLav(string MatrCv,EspLav esp) {
			try{
				SqlParameter[] param = { 
					new SqlParameter("@AnnoI",esp.AnnoInizio),
					new SqlParameter("@AnnoF",esp.AnnoFine),
					new SqlParameter("@Qualifica",esp.Qualifica),
					new SqlParameter("@Descrizione",esp.Descrizione),
					new SqlParameter("@matr",MatrCv)
				};
				int output = DB.ExecNonQProcedure("AddEspLav", param, "GeCv");
				if (output == 0) { 
					throw new Exception("Nessuna Esperienza Inserita");
				}
			} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}			
			//SqlConnection con= new SqlConnection(GetStringBuilderCV());
			//try {
			//	con.Open();
			//	SqlCommand command = new SqlCommand("AddEspLav",con);
			//	command.CommandType=CommandType.StoredProcedure;
			//	command.Parameters.Add("@AnnoI",SqlDbType.Int).Value=esp.AnnoInizio;
			//	command.Parameters.Add("@AnnoF",SqlDbType.Int).Value=esp.AnnoFine;
			//	command.Parameters.Add("@Qualifica",SqlDbType.NVarChar).Value=esp.Qualifica;
			//	command.Parameters.Add("@Descrizione",SqlDbType.NVarChar).Value=esp.Descrizione;
			//	command.Parameters.Add("@matr",SqlDbType.NVarChar).Value=MatrCv;
   //             int x = command.ExecuteNonQuery();
			//	command.Dispose();
			//	if (x == 0) { 
			//		throw new Exception("Nessuna Esperienza Inserita");
			//		}
				
			//}catch(Exception e) {
			//	throw e;
			//}finally {
			//	con.Dispose();
			//}
   //     }
		public void CaricaCV(string path) {
			throw new NotImplementedException();
		}
		public Commessa CercaCommessa(string nomeCommessa) {
			try {
				SqlParameter[] parameter = {
					new SqlParameter("@nomeCommessa", nomeCommessa)
				};			
				return DB.ExecQProcedureReader("SP_CercaCommessa", transf.TrasformInCommessa, parameter, "GeTime");
			} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}
		public void Compila(DateTime data, int ore, HType tipoOre, string idUtente) {
            try{ 
                SqlParameter[] parameters = {
					new SqlParameter("@giorno",data.ToString("yyyy-MM-dd")),
					new SqlParameter("@idUtente",idUtente),
					new SqlParameter("@ore", ore),
					new SqlParameter("@TipoOre", (int)tipoOre)
					};
				DB.ExecNonQProcedure("SP_Compila", parameters, "GeTime");
            } catch (SqlException e) {
                throw new Exception(e.Message);
            } catch (Exception e) {
                throw e;
            }
        }
		public void CompilaHLavoro(DateTime data,int ore,int idCommessa,string idUtente) {
			try {
                SqlParameter[] parameters = {
					new SqlParameter("@data",data.ToString("yyyy-MM-dd")),
					new SqlParameter("@ore", ore),
					new SqlParameter("@idCommessa",idCommessa),
					new SqlParameter("@idUtente", idUtente)
					};
                DB.ExecNonQProcedure("SP_AddHLavoro", parameters, "GeTime");
			} catch (Exception e){
				throw e;
			}
		}
		public void EliminaCV(CV curriculum) {
			try{
				SqlParameter[] param ={ new SqlParameter("@idcurr", curriculum.Matricola)};
				int output = DB.ExecNonQProcedure("dbo.DeleteCurriculum", param, "GeCv");
				if (output == 0) { 
					throw new Exception("Nessun curriculum eliminato!");
					}				
			}catch(SqlException e){
				throw new Exception(e.Message);
			}catch(Exception e){
				throw e;
			}
		}
		//	SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder {
		//		DataSource = @"(localdb)\MSSQLLocalDB",
		//		InitialCatalog = "GECV"
		//	};
		//	SqlConnection connection = new SqlConnection(builder.ToString());
		//	int x;
		//	try {
		//		connection.Open();
		//		SqlCommand command = new SqlCommand("dbo.DeleteCurriculum",connection) {
		//			CommandType = CommandType.StoredProcedure
		//		};
		//		command.Parameters.Add("@idcurr",SqlDbType.NVarChar).Value=curriculum.Matricola;
		//		 x = command.ExecuteNonQuery();
		//		command.Dispose();
		//		if (x == 0) { 
		//			throw new Exception("Nessun curriculum eliminato!");
		//			}				
		//	}catch(Exception e) {
		//		throw e;
		//	}finally {
		//		connection.Dispose();
		//	}
		//}
		public List<Giorno> GiorniCommessa(int idCommessa,string idUtente) {
			try{
				SqlParameter[] parameter = {
					new SqlParameter("@idC", idCommessa),				
					new SqlParameter("@idU",idUtente)
				};
				return DB.ExecQProcedureReader("SP_VisualizzaCommessa", transf.TrasformInListGiorno,parameter,"GeTime");
			}catch(SqlException e){
				throw new Exception(e.Message);
			}catch(Exception e){
				throw e;
			}
		}
		public List<Corso> ListaCorsi() {
		try{
			return DB.ExecQProcedureReader("ListaCorsi",transf.TrasformInListaCorso, null,"GeCorsi");       
		} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}
		public List<Corso> ListaCorsi(string idUtente) {
			try{
				SqlParameter[] param = { new SqlParameter ("@idStudente", idUtente) };
				return DB.ExecQProcedureReader("ListaCorsiStudenti",transf.TrasformInListaCorso,param,"GeCorsi");
			} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}
        //private string GetConnectinoCv() {
        //    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(){ 
        //        DataSource = @"(localdb)\MSSQLLocalDB",
        //        InitialCatalog = "GECV"
        //    };
        //    return builder.ToString();
        //}
        public void ModificaCV(string nome,string cognome,int eta,string email,string residenza,string telefono,string matr) {
			try{	
				SqlParameter[] parameter = {
					new SqlParameter("@cognome", cognome),
					new SqlParameter("@matr", matr),
					new SqlParameter("@nome", nome),
					new SqlParameter("@eta", eta),
					new SqlParameter("@email", email),
					new SqlParameter("@residenza", residenza),
					new SqlParameter("@telefono", telefono)
				};
				DB.ExecNonQProcedure("ModificaCV",parameter,"GeCv");
			}catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}		
		//SqlConnection con = new SqlConnection (GetConnectinoCv());
   //         try{ 
   //             con.Open();
   //             SqlCommand cmd = new SqlCommand ("ModificaCV",con){ CommandType = CommandType.StoredProcedure};
   //             cmd.Parameters.Add("@matr",SqlDbType.NVarChar).Value = matr;
   //             cmd.Parameters.Add("@nome",SqlDbType.VarChar).Value = nome;
   //             cmd.Parameters.Add("@cognome",SqlDbType.VarChar).Value = cognome;
   //             cmd.Parameters.Add("@eta",SqlDbType.Int).Value = eta;
   //             cmd.Parameters.Add("@email",SqlDbType.NVarChar).Value = email;
   //             cmd.Parameters.Add("@residenza",SqlDbType.VarChar).Value = residenza;
   //             cmd.Parameters.Add("@telefono",SqlDbType.NVarChar).Value = telefono;
   //             int x = cmd.ExecuteNonQuery();
   //             cmd.Dispose();
   //             if(x==0){
   //                 throw new Exception();      
   //             }
   //         }catch(Exception e){ 
   //             throw e;        
   //         }finally{ 
   //             con.Dispose();    
   //         }
   //     }
        public List<CV> SearchChiava(string chiave) {
			try{
				SqlParameter[] parameter ={ new SqlParameter("@parola", chiave) };
				return DB.ExecQProcedureReader ("dbo.CercaParolaChiava", transf.TransfListCV0,parameter,"GeCv");				
			}catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}		
		//	List<CV> trovati = new List<CV>();
		//	SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder {
		//		DataSource = @"(localdb)\MSSQLLocalDB",
		//		InitialCatalog = "GECV"
		//	};
		//	SqlConnection connection = new SqlConnection(builder.ToString());
		//	try {
		//		connection.Open();
		//		SqlCommand command = new SqlCommand("dbo.CercaParolaChiava",connection) {
		//			CommandType = CommandType.StoredProcedure
		//		};
		//		command.Parameters.Add("@parola",SqlDbType.NVarChar).Value=chiava;
		//		SqlDataReader reader = command.ExecuteReader();
		//		while (reader.Read()){
		//			trovati.Add(Search(reader.GetString(0)));
		//		}
		//		reader.Close();
		//		command.Dispose();
		//		return trovati;				
		//	}catch(Exception e) {
		//		throw e;
		//	}finally {
		//		connection.Dispose();
		//	}
		//}
		public List<CV> SearchCognome(string cognome) {
			try{
				SqlParameter[] param ={ new SqlParameter("@cognome",cognome) };
				return DB.ExecQProcedureReader("dbo.CercaCognome", transf.TransfListCV0,param,"GeCv");				
			}catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}		
		//	List<CV> trovati = new List<CV>();
		//	SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder {
		//		DataSource = @"(localdb)\MSSQLLocalDB",
		//		InitialCatalog = "GECV"
		//	};
		//	SqlConnection connection = new SqlConnection(builder.ToString());
		//	try {
		//		connection.Open();
		//		SqlCommand command = new SqlCommand("dbo.CercaCognome",connection) {
		//			CommandType = CommandType.StoredProcedure
		//		};
		//		command.Parameters.Add("@cognome", SqlDbType.NVarChar).Value=cognome;
		//		SqlDataReader reader = command.ExecuteReader();
		//		while (reader.Read()){
		//			trovati.Add(Search(reader.GetString(0)));
		//		}
		//		reader.Close();
		//		command.Dispose();
		//		return trovati;				
		//	}catch(Exception e) {
		//		throw e;
		//	}finally {
		//		connection.Dispose();
		//	}
		//}
		public Corso SearchCorsi(int idCorso) {
			try{
				SqlParameter[] param = {new SqlParameter("@IdCorso",idCorso)};
				return DB.ExecQProcedureReader("SearchCorso", transf.TrasformInCorso,param,"GeCorsi");
			} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}		
		public void Iscriviti(int idCorso,string idStudente) {
			try{
				SqlParameter[] param = {new SqlParameter("@IdCorso",idCorso), new SqlParameter("@matr",idStudente)};
				DB.ExecNonQProcedure("Iscrizione",param,"GeCorsi");
			} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}
		public List<Corso> SearchCorsi(string descrizione) {
			try{
				SqlParameter [] param = {new SqlParameter("@descrizione", descrizione)};
				return DB.ExecQProcedureReader("SearchCorsi", transf.TrasformInListaCorso,param, "GeCorsi");
			} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}
		public List<Corso> SearchCorsi(string descrizione,string idUtente) {
			try{
				SqlParameter [] param = {new SqlParameter("@descrizione", descrizione),
					new SqlParameter("@idStudente", idUtente)};
				return DB.ExecQProcedureReader("SearchCorsiStud", transf.TrasformInListaCorso,param,"GeCorsi");
			} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}
		public List<CV> SearchEta(int eta) {
			try{
				SqlParameter[] param = { new SqlParameter("@eta",eta)};
				return DB.ExecQProcedureReader("dbo.CercaEta",transf.TransfListCV0,param, "GeCv");
			}catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}
		//List<CV> trovati = new List<CV>();
		//	SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder {
		//		DataSource = @"(localdb)\MSSQLLocalDB",
		//		InitialCatalog = "GECV"
		//	};
		//	SqlConnection connection = new SqlConnection(builder.ToString());
		//	try {
		//		connection.Open();
		//		SqlCommand command = new SqlCommand("dbo.CercaEta",connection) {
		//			CommandType = CommandType.StoredProcedure
		//		};
		//		command.Parameters.Add("@eta",SqlDbType.Int).Value=eta;
		//		SqlDataReader reader = command.ExecuteReader();
		//		while (reader.Read()){
		//			trovati.Add(Search(reader.GetString(0)));
		//		}
		//		reader.Close();
		//		command.Dispose();
		//		return trovati;				
		//	}catch(Exception e) {
		//		throw e;
		//	}finally {
		//		connection.Dispose();
		//	}
		//}
		public List<CV> SearchRange(int etmin,int etmax) {
			try{
				SqlParameter[] parameters = {
					new SqlParameter("@e_min", etmin),
					new SqlParameter("@e_max", etmax)					
				};
				return DB.ExecQProcedureReader("dbo.CercaEtaMinMax", transf.TransfListCV0,parameters, "GeCv");
			}catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}
		//	List<CV> trovati = new List<CV>();
		//	SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder {
		//		DataSource = @"(localdb)\MSSQLLocalDB",
		//		InitialCatalog = "GECV"
		//	};
		//	SqlConnection connection = new SqlConnection(builder.ToString());
		//	try {
		//		connection.Open();
		//		SqlCommand command = new SqlCommand("dbo.CercaEtaMinMax",connection) {
		//			CommandType = CommandType.StoredProcedure
		//		};
		//		command.Parameters.Add("@e_min",SqlDbType.Int).Value=etmin;
		//		command.Parameters.Add("@e_max",SqlDbType.Int).Value=etmax;
		//		SqlDataReader reader = command.ExecuteReader();
		//		while (reader.Read()){
		//			trovati.Add(Search(reader.GetString(0)));
		//		}
		//		reader.Close();
		//		command.Dispose();
		//		return trovati;				
		//	}catch(Exception e) {
		//		throw e;
		//	}finally {
		//		connection.Dispose();
		//	}
		//}
		public Giorno VisualizzaGiorno(DateTime data, string idUtente) {
            try {
                SqlParameter[] parameter = {
					new SqlParameter("@Data", data.ToString("yyyy-MM-dd")),               
					new SqlParameter("@IdUtente", idUtente)
				};                
                Giorno result = DB.ExecQProcedureReader("SP_VisualizzaGiorno", transf.TrasformInGiorno, parameter, "GeTime");
                if(result!=null)
                    result.Data=data;
                return result;
            } catch (Exception e) {
                throw e;
            } 
		}
	}
	[Serializable]
	internal class LezionNonModificataException : Exception {
		public LezionNonModificataException() {}
		public LezionNonModificataException(string message) : base(message) {}
		public LezionNonModificataException(string message,Exception innerException) : base(message,innerException) {}
		protected LezionNonModificataException(SerializationInfo info,StreamingContext context) : base(info,context) {}
	}
	[Serializable]
	internal class LezioneNonAggiuntaException : Exception {
		public LezioneNonAggiuntaException() {}
		public LezioneNonAggiuntaException(string message) : base(message) {}
		public LezioneNonAggiuntaException(string message,Exception innerException) : base(message,innerException){}
		protected LezioneNonAggiuntaException(SerializationInfo info,StreamingContext context) : base(info,context){}
	}
	[Serializable]
	internal class CorsoNonAggiuntaException : Exception {
		public CorsoNonAggiuntaException() {}
		public CorsoNonAggiuntaException(string message) : base(message) { }
		public CorsoNonAggiuntaException(string message,Exception innerException) : base(message,innerException) {}
		protected CorsoNonAggiuntaException(SerializationInfo info,StreamingContext context) : base(info,context) {}
	}
	}