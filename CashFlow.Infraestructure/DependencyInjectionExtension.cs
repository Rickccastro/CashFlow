using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Repositories.User;
using CashFlow.Domain.Security.Cryptography;

using CashFlow.Infraestructure.DataAcess;
using CashFlow.Infraestructure.DataAcess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CashFlow.Domain.Security.Token;
using CashFlow.Infraestructure.Security.Tokens;

namespace CashFlow.Infraestructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepositories(services);
        AddDbContext(services, configuration);
        AddPassword(services);
        AddDbContext(services, configuration);
        AddToken(services, configuration);  
    }   
    public static void AddPassword(IServiceCollection services)
    {
      services.AddScoped<IPasswordEncripter, Security.Cryptography.BCrypt>();
    }
    public static void AddToken(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        services.AddScoped<IAcessTokenGenerator>(config => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
    }
    private static void AddRepositories (IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IExpensesWriteOnlyRepository, ExpensesRepository>();
        services.AddScoped<IExpensesReadOnlyRepository, ExpensesRepository>();
        services.AddScoped<IExpensesUpdateOnlyRepository, ExpensesRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
    }
    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");    
        var version = new Version(8, 0, 39);
        var serverVersion = new MySqlServerVersion(version);
        services.AddDbContext<CashFlowDbContext>(config => config.UseMySql(connectionString, serverVersion));
    }
}
