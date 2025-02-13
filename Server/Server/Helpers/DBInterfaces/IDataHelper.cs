using Microsoft.Data.SqlClient;
using System.Data;

namespace Server.Helpers.DBInterfaces
{
    /// <summary>
    /// The interface responsible for Structure declaration for DataHelper
    /// </summary>
    public interface IDataHelper
    {
        Task<object?> ExecSPAsScalar(string connectionString, string storedProcedureName, SqlParameter[] parameters);

        Task<bool> ExecSPWithoutRes(string connectionString, string storedProcedureName, SqlParameter[] parameters);
        Task<DataTable?> ExecSPWithRes(string connectionString, string storedProcedureName, SqlParameter[] parameters);
    }
}
