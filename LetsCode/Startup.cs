using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Extensions.DependencyInjection;
using static Microsoft.Azure.Management.ResourceManager.Fluent.Core.HttpLoggingDelegatingHandler;

namespace LetsCode
{
    public class Startup
    {

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public Startup()
        {
            
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IAzureClient, AzureClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IAzureClient client)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.Run(async (context) =>
            {
                
                var azureClient = client.GetClient();

                var vms = azureClient.VirtualMachines.List().ToList()[0];


                await context.Response.WriteAsync(vms.Name);
            });
        }
    }

    public interface IAzureClient
    {
      //  AzureCredentials GetCredentials();
        
        
        IAzure GetClient();
    }

    public class AzureClient : IAzureClient
    {
        private readonly AzureCredentials _credentials;
        // public AzureCredentials Credentials = SdkContext.AzureCredentialsFactory.FromFile(@"auth.json");

        public AzureClient()
        {
            _credentials = SdkContext.AzureCredentialsFactory.FromFile(@"auth.json");
        }

        

        public IAzure GetClient() => Azure.Configure()
            .WithLogLevel(Level.BodyAndHeaders)
            .Authenticate(_credentials).WithDefaultSubscription();
    }
}