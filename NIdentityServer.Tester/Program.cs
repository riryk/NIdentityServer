using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;

namespace Fiver.Security.AuthServer.Client.RO
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var token1 = RequestTokenByLoginAndPassword();
                var token2 = RequestTokenByClientIdAndSecret();

                RequestMovies(token2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        private static void RequestMovies(string token)
        {
            var client = new HttpClient();
            client.SetBearerToken(token);

            var response = client.GetAsync("https://localhost:44318/api/movies").Result;
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(JArray.Parse(content));
            }
        }

        private static string RequestTokenByLoginAndPassword()
        {
            var disco = DiscoveryClient.GetAsync("https://localhost:44308/").Result;
            var tokenClient = new TokenClient(disco.TokenEndpoint, "fiver_auth_client_ro", "secret");
            var tokenResponse = tokenClient.RequestResourceOwnerPasswordAsync("james", "password", "fiver_auth_api").Result;
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return null;
            }

            return tokenResponse.AccessToken;
        }

        private static string RequestTokenByClientIdAndSecret()
        {
            var disco = DiscoveryClient.GetAsync("https://localhost:44308/").Result;
            var tokenClient = new TokenClient(disco.TokenEndpoint, "fiver_auth_client", "secret");
            var tokenResponse = tokenClient.RequestClientCredentialsAsync("fiver_auth_api").Result;
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return null;
            }

            return tokenResponse.AccessToken;
        }
    }
}