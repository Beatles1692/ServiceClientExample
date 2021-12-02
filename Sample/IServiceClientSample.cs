namespace ServiceClientExample
{
    public interface IServiceClientSample : IServiceClient
    {
        void SystemStrategy();

        void UserStrategy();

        void NoStrategy();

        void CustomStrategy();

        void WithoutStrategy();
    }
}