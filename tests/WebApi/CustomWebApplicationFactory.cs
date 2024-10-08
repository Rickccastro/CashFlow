using CashFlow.Infraestructure.DataAcess;
using CommonTestUtilities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Token;

namespace WebApi;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private User _user;

    private  string _passwordSemCriptografia;

    private  string _token;

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

    public string GetToken()
    {
        return _token;
    }
    public string GetEmail()
    {
        return _user.Email;
    }  
    public string GetName()
    {
        return _user.Name;
    }  
    public string GetPassword()
    {
        return _passwordSemCriptografia;
    }
   
    private void StartDataBase(CashFlowDbContext dbContext, IPasswordEncripter passwordEncripter, IAcessTokenGenerator tokenGenerator) 
    {

        AddUsers(dbContext, passwordEncripter, tokenGenerator);
        AddExpenses(dbContext, _user);

        dbContext.SaveChanges();
    }

    private void AddUsers(CashFlowDbContext dbContext,IPasswordEncripter passwordEncripter, IAcessTokenGenerator tokenGenerator)
    {
        _user = UserBuilder.Build();
        _passwordSemCriptografia = _user.Password;
        _token = tokenGenerator.Generate(_user);
        _user.Password = passwordEncripter.Encrypt(_user.Password);
        dbContext.Users.Add(_user);

    }

    private void AddExpenses(CashFlowDbContext dbContext,  User user)
    {
        var expenses = ExpenseBuilder.Build(user);

        dbContext.Expenses.Add(expenses);
    }
}
