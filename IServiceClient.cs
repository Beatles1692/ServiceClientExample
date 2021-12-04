using System.Net.Http;

namespace ServiceClientExample
{
    public interface IServiceClient
    {
        HttpClient HttpClient {get;}
    }
}