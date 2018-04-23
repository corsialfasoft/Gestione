using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace LibreriaDB{
    public sealed class DB{
		private DB(){ }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="DBName">Inserire il nome DateBase, Default "master"</param>
		/// <param name="ServerName">Inserire il nome del Server, Default "(localdb)\MSSQLLocalDB"</param>
		/// <returns>SqlConnectionString</returns>
		public static string GetConnectionString(string DBName="master", string ServerName= @"(localdb)\MSSQLLocalDB") {
			SqlConnectionStringBuilder SB = new SqlConnectionStringBuilder {
				DataSource = ServerName,
				InitialCatalog = DBName
			};
			return SB.ToString();
		}
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T">Rappresenta il dato da ritornare</typeparam>
		/// <param name="data">SqlDataReader da cui leggere i dati e trasformarli in tipo "T"</param>
		/// <returns>"T"</returns>
		public delegate T Trasform<T>(SqlDataReader data);
		/// <summary>
		/// Esegue le Query e salva i dati in un SqlDataReader
		/// </summary>
		/// <typeparam name="T">Rappresenta il dato da ritornare</typeparam>
		/// <param name="trasform">Il delegato che si occupa di trasformare i Dati che ha ricevuto della Query</param>
		/// <param name="sql">La Query da esequire</param> 
		/// <param name="DBName">Inserire il nome DateBase, Default "master"</param>
		/// <param name="ServerName">Inserire il nome del Server, Default "(localdb)\MSSQLLocalDB"</param>
		/// <returns>"T"</returns>
		public static T ExecQReader<T>(Trasform<T> trasform, string sql, string DBName = "master", string ServerName = @"(localdb)\MSSQLLocalDB") {
			SqlConnection connection = new SqlConnection(GetConnectionString(DBName, ServerName));
			try {
				connection.Open();
				SqlCommand command = new SqlCommand(sql, connection);
				SqlDataReader data = command.ExecuteReader();
				T ris = trasform(data);
				data.Close();
				command.Dispose();
				return ris;
			} catch (Exception e) {
				throw e;
			} finally {
				connection.Dispose();
			}
		}
		/// <summary>
		///  Esegue le NONQuery
		/// </summary>
		/// <param name="sql">La NONQuery da esequire</param> 
		/// <param name="DBName">Inserire il nome DateBase, Default "master"</param>
		/// <param name="ServerName">Inserire il nome del Server, Default "(localdb)\MSSQLLocalDB"</param>
		/// <returns>Al numero di righe "affected" (cambiate)</returns>
		public static int ExecNonQ(string sql, string DBName = "master", string ServerName = @"(localdb)\MSSQLLocalDB") {
			SqlConnection connection = new SqlConnection(GetConnectionString(DBName, ServerName));
			try {
				connection.Open();
				SqlCommand command = new SqlCommand(sql, connection);
				int i = command.ExecuteNonQuery();
				command.Dispose();
				return i;
			} catch (Exception e) {
				throw e;
			} finally {
				connection.Dispose();
			}
		}
		/// <summary>
		/// Esegue le Query e salva i dati in un DataSet.
		/// La sequenza delle query SQL(separate da ';') devono avere lo stesso ordine dei Nomi in nameTable [sql[i] corrisponde nameTable[i]];
		/// </summary>
		/// <param name="sql">La Query da esequire</param> 
		/// <param name="nameTable">Nome delle tabelle che vengono salvate in DataSet</param>
		/// <param name="DBName">Inserire il nome DateBase, Default "master"</param>
		/// <param name="ServerName">Inserire il nome del Server, Default "(localdb)\MSSQLLocalDB"</param>
		/// <returns>DataSet</returns>
		public static DataSet ExecQDataSet(string sql, string[] nameTable, string DBName = "master", string ServerName = @"(localdb)\MSSQLLocalDB") {
			SqlConnection connection = new SqlConnection(GetConnectionString(DBName, ServerName));
			try {
				connection.Open();
				SqlCommand command = new SqlCommand(sql, connection);
				DataSet ris = GetDataSet(nameTable, command);
				command.Dispose();
				return ris;
			} catch (Exception e) {
				throw e;
			} finally {
				connection.Dispose();
			}
		}
		private static DataSet GetDataSet(string[] nameTable, SqlCommand command) {
			SqlDataAdapter data = new SqlDataAdapter();
			for (int i = 0; i < nameTable.Length; i++) {
				if (i == 0)
					data.TableMappings.Add("Table", nameTable[i]);
				else
					data.TableMappings.Add("Table" + i, nameTable[i]);
			}
			data.SelectCommand = command;
			DataSet dataSet = new DataSet();
			data.Fill(dataSet);
			data.Dispose();
			return dataSet;
		}
        /// <summary>
        /// Legge da un file che contiene codice Sql(ogni query deve essere separato da "default ';') e lo eseque come singolo query sql")
        /// </summary>
        /// <param name="Path">il path Assoluto/relativo del file da cui leggere le query ed esguire</param>
        /// <param name="charSeparator">Separatore di query di default ';'</param>
        /// <param name="DBName">Inserire il nome DateBase, Default "master"</param>
        /// <param name="ServerName">Inserire il nome del Server, Default "(localdb)\MSSQLLocalDB"</param>
        /// <returns>Al numero di operazioni esequite</returns>
        public static int ExecQFromFile(string Path,char charSeparator = ';', string DBName = "master", string ServerName = @"(localdb)\MSSQLLocalDB") {
			int NExec = 0;
			string str = "";
			try{ 
				str = File.ReadAllText(@Path);
			}catch(Exception e){
				throw e;
			}
			str = str.Replace("\t","");
            str = str.Replace("\r","");
			str = str.Replace('\n',' ');
			char[] charSeparators = new char[] {charSeparator};
			string[] queries = str.Split(charSeparators,StringSplitOptions.RemoveEmptyEntries);
			try{
				foreach(string query in queries){
					if(query.ToLower().Contains("use ")){
						string use = query.Replace("use ","");
						use = use.Replace("USE ", "");
						use = use.Replace(" ","");
						DBName = use;
					}else{
						ExecNonQ(query,DBName,ServerName);
					}
					NExec++;
				}
			} catch (Exception e) {
				throw e;
			}
			return NExec;
		}
		/// <summary>
        /// implementazione del delegato
        /// </summary>
        /// <param name="data">SqlDataReader da cui leggere i dati e</param>
        /// <returns></returns>
		public static int TrasformInt(SqlDataReader data) {
			if (data.Read()) {
				if (data.GetValue(0).GetType().Name == typeof(decimal).GetType().Name)
					return Decimal.ToInt32((Decimal)data.GetValue(0));
				return Convert.ToInt32(data.GetValue(0));
			}
			return 0;
		}
		/// <summary>
        /// implementazione del delegato
        /// </summary>
        /// <param name="data">SqlDataReader da cui leggere i dati. </param>
        /// <returns></returns>
		public static List<int> TrasformLInt(SqlDataReader data) {
			List<int> ris = new List<int>();
			while (data.Read()) {
				ris.Add(data.GetInt32(0));
			}
			return ris;
		}
        /// <summary>
        /// Drop di un DataBase
        /// </summary>
        /// <param name="DBName">Nome del DB</param>
        public static void DropDB(string DBName) {
			string sql = $"drop database {DBName}";
			ExecNonQ(sql);
		}
		public static int ExecNonQProcedure(string procedureName,SqlParameter[] sqlParameters=null, string DBName = "master", string ServerName = @"(localdb)\MSSQLLocalDB") {
			SqlConnection connection = new SqlConnection(GetConnectionString(DBName, ServerName));
			try {
				connection.Open();
				SqlCommand command = new SqlCommand(procedureName,connection) {
					CommandType = CommandType.StoredProcedure
				};
				if(sqlParameters!=null){
					command.Parameters.AddRange(sqlParameters);
				}
				int i = command.ExecuteNonQuery();
				command.Dispose();
				return i;
			} catch (Exception e) {
				throw e;
			} finally {
				connection.Dispose();
			}
		}
		public static T ExecQProcedureReader<T>(string procedureName,Trasform<T> trasform, SqlParameter[] sqlParameters=null, string DBName = "master", string ServerName = @"(localdb)\MSSQLLocalDB") {
			SqlConnection connection = new SqlConnection(GetConnectionString(DBName, ServerName));
			try {
				connection.Open();
				SqlCommand command = new SqlCommand(procedureName,connection) {
					CommandType = CommandType.StoredProcedure
				};
				if (sqlParameters != null) {
					command.Parameters.AddRange(sqlParameters);
				}
				SqlDataReader reader = command.ExecuteReader();
				T ris = trasform(reader);
				reader.Close();
				command.Dispose();
				return ris;
			} catch (Exception e) {
				throw e;
			} finally{
				connection.Dispose();
			}
		}
		public static int ExecQFromFileProcedure(string Path, string stringSeparator = "go", string DBName = "master", string ServerName = @"(localdb)\MSSQLLocalDB") {
			int NExec = 0;
			string str = "";
			try {
				str = File.ReadAllText(@Path);
			} catch (Exception e) {
				throw e;
			}
			str = str.Replace("\t", "");
			str = str.Replace("\r", "");
			str = str.Replace('\n', ' ');
			string[] queries = str.Split(new string[]{ stringSeparator }, StringSplitOptions.RemoveEmptyEntries);
			try {
				foreach (string query in queries) {
					ExecNonQ(query, DBName, ServerName);
					NExec++;
				}
			} catch (Exception e) {
				throw e;
			}
			return NExec;
		}
        public static List<T> TrasformInList<T>(SqlDataReader reader, Trasform<T> trasform) {
            List<T> output = new List<T>();
            do {
                T tmp = trasform(reader);
                if (tmp == null)
                    break;
                output.Add(tmp);
            } while (true);
            return output;
        }
    }

}
