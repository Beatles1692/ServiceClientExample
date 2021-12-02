using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;

namespace ServiceClientExample
{
    public class Interceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
           ApplyAuthenticationStrategy(invocation);
           invocation.Proceed();
        }

        private void ApplyAuthenticationStrategy(IInvocation invocation)
        {

           var serviceClient = invocation.InvocationTarget as IServiceClient;
           if(serviceClient==null) return;
           var strategyAttribute = invocation.Method.GetCustomAttribute<AuthenticationStrategyAttribute>() ?? invocation.TargetType.GetCustomAttribute<AuthenticationStrategyAttribute>();
           if(strategyAttribute == null) return ;
           AuthenticationStrategies.GetStrategy(strategyAttribute.Strategy)(serviceClient);
        }
         

        
    }
}