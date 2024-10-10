using CashFlow.Application.UseCase.Expenses.GetAll;
using CashFlow.Application.UseCase.Expenses.GetById;
using CashFlow.Domain.Entities;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Login;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Respositories.Expenses;
using FluentAssertions;

namespace UseCases.Tests.Expenses.GetExpensesById;

public class GetExpensesByIdUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var expense = ExpenseBuilder.Build(loggedUser);
        var useCase = CreateUser(loggedUser, expense);

        var result = await useCase.Execute(expense.Id);

        result.Should().NotBeNull();

        result.Id.Should().Be(expense.Id);  
        result.Title.Should().Be(expense.Title);  
        result.Description.Should().Be(expense.Description);  
        result.Date.Should().Be(expense.Date);  
        result.Amount.Should().Be(expense.Amount);  
        result.PaymentType.Should().Be((CashFlow.Communication.Enums.PaymentType)expense.PaymentType);   
       
    }


    [Fact]
    public async Task Error_Expense_Not_Found()
    {
        var loggedUser = UserBuilder.Build();

        var useCase = CreateUser(loggedUser);

        var act = async ()=> await useCase.Execute(id:1000);

        var result = await act.Should().ThrowAsync<NotFoundException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMenssages.EXPENSE_NOT_FOUND));
    }




    private GetExpenseByIdUseCase CreateUser(User user, Expense? expenses = null)
    {
        var repository = new ExpensesReadOnlyRepositoryBuilder().GetById(user, expenses).Build();
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);


        return new GetExpenseByIdUseCase(repository, mapper, loggedUser);
    }
}
