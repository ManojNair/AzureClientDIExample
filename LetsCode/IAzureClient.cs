using Microsoft.Azure.Management.Fluent;

namespace LetsCode
{
    public interface IAzureClient
    {
        //  AzureCredentials GetCredentials();
        
        
        IAzure GetClient();
    }
}