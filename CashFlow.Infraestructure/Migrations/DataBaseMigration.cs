using CashFlow.Infraestructure.DataAcess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infraestructure.Migrations;

public static class DataBaseMigration
{
    public async static Task MigrateDatabase(IServiceProvider serviceProvider)
    {
       var dbcontext = serviceProvider.GetRequiredService<CashFlowDbContext>();

       await dbcontext.Database.MigrateAsync();
    }
}
