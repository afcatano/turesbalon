using CommonsWeb.Properties;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;

namespace CommonsWeb.DAL
{
    public class OracleServerHelper
    {
        private static readonly string connectionString;

        static OracleServerHelper()
        {
            connectionString = GetConectionString(Settings.Default.DataSourceOracle, Settings.Default.DataBaseNameOracle, Settings.Default.UserIDOracle, Settings.Default.PasswordOracle);
        }

        private static string GetConectionString(string dataSource, string dataBaseName, string userId, string password)
        {
            OracleConnectionStringBuilder builder = new OracleConnectionStringBuilder();
            builder.DataSource = dataSource.Trim() + '/' + dataBaseName.Trim();
            builder.UserID = userId.Trim();
            builder.Password = password.Trim();
            return builder.ConnectionString;
        }

        public DataSet ExecuteProcedureToDataSet(string procedureName, IList<OracleParameter> parameters)
        {
            ImprimirParametros(procedureName, parameters);

            OracleCommand command = null;
            OracleDataAdapter dbAdapter = null;
            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    DataSet ResultsDataSet = new DataSet();
                    ResultsDataSet.Locale = CultureInfo.InvariantCulture;
                    command = PrepareCommandProcedure(procedureName, parameters, connection);
                    dbAdapter = new OracleDataAdapter(command);
                    dbAdapter.Fill(ResultsDataSet);
                    return ResultsDataSet;
                }
            }
            catch (Exception ex)
            {
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Debug, "ERROR EN EJECUCION PROCEDIMIENTO :: " + procedureName + "  " + ex.Message);
                return null;
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

        private void ImprimirParametros(string procedureName, IList<OracleParameter> parameters)
        {
            string cadena = "[" + procedureName + "] ";
            if (parameters != null)
            {
                for (int i = 0; i < parameters.Count; i++)
                    cadena = cadena + "[" + parameters[i].ParameterName + "|" + parameters[i].Value + "]";

                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Debug, cadena);
            }
        }

        private OracleCommand PrepareCommandProcedure(string procedureName, IList<OracleParameter> parameters, OracleConnection connection)
        {

            using (OracleCommand command = new OracleCommand())
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

        public DataSet ExecuteSqlToDataSet(string sentenceSql, List<OracleParameter> parameters)
        {
            ImprimirParametros(sentenceSql, parameters);

            OracleCommand command = null;
            OracleDataAdapter dbAdapter = null;
            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {

                    DataSet ResultsDataSet = new DataSet();
                    ResultsDataSet.Locale = CultureInfo.InvariantCulture;
                    //command = PrepareCommand(sentenceSql, parameters, connection);
                    command = new OracleCommand(sentenceSql, connection);
                    command.BindByName = true;
                    command.CommandTimeout = 600;
                    command.Parameters.Clear();
                    foreach (OracleParameter par in parameters)
                    {
                        command.Parameters.Add(par.Clone());
                    }
                    dbAdapter = new OracleDataAdapter(command);
                    dbAdapter.Fill(ResultsDataSet);
                    return ResultsDataSet;
                }
            }
            catch (Exception ex)
            {
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Debug, "ERROR EN EJECUCION SENTENCIA SQL :: " + sentenceSql + "  " + ex.Message);
                return null;
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

        public DataSet ExecuteSqlToDataSet2(string sentenceSql, List<OracleParameter> parameters)
        {
            ImprimirParametros(sentenceSql, parameters);

            OracleCommand command = null;
            OracleDataAdapter dbAdapter = null;
            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {

                    DataSet ResultsDataSet = new DataSet();
                    ResultsDataSet.Locale = CultureInfo.InvariantCulture;
                    //command = PrepareCommand(sentenceSql, parameters, connection);
                    command = new OracleCommand(sentenceSql, connection);
                    command.BindByName = true;
                    command.CommandTimeout = 600;
                    command.Parameters.Clear();
                    foreach (OracleParameter par2 in parameters)
                    {
                        command.Parameters.Add(par2.Clone());
                    }
                    dbAdapter = new OracleDataAdapter(command);
                    dbAdapter.Fill(ResultsDataSet);
                    command.Parameters.Clear();
                    return ResultsDataSet;
                }
            }
            catch (Exception ex)
            {
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Debug, "ERROR EN EJECUCION SENTENCIA SQL :: " + sentenceSql + "  " + ex.Message);
                return null;
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
        public int ExecuteSql(string sentenceSql, List<OracleParameter> parameters)
        {
            ImprimirParametros(sentenceSql, parameters);

            OracleCommand command = null;
            OracleDataAdapter dbAdapter = null;
            int rsta;

            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    command = new OracleCommand(sentenceSql, connection);
                    command.BindByName = true;
                    command.CommandTimeout = 600;
                    foreach (OracleParameter par in parameters)
                    {
                        command.Parameters.Add(par);
                    }

                    rsta = command.ExecuteNonQuery();
                    return rsta;
                }
            }
            catch (Exception ex)
            {
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Debug, "ERROR EN EJECUCION SENTENCIA SQL :: " + sentenceSql + "  " + ex.Message);
                return 0;
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

        public int ExecuteSqlEscalar(string sentenceSql, List<OracleParameter> parameters)
        {
            ImprimirParametros(sentenceSql, parameters);

            OracleCommand command = null;
            OracleDataAdapter dbAdapter = null;
            int rsta;

            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    command = new OracleCommand(sentenceSql, connection);
                    command.BindByName = true;
                    command.CommandTimeout = 600;
                    foreach (OracleParameter par in parameters)
                    {
                        command.Parameters.Add(par);
                    }

                    rsta = (int)command.ExecuteScalar();
                    return rsta;
                }
            }
            catch (Exception ex)
            {
                Common.CreateTrace.WriteLog(Common.CreateTrace.LogLevel.Debug, "ERROR EN EJECUCION SENTENCIA SQL :: " + sentenceSql + "  " + ex.Message);
                return 0;
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

        private OracleCommand PrepareCommand(string sentenceSql, List<OracleParameter> parameters, OracleConnection connection)
        {

            using (OracleCommand command = new OracleCommand(sentenceSql, connection))
            {
                command.CommandTimeout = 600;
                command.BindByName = true;
                if (parameters.Count > 0)
                {
                    foreach (OracleParameter par in parameters)
                    {
                        command.Parameters.Add(par);
                    }
                }

                return command;
            }
        }


    }


}