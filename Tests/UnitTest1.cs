using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ServiceClientExample
{
    public class AuthenticationStrategiesTests
    {
        [Fact]
        public void Test1()
        {
            AuthenticationStrategies.AddCustomStratrgey(nameof(IServiceClientSample.CustomStrategy), client => client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", nameof(IServiceClientSample.CustomStrategy)));
            AuthenticationStrategies.AddCustomStratrgey(nameof(ServiceClientSample),  client => client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",nameof(ServiceClientSample)));
            
            IServiceCollection services = new ServiceCollection();


            HttpClient httpClient = new HttpClient();
            services.AddSingleton<HttpClient>(httpClient);
            services.AddServiceClient<IServiceClientSample, ServiceClientSample>();

            var app = services.BuildServiceProvider();

            var serviceClientSample = app.CreateScope().ServiceProvider.GetService<IServiceClientSample>();

            serviceClientSample.UserStrategy();
            Assert.Equal("UserToken", httpClient.DefaultRequestHeaders.Authorization.Parameter);
            ResetDefaultAuthorizationHeader(httpClient);
            serviceClientSample.CustomStrategy();
            Assert.Equal(nameof(IServiceClientSample.CustomStrategy), httpClient.DefaultRequestHeaders.Authorization.Parameter);
            ResetDefaultAuthorizationHeader(httpClient);
            serviceClientSample.SystemStrategy();
            Assert.Equal("SystemToken", httpClient.DefaultRequestHeaders.Authorization.Parameter);
            ResetDefaultAuthorizationHeader(httpClient);
            serviceClientSample.NoStrategy();
            Assert.Null(httpClient.DefaultRequestHeaders.Authorization);
            ResetDefaultAuthorizationHeader(httpClient);
            serviceClientSample.WithoutStrategy();
            Assert.Equal(nameof(ServiceClientSample), httpClient.DefaultRequestHeaders.Authorization.Parameter);
            ResetDefaultAuthorizationHeader(httpClient);
        }

        private void ResetDefaultAuthorizationHeader(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
