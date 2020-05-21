using Api1.Poco;
using CommonLib.Sql;
using Dapper.Contrib.Extensions;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;

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
                    TestTable testTable = new TestTable()
                    {
                        A = "f",
                        B = "g"
                    };
                    connection.Insert<TestTable>(testTable, transaction);
                    _capBus.Publish("testtable.insert", testTable);
                }
            }
            return Ok();
        }
    }
}
