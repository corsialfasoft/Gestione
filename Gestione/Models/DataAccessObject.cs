using System;
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
		public void AddCorso(Corso corso) {
			throw new NotImplementedException();
		}

		public void AddLezione(int idCorso,Lezione lezione) {
			throw new NotImplementedException();
		}

		public void AggiungiCV(CV a) {
			//
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
            //
			throw new NotImplementedException();
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

		public void ModificaCV(CV a,CV b) {
			//
		}

		public CV Search(string id) {
			return new CV {nome="Massimo",cognome="franzoso",telefono="3391627441",eta=33};
		}

		public List<CV> SearchChiava(string chiava) {
			List<CV> trovati = new List<CV>();
			if (chiava == "truzzotunztunz"){
			CV a = new CV { nome="Pino",cognome="Panino",telefono="123",email="truzzotunztunz"};
			CV b = new CV { nome ="Alex",cognome="dimitri",email="truzzotunztunz"};
			CV c = new CV { nome="Dino",cognome="sauro",email="truzzotunztunz"};
			trovati.Add(a);
			trovati.Add(b);
			trovati.Add(c);
			}
			return trovati;
		}

		public List<CV> SearchCognome(string cognome) {
			List<CV> trovati = new List<CV>();
			if (cognome == "Franzoso"){
			trovati.Add(new CV{cognome="Franzoso"});
			}
			return trovati;
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
			if (eta == 22){
			CV a = new CV { nome="Pino",cognome="Panino",telefono="123",email="truzzotunztunz",eta=22};
			CV b = new CV { nome ="Alex",cognome="dimitri",email="weasd",eta=22};
			CV c = new CV { nome="Dino",cognome="sauro",email="eeeeee",eta=22};
			trovati.Add(a);
			trovati.Add(b);
			trovati.Add(c);
			}
			return trovati;
		}

		public List<CV> SearchRange(int etmin,int etmax) {
			List<CV> trovati = new List<CV>();
			if (etmin>=22 && etmax<=25){
			CV a = new CV { nome="Pino",cognome="Panino",telefono="123",email="truzzotunztunz",eta=25};
			CV b = new CV { nome ="Alex",cognome="dimitri",email="weasd",eta=22};
			
			trovati.Add(a);
			trovati.Add(b);
			}
			return trovati;
		}

		public Giorno VisualizzaGiorno(DateTime data,int idUtente) {
			throw new NotImplementedException();
		}


        public void IscrizioneAlPortale(string nome,string cognome,string usr,string pass) {
            if(Reader<string>(TakeMatricola, $"SELECT Users FROM Utente WHERE Users='{usr}'") == ""){
                Procedura($"exec CreateMatricola '{nome}','{cognome}','{usr}','{pass}',4");
            } else {
                throw new Exception();
            }
        }

        public string SearchMatricola(string usr,string psw) {
            return Reader<string>(TakeMatricola, $"SELECT Matricola FROM Utente WHERE Users='{usr}' and Passwd='{psw}'");
        }

        public Profilo SearchProfile(string matricola) {
            return Reader<Profilo>(TakeProfilo,$"SELECT Utente.Matricola,Utente.Nome,Utente.Cognome,Utente.Users,Utente.Passwd,TipoUtente.Tipo,F.id,F.sistema,F.descrizione "+
                "FROM Utente INNER JOIN TipoUtente ON Utente.fkTipo = TipoUtente.Id INNER JOIN TipoUtenteFunzioni UF on Utente.fkTipo = UF.idtipoUtente"
                +" INNER JOIN Funzioni F on UF.idFunzione = F.idWHERE Matricola ='{matricola}'");
        }

        private Profilo TakeProfilo(SqlDataReader reader) {
            Profilo profile = new Profilo();
            while (reader.Read()){
                profile.Matricola = reader.GetString(0);
                profile.Nome = reader.GetString(1);
                profile.Cognome =  reader.GetString(2);
                profile.usr = reader.GetString(3);
                profile.pswd = reader.GetString(4);
                profile.Ruolo = reader.GetString(5);
            }
            reader.Close();
            return profile;
        }

        private SqlConnection InitConnection() {
			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
			builder.DataSource = @"(localdb)\MSSQLLocalDB";
			builder.InitialCatalog = "Profilatura";
			return new SqlConnection(builder.ConnectionString);
		}

        public void Procedura(string sql){
            SqlConnection connection = InitConnection();
			try {
				connection.Open();				
				SqlCommand cmd = new SqlCommand(sql, connection);
				cmd.ExecuteNonQuery();
				cmd.Dispose();
			} catch (Exception e) {
				throw e;
			} finally {
				connection.Dispose();
			}
        }

        public delegate T Delelato<T>(SqlDataReader reader);
        public T Reader<T>(Delelato<T> metodo, string sql){
            SqlConnection con = InitConnection();
            try{
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();
                T ris = metodo(reader);
                reader.Dispose();
                cmd.Dispose();
                return ris;
            }
            catch(Exception e){
                throw e;
            }
            finally{
                con.Dispose();
            }
        }
        public string TakeMatricola(SqlDataReader reader){
            string matricola = "";
            while (reader.Read()){
                matricola = reader.GetString(0);
            }
            reader.Close();
            return matricola;
        }
	}
}