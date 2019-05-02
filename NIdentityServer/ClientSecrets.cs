using System;
using System.Collections.Generic;

namespace NIdentityServer
{
    public static class ClientSecrets
    {
        private static readonly IDictionary<string, string> clientSecrets = new Dictionary<string, string>
        {
            { "test.client.secret", "F9F10A06-7709-4359-85F5-C71059669374" },
            { "another.test.client.secret", "4E6D6453-9785-4693-AEE3-5F9C8165E51D" }
        };

        public static string GetClientSecretFor(string clientId)
        {
            if (!clientSecrets.TryGetValue(clientId, out var secret))
            {
                throw new Exception($"Client {clientId} was not found");
            }

            return secret;
        }
    }
}
