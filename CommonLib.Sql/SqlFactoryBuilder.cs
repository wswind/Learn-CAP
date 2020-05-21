using Microsoft.Extensions.Configuration;
using System;

namespace CommonLib.Sql
{
    public interface ISqlFactoryBuilder
    {
        ISqlFactory GetSqlFactory(string connName);
    }

    internal class SqlFactoryBuilder : ISqlFactoryBuilder
    {
        private readonly IConfiguration _configuration;

        public SqlFactoryBuilder(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ISqlFactory GetSqlFactory(string connName)
        {
            if(string.IsNullOrWhiteSpace(connName))
            {
                throw new ArgumentException("Connect String Config Name Can't Be Empty!");
            }
            string conn = _configuration.GetConnectionString(connName);
            return new SqlFactory(conn);
        }

    }
}
