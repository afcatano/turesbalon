
using DAL.Impl.SQLServer.Helper;

namespace DAL.Impl
{
    /// <summary>
    /// Clase abstracta que define las operaciones de la fábrica SQLDALAdapter.
    /// </summary>
    public abstract class DALSQLImplementation
    {
        protected abstract IDataBaseSQLHelper GetDataBaseHelper();
    }
}