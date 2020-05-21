using System.Data;
using System.Data.SqlClient;

namespace CommonLib.Sql
{
    public interface ISqlFactory
    {
        IDbConnection CreateConnection();
    }

    internal class SqlFactory : ISqlFactory
    {
        private readonly string _connString;

        public SqlFactory(string connString)
        {
            if (string.IsNullOrWhiteSpace(connString))
            {
                throw new System.ArgumentException("Connect String Can't Be Empty!");
            }
            _connString = connString;
        }

        public IDbConnection CreateConnection()
        {
            IDbConnection connection = new SqlConnection(_connString);
            return connection;
        }
    }
}
