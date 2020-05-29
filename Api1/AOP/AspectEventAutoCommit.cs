using Api1.EfDbContext;
using Castle.DynamicProxy;
using DotNetCore.CAP;
using System;

namespace Api1.AOP
{
    public class AspectEventAutoCommit : IInterceptor
    {
        private AppDbContext _dbContext;
        private ICapPublisher _capPublisher;

        public AspectEventAutoCommit(AppDbContext dbContext, ICapPublisher capPublisher)
        {
            _dbContext = dbContext;
            _capPublisher = capPublisher;
        }

        public void Intercept(IInvocation invocation)
        {
            using (var trans = _dbContext.Database.BeginTransaction(_capPublisher, autoCommit: true))
            {

            }
                
        }
    }
}
