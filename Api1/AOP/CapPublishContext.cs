using Api1.EfDbContext;
using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace Api1.AOP
{
    public interface ICapPublishContext
    {
        public void ConfigurePublishAction(Action<ICapPublisher> action);
        public IDbContextTransaction BeginTransaction();
        public void Publish();
    }

    public class CapPublishContext : ICapPublishContext
    {
        public CapPublishContext(ICapPublisher capPublisher, AppDbContext dbContext)
        {
            _action = x => { };
            _capPublisher = capPublisher;
            _dbContext = dbContext;
        }

        private Action<ICapPublisher> _action;
        private readonly ICapPublisher _capPublisher;
        private readonly AppDbContext _dbContext;

        public void ConfigurePublishAction(Action<ICapPublisher> action)
        {
            _action = action;
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction(_capPublisher, autoCommit: true);
        }

        public void Publish()
        {
            _action.Invoke(_capPublisher); 
        }
    }
}
