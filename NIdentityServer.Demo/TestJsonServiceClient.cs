using ServiceStack;
using System;

namespace NIdentityServer.Demo
{
    public class TestJsonServiceClient : JsonServiceClient
    {
        public void AcquireAccessToken()
        {
            var clientId = "test.client";
        }
    }
}
