using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;

namespace PGSUpskill
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(SetupKeyVault())
             .ConfigureServices(services => services.AddAutofac())
             .UseStartup<Startup>();

        private static Action<WebHostBuilderContext, IConfigurationBuilder> SetupKeyVault()
        {
            return (context, config) =>
            {
                var builtConfig = config.Build();

                config.AddAzureKeyVault(
                    $"https://{builtConfig["KeyVaultName"]}.vault.azure.net/",
                    builtConfig["AzureADApplicationId"],
                    builtConfig["AzureADPassword"]);
            };
        }
    }
}
