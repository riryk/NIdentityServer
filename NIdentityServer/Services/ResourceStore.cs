using IdentityServer4.Models;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NIdentityServer.Services
{
    public class ResourceStore : IResourceStore
    {
        private readonly IEnumerable<ApiResource> apiResources;
        private readonly IEnumerable<IdentityResource> identityResource;

        public ResourceStore()
        {
            apiResources = GetApiResources();
            identityResource = GetIdentityResources();
        }

        public Task<ApiResource> FindApiResourceAsync(string name)
        {
            var apiResource = apiResources.FirstOrDefault(ar => ar.Name == name);
            return Task.FromResult(apiResource);
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            if (scopeNames == null)
            {
                throw new ArgumentNullException(nameof(scopeNames));
            }

            var apiResourcesEntities = from i in apiResources
                                       where scopeNames.Contains(i.Name)
                                       select i;

            return Task.FromResult(apiResourcesEntities);
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            if (scopeNames == null)
            {
                throw new ArgumentNullException(nameof(scopeNames));
            }

            var identityResourcesEntities = from i in identityResource
                                            where scopeNames.Contains(i.Name)
                                            select i;

            return Task.FromResult(identityResourcesEntities);
        }

        public Task<Resources> GetAllResourcesAsync()
        {
            return Task.FromResult(new Resources());
        }

        private IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        private IEnumerable<ApiResource> GetApiResources()
        {
            yield return new ApiResource(name: KnownResourceScopes.TestScope1, displayName: "Test Scope 1")
            {
                ApiSecrets = { new Secret("secret".Sha256()) }
            };

            yield return new ApiResource(name: KnownResourceScopes.TestScope2, displayName: "Test Scope 2")
            {
                ApiSecrets = { new Secret("secret".Sha256()) }
            };
        }
    }
}
