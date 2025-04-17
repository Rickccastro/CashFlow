using CashFlow.Infraestructure.DataAcess;
using CommonTestUtilities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Token;
using WebApi.Test.Resources;

namespace WebApi;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    public ExpenseIdentityManager Expense { get; private set; } = default!;
    public UserIdentityManager User_Team_Member { get; private set; } = default!;
    public UserIdentityManager User_Admin { get; private set; } = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
            .ConfigureServices(services =>
            {
                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
                services.AddDbContext<CashFlowDbContext>(config =>
                {
                    config.UseInMemoryDatabase("InMemoryDbforTesting");
                    config.UseInternalServiceProvider(provider);
                });

                var scope = services.BuildServiceProvider().CreateScope();

                var dbContext = scope.ServiceProvider.GetRequiredService<CashFlowDbContext>();
                var passwordEncripter = scope.ServiceProvider.GetRequiredService<IPasswordEncripter>();
                var tokenGenerator = scope.ServiceProvider.GetService<IAcessTokenGenerator>();
                StartDataBase(dbContext, passwordEncripter,tokenGenerator);
            });
    }
   
    private void StartDataBase(CashFlowDbContext dbContext, IPasswordEncripter passwordEncripter, IAcessTokenGenerator tokenGenerator) 
    {

        var user = AddUsers(dbContext, passwordEncripter, tokenGenerator);
        AddExpenses(dbContext, user);

        dbContext.SaveChanges();
    }

    private User AddUsers(CashFlowDbContext dbContext,IPasswordEncripter passwordEncripter, IAcessTokenGenerator tokenGenerator)
    {
        var user = UserBuilder.Build();
        var passwordSemCriptografia = user.Password;
        var token = tokenGenerator.Generate(user);

        user.Password = passwordEncripter.Encrypt(user.Password);
        dbContext.Users.Add(user);
 
        User_Team_Member = new UserIdentityManager(user, passwordSemCriptografia, token);

        return user;
    }

    private void AddExpenses(CashFlowDbContext dbContext,  User user)
    {
        var expense = ExpenseBuilder.Build(user); 

        dbContext.Expenses.Add(expense);

        Expense = new ExpenseIdentityManager(expense);
    }
}
