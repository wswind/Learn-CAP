using Castle.DynamicProxy;

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
                var attr = CapPublishAttribute.GetAttributeByMethodInfo(invocation.MethodInvocationTarget);
                if(attr != null)
                {
                    _capPublishContext.Publish(attr.EventName);
                }
            }
        }
    }
}
