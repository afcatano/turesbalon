using CommonsWeb.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;

namespace CommonsWeb.DAL
{
    /// <summary>
    /// Clase que implementa las operaciones de la conexión a la base de datos
    /// </summary>
    public class SqlServerHelper
    {
        private static readonly string connectionString;
        private static object syncRoot;
        private static SqlServerHelper instance;

        static SqlServerHelper()
        {
            syncRoot = new Object();
            connectionString = GetConectionString(Settings.Default.DataSourceSQL, Settings.Default.DataBaseNameSQL, Settings.Default.UserIDSQL, Settings.Default.PasswordSQL, Settings.Default.TimeOutConnectionSQL);
            instance = null;
        }

        public static SqlServerHelper GetInstace()
        {
            lock (syncRoot)
            {
                if (instance == null)
                {
                    instance = new SqlServerHelper();
                }
            }
            return instance;
        }

        /// <summary>
        /// stringBuilder Nativo de SqlClient
        /// </summary>
        /// <param name="Server">DataSource IpServer</param>
        /// <param name="DataSource">Nombre de base de datos</param>
        /// <param name="UserName">User Id</param>
        /// <param name="Password">Password</param>
        /// <returns>string</returns>
        private static string GetConectionString(string dataSource, string dataBaseName, string userId, string password, int timeOut)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = dataSource.Trim();
            builder.InitialCatalog = dataBaseName.Trim();
            builder.UserID = userId.Trim();
            builder.Password = password.Trim();
            builder.AsynchronousProcessing = true;
            builder.ConnectTimeout = timeOut;
            return builder.ConnectionString;
        }


        /// <summary>
        /// Ejecuta el script SQL que recibe como parametro y 
        /// retorna un DataSet con los valores de la consulta.
        /// </summary>
        /// <param name="sentenceSql"></param>
        /// <returns>DataSet</returns>
        /// <exception cref="DAL.Helper.DALException"/>
        public DataSet ExecuteQuery(string sentenceSql)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                DataSet ResultsDataSet = new DataSet();
                ResultsDataSet.Locale = CultureInfo.InvariantCulture;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    adapter.SelectCommand = new SqlCommand(sentenceSql, connection);
                    adapter.Fill(ResultsDataSet);
                    return ResultsDataSet;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                if (adapter != null)
                {
                    adapter.Dispose();
                    adapter = null;
                }
            }
        }

        /// <summary>
        /// Ejecuta el script SQL que recibe como parametro. 
        /// Retorna un único valor de tipo string
        /// La lista "IList<SqlParameter>" es usada como parametros de entra de la consulta.
        /// </summary>
        /// <param name="sentenceSql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="DAL.Helper.DALException"/>
        public string ExecuteScalar(string sentenceSql, IList<SqlParameter> parameters)
        {
            SqlCommand command = null;
            try
            {
                string result = string.Empty;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    command = PrepareCommand(sentenceSql, parameters, connection);

                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    Object res = command.ExecuteScalar();
                    if (res != null)
                    {
                        result = command.ExecuteScalar().ToString();
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                    command = null;
                }
            }
        }

        /// <summary>
        /// Ejecuta el procedimiento almacenado cuyo nombre recibe como parametro. 
        /// La lista "IList<SqlParameter>" es usada como parametros de entra para el procedimiento.
        /// Retorna un DataSet con los valores de la consulta
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="DAL.Helper.DALException"/>
        public DataSet ExecuteProcedureToDataSet(string procedureName, IList<SqlParameter> parameters)
        {
            ImprimirParametros(procedureName, parameters);

            SqlCommand command = null;
            SqlDataAdapter dbAdapter = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    DataSet ResultsDataSet = new DataSet();
                    ResultsDataSet.Locale = CultureInfo.InvariantCulture;
                    command = PrepareCommandProcedure(procedureName, parameters, connection);
                    dbAdapter = new SqlDataAdapter(command);
                    dbAdapter.Fill(ResultsDataSet);
                    return ResultsDataSet;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Debug, "----------------------------------------- FIN");
                if (command != null)
                {
                    command.Dispose();
                    command = null;
                }

                if (dbAdapter != null)
                {
                    dbAdapter.Dispose();
                    dbAdapter = null;
                }
            }
        }

        /// <summary>
        /// Ejecuta la sentencia SQL que recibe como parametro. 
        /// La lista "IList<SqlParameter>" es usada como parametros de entra para el procedimiento.
        /// Retorna un DataSet con los valores de la consulta
        /// </summary>
        /// <param name="sentenceSql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="DAL.Helper.DALException"/>
        public DataSet ExecuteSqlToDataSet(string sentenceSql, IList<SqlParameter> parameters)
        {
            ImprimirParametros(sentenceSql, parameters);

            SqlCommand command = null;
            SqlDataAdapter dbAdapter = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    DataSet ResultsDataSet = new DataSet();
                    ResultsDataSet.Locale = CultureInfo.InvariantCulture;
                    command = PrepareCommand(sentenceSql, parameters, connection);
                    dbAdapter = new SqlDataAdapter(command);
                    dbAdapter.Fill(ResultsDataSet);
                    return ResultsDataSet;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Debug, "----------------------------------------- FIN");
                if (command != null)
                {
                    command.Dispose();
                    command = null;
                }

                if (dbAdapter != null)
                {
                    dbAdapter.Dispose();
                    dbAdapter = null;
                }
            }
        }

        /// <summary>
        /// Ejecuta el procedimiento almacenado cuyo nombre recibe como parametro. 
        /// La lista "IList<SqlParameter>" es usada como parametros de entra para el procedimiento.
        /// Retorna un único valor de tipo string
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="DAL.Helper.DALException"/>
        [Obsolete]
        public string ExecuteProcedureScalar(string procedureName, IList<SqlParameter> parameters)
        {
            ImprimirParametros(procedureName, parameters);

            SqlCommand command = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string result = string.Empty;

                    DataSet ResultsDataSet = new DataSet();
                    ResultsDataSet.Locale = CultureInfo.InvariantCulture;

                    command = PrepareCommandProcedure(procedureName, parameters, connection);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    result = command.ExecuteScalar().ToString();

                    return result;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Debug, "----------------------------------------- FIN");
                if (command != null)
                {
                    command.Dispose();
                    command = null;
                }
            }
        }


        private SqlCommand PrepareCommand(string sentenceSql, IList<SqlParameter> parameters, SqlConnection connection)
        {

            using (SqlCommand command = new SqlCommand(sentenceSql, connection))
            {
                command.CommandTimeout = 600;

                if (parameters.Count > 0)
                {
                    foreach (SqlParameter par in parameters)
                    {
                        command.Parameters.Add(par);
                    }
                }

                return command;
            }
        }

        private SqlCommand PrepareCommandProcedure(string procedureName, IList<SqlParameter> parameters, SqlConnection connection)
        {

            using (SqlCommand command = new SqlCommand())
            {
                command.CommandText = procedureName;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;
                command.CommandTimeout = 600;

                for (int i = 0; i < parameters.Count; i++)
                {
                    command.Parameters.Add(parameters[i]);
                }

                return command;
            }
        }



        private void ImprimirParametros(string procedureName, IList<SqlParameter> parameters)
        {
            string cadena = "[" + procedureName + "] ";
            if (parameters != null)
            {
                for (int i = 0; i < parameters.Count; i++)
                    cadena = cadena + "[" + parameters[i].ParameterName + "|" + parameters[i].Value + "]";

                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Debug, cadena);
            }
        }

        public string ExecuteProcedureToString(string procedureName, IList<SqlParameter> parameters)
        {
            throw new NotImplementedException();
        }
    }
}