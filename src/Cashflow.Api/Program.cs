using Cashflow.Api.Filter;
using Cashflow.Api.Middleware;
using CashFlow.Application;
using CashFlow.Infraestructure;
using CashFlow.Infraestructure.Migrations;
using Microsoft.OpenApi.Models;

namespace Cashflow.Api;

public class Program
{
    public static async Task Main(string[] args)

    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        builder.Services.AddMvc(options =>options.Filters.Add(typeof(ExceptionFilter)));
       
        builder.Services.AddInfrastructure(builder.Configuration);

        builder.Services.AddApplication();

        builder.Services.AddSwaggerGen(options => {
            options.MapType<DateOnly>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "date"
            });
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<CultureMiddleware>();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
        await MigrateDatabase(); 

        app.Run();

        async Task MigrateDatabase()
        {
           await using var scope = app.Services.CreateAsyncScope();
           
           await DataBaseMigration.MigrateDatabase(scope.ServiceProvider);

        }
    }
}
