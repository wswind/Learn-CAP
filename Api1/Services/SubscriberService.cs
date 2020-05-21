using Api1.Poco;
using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Reflection.Emit;

namespace Api1.Services
{
    public interface ISubscriberService
    {
        public void CheckReceivedMessage(TestTable testTable);
    }
    public class SubscriberService : ISubscriberService, ICapSubscribe
    {
        private ILogger<SubscriberService> _logger;

        public SubscriberService(ILogger<SubscriberService> logger)
        {
            _logger = logger;
        }

        [CapSubscribe("testtable.insert")]
        public void CheckReceivedMessage(TestTable testTable)
        {
            _logger.LogDebug("get message with testtable");
        }
    }
}
