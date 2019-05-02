using IdentityServer4.Models;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NIdentityServer.Services
{
    public class ClientStore : IClientStore
    {
        private readonly int accessTokenLifeTime;
        private readonly List<Client> clients;

        public ClientStore(int accessTokenLifeTime)
        {
            this.accessTokenLifeTime = accessTokenLifeTime;

            clients = new List<Client>();
            InitializeClients();
        }

        public Task<Client> FindClientByIdAsync(string clientId)
        {
            return Task.FromResult(this.clients.SingleOrDefault(c => c.ClientId.Equals(clientId)));
        }

        private void InitializeClients()
        {
            foreach (var client in this.GetClientsInternal())
            {
                try
                {
                    client.ClientSecrets = this.GetSecretsFor(client);
                    this.clients.Add(client);
                }
                catch (Exception ex)
                {
                    ///TODO: Need to log this exception
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private IEnumerable<Client> GetClientsInternal()
        {
            yield return new Client
            {
                ClientId = "test.client",
                ClientName = "test.api.client",
                Enabled = true,
                AllowedScopes = new List<string>
                {
                    KnownResourceScopes.TestScope1,
                    KnownResourceScopes.TestScope2
                },
                RequireConsent = false,
                AccessTokenLifetime = accessTokenLifeTime,
                AccessTokenType = AccessTokenType.Jwt
            };

            yield return new Client
            {
                ClientId = "another.test.client",
                ClientName = "another.test.api.client",
                Enabled = true,
                AllowedScopes = new List<string>
                {
                    KnownResourceScopes.TestScope1,
                    KnownResourceScopes.TestScope2
                },
                RequireConsent = false,
                AccessTokenLifetime = accessTokenLifeTime,
                AccessTokenType = AccessTokenType.Jwt
            };
        }

        private List<Secret> GetSecretsFor(Client client)
        {
            var secretKey = client.ClientId + ".secret";

            var secret = ClientSecrets.GetClientSecretFor(secretKey);

            return new List<Secret> { new Secret(secret.Sha256()) };
        }
    }
}
