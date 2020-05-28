using DotNetCore.CAP;
using Microsoft.Extensions.Logging;

namespace Api1.Services
{
    public interface ISubscriberService
    {
        public void CheckReceivedMessageDapper(Api1.Poco.TestTable testTable);
        public void CheckReceivedMessageEfCore(Api1.Models.TestTable testTable);
    }
    public class SubscriberService : ISubscriberService, ICapSubscribe
    {
        private ILogger<SubscriberService> _logger;

        public SubscriberService(ILogger<SubscriberService> logger)
        {
            _logger = logger;
        }

        [CapSubscribe("testtable.insert.dapper")]
        public void CheckReceivedMessageDapper(Api1.Poco.TestTable testTable)
        {
            _logger.LogDebug("get message with testtable");
        }

        [CapSubscribe("testtable.insert.efcore")]
        public void CheckReceivedMessageEfCore(Api1.Models.TestTable testTable)
        {
            _logger.LogDebug("get message with testtable");
        }
    }
}
