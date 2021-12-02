using System;

namespace ServiceClientExample
{
    public interface IServiceClient
    {
        DummyHttpClient HttpClient {get; set;}
    }
}