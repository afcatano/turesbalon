using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Impl.SQLServer.Helper
{
    /// <summary>
    /// Interfaz que define las operaciones de la base de datos
    /// </summary>
    public interface IDataBaseSQLHelper
    {
        DataSet ExecuteQuery(string sentenceSql);

        string ExecuteScalar(string sentenceSql, IList<SqlParameter> parameters);

        DataSet ExecuteProcedureToDataSet(string procedureName, IList<SqlParameter> parameters);

        DataSet ExecuteSqlToDataSet(string sentenceSql, IList<SqlParameter> parameters);

        string ExecuteProcedureScalar(string procedureName, IList<SqlParameter> parameters);

    }
}