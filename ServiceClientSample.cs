namespace ServiceClientExample
{
    [AuthenticationStrategy(nameof(ServiceClientSample))]
    public class ServiceClientSample : IServiceClientSample
    {
        public ServiceClientSample(DummyHttpClient httpClient)
        {
            HttpClient = httpClient;
        }
        public DummyHttpClient HttpClient {get;private set;}

        [AuthenticationStrategy(nameof(CustomStrategy))]
        public void CustomStrategy()
        {
        }

        [AuthenticationStrategy(AuthenticationStrategies.NoAuthentication)]
        public void NoStrategy()
        {
        }

        [AuthenticationStrategy(AuthenticationStrategies.SystemAuthentication)]
        public void SystemStrategy()
        {
        }

        [AuthenticationStrategy(AuthenticationStrategies.UserAuthentication)]
        public void UserStrategy()
        {
        }

        public void WithoutStrategy()
        {
        }
    }
}