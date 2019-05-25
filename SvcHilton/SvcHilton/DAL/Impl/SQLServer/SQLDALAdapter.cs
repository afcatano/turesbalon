using DAL.Impl.SQLServer.Helper;

namespace DAL.Impl.SQLServer
{
    /// <summary>
    ///  Fábrica abstracta que implementa las operaciones de acceso a datos para DALSQLImplementation
    /// </summary>
    public abstract class SQLDALAdapter : DALSQLImplementation
    {
        protected override sealed IDataBaseSQLHelper GetDataBaseHelper()
        {
            return SqlServerHelper.GetInstace();
        }
    }
}