using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace LetsCode
{
    public class AzureClient : IAzureClient
    {
        private readonly AzureCredentials _credentials;
        // public AzureCredentials Credentials = SdkContext.AzureCredentialsFactory.FromFile(@"auth.json");

        public AzureClient()
        {
            _credentials = SdkContext.AzureCredentialsFactory.FromFile(@"auth.json");
        }

        

        public IAzure GetClient() => Azure.Configure()
            .WithLogLevel(HttpLoggingDelegatingHandler.Level.BodyAndHeaders)
            .Authenticate(_credentials).WithDefaultSubscription();
    }
}