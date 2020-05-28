using CommonLib.Sql;
using Dapper.Contrib.Extensions;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Api1.EfDbContext;
using System;

namespace Api1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublishController : ControllerBase
    {
        private readonly ICapPublisher _capBus;
        private readonly ISqlFactory _sqlFactory;

        public PublishController(ICapPublisher capPublisher , ISqlFactory sqlFactory)
        {
            _capBus = capPublisher;
            _sqlFactory = sqlFactory;
        }

        [Route("~/adonet/transaction")]
        public IActionResult AdonetWithTransaction()
        {
            using (var connection = _sqlFactory.CreateConnection())
            {
                using (var transaction = connection.BeginTransaction(_capBus, autoCommit: true))
                {
                    //your business logic code
                    Api1.Poco.TestTable testTable = new Api1.Poco.TestTable()
                    {
                        A = "f",
                        B = "g"
                    };
                    connection.Insert<Api1.Poco.TestTable>(testTable, transaction);
                    _capBus.Publish("testtable.insert.dapper", testTable);
                }
            }
            return Ok("dapper ok");
        }
        [Route("~/ef/transaction")]
        public IActionResult EntityFrameworkWithTransaction([FromServices] AppDbContext dbContext)
        {
            using (var trans = dbContext.Database.BeginTransaction(_capBus, autoCommit: true))
            {
                Api1.Models.TestTable testTable = new Api1.Models.TestTable()
                {
                    A = "f",
                    B = "g"
                };
                dbContext.TestTable.Add(testTable);
                dbContext.SaveChanges();
                _capBus.Publish("testtable.insert.efcore", testTable);
            }

            return Ok("ef ok");
        }
    }
}
