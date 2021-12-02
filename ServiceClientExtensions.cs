using Castle.DynamicProxy;
using  Microsoft.Extensions.DependencyInjection;

namespace ServiceClientExample
{
    public static class ServiceClientExtensions
    {
        public static void AddServiceClient<TInterface, TObject>(this IServiceCollection services) 
        where TInterface : class, IServiceClient 
        where TObject : class, TInterface
        {
            services.AddTransient<TObject>();
            services.AddScoped<TInterface>(app => {
                var clientService = app.GetService<TObject>();
                var proxy = new ProxyGenerator().CreateInterfaceProxyWithTarget<TInterface>(
                    clientService,
                    ProxyGenerationOptions.Default,
                    new Interceptor());
                return proxy;
            });
        }
    }
}