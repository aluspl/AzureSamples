using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;

namespace DurableFunction
{
    public class Config
    {
        public static IConfigurationRoot GetConfig(ExecutionContext context)
        {
            return new ConfigurationBuilder()
                 .SetBasePath(context.FunctionAppDirectory)
                 .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                 .AddEnvironmentVariables()
                 .Build();
        }
    }
}
