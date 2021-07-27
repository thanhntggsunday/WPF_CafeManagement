using System.Data.SqlClient;

namespace WPF_CafeManagement.Service
{
    public class ServiceBase
    {
        public SqlTransaction BeginTransaction(SqlConnection sqlConnection)
        {
            return sqlConnection.BeginTransaction();
        }

        public void Commit(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            sqlTransaction.Commit();
            sqlConnection.Close();
        }

        public void RollBack(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            sqlTransaction.Rollback();
            sqlConnection.Close();
        }
    }
}