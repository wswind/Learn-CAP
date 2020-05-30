using Api1.EfDbContext;
using Castle.DynamicProxy;
using DotNetCore.CAP;
using System;

namespace Api1.AOP
{
    public class AspectEventAutoCommit : IInterceptor
    {
        private readonly ICapPublishContext _capPublishContext;

        public AspectEventAutoCommit(ICapPublishContext capPublishContext)
        {
            _capPublishContext = capPublishContext;
           
        }

        public void Intercept(IInvocation invocation)
        {
            using (var trans = _capPublishContext.BeginTransaction())
            {
                invocation.Proceed();
                _capPublishContext.Publish();
            }
        }
    }
}
