using Api1.EfDbContext;
using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace Api1.AOP
{
    public interface ICapPublishContext
    {       
        public IDbContextTransaction BeginTransaction();
        public void Publish(string eventName);
        public void SetContentObj(object obj);
    }

    public class CapPublishContext : ICapPublishContext
    {
        public CapPublishContext(ICapPublisher capPublisher, DbContext dbContext)
        {
            _capPublisher = capPublisher;
            _dbContext = dbContext;
            _contentObj = null;
        }

     
        private readonly ICapPublisher _capPublisher;
        private readonly DbContext _dbContext;
        private object _contentObj;
        public IDbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction(_capPublisher, autoCommit: true);
        }

        public void Publish(string eventName)
        {
            if(_contentObj != null)
                _capPublisher.Publish(eventName, _contentObj);
        }

        public void SetContentObj(object obj)
        {
            _contentObj = obj;
        }
    }
}
