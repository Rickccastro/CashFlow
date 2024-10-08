using CashFlow.Application.UseCase.Expenses.GetAll;
using CashFlow.Domain.Entities;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Login;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Respositories.Expenses;
using FluentAssertions;

namespace UseCases.Tests.Expenses.GetAllExpenses;

public class GetAllExpensesUseCaseTest
{

    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var expense = ExpenseBuilder.Collection(loggedUser);
        var useCase = CreateUser(loggedUser,expense);

        var result = await useCase.Execute();

        result.Should().NotBeNull();
        result.Expenses.Should().NotBeNullOrEmpty()
            .And.AllSatisfy(expense =>
            {
                expense.Id.Should().BeGreaterThan(0);
                expense.Amount.Should().BeGreaterThan(0);
                expense.Title.Should().NotBeNullOrEmpty();         
            });
    }

    private GetAllExpenseUseCase CreateUser(User user,List<Expense> expenses)
    {
        var repository = new ExpensesReadOnlyRepositoryBuilder().GetAll(user,expenses).Build();
        var mapper = MapperBuilder.Build();   
        var loggedUser =LoggedUserBuilder.Build(user); 


        return new GetAllExpenseUseCase(repository, mapper, loggedUser);
    }
}
