using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ServiceClientExample
{
    public class AuthenticationStrategiesTests
    {
        [Fact]
        public void Test1()
        {
            AuthenticationStrategies.AddCustomStratrgey(nameof(IServiceClientSample.CustomStrategy), client => client.HttpClient.AuthenticationStrategy = nameof(IServiceClientSample.CustomStrategy));
            AuthenticationStrategies.AddCustomStratrgey(nameof(ServiceClientSample), client => client.HttpClient.AuthenticationStrategy = nameof(ServiceClientSample));
            
            IServiceCollection services = new ServiceCollection();


            DummyHttpClient httpClient = new DummyHttpClient();
            services.AddSingleton<DummyHttpClient>(httpClient);
            services.AddServiceClient<IServiceClientSample, ServiceClientSample>();

            var app = services.BuildServiceProvider();

            var serviceClientSample = app.CreateScope().ServiceProvider.GetService<IServiceClientSample>();

            serviceClientSample.UserStrategy();
            Assert.Equal(AuthenticationStrategies.UserAuthentication, httpClient.AuthenticationStrategy);
            
            serviceClientSample.CustomStrategy();
            Assert.Equal(nameof(IServiceClientSample.CustomStrategy), httpClient.AuthenticationStrategy);

            serviceClientSample.SystemStrategy();
            Assert.Equal(AuthenticationStrategies.SystemAuthentication, httpClient.AuthenticationStrategy);

            serviceClientSample.NoStrategy();
            Assert.Equal(AuthenticationStrategies.NoAuthentication, httpClient.AuthenticationStrategy);

            serviceClientSample.WithoutStrategy();
            Assert.Equal(nameof(ServiceClientSample), httpClient.AuthenticationStrategy);

        }
    }
}
