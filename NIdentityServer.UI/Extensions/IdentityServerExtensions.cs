using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NIdentityServer.Repositories;
using NIdentityServer.Services;

namespace NIdentityServer.UI.Extensions
{
    public static class IdentityServerExtensions
    {
        public static IServiceCollection ConfigureIdentityServer(this IServiceCollection services, IConfiguration configuration)
        {
            var publicOrigin = configuration.GetSection("PublicOrigin");
            var certificate = configuration.GetSection("Certificate").Get<CertificateConfiguration>();
            
            services.AddIdentityServer(options =>
            {
                options.PublicOrigin = publicOrigin?.Value;
                options.InputLengthRestrictions.Scope = 3000;
            })
            .AddInMemoryCaching()
            .GetSigningCredential(certificate.Name)
            .AddClientStoreCache<ClientStore>()
            .AddResourceStoreCache<ResourceStore>();
          
            return services;
        }

        private static IIdentityServerBuilder GetSigningCredential(this IIdentityServerBuilder builder, string certificate)
        {
            builder.AddSigningCredential(CertificateRepository.GetTokenSigningCertificate(certificate));
            return builder;
        }
    }
}
