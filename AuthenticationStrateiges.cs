using System;
using System.Collections.Generic;

namespace ServiceClientExample
{
    public static class AuthenticationStrategies
    {
        public const string SystemAuthentication = nameof(SystemAuthentication);

        public const string UserAuthentication = nameof(UserAuthentication);

        public const string NoAuthentication = nameof(NoAuthentication);

        private static Dictionary<string, Action<IServiceClient>> _strategies = new Dictionary<string, Action<IServiceClient>>{
            {
                SystemAuthentication,
                new Action<IServiceClient>(client => client.HttpClient.AuthenticationStrategy = SystemAuthentication)
            },
            {
                UserAuthentication,
                new Action<IServiceClient>(client => client.HttpClient.AuthenticationStrategy = UserAuthentication)
            },
            {
                NoAuthentication,
                new Action<IServiceClient>(client => client.HttpClient.AuthenticationStrategy = NoAuthentication)
            }
        };

        public static void AddCustomStratrgey(string key, Action<IServiceClient> strategy)
        {
            _strategies.Add(key, strategy);
        }

        public static Action<IServiceClient> GetStrategy(string key)
        {
            return _strategies[key];
        }
    }
}