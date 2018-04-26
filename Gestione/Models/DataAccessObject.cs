using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using Interfaces;
using LibreriaDB;
using System.Data;

namespace DAO {
	public interface IDao{
        //modifica un curriculum nel db
		void ModificaCV(CV c);
        //quando sei loggato, puoi aggiungere un curriculum nel db
		void AggiungiCV(CV a);
        //quando non sei loggato, puoi spedire un curriuculum
		void CaricaCV(string path);
        //search di un curriculum per id di un curriculum
		CV Search(string id);
        //search generale per parole chiava
		List<CV> SearchChiava(string chiava);
        //search solo per quella precisa età
		List<CV> SearchEta(int eta);
        //search per un range di età minimo e massimo
		List<CV> SearchRange(int etmin, int etmax);
        //Elimina un CV dal db
		void EliminaCV(CV curriculum);
        //Ricerca solo per cognome
		List<CV> SearchCognome(string cognome);
        void AddCvStudi(string MatrCv,PerStud studi);
        void AddEspLav(string MatrCv, EspLav esp);
        void AddCompetenze(string MatrCv, Competenza comp);
		void DelEspLav(EspLav espLav , string matricola);
		void DelCompetenza(Competenza comp , string matricola);
		void DelPerStud(PerStud ps , string matricola);
        void ModEspLav(string MatrCv, EspLav espV, EspLav esp );
        //Modifica la singola competenza
		void ModComp(string matricola, Competenza daMod , Competenza Mod );
        void ModPerStudi(string matricola, PerStud daMod, PerStud Mod);
        void CompilaHLavoro(DateTime data, int ore, int idCommessa, string idUtente);
		void Compila(DateTime data, int ore, HType tipoOre, string idUtente);
		Giorno VisualizzaGiorno(DateTime data, string idUtente);
		List<Giorno> GiorniCommessa(int idCommessa, string idUtente);
		Commessa CercaCommessa(string nomeCommessa);
        //Aggiungi nuovo corso. Lo puo fare solo l'admin
        void AddCorso(Corso corso);
        //Aggiungi una lezione a un determinato corso. Lo puo fare solo il prof
        void AddLezione(int idCorso, Lezione lezione);
		void ModLezione(Lezione lezione);
        //Iscrizione di uno studente a un determinato corso. Lo puo fare solo lo studente specifico
        void Iscriviti(int idCorso, string idStudente);
        //Cerca un determinato corso 
        Corso SearchCorsi(int idCorso);
        //Cerca tutti i corsi che contine la "descrizione" nei suoi attributi(nome,descrizione)
        List<Corso> SearchCorsi(string descrizione);
        //Cerca tutti i corsi che contiene la "descrizione" di un determinato studente(idStudente)
        List<Corso> SearchCorsi(string descrizione, string idUtente);
        //Mostra tutti i corsi presenti nel db
        List<Corso> ListaCorsi();
        //Mostra tutti i corsi a cui è iscritto un determinato studente(idStudente)
        List<Corso> ListaCorsi(string idUtente);
		//mostra tutte le lezioni associate a un corso
		List<Lezione> ListaLezioni(Corso corso);
    }
	
