using System;

namespace ServiceClientExample
{
    [System.AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited=true)]
    public class AuthenticationStrategyAttribute : Attribute
    {
        public string Strategy {get; private set;}

        public AuthenticationStrategyAttribute(string strategy)
        {
            Strategy = strategy;
        }
    }
}