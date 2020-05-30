using Api1.AOP;
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
        private ICapPublishContext _capPublishContext;

        public AppService(AppDbContext dbContext, ICapPublishContext capPublishContext)
        {
            _dbContext = dbContext;
            _capPublishContext = capPublishContext;
        }

        public void InsertTesttable()
        {
            Api1.Models.TestTable testTable = new Api1.Models.TestTable()
            {
                A = "f",
                B = "g"
            };
            _dbContext.TestTable.Add(testTable);
            _dbContext.SaveChanges();
            _capPublishContext.ConfigurePublishAction(p => p.Publish("testtable.insert.efcore", testTable));
        }
    }
}
