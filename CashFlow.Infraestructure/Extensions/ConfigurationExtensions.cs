using Microsoft.Extensions.Configuration;

namespace CashFlow.Infraestructure.Extensions;

public static class ConfigurationExtensions
{
    public static bool IsTestEnviroment(this IConfiguration config)
    {
        return config.GetValue<bool>("InMemoryTest"); ;
    }
}