	public partial class DataAccesObject : IDao {
		ITrasformer transf = new Trasformator();
		//GeCv
		public void ModComp(string matricola , Competenza daMod , Competenza Mod){
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
			} catch (SqlException) {
				throw new Exception("Errore server!");
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
			} catch (SqlException) {
				throw new Exception("Errore server!");
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
			} catch (SqlException){
				throw new Exception("Errore server!");
			} catch (Exception e){
				throw e;
			}
		}
		public void AggiungiCV(CV c){
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
			} catch(SqlException){
				throw new Exception("Errore server!");
			} catch(Exception e){
				throw e;
			}
		}
		private List<EspLav> GetEspLav(string matricola){
		try{			
			SqlParameter[] param = {new SqlParameter("@Matricola",matricola)};
			List<EspLav> output = DB.ExecQProcedureReader("GetEspLav",transf.TransfInListEspLav,param, "GeCv");
				return output;
			} catch(SqlException){
				throw new Exception("Errore server!");
			} catch(Exception e) {
				throw e;
			}
		}
		private List<PerStud> GetPerStudi(string matricola){
			try{
				SqlParameter[] param = {new SqlParameter("@Matricola", matricola)};
				List<PerStud> output = DB.ExecQProcedureReader("GetPerStudi",transf.TransfInListPerstud,param, "GeCv");
				return output;
			} catch(SqlException){
				throw new Exception("Errore server!");
			} catch(Exception e){
				throw e;
			}
		}
		private List<Competenza> GetComp(string matricola){
			try{
				SqlParameter[] param = {new SqlParameter("@Matricola", matricola)};
				List<Competenza> output = DB.ExecQProcedureReader("GetComp",transf.TransfInCompetenze,param, "GeCv");
				return output;
			} catch(SqlException){
				throw new Exception("Errore server!");
			} catch(Exception e){
				throw e;
			}
		}
		public CV Search(string matr){
			try{
				SqlParameter[] param = {new SqlParameter("@Matricola",matr)};
				CV output = DB.ExecQProcedureReader("GetCv",transf.TransfInCv,param,"GeCv");
				output.Percorsostudi= GetPerStudi(output.Matricola);
				output.Esperienze = GetEspLav(output.Matricola);
				output.Competenze = GetComp(output.Matricola);
				return output;
			} catch(SqlException){
				throw new Exception("Errore server!");
			} catch(Exception e){
				throw e;
			}
		}

        //Da controllare
        public void ModificaCV(CV a,CV b){
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
			} catch(SqlException){
				throw new Exception("Errore server!");
			} catch(Exception e){
				throw e;
			}
		}
		public void AddCompetenze(string MatrCv,Competenza comp){
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
			} catch(SqlException){
				throw new Exception("Errore server!");
			} catch(Exception e){
				throw e;
			}
		}
        public void AddCvStudi(string MatrCv, PerStud studi){
            try {
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
            } catch(SqlException){
                throw new Exception("Errore server!");
            } catch(Exception e){
                throw e;
            }
        }
        public void AddEspLav(string MatrCv, EspLav esp){
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
            } catch(SqlException){
                throw new Exception("Errore server!");
            } catch (Exception e){
                throw e;
            }
        }
        public void ModificaCV(CV c){
            try{
                SqlParameter[] parameter = {
                    new SqlParameter("@cognome", c.Cognome),
                    new SqlParameter("@matr", c.Matricola),
                    new SqlParameter("@nome", c.Nome),
                    new SqlParameter("@eta", c.Eta),
                    new SqlParameter("@email", c.Email),
                    new SqlParameter("@residenza", c.Residenza),
                    new SqlParameter("@telefono", c.Telefono)
                };
                DB.ExecNonQProcedure("ModificaCV", parameter, "GeCv");
            } catch(SqlException){
                throw new Exception("Errore server!");
            } catch(Exception e){
                throw e;
            }
        }
        public List<CV> SearchChiava(string chiave){
            try {
                SqlParameter[] parameter = { new SqlParameter("@parola", chiave) };
                return DB.ExecQProcedureReader("dbo.CercaParolaChiava", transf.TransfListCV0, parameter, "GeCv");
            } catch(SqlException){
                throw new Exception("Errore server!");
            } catch(Exception e){
                throw e;
            }
        }
        public List<CV> SearchCognome(string cognome){
            try {
                SqlParameter[] param = { new SqlParameter("@cognome", cognome) };
                return DB.ExecQProcedureReader("dbo.CercaCognome", transf.TransfListCV0, param, "GeCv");
            } catch(SqlException){
                throw new Exception("Errore server!");
            } catch(Exception e){
                throw e;
            }
        }
        public List<CV> SearchRange(int etmin, int etmax){
            try {
                SqlParameter[] parameters = {
                    new SqlParameter("@e_min", etmin),
                    new SqlParameter("@e_max", etmax)
                };
                return DB.ExecQProcedureReader("dbo.CercaEtaMinMax", transf.TransfListCV0, parameters, "GeCv");
            } catch(SqlException){
                throw new Exception("Errore server!");
            } catch(Exception e){
                throw e;
            }
        }
        public List<CV> SearchEta(int eta){
            try {
                SqlParameter[] param = { new SqlParameter("@eta", eta) };
                return DB.ExecQProcedureReader("dbo.CercaEta", transf.TransfListCV0, param, "GeCv");
            } catch(SqlException){
                throw new Exception("Errore server!");
            } catch(Exception e){
                throw e;
            }
        }
		public void EliminaCV(CV curriculum){
			try{
				SqlParameter[] param ={ new SqlParameter("@idcurr", curriculum.Matricola)};
				int output = DB.ExecNonQProcedure("dbo.DeleteCurriculum", param, "GeCv");
				if (output == 0) { 
					throw new Exception("Nessun curriculum eliminato!");
					}				
			}catch(SqlException){
				throw new Exception("Errore server!");
			}catch(Exception e){
				throw e;
			}
		}		
        public void CaricaCV(string path){
            throw new NotImplementedException();
        }

        //GeTime 
		public Commessa CercaCommessa(string nomeCommessa){
			try {
				SqlParameter[] parameter = {
					new SqlParameter("@nomeCommessa", nomeCommessa)
				};			
				return DB.ExecQProcedureReader("SP_CercaCommessa", transf.TrasformInCommessa, parameter, "GeTime");
			} catch(SqlException){
				throw new Exception("Errore server!");
			} catch(Exception e){
				throw e;
			}
		}
		public void Compila(DateTime data, int ore, HType tipoOre, string idUtente){
            try{ 
                SqlParameter[] parameters = {
					new SqlParameter("@giorno",data.ToString("yyyy-MM-dd")),
					new SqlParameter("@idUtente",idUtente),
					new SqlParameter("@ore", ore),
					new SqlParameter("@TipoOre", (int)tipoOre)
					};
				DB.ExecNonQProcedure("SP_Compila", parameters, "GeTime");
            } catch(SqlException){
                throw new Exception("Errore server!");
            } catch(Exception e){
                throw e;
            }
        }
		public void CompilaHLavoro(DateTime data,int ore,int idCommessa,string idUtente){
			try{
                SqlParameter[] parameters = {
					new SqlParameter("@data",data.ToString("yyyy-MM-dd")),
					new SqlParameter("@ore", ore),
					new SqlParameter("@idCommessa",idCommessa),
					new SqlParameter("@idUtente", idUtente)
					};
                DB.ExecNonQProcedure("SP_AddHLavoro", parameters, "GeTime");
			} catch(SqlException){
                throw new Exception("Errore server!");
			} catch(Exception e){
				throw e;
			}
		}
		public Giorno VisualizzaGiorno(DateTime data, string idUtente){
            try{
                SqlParameter[] parameter = {
					new SqlParameter("@Data", data.ToString("yyyy-MM-dd")),               
					new SqlParameter("@IdUtente", idUtente)
				};                
                Giorno result = DB.ExecQProcedureReader("SP_VisualizzaGiorno", transf.TrasformInGiorno, parameter, "GeTime");
                if(result!=null)
                    result.Data=data;
                return result;
			} catch(SqlException){
                throw new Exception("Errore server!");
            } catch(Exception e){
                throw e;
            } 
		}
		public List<Giorno> GiorniCommessa(int idCommessa,string idUtente){
			try{
				SqlParameter[] parameter = {
					new SqlParameter("@idC", idCommessa),				
					new SqlParameter("@idU",idUtente)
				};
				return DB.ExecQProcedureReader("SP_VisualizzaCommessa", transf.TrasformInGiorni,parameter,"GeTime");
			}catch(SqlException){
				throw new Exception("Errore server!");
			}catch(Exception e){
				throw e;
			}
		}

		//GeCo
        public List<Lezione> ListaLezioni(Corso corso){
			try{
				SqlParameter[] param = {new SqlParameter("@IdCorso",corso.Id)};
				return DB.ExecQProcedureReader("ListaLezioni",transf.TrasformInLezioni,param,"GeCorsi");
			}catch(SqlException){
				throw new Exception("Errore server!");
			}catch(Exception e){
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
				int RowAffected =DB.ExecNonQProcedure("ModLezione",param,"GeCorsi");
				if (RowAffected == 0) {
					throw new LezionNonModificataException("Non hai modificato la lezione");
				}
			} catch(SqlException){
				throw new Exception("Errore server!");
			} catch(Exception e){
				throw e;
			}
		}
        public void ModCorso(Corso corso) {
            try {
                SqlParameter[] param = {
                    new SqlParameter("@idCorso",corso.Id),
                    new SqlParameter("@nome",corso.Nome),
                    new SqlParameter("@descrizione",corso.Descrizione),
                    new SqlParameter("@inizio",corso.Inizio),
                    new SqlParameter("@fine",corso.Fine)
                };
                int RowAffected = DB.ExecNonQProcedure("ModCorso", param, "GeCorsi");
                if (RowAffected == 0) {
                    throw new LezionNonModificataException("Non hai modificato la lezione");
                }
            } catch (SqlException) {
                throw new Exception("Errore server!");
            } catch (Exception e) {
                throw e;
            }

        }
        public void AddLezione(int idCorso,Lezione lezione){
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
			} catch(SqlException){
				throw new Exception("Errore server!");
			} catch(Exception e){
				throw e;
			}
		}
        public void AddCorso(Corso corso){
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
			} catch(SqlException){
				throw new Exception("Errore server!");
			} catch(Exception e){
				throw e;
			}
		}
		public List<Corso> ListaCorsi(){
		    try{
			    return DB.ExecQProcedureReader("ListaCorsi",transf.TrasformInCorsi, null,"GeCorsi");       
		    } catch(SqlException){
				throw new Exception("Errore server!");
			} catch(Exception e){
				throw e;
			}
		}
		public List<Corso> ListaCorsi(string idUtente){
			try{
				SqlParameter[] param = { new SqlParameter ("@idStudente", idUtente) };
				return DB.ExecQProcedureReader("ListaCorsiStudenti",transf.TrasformInCorsi,param,"GeCorsi");
			} catch(SqlException){
				throw new Exception("Errore server!");
			} catch(Exception e){
				throw e;
			}
		}     	
		public Corso SearchCorsi(int idCorso){
			try{
				SqlParameter[] param = {new SqlParameter("@IdCorso",idCorso)};
				return DB.ExecQProcedureReader("SearchCorso", transf.TrasformInCorso,param,"GeCorsi");
			} catch(SqlException){
				throw new Exception("Errore server!");
			} catch(Exception e){
				throw e;
			}
		}		
		public void Iscriviti(int idCorso,string idStudente){
			try{
				SqlParameter[] param = {new SqlParameter("@IdCorso",idCorso), new SqlParameter("@matr",idStudente)};
				DB.ExecNonQProcedure("Iscrizione",param,"GeCorsi");
			} catch(SqlException){
				throw new Exception("Errore server!");
			} catch(Exception e){
				throw e;
			}
		}
		public List<Corso> SearchCorsi(string descrizione){
			try{
				SqlParameter [] param = {new SqlParameter("@descrizione", descrizione)};
				return DB.ExecQProcedureReader("SearchCorsi", transf.TrasformInCorsi,param, "GeCorsi");
			} catch(SqlException){
				throw new Exception("Errore server!");
			} catch(Exception e){
				throw e;
			}
		}
		public List<Corso> SearchCorsi(string descrizione,string idUtente){
			try{
				SqlParameter [] param = {new SqlParameter("@descrizione", descrizione),
					new SqlParameter("@idStudente", idUtente)};
				return DB.ExecQProcedureReader("SearchCorsiStud", transf.TrasformInCorsi,param,"GeCorsi");
			} catch(SqlException){
				throw new Exception("Errore server!");
			} catch(Exception e){
				throw e;
			}
		}

		public void DelEspLav(EspLav espLav,string matricola) {
			SqlConnection con= new SqlConnection(GetStringBuilderCV());
			try {
				con.Open();
				SqlCommand command = new SqlCommand("DelEspLav",con);
				command.CommandType=CommandType.StoredProcedure;
				command.Parameters.Add("@annoIdaDel",SqlDbType.Int).Value=espLav.AnnoInizio;
				command.Parameters.Add("@annoFdaDel",SqlDbType.Int).Value=espLav.AnnoFine;
				command.Parameters.Add("@qualificaDaDel",SqlDbType.NVarChar).Value=espLav.Qualifica;
				command.Parameters.Add("@descrDaDel",SqlDbType.NVarChar).Value=espLav.Descrizione;
				command.Parameters.Add("@matricola",SqlDbType.NVarChar).Value=matricola;
                int x = command.ExecuteNonQuery();
				command.Dispose();
				if (x == 0) { 
					throw new Exception("Nessuna Esperienza eliminata");
					}
				
			}catch(Exception e) {
				throw e;
			}finally {
				con.Dispose();
			}
		}

		public void DelCompetenza(Competenza comp,string matricola) {
			SqlConnection con= new SqlConnection(GetStringBuilderCV());
			try {
				con.Open();
				SqlCommand command = new SqlCommand("DelComp",con);
				command.CommandType=CommandType.StoredProcedure;
				command.Parameters.Add("@titolo",SqlDbType.NVarChar).Value=comp.Titolo;
				command.Parameters.Add("@livello",SqlDbType.Int).Value=comp.Livello;
				command.Parameters.Add("@matricola",SqlDbType.NVarChar).Value=matricola;
                int x = command.ExecuteNonQuery();
				command.Dispose();
				if (x == 0) { 
					throw new Exception("Nessuna Esperienza eliminata");
					}
				
			}catch(Exception e) {
				throw e;
			}finally {
				con.Dispose();
			}
		}

		public void DelPerStud(PerStud ps,string matricola) {
			SqlConnection connection = new SqlConnection(GetStringBuilderCV());
			try{
				connection.Open();
				SqlCommand command = new SqlCommand("DelPerStud",connection);
				command.CommandType = System.Data.CommandType.StoredProcedure;
				command.Parameters.Add("@matricola", System.Data.SqlDbType.NVarChar).Value= matricola;
				command.Parameters.Add("@AnnoIniz", System.Data.SqlDbType.Int).Value= ps.AnnoInizio;
				command.Parameters.Add("@AnnoFine", System.Data.SqlDbType.Int).Value= ps.AnnoFine;
				command.Parameters.Add("@Titolo", System.Data.SqlDbType.NVarChar).Value= ps.Titolo;
				command.Parameters.Add("@Descr", System.Data.SqlDbType.NVarChar).Value= ps.Descrizione;
				command.ExecuteNonQuery();
				command.Dispose();
			}catch(Exception e ){
				throw e ;
			}finally{
				connection.Dispose();
			}
		}
		private string GetStringBuilderCV() {
			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder {
				DataSource = @"(localdb)\MSSQLLocalDB",
				InitialCatalog = "GECV"
			};
			return builder.ToString();
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