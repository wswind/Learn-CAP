using Api1.EfDbContext;
using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace Api1.AOP
{
    public interface ICapPublishContext
    {       
        public IDbContextTransaction BeginTransaction();
        public void Publish(string eventName);
        public object ContentObj { get; set; }
    }

    public class CapPublishContext : ICapPublishContext
    {
        public CapPublishContext(ICapPublisher capPublisher, AppDbContext dbContext)
        {
            _capPublisher = capPublisher;
            _dbContext = dbContext;
            ContentObj = null;
        }

     
        private readonly ICapPublisher _capPublisher;
        private readonly AppDbContext _dbContext;
        public object ContentObj { get; set; }
        public IDbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction(_capPublisher, autoCommit: true);
        }

        public void Publish(string eventName)
        {
            if(ContentObj != null)
                _capPublisher.Publish(eventName, ContentObj);
        }
    }
}
