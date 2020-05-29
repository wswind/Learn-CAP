using Api1.EfDbContext;
using DotNetCore.CAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api1.Services
{
    public interface IAppService
    {
        void InsertTesttable();
    }

    public class AppService : IAppService
    {
        private AppDbContext _dbContext;
        private ICapPublisher _capPublisher;

        public AppService(AppDbContext dbContext, ICapPublisher capPublisher)
        {
            _dbContext = dbContext;
            _capPublisher = capPublisher;
        }

        public void InsertTesttable()
        {
            using (var trans = _dbContext.Database.BeginTransaction(_capPublisher, autoCommit: true))
            {
                Api1.Models.TestTable testTable = new Api1.Models.TestTable()
                {
                    A = "f",
                    B = "g"
                };
                _dbContext.TestTable.Add(testTable);
                _dbContext.SaveChanges();
                _capPublisher.Publish("testtable.insert.efcore", testTable);
            }
        }
    }
}
